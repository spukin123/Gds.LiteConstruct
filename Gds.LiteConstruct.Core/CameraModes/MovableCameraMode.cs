using System;
using System.Collections.Generic;
using System.Text;
using GDS.LiteEngine.ObjectBuilder.RenderManager;
using GDS.LiteEngine.ObjectBuilder.BusinessClasses;

namespace GDS.LiteEngine.ObjectBuilder.Core.CameraModes
{
    class MovableCameraMode : CameraMode
    {
        public override void Execute(CameraBase camera, int mx, int my)
        {
            ((FreeCamera)camera).MoveForward(my / 5f);
            ((FreeCamera)camera).MoveSide(mx / 5f);
        }
    }
}
