using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Rendering;

namespace Gds.LiteConstruct.Core.CameraModes
{
    abstract class CameraMode
    {
        abstract public void Execute(CameraBase camera, int mx, int my);
    }
}
