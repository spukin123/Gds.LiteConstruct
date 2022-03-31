using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Axises;
using Gds.LiteConstruct.BusinessObjects;
using Gds.LiteConstruct.PrimitivesManagement.AxisBindings.BindingAxisControllerManagement;
using System.Drawing;
using PrimitivesManagement.AxisBindings.BindingAxisControllerManagement;
using Gds.LiteConstruct.PrimitivesManagement.AxisBindings.Interfaces;
using PrimitivesManagement.AxisBindings;
using PrimitivesManagement.AxisBindings.Interfaces;

namespace Gds.LiteConstruct.PrimitivesManagement.AxisBindings
{
    internal class UserInterfaceController : IMouseInteractable, IRenderable, IDisposable, IUserInterfaceControllerPresenter
    {
        private List<IBindingAxisControllerPresenter> primitive1AssociatedAxisControllers = new List<IBindingAxisControllerPresenter>();
        private List<IBindingAxisControllerPresenter> primitive2AssociatedAxisControllers = new List<IBindingAxisControllerPresenter>();
        private List<IBindingAxisControllerPresenter> allAssociatedAxisControllers = new List<IBindingAxisControllerPresenter>();

        private List<IBindingAxisControllerPresenter> primitive1AxisControllers = new List<IBindingAxisControllerPresenter>();
        private List<IBindingAxisControllerPresenter> primitive2AxisControllers = new List<IBindingAxisControllerPresenter>();

        private List<IBindingAxisControllerPresenter> primitiveUnboundControllers = new List<IBindingAxisControllerPresenter>();

        private IBindingAxisControllerPresenter activeController;
        private IBindingAxisControllerPresenter primitive1SelectedController;
        private IBindingAxisControllerPresenter primitive2SelectedController;

        private Point lastMousePosition;
        private bool canRender = true;

        private IConnectorInterfaceSide connector;

        private bool primitive1SelectedControllerFirst;

        private bool Primitive1SelectedControllerIsFree
        {
            get { return !primitive1AssociatedAxisControllers.Contains(primitive1SelectedController); }
        }

        private bool Primitive2SelectedControllerIsFree
        {
            get { return !primitive2AssociatedAxisControllers.Contains(primitive2SelectedController); }
        }

        private bool ActiveControllerBelongsToPrimitive1
        {
            get { return primitive1AxisControllers.Contains(activeController); }
        }

        private bool ActiveControllerBelongsToPrimitive2
        {
            get { return primitive2AxisControllers.Contains(activeController); }
        }

        public UserInterfaceController(IConnectorInterfaceSide connector)
        {
            this.connector = connector;
        }

        //--

        private bool ControllerBelongsToAssociatedPrimitive(List<IBindingAxisControllerPresenter> controllers, IBindingAxisControllerPresenter primitiveSelectedController)
        {
            bool hasAssociatedController = false;
            foreach (IBindingAxisControllerPresenter controller in controllers)
            {
                foreach (IBindingAxisControllerPresenter associatedController in allAssociatedAxisControllers)
                {
                    if (controller == associatedController)
                    {
                        hasAssociatedController = true;
                    }
                }   
            }

            return hasAssociatedController && !IsSelectedOppositeTo(primitiveSelectedController);
        }

        private bool IsSelectedOppositeTo(IBindingAxisControllerPresenter selectedController)
        {
            if (selectedController == primitive1SelectedController)
            {
                return primitive2SelectedController != null;
            }
            else
            {
                return primitive1SelectedController != null;
            }
        }

        private bool ControllerIsFree(IBindingAxisControllerPresenter controller)
        {
            if (primitive1AssociatedAxisControllers.Contains(controller))
            {
                return false;
            }

            if (primitive2AssociatedAxisControllers.Contains(controller))
            {
                return false;
            }

            return true;
        }

