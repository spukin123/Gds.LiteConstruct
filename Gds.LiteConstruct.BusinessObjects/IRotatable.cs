using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
    public interface IRotatable
    {
        bool CanRotateX { get; }
        bool CanRotateY { get; }
        bool CanRotateZ { get; }
        void RotateX(Angle angle);
        void RotateY(Angle angle);
        void RotateZ(Angle angle);

        RotationVector Rotation { get; set; }
    }
}
