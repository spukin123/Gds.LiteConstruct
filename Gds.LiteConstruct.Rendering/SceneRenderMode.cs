using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects;
using System.IO;
using System.Threading;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections;
using Gds.LiteConstruct.Environment;

namespace Gds.LiteConstruct.Rendering
{
    public class SceneRenderMode : RenderModeBase
    {
        protected Model model = new Model();

        public Model Model
        {
            set { model = value; }
        }
        
        protected SceneCoordinateSystem coordinateSystem = null;

        public SceneRenderMode(Model model)
        {
            this.model = model;
        }

        public PrimitiveBase GetPrimitiveByScreenPosition(int x, int y)
        {
            Ray ray = Ray.GetRayFromScreenCoordinates(x, y);
            Vertex point;
            PrimitiveBase nearestPrimitive = null;
            float nearestDist = 0;

            foreach (PrimitiveBase prim in model.Primitives)
            {
                point = prim.GetIntersectionPoint(ray);
                if (point != null)
                {
                    if (nearestPrimitive == null || Vector3Utils.DistanceBetweenPoints(ray.Position, point.Vector) < nearestDist)
                    {
                        nearestPrimitive = prim;
                        nearestDist = Vector3Utils.DistanceBetweenPoints(ray.Position, point.Vector);
                    }
                }
            }
            return nearestPrimitive;
        }

        public Vector3 GetGroundIntersectionVectorByScreenPosition(int x, int y)
        {
            Ray ray = Ray.GetRayFromScreenCoordinates(x, y);
            Plane plane = Plane.FromPoints(new Vector3(1f, 1f, 0f), new Vector3(1f, 0f, 0f), new Vector3(0f, 0f, 0f));
            return Plane.IntersectLine(plane, ray.Position, ray.Position + ray.Direction);
        }

        public void AddPrimitiveByScreenPosition(PrimitiveBase primitive, int x, int y)
        {
            Vector3 intersectionPoint = GetGroundIntersectionVectorByScreenPosition(x, y);
            primitive.MoveTo(intersectionPoint);
            model.AddPrimitive(primitive);
        }

        private void RaiseRenderEvent()
        {
            if (Render != null)
            {
                Render();
            }
        }

        #region Overriden Members

        protected override void DoInitializeDeviceObjects()
        {
            DeviceObject.Device = Device;
            Camera = new FreeCamera(Device, new Vector3(40.0f, 40.0f, 40.0f), new Vector3(-1.0f, -1.0f, -1.0f));
            coordinateSystem = new SceneCoordinateSystem(Device, true);
       }

        public override void RestoreDeviceObjects(object sender, EventArgs e)
        {
            device.SamplerState[0].MinFilter = TextureFilter.Linear;
            device.SamplerState[0].MagFilter = TextureFilter.Linear;
            //device.TextureState[0].ColorOperation = TextureOperation.Modulate;
            /*device.TextureState[0].AlphaOperation = TextureOperation.Modulate;
            device.TextureState[1].ColorOperation = TextureOperation.Disable;
            device.TextureState[1].AlphaOperation = TextureOperation.Disable;

            device.RenderState.SourceBlend = Blend.One;
            device.RenderState.DestinationBlend = Blend.One;*/
            Device.RenderState.Lighting = false;
            Device.RenderState.CullMode = Cull.CounterClockwise;
            Device.RenderState.ShadeMode = ShadeMode.Flat;
            Device.RenderState.ZBufferEnable = true;
            //device.RenderState.AntiAliasedLineEnable = true;
            device.RenderState.Ambient = Color.White;
            device.RenderState.CullMode = Cull.None;

            //Transparency
            //1
            device.RenderState.DiffuseMaterialSource = ColorSource.Color1;
            device.RenderState.ColorVertex = true;
            //device.RenderState.AlphaBlendEnable = true;
            //- Here bug(maybe)
            //device.RenderState.SourceBlend = Blend.SourceAlpha;
            //device.RenderState.DestinationBlend = Blend.SourceColor;
            device.RenderState.SourceBlend = Blend.SourceAlpha;
            device.RenderState.DestinationBlend = Blend.DestinationColor;
            

            //2
            //device.RenderState.AlphaBlendEnable = true;
            //device.RenderState.SourceBlend = Blend.SourceAlpha;
            //device.RenderState.AlphaSourceBlend = Blend.One;
            //device.RenderState.DestinationBlend = Blend.InvSourceAlpha;
            //device.RenderState.AlphaDestinationBlend = Blend.InvSourceAlpha;

            ////  don't write z depth if there is no alpha
            device.RenderState.AlphaTestEnable = true;
            //device.RenderState.AlphaFunction = Compare.Greater;
            //device.RenderState.ReferenceAlpha = 0;

            //3

        }

        public override void DeleteDeviceObjects(object sender, EventArgs e)
        {
        }

        public override void DoRender()
        {
            Device.Transform.World = Matrix.Identity;
            Device.Transform.View = Camera.GetViewMatrix();
            Device.Transform.Projection = Matrix.PerspectiveFovLH((float)ScreenWidth / (float)ScreenHeight, 1f, 1f, 1000f);

            try
            {
                coordinateSystem.Render();

                foreach (PrimitiveBase primitive in model.Primitives)
                {
                    primitive.Render();
                }

                RaiseRenderEvent();
            }
            catch { }
        }

        #endregion

        public event NotifyHandler Render;
    }
}
