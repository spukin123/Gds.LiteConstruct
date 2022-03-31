using System;
using System.Collections.Generic;
using System.Text;
using GDS.LiteEngine.ObjectBuilder.RenderManager;

namespace GDS.LiteEngine.ObjectBuilder.Core.CameraModes
{
    class ZoomableCameraMode : CameraMode
    {
        public override void Execute(CameraBase camera, int mx, int my)
        {
            camera.Zoom(my / 5f);
        }
    }
}
