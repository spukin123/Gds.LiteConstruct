using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.PrimitivesManagement.AxisBindings.BindingAxisControllerManagement;

namespace Gds.LiteConstruct.PrimitivesManagement.AxisBindings.Interfaces
{
    internal interface IUserInterfaceControllerPresenter
    {
        void AddPrimitive1AssociatedController(IBindingAxisControllerPresenter controller);
        void AddPrimitive1FreeController(IBindingAxisControllerPresenter controller);
        void AddPrimitive2AssociatedController(IBindingAxisControllerPresenter controller);
        void AddPrimitive2FreeController(IBindingAxisControllerPresenter controller);

        bool ControllerBelongsToPrimitive1(IBindingAxisControllerPresenter controller);
        bool ControllerBelongsToPrimitive2(IBindingAxisControllerPresenter controller);
    }
}