        private void ProcessFreeMouseMoveFor(List<IBindingAxisControllerPresenter> controllers, IBindingAxisControllerPresenter primitiveSelectedController)
        {
            foreach (IBindingAxisControllerPresenter controller in controllers)
            {
                if (controller.Interacted(lastMousePosition.X, lastMousePosition.Y))
                {
                    if (activeController == null && primitiveSelectedController == null)
                    {
                        if (ControllerBelongsToAssociatedPrimitive(controllers, primitiveSelectedController))
                        {
                            if (connector.CanAssociatedPrimitiveBeDynamic())
                            {
                                activeController = controller;
                                activeController.MakeActive();
                            }
                        }
                        else
                        {
                            if (connector.CanFreePrimitiveBeDynamic())
                            {
                                activeController = controller;
                                activeController.MakeActive();
                            }
                        }
                    }
                }
                else
                {
                    if (controller == activeController)
                    {
                        activeController.MakeUnactive();
                        activeController = null;
                    }
                }
            }
        }

        private void HideUnselectedControllers(List<IBindingAxisControllerPresenter> controllers, IBindingAxisControllerPresenter selectedController)
        {
            foreach (IBindingAxisControllerPresenter controller in controllers)
            {
                if (controller != selectedController)
                {
                    controller.Hide();
                }
            }
        }

        private void HideUnboundControllers(List<IBindingAxisControllerPresenter> controllers, IBindingAxisControllerPresenter selectedController)
        {
            if (ControllerIsFree(selectedController))
            {
                foreach (IBindingAxisControllerPresenter controller in controllers)
                {
                    if (!connector.CanBind(controller, selectedController))
                    {
                        primitiveUnboundControllers.Add(controller);
                        controller.Hide();
                    }
                }
            }
            else
            {
                throw new Exception("Associated controller can't be dynamic");
            }
        }

        private void CheckForSelectedController()
        {
            if (activeController != null)
            {
                if (ActiveControllerBelongsToPrimitive1)
                {
                    primitive1SelectedController = activeController;
                    activeController = null;

                    HideUnselectedControllers(primitive1AxisControllers, primitive1SelectedController);
                    HideUnboundControllers(primitive2AxisControllers, primitive1SelectedController);

                    primitive1SelectedControllerFirst = true;
                }
                else if (ActiveControllerBelongsToPrimitive2)
                {
                    primitive2SelectedController = activeController;
                    activeController = null;

                    HideUnselectedControllers(primitive2AxisControllers, primitive2SelectedController);
                    HideUnboundControllers(primitive1AxisControllers, primitive2SelectedController);

                    primitive1SelectedControllerFirst = false;
                }
            }
        }

        private IBindingAxisControllerPresenter GetPrimitiveControllerInteracted(List<IBindingAxisControllerPresenter> primitiveControllers)
        {
            foreach (IBindingAxisControllerPresenter controller in primitiveControllers)
            {
                if (controller.Interacted(lastMousePosition.X, lastMousePosition.Y))
                {
                    return controller;
                }
            }

            return null;
        }

        private void ProcessVoidClickAfterControllerSelected()
        {
            if (primitive1SelectedController != null)
            {
                ShowHiddenControllers(primitive1SelectedController, primitive1AxisControllers);
            }
            else if (primitive2SelectedController != null)
            {
                ShowHiddenControllers(primitive2SelectedController, primitive2AxisControllers);
            }

            ShowUnboundControllers();
            primitiveUnboundControllers.Clear();
        }

        private void ProcessSelectedControllers()
        {
            if (Primitive1SelectedControllerIsFree && Primitive2SelectedControllerIsFree)
            {
                if (primitive1SelectedControllerFirst)
                {
                    connector.FreeControllersSelected(primitive2SelectedController, primitive1SelectedController);
                }
                else
                {
                    connector.FreeControllersSelected(primitive1SelectedController, primitive2SelectedController);
                }
            }
            else
            {
                if (!Primitive1SelectedControllerIsFree && !Primitive2SelectedControllerIsFree)
                {
                    throw new Exception("Can't bind two associated axes.");
                }
                else if (!Primitive1SelectedControllerIsFree)
                {
                    connector.MixedControllersSelected(primitive1SelectedController, primitive2SelectedController);
                }
                else if (!Primitive2SelectedControllerIsFree)
                {
                    connector.MixedControllersSelected(primitive2SelectedController, primitive1SelectedController);
                }
            }

            primitive1SelectedController = null;
            primitive2SelectedController = null;
        }

