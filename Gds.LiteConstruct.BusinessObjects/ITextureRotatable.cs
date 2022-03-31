using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public interface ITextureRotatable
    {
        void RotateRightBy(Angle angle);
        void RotateLeftBy(Angle angle);
        void Rotate(Angle angle);
        Vector2 GetTextureCoordinatesForPoint(Vertex point);

        Angle CurrentAngle { get; }
    }
}
