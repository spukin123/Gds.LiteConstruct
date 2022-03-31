using System;
using System.Collections.Generic;
using System.Text;
using GDS.LiteEngine.ObjectBuilder.RenderManager;
using GDS.LiteEngine.ObjectBuilder.BusinessClasses;

namespace GDS.LiteEngine.ObjectBuilder.Core.CameraModes
{
    class RotatableCameraMode : CameraMode
    {
        public override void Execute(CameraBase camera, int mx, int my)
        {
            Angle horAngle = Angle.FromDegrees(mx / 7.0f);
            Angle verAngle = Angle.FromDegrees(my / 7.0f);
            camera.RotateCameraHorizontally(horAngle);
            camera.RotateCameraVertically(verAngle);
        }
    }
}
