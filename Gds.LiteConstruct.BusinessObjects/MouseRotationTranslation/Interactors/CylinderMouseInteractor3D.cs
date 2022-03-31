using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interactors
{
    public class CylinderMouseInteractor3D : CylindricalInteractor, IScalable, IColorable, ITranspareable
    {
        public CylinderMouseInteractor3D(Ray axis, int sidesNumber, float radius, Color color)
            : base(axis, sidesNumber, radius, color)
        {
            cylinder = new Cylinder(axis, sidesNumber, radius);

            InitStartRotation();
            InitBuffers();
            InitStruct();

            Origin = axis.Position + HalfDirection;
        }

        #region Overriden Members

        protected override int TrianglesNumber
        {
            get { return cylinder.IndicesCount / 3; }
        }

        protected override int VerticesCount
        {
            get { return cylinder.PointsCount; }
        }

        protected override int IndicesCount
        {
            get { return cylinder.IndicesCount; }
        }

        protected override void CreateVertexData(object sender, EventArgs e)
        {
            CustomVertex.PositionColored[] vertices = LockVB();

            for (int cnt = 0; cnt < vertices.Length; cnt++)
            {
                vertices[cnt].Position = cylinder.Points[cnt];
                vertices[cnt].Color = defaultColor.ToArgb();
            }

            UnlockVB();
        }

        protected override void CreateIndexData(object sender, EventArgs e)
        {
            short[] indices = LockIB();

            for (int cnt = 0; cnt < indices.Length; cnt++)
            {
                indices[cnt] = cylinder.Indices[cnt];
            }

            UnlockIB();
        }

        #endregion

        #region IRenderable Members

        //public override void Render()
        //{
        //    device.Transform.World = scaleMatrix * rotationMatrix * translationMatrix;
        //    device.VertexFormat = CustomVertex.PositionColored.Format;

        //    device.SetStreamSource(0, vertexBuffer, 0);
        //    device.Indices = indexBuffer;

        //    if (transparent)
        //    {
        //        DeviceObject.Device.RenderState.AlphaBlendEnable = true;
        //    }

        //    device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, VerticesCount, 0, TrianglesNumber);

        //    if (transparent)
        //    {
        //        DeviceObject.Device.RenderState.AlphaBlendEnable = false;
        //    }
        //}

        #endregion

        #region IColorable Members

        public void SetColor(Color color)
        {
            defaultColor = color;
            ApplyColorToVertices();
        }

        #endregion

        #region ITranspareable Members

        public void SetTransparency(int transparency)
        {
            defaultColor = Color.FromArgb(transparency, defaultColor.R, defaultColor.G, defaultColor.B);
            ApplyColorToVertices();
        }

        #endregion

        #region IScalable Members

        public void SetScale(float scaleFactor)
        {
            //scaleMatrix = Matrix.Scaling(1f, 1f, scaleFactor);
            scaleMatrix = Matrix.Scaling(scaleFactor, scaleFactor, scaleFactor);
            ApplyChangesToStruct();
        }

        #endregion

        #region IDisposable Members

        public override void Dispose()
        {
            disposed = true;
            vertexBuffer.Dispose();
            indexBuffer.Dispose();
        }

        #endregion
    }
}
