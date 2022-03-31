using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Gds.LiteConstruct.BusinessObjects.Axises;
using Gds.LiteConstruct.Environment;
using Gds.LiteConstruct.PrimitivesManagement.AxisBindings.Interfaces;
using PrimitivesManagement.AxisBindings;
using PrimitivesManagement.AxisBindings.Interfaces;
using Gds.LiteConstruct.BusinessObjects;

namespace Gds.LiteConstruct.PrimitivesManagement.AxisBindings
{
    public class AxisBindingManager : BindingsManager, IManagerPresenter
    {
        private UserInterfaceController uiController;
        private Model model;
        private ManagerToInterfaceConnector connector;
        private Binder binder;

        private bool uiStopped = true;

        public AxisBindingManager(PrimitiveBase primitive1, PrimitiveBase primitive2, Model model)
            : base(primitive1, primitive2)
        {
            this.model = model;
            connector = new ManagerToInterfaceConnector();
            uiController = new UserInterfaceController(connector);
            
            connector.AxisManager = this;
            connector.UserInterfaceController = uiController;

            binder = new Binder();
        }

        private void AssociatedAxisFoundForPrimitive(PrimitiveBase primitive, AssociatedBindingAxis associatedAxis)
        {
            if (primitive == primitive1)
            {
                connector.AssociatedAxisFoundForPrimitive1(associatedAxis);
            }
            else
            {
                connector.AssociatedAxisFoundForPrimitive2(associatedAxis);
            }
        }

        private void FreeAxisFoundForPrimitive(PrimitiveBase primitive, FreeBindingAxis freeAxis)
        {
            if (primitive == primitive1)
            {
                connector.FreeAxisFoundForPrimitive1(freeAxis);
            }
            else
            {
                connector.FreeAxisFoundForPrimitive2(freeAxis);
            }
        }

        private void AnalyzeConcretePrimitiveAxes(PrimitiveBase primitive)
        {
            bool connectionFound;
            FreeBindingAxis[] freeAxes = primitive.GetAxes();

            foreach (FreeBindingAxis freeAxis in freeAxes)
            {
                connectionFound = false;
                foreach (AssociatedBindingAxis associatedAxis in model.AssociatedBindingAxes)
                {
                    if (associatedAxis.IsFreeAxisConnected(freeAxis.Id))
                    {
                        AssociatedAxisFoundForPrimitive(primitive, associatedAxis);

                        connectionFound = true;
                        break;
                    }
                }

                if (!connectionFound)
                {
                    FreeAxisFoundForPrimitive(primitive, freeAxis);
                }
            }
        }

        private void UIControllerFreeMouseMove(int x, int y)
        {
            if (!uiStopped)
            {
                uiController.FreeMouseMove(x, y);
            }
        }

        private void UIControllerClampedMouseMove(int x, int y)
        {
            if (!uiStopped)
            {
                uiController.ClampedMouseMove(x, y);
            }
        }

        private void UIControllerPrimaryMouseClick(int x, int y)
        {
            if (!uiStopped)
            {
                uiController.PrimaryMouseClick(x, y);
            }
        }

        private void UIControllerSecondaryMouseClick(int x, int y)
        {
            if (!uiStopped)
            {
                uiController.SecondaryMouseClick(x, y);
            }
        }

        private void HidePrimitives()
        {
            //primitive1.Transparent = true;
            //primitive2.Transparent = true;
        }

        private void ShowPrimitives()
        {
            //primitive1.Transparent = false;
            //primitive2.Transparent = false;
        }

        private void StopInterface()
        {
            uiStopped = true;
            uiController.StopUI();
            uiController = null;
        }

        private void StopConnector()
        {
            connector.Dispose();
            connector = null;
        }

        #region Binder Callbacks

        private void Binder_GeometricBindingFinishedForMixedAxes()
        {
            binder.GeometricBindingFinishedForMixedAxes -= Binder_GeometricBindingFinishedForMixedAxes;
            RaiseBindingFinished();
        }

        private void Binder_GeometricBindingFinishedForFreeAxes(AssociatedBindingAxis result)
        {
            binder.GeometricBindingFinishedForFreeAxes -= Binder_GeometricBindingFinishedForFreeAxes;
            model.AddAssociatedBindingAxis(result);
            RaiseBindingFinished();
        }

        #endregion

        #region Overriden Members

        public override void StartBinding()
        {
            HidePrimitives();
            uiController.StartUI();
            uiStopped = false;
        }

        public override void StopBinding()
        {
            ShowPrimitives();
            StopInterface();
            StopConnector();
        }

        #endregion

        #region IMouseInteractable Members

        public override void FreeMouseMove(int x, int y)
        {
            UIControllerFreeMouseMove(x, y);
        }

        public override void ClampedMouseMove(int x, int y)
        {
            UIControllerClampedMouseMove(x, y);
        }

        public override void PrimaryMouseClick(int x, int y)
        {
            UIControllerPrimaryMouseClick(x, y);
        }

        public override void SecondaryMouseClick(int x, int y)
        {
            UIControllerSecondaryMouseClick(x, y);
        }

        #endregion

        #region AxisBindingManagerPresenter Members

        public PrimitiveBase Primitive1
        {
            get { return primitive1; }
        }

        public PrimitiveBase Primitive2
        {
            get { return primitive2; }
        }

        public bool CanAssociatedPrimitiveBeDynamic()
        {
            return Binder.CanAssociatedPrimitiveBeDynamic();
        }

        public bool CanFreePrimitiveBeDynamic()
        {
            return Binder.CanFreePrimitiveBeDynamic();
        }

        public void AnalyzePrimitiveAxes()
        {
            AnalyzeConcretePrimitiveAxes(primitive1);
            AnalyzeConcretePrimitiveAxes(primitive2);
        }
        
        public void ProcessFreeAxes(FreeBindingAxis staticFreeAxis, FreeBindingAxis dynamicFreeAxis)
        {
            ShowPrimitives();
            StopInterface();
            StopConnector();

            binder.GeometricBindingFinishedForFreeAxes += Binder_GeometricBindingFinishedForFreeAxes;
            binder.BindFreeAxisToFreeAxis(staticFreeAxis, dynamicFreeAxis);
        }

        public void ProcessMixedAxes(AssociatedBindingAxis staticAssociatedAxis, FreeBindingAxis dynamicFreeAxis)
        {
            ShowPrimitives();
            StopInterface();
            StopConnector();

            binder.GeometricBindingFinishedForMixedAxes += Binder_GeometricBindingFinishedForMixedAxes;
            binder.BindFreeAxisToAssociated(staticAssociatedAxis, dynamicFreeAxis);
        }

        public bool CanBind(Axis staticAxis, FreeBindingAxis dynamicFreeAxis)
        {
            return Binder.CanBind(staticAxis, dynamicFreeAxis);
        }

        #endregion

        #region IRenderable Members

        public override void Render()
        {
            uiController.Render();
        }

        #endregion
    }
}
