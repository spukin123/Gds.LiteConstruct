using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.PrimitivesManagement.AxisBindings.BindingAxisControllerManagement;

namespace PrimitivesManagement.AxisBindings.Interfaces
{
    internal interface IConnectorInterfaceSide
    {
        void CreateControllers();
        void FreeControllersSelected(IBindingAxisControllerPresenter staticFreeController, IBindingAxisControllerPresenter dynamicFreeController);
        void MixedControllersSelected(IBindingAxisControllerPresenter staticAssociatedController, IBindingAxisControllerPresenter dynamicFreeController);
        bool CanBind(IBindingAxisControllerPresenter staticController, IBindingAxisControllerPresenter dynamicFreeController);
        bool CanAssociatedPrimitiveBeDynamic();
        bool CanFreePrimitiveBeDynamic();
    }
}
