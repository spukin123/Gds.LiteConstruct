using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
    public interface IRotationVectorLimitable
    {
        bool CanRotateX { get; }
        bool CanRotateY { get; }
        bool CanRotateZ { get; }
    }
}
