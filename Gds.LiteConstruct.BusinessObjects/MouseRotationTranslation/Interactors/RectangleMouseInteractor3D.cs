using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interactors
{
    public class RectangleMouseInteractor3D : MouseInteractor3D, IScalable, IColorable, ITranspareable, ISelectiveScalable
    {
        protected override int VerticesCount
        {
            get { return 4; }
        }
        protected override int IndicesCount
        {
            get { return 6; }
        }

        protected Matrix scaleMat = Matrix.Identity;
        protected Matrix translationMat = Matrix.Identity;
        protected Matrix rotationMat = Matrix.Identity;

        private Vector3 position;
        protected override Vector3 Origin
        {
            get { return position; }
            set 
            { 
                position = value;
                UpdateTranslation(); 
                ApplyChangesToStruct(); 
            }
        }

        private float scaleFactor;
        private float ScaleFactor
        {
            get { return scaleFactor; }
            set 
            { 
                scaleFactor = value; 
                UpdateScaling();
                ApplyChangesToStruct();
            }
        }

        protected Size2 size;
        public Size2 Size
        {
            get { return size; }
            set
            {
                size = value;
                InitStruct();
                ApplyChangesToStruct();
                UpdateVerticesPosition();
            }
        }

        private bool transparent = false;
        public bool Transparent
        {
            get { return transparent; }
            set { transparent = value; }
        }

        public RectangleMouseInteractor3D(Size2 size, Color color, Bitmap textureBitmap, Vector3 position) : base(color, textureBitmap)
        {
            CreateIS();
         
            this.size = size;
            InitStruct();
            Origin = position;
        }

        protected void CreateIS()
        {
            etalonIS = new Triangle[2];
            etalonIS[0] = new Triangle();
            etalonIS[1] = new Triangle();

            currentIS = new Triangle[2];
            currentIS[0] = new Triangle();
            currentIS[1] = new Triangle();
        }

        protected void InitTextureCoordinates()
        {
            CustomVertex.PositionColoredTextured[] vertices = (CustomVertex.PositionColoredTextured[])vertexBuffer.Lock(0, 0);

            vertices[0].Tu = 0f;
            vertices[0].Tv = 0f;
            vertices[1].Tu = 1f;
            vertices[1].Tv = 0f;
            vertices[2].Tu = 1f;
            vertices[2].Tv = 1f;
            vertices[3].Tu = 0f;
            vertices[3].Tv = 1f;

            vertexBuffer.Unlock();
        }

        protected void DrawPrimitives()
        {
            if (transparent)
            {
                DeviceObject.Device.RenderState.AlphaBlendEnable = true;
            }

            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, VerticesCount, 0, IndicesCount / 3);

            if (transparent)
            {
                DeviceObject.Device.RenderState.AlphaBlendEnable = false;
            }
        }

        protected void UpdateVerticesPosition()
        {
            CustomVertex.PositionColoredTextured[] vertices = (CustomVertex.PositionColoredTextured[])vertexBuffer.Lock(0, 0);

            vertices[0].Position = new Vector3(size.Width, 0f, size.Height);
            vertices[1].Position = new Vector3(-size.Width, 0f, size.Height);
            vertices[2].Position = new Vector3(-size.Width, 0f, -size.Height);
            vertices[3].Position = new Vector3(size.Width, 0f, -size.Height);

            vertexBuffer.Unlock();
        }

        protected void ApplyColorToVertices(Color color)
        {
            CustomVertex.PositionColoredTextured[] vertices = (CustomVertex.PositionColoredTextured[])vertexBuffer.Lock(0, 0);

            int colorInARGB = color.ToArgb();
            vertices[0].Color = colorInARGB;
            vertices[1].Color = colorInARGB;
            vertices[2].Color = colorInARGB;
            vertices[3].Color = colorInARGB;

            vertexBuffer.Unlock();
        }

        protected void ApplyTransparencyToVertices(int transparency)
        {
            Color temp;
            temp = Color.FromArgb(transparency, DefaultColor.R, DefaultColor.G, DefaultColor.B);

            ApplyColorToVertices(temp);
        }

        protected void UpdateTranslation()
        {
            translationMat.Translate(position);
        }

        protected void UpdateScaling()
        {
            scaleMat.Scale(new Vector3(scaleFactor, scaleFactor, scaleFactor));
        }

        #region Overriden Members

        protected override void ApplyChangesToStruct()
        {
            Matrix res = scaleMat * rotationMat * translationMat;
            //Matrix res = scaleMat * translationMat;
            for (int cnt1 = 0; cnt1 < 2; cnt1++)
            {
                currentIS[cnt1].Point1 = Vector3.TransformCoordinate(etalonIS[cnt1].Point1, res);
                currentIS[cnt1].Point2 = Vector3.TransformCoordinate(etalonIS[cnt1].Point2, res);
                currentIS[cnt1].Point3 = Vector3.TransformCoordinate(etalonIS[cnt1].Point3, res);
            }
        }

        protected override void InitStruct()
        {
            etalonIS[0].Point1 = new Vector3(size.Width, 0f, size.Height);
            etalonIS[0].Point2 = new Vector3(-size.Width, 0f, size.Height);
            etalonIS[0].Point3 = new Vector3(-size.Width, 0f, -size.Height);

            etalonIS[1].Point1 = new Vector3(-size.Width, 0f, -size.Height);
            etalonIS[1].Point2 = new Vector3(size.Width, 0f, -size.Height);
            etalonIS[1].Point3 = new Vector3(size.Width, 0f, size.Height);

            currentIS[0].Point1 = new Vector3(size.Width, 0f, size.Height);
            currentIS[0].Point2 = new Vector3(-size.Width, 0f, size.Height);
            currentIS[0].Point3 = new Vector3(-size.Width, 0f, -size.Height);

            currentIS[1].Point1 = new Vector3(-size.Width, 0f, -size.Height);
            currentIS[1].Point2 = new Vector3(size.Width, 0f, -size.Height);
            currentIS[1].Point3 = new Vector3(size.Width, 0f, size.Height);
        }

        protected override void CreateVertexData(object sender, EventArgs e)
        {
            UpdateVerticesPosition();
            InitTextureCoordinates();
            ApplyColorToVertices(DefaultColor);
        }

        protected override void CreateIndexData(object sender, EventArgs e)
        {
            short[] indices = (short[])indexBuffer.Lock(0, 0);

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;

            indices[3] = 2;
            indices[4] = 3;
            indices[5] = 0;

            indexBuffer.Unlock();
        }

        public override void Render()
        {
            device.Transform.World = scaleMat * rotationMat * translationMat;
            device.RenderState.CullMode = Cull.None;
            LoadBuffers();
            DrawPrimitives();
        }

        public override bool Interacted(Point mouseCoords)
        {
            Ray ray;
            ray = Ray.GetRayFromScreenCoordinates(mouseCoords.X, mouseCoords.Y);

            Vertex i1 = ray.GetIntersectionPointWithTriangle(Vertex.FromVector3(currentIS[0].Point1), Vertex.FromVector3(currentIS[0].Point2), Vertex.FromVector3(currentIS[0].Point3));
            if (i1 != null)
            {
                interactionPoint = i1.Vector;
                return true;
            }

            Vertex i2 = ray.GetIntersectionPointWithTriangle(Vertex.FromVector3(currentIS[1].Point1), Vertex.FromVector3(currentIS[1].Point2), Vertex.FromVector3(currentIS[1].Point3));
            if (i2 != null)
            {
                interactionPoint = i2.Vector;
                return true;
            }

            return false;
        }

        public override void Dispose()
        {
            base.Dispose();
            texture.Dispose();
        }

        #endregion

        #region IScalable Members

        public void SetScale(float scaleFactor)
        {
            ScaleFactor = scaleFactor;
        }

        #endregion

        #region IColorable Members

        public void SetColor(Color color)
        {
            ApplyColorToVertices(color);
        }

        #endregion

        #region ITranspareable Members

        public void SetTransparency(int transparency)
        {
            ApplyTransparencyToVertices(transparency);
        }

        #endregion

        #region ISelectiveScalable Members

        public void ScaleWidth(float factor)
        {
            scaleMat = Matrix.Scaling(factor, 1f, 1f);
            ApplyChangesToStruct();
        }

        public void ScaleLength(float factor)
        {
        }

        public void ScaleHeight(float factor)
        {
            scaleMat = Matrix.Scaling(1f, 1f, factor);
            ApplyChangesToStruct();
        }

        #endregion
    }
}
