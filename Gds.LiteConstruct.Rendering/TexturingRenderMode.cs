using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Microsoft.DirectX;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;
using Gds.LiteConstruct.BusinessObjects.Sides;

namespace Gds.LiteConstruct.Rendering
{
    public class TexturingRenderMode : RenderModeBase
    {
        private PrimitiveBase primitive;
        private SceneCoordinateSystem coordinateSystem = null;

        public TexturingRenderMode(PrimitiveBase primitive)
        {
            this.primitive = primitive;
        }

        #region Overriden Members

        protected override void DoInitializeDeviceObjects()
        {
            DeviceObject.Device = Device;
            float distance = primitive.FarestPointDistance * 1.4f;
            Camera = new RotatableCamera(Device, new Vector3(distance, distance, distance), Vector3Utils.ZeroVector);
            coordinateSystem = new SceneCoordinateSystem(Device, false);
        }

        public override void RestoreDeviceObjects(object sender, EventArgs e)
        {
            Device.RenderState.Lighting = false;
            Device.RenderState.CullMode = Cull.CounterClockwise;
            Device.RenderState.ShadeMode = ShadeMode.Flat;
            Device.RenderState.ZBufferEnable = true;
        }

        public override void DeleteDeviceObjects(object sender, EventArgs e)
        {
        }

        public override void DoRender()
        {
            Device.Transform.World = Matrix.Identity;
            Device.Transform.View = Camera.GetViewMatrix();
            Device.Transform.Projection = Matrix.PerspectiveFovLH((float)Device.Viewport.Width / (float)Device.Viewport.Height, 1.0f, 1.0f, 1000.0f);

            try
            {
                coordinateSystem.Render();
                primitive.RenderInLCS();
            }
            catch { }
        }

        #endregion

        public SideBase GetSideByScreenPosition(int x, int y)
        {
            Ray ray = Ray.GetRayFromScreenCoordinates(x, y);
            return primitive.GetIntersectionSideInLCS(ray);
        }
    }
}
