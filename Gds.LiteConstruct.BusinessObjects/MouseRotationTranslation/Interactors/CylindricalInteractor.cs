using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using System.Drawing;
using Microsoft.DirectX.Direct3D;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interactors
{
    public abstract class CylindricalInteractor : MouseInteractor3D
    {
        protected Ray axis;
        private float radius;
        protected int sidesNumber;

        private Vector3 position;
        protected Cylinder cylinder;

        private Matrix translationMatrix = Matrix.Identity;
        protected Matrix scaleMatrix = Matrix.Identity;
        private Matrix rotationMatrix = Matrix.Identity;

        private int indexCnt;

        protected Vector3 HalfDirection
        {
            get { return axis.Direction * 0.5f; }
        }

        private bool transparent = false;
        public bool Transparent
        {
            get { return transparent; }
            set { transparent = value; }
        }

        protected abstract int TrianglesNumber { get; }

        public CylindricalInteractor(Ray axis, int sidesNumber, float radius, Color color)
            : base(color)
        {
            this.axis = axis;
            this.sidesNumber = sidesNumber;
            this.radius = radius;
        }

        protected void InitStartRotation()
        {
            AxisAngle axisAngle;
            axisAngle = Vector3Utils.TransitionRotationByAxis(Vector3Utils.AlignedZVector, axis.Direction);

            rotationMatrix = axisAngle.GetRotationMatrix();
        }

        protected void ApplyColorToVertices()
        {
            CustomVertex.PositionColored[] vertices = LockVB();

            int colorARGB = defaultColor.ToArgb();
            for (int cnt = 0; cnt < vertices.Length; cnt++)
            {
                vertices[cnt].Color = colorARGB;
            }

            UnlockVB();
        }

        #region Overriden Members

        protected override Vector3 Origin
        {
            get { return position; }
            set
            {
                position = value;
                translationMatrix = Matrix.Translation(value);
                ApplyChangesToStruct();
            }
        }

        protected override void InitBuffers()
        {
            vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionColored), VerticesCount, device, Usage.Dynamic, CustomVertex.PositionColored.Format, Pool.Default);
            indexBuffer = new IndexBuffer(typeof(short), IndicesCount, device, Usage.Dynamic, Pool.Default);

            ConnectToBuffersEvents();

            CreateVertexData(vertexBuffer, null);
            CreateIndexData(indexBuffer, null);
        }

        protected override void ApplyChangesToStruct()
        {
            Matrix result = scaleMatrix * rotationMatrix * translationMatrix;

            for (int cnt = 0; cnt < TrianglesNumber; cnt++)
            {
                currentIS[cnt] = etalonIS[cnt].Transform(result);
            }
        }

        public override bool Interacted(Point mouseCoords)
        {
            Ray ray;
            ray = Ray.GetRayFromScreenCoordinates(mouseCoords.X, mouseCoords.Y);

            for (int cnt = 0; cnt < TrianglesNumber; cnt++)
            {
                if (ray.GetIntersectionPointWithTriangle(currentIS[cnt]) != null)
                {
                    return true;
                }
            }

            return false;
        }

        protected override void InitStruct()
        {
            etalonIS = new Triangle[TrianglesNumber];
            currentIS = new Triangle[TrianglesNumber];

            short[] indices = LockIB();
            CustomVertex.PositionColored[] vertices = LockVB();

            indexCnt = 0;
            Vector3 point1, point2, point3;
            for (int cnt = 0; cnt < TrianglesNumber; cnt++)
            {
                point1 = vertices[indices[indexCnt]].Position;
                indexCnt++;
                point2 = vertices[indices[indexCnt]].Position;
                indexCnt++;
                point3 = vertices[indices[indexCnt]].Position;
                indexCnt++;

                etalonIS[cnt] = new Triangle(point1, point2, point3);
            }

            UnlockVB();
            UnlockIB();
        }

        public override void Render()
        {
            device.Transform.World = scaleMatrix * rotationMatrix * translationMatrix;
            device.VertexFormat = CustomVertex.PositionColored.Format;

            device.SetStreamSource(0, vertexBuffer, 0);
            device.Indices = indexBuffer;

            if (transparent)
            {
                DeviceObject.Device.RenderState.AlphaBlendEnable = true;
            }

            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, VerticesCount, 0, TrianglesNumber);

            if (transparent)
            {
                DeviceObject.Device.RenderState.AlphaBlendEnable = false;
            }
        }

        #endregion

        #region BuffersManagement

        protected CustomVertex.PositionColored[] LockVB()
        {
            return (CustomVertex.PositionColored[])vertexBuffer.Lock(0, 0f);
        }

        protected void UnlockVB()
        {
            vertexBuffer.Unlock();
        }

        protected short[] LockIB()
        {
            return (short[])indexBuffer.Lock(0, 0);
        }

        protected void UnlockIB()
        {
            indexBuffer.Unlock();
        }

        #endregion
    }
}
