using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Gds.LiteConstruct.BusinessObjects.Axises;
using Gds.LiteConstruct.Environment;

namespace Gds.LiteConstruct.PrimitivesManagement.AxisBindings.Interfaces
{
    internal interface IManagerPresenter
    {
        PrimitiveBase Primitive1 { get; }
        PrimitiveBase Primitive2 { get; }

        void AnalyzePrimitiveAxes();
        void ProcessFreeAxes(FreeBindingAxis staticFreeAxis, FreeBindingAxis dynamicFreeAxis);
        void ProcessMixedAxes(AssociatedBindingAxis staticAssociatedAxis, FreeBindingAxis dynamicFreeAxis);

        bool CanBind(Axis staticAxis, FreeBindingAxis dynamicFreeAxis);
        bool CanAssociatedPrimitiveBeDynamic();
        bool CanFreePrimitiveBeDynamic();
    }
}
