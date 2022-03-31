using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Rendering;
using Gds.LiteConstruct.BusinessObjects;

namespace Gds.LiteConstruct.Core.CameraModes
{
    static class CameraModeManager
    {
        static public readonly CameraMode Rotatable = new RotatableCameraMode();
        static public readonly CameraMode Movable = new MovableCameraMode();
        static public readonly CameraMode Zoomable = new ZoomableCameraMode();

        static private CameraMode[] cameraModes = { Rotatable, Movable, Zoomable };
        static private List<CameraMode> cameraModeList = new List<CameraMode>(cameraModes); 

        static public CameraMode GetNextMode(CameraMode cameraMode)
        {
            int cameraModeIndex = cameraModeList.IndexOf(cameraMode);
            if (cameraModeIndex != (cameraModeList.Count - 1))
            {
                return cameraModeList[cameraModeIndex + 1];
            }
            else
            {
                return cameraModeList[0];
            }
        }

        static public CameraMode GetPrevMode(CameraMode cameraMode)
        {
            int cameraModeIndex = cameraModeList.IndexOf(cameraMode);
            if (cameraModeIndex != 0)
            {
                return cameraModeList[cameraModeIndex - 1];
            }
            else
            {
                return cameraModeList[cameraModeList.Count - 1];
            }
        }


        private class RotatableCameraMode : CameraMode
        {
            public override void Execute(CameraBase camera, int mx, int my)
            {
                Angle horAngle = Angle.FromDegrees(mx / 7.0f);
                Angle verAngle = Angle.FromDegrees(my / 7.0f);
                camera.RotateCameraHorizontally(horAngle);
                camera.RotateCameraVertically(verAngle);
            }
        }

        private class MovableCameraMode : CameraMode
        {
            public override void Execute(CameraBase camera, int mx, int my)
            {
                ((FreeCamera)camera).MoveForward(my / 5f);
                ((FreeCamera)camera).MoveSide(mx / 5f);
            }
        }

        private class ZoomableCameraMode : CameraMode
        {
            public override void Execute(CameraBase camera, int mx, int my)
            {
                camera.Zoom(my / 5f);
            }
        }
    }
}
