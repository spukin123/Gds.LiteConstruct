using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects;
using Microsoft.DirectX.Direct3D;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.Rendering
{
    public abstract class RenderModeBase
    {
        protected CameraBase camera;
        
        protected Device device = null;

        public Device Device
        {
            get { return device; }
            set { device = value; }
        }

        public CameraBase Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        public int ScreenWidth
        {
            get { return device.Viewport.Width; }
        }

        public int ScreenHeight
        {
            get { return device.Viewport.Height; }
        }

        protected bool initialized = false;

        public bool Initialized
        {
            get { return initialized; }
            set { initialized = value; }
        }
	

        public abstract void DoRender();
        protected abstract void DoInitializeDeviceObjects();
        public abstract void RestoreDeviceObjects(object sender, EventArgs e);
        public abstract void DeleteDeviceObjects(object sender, EventArgs e);

        public void InitializeDeviceObjects()
        {
            if (initialized == false)
            {
                DoInitializeDeviceObjects();
                initialized = true;
            }
        }

        //public Ray GetRayFromScreenCoordinates(int x, int y)
        //{
        //    Matrix matProj;
        //    matProj = device.Transform.Projection;

        //    // Compute the vector of the pick ray in screen space
        //    Vector3 vec;
        //    vec.X = (((2f * x) / ScreenWidth) - 1) / matProj.M11;
        //    vec.Y = -(((2f * y) / ScreenHeight) - 1) / matProj.M22;
        //    vec.Z = 1f;

        //    // Get the inverse view matrix
        //    Matrix matView;
        //    matView = device.Transform.View;
        //    matView.Invert();

        //    // Transform the screen space pick ray into 3D space
        //    Vector3 vecPosition, vecDirection;

        //    vecDirection.X = vec.X * matView.M11 + vec.Y * matView.M21 + vec.Z * matView.M31;
        //    vecDirection.Y = vec.X * matView.M12 + vec.Y * matView.M22 + vec.Z * matView.M32;
        //    vecDirection.Z = vec.X * matView.M13 + vec.Y * matView.M23 + vec.Z * matView.M33;
        //    vecPosition.X = matView.M41;
        //    vecPosition.Y = matView.M42;
        //    vecPosition.Z = matView.M43;

        //    return new Ray(vecPosition, vecDirection);
        //}
    }
}
