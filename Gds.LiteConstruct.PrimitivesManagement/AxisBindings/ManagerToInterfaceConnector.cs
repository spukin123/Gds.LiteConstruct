using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.PrimitivesManagement.AxisBindings;
using Gds.LiteConstruct.PrimitivesManagement.AxisBindings.Interfaces;
using PrimitivesManagement.AxisBindings.Interfaces;
using Gds.LiteConstruct.BusinessObjects.Axises;
using Gds.LiteConstruct.PrimitivesManagement.AxisBindings.BindingAxisControllerManagement;
using PrimitivesManagement.AxisBindings.BindingAxisControllerManagement;

namespace PrimitivesManagement.AxisBindings
{
    internal class ManagerToInterfaceConnector : IConnectorInterfaceSide, IDisposable
    {
        private IManagerPresenter manager;
        public IManagerPresenter AxisManager
        {
            set { manager = value; }
        }

        private IUserInterfaceControllerPresenter uiController;
        public IUserInterfaceControllerPresenter UserInterfaceController
        {
            set { uiController = value; }
        }

        public ManagerToInterfaceConnector()
        {
        }

        public void AssociatedAxisFoundForPrimitive1(AssociatedBindingAxis associatedAxis)
        {
            AssociatedBindingAxisController associatedController;
            associatedController = new AssociatedBindingAxisController(associatedAxis);
            uiController.AddPrimitive1AssociatedController(associatedController);
        }

        public void AssociatedAxisFoundForPrimitive2(AssociatedBindingAxis associatedAxis)
        {
            AssociatedBindingAxisController associatedController;
            associatedController = new AssociatedBindingAxisController(associatedAxis);
            uiController.AddPrimitive2AssociatedController(associatedController);
        }

        public void FreeAxisFoundForPrimitive1(FreeBindingAxis freeAxis)
        {
            FreeBindingAxisController freeController;
            freeController = new FreeBindingAxisController(freeAxis);
            uiController.AddPrimitive1FreeController(freeController);
        }

        public void FreeAxisFoundForPrimitive2(FreeBindingAxis freeAxis)
        {
            FreeBindingAxisController freeController;
            freeController = new FreeBindingAxisController(freeAxis);
            uiController.AddPrimitive2FreeController(freeController);
        }

        #region IConnectorInterfaceSide Members

        bool IConnectorInterfaceSide.CanAssociatedPrimitiveBeDynamic()
        {
            return manager.CanAssociatedPrimitiveBeDynamic();
        }

        bool IConnectorInterfaceSide.CanFreePrimitiveBeDynamic()
        {
            return manager.CanFreePrimitiveBeDynamic();
        }

        bool IConnectorInterfaceSide.CanBind(IBindingAxisControllerPresenter staticController, IBindingAxisControllerPresenter dynamicFreeController)
        {
            BindingAxisController bindingController;
            bindingController = staticController as BindingAxisController;

            FreeBindingAxisController freeBindingController;
            freeBindingController = dynamicFreeController as FreeBindingAxisController;

            return manager.CanBind(bindingController.BindingAxis, freeBindingController.FreeBindingAxis);
        }

        void IConnectorInterfaceSide.CreateControllers()
        {
            manager.AnalyzePrimitiveAxes();
        }

        void IConnectorInterfaceSide.FreeControllersSelected(IBindingAxisControllerPresenter staticFreeController, IBindingAxisControllerPresenter dynamicFreeController)
        {
            FreeBindingAxisController staticController, dynamicController;
            staticController = staticFreeController as FreeBindingAxisController;
            dynamicController = dynamicFreeController as FreeBindingAxisController;

            manager.ProcessFreeAxes(staticController.FreeBindingAxis, dynamicController.FreeBindingAxis);
        }

        void IConnectorInterfaceSide.MixedControllersSelected(IBindingAxisControllerPresenter staticAssociatedController, IBindingAxisControllerPresenter dynamicFreeController)
        {
            AssociatedBindingAxisController staticController;
            staticController = staticAssociatedController as AssociatedBindingAxisController;
            FreeBindingAxisController dynamicController;
            dynamicController = dynamicFreeController as FreeBindingAxisController;

            manager.ProcessMixedAxes(staticController.AssociatedBindingAxis, dynamicController.FreeBindingAxis);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            manager = null;
            uiController = null;
        }

        #endregion
    }
}
