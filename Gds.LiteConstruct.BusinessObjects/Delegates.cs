using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.Sides;

namespace Gds.LiteConstruct.BusinessObjects
{
    public delegate void PrimitivePositionChangedHandler(float x, float y, float z);
    public delegate void PrimitiveRotationChangedHandler(RotationVector vector);
    public delegate void NotifyHandler();
    public delegate void PrimitiveSelectedSideChanged(SideBase newSide);
    public delegate void StateHandler(bool state);
    public delegate void MousePositionHandler(int x, int y);
}