        private void ShowHiddenControllers(IBindingAxisControllerPresenter selectedController, List<IBindingAxisControllerPresenter> primitiveControllers)
        {
            foreach (IBindingAxisControllerPresenter controller in primitiveControllers)
            {
                if (controller != selectedController)
                {
                    controller.EnableTransactionMode();
                    controller.Show();
                }
                else
                {
                    controller.EnableTransactionMode();
                    controller.MakeUnactive();
                }
            }
        }

        private void ShowUnboundControllers()
        {
            foreach (IBindingAxisControllerPresenter controller in primitiveUnboundControllers)
            {
                controller.Show();
            }
        }

        //--

        public void StopUI()
        {
            Dispose();
            connector = null;
        }

        public void StartUI()
        {
            connector.CreateControllers();
        }

        #region IMouseInteractable Members

        public void FreeMouseMove(int x, int y)
        {
            lastMousePosition = new Point(x, y);

            ProcessFreeMouseMoveFor(primitive1AxisControllers, primitive1SelectedController);
            ProcessFreeMouseMoveFor(primitive2AxisControllers, primitive2SelectedController);
        }

        public void PrimaryMouseClick(int x, int y)
        {
            lastMousePosition = new Point(x, y);

            if (primitive1SelectedController == null && primitive2SelectedController == null)
            {
                CheckForSelectedController();
            }
            else if (primitive1SelectedController != null)
            {
                primitive2SelectedController = GetPrimitiveControllerInteracted(primitive2AxisControllers);
                if (primitive2SelectedController != null)
                {
                    ProcessSelectedControllers();
                }
                else
                {
                    ProcessVoidClickAfterControllerSelected();
                    primitive1SelectedController = null;
                }
            }
            else if (primitive2SelectedController != null)
            {
                primitive1SelectedController = GetPrimitiveControllerInteracted(primitive1AxisControllers);
                if (primitive1SelectedController != null)
                {
                    ProcessSelectedControllers();
                }
                else
                {
                    ProcessVoidClickAfterControllerSelected();
                    primitive2SelectedController = null;
                }
            }
        }

        public void SecondaryMouseClick(int x, int y)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void ClampedMouseMove(int x, int y)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IRenderable Members

        public void Render()
        {
            if (canRender)
            {
                if (primitive1AxisControllers != null)
                {
                    foreach (IBindingAxisControllerPresenter controller in primitive1AxisControllers)
                    {
                        controller.Render();
                    }
                }

                if (primitive2AxisControllers != null)
                {
                    foreach (IBindingAxisControllerPresenter controller in primitive2AxisControllers)
                    {
                        controller.Render();
                    }
                }
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            canRender = false;
            if (primitive1AxisControllers != null)
            {
                foreach (IBindingAxisControllerPresenter controller in primitive1AxisControllers)
                {
                    controller.Dispose();
                }
            }

            if (primitive2AxisControllers != null)
            {
                foreach (IBindingAxisControllerPresenter controller in primitive2AxisControllers)
                {
                    controller.Dispose();
                }
            }
            canRender = true;

            primitive1AxisControllers.Clear();
            primitive2AxisControllers.Clear();

            primitive1AxisControllers = null;
            primitive2AxisControllers = null;
        }

        #endregion

        #region IUserInterfaceControllerPresenter Members

        public void AddPrimitive1AssociatedController(IBindingAxisControllerPresenter controller)
        {
            primitive1AssociatedAxisControllers.Add(controller);
            primitive1AxisControllers.Add(controller);
            allAssociatedAxisControllers.Add(controller);
        }

        public void AddPrimitive1FreeController(IBindingAxisControllerPresenter controller)
        {
            primitive1AxisControllers.Add(controller);
        }

        public void AddPrimitive2AssociatedController(IBindingAxisControllerPresenter controller)
        {
            primitive2AssociatedAxisControllers.Add(controller);
            primitive2AxisControllers.Add(controller);
            allAssociatedAxisControllers.Add(controller);
        }

        public void AddPrimitive2FreeController(IBindingAxisControllerPresenter controller)
        {
            primitive2AxisControllers.Add(controller);
        }

        public bool ControllerBelongsToPrimitive1(IBindingAxisControllerPresenter controller)
        {
            return primitive1AxisControllers.Contains(controller);
        }

        public bool ControllerBelongsToPrimitive2(IBindingAxisControllerPresenter controller)
        {
            return primitive2AxisControllers.Contains(controller);
        }

        #endregion
    }
}
