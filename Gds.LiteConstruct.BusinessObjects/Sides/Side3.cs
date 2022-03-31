using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;

namespace Gds.LiteConstruct.BusinessObjects.Sides
{
    [Serializable]
    internal class Side3 : SimpleSide
    {
        [NonSerialized]
        private Side3Dimension dimension;
        [NonSerialized]
        private TextureAllocation3 textureAllocation;

        protected override int VerticesCount
        {
            get { return 3; }
        }

        protected override int IndicesCount
        {
            get { return 3; }
        }

        protected override SideDimension Dimension
        {
            set { dimension = value as Side3Dimension; }
        }

        internal Side3(SideDimension dimension)
            : base(dimension)
        {
        }

        internal Side3(SideDimension dimension, TextureAllocation textureAllocation)
            : base(dimension, textureAllocation)
        {
        }

        private void SetVerticesPositions()
        {
            CustomVertex.PositionColoredTextured[] vertices = (CustomVertex.PositionColoredTextured[])vertexBuffer.Lock(0, 0);

            vertices[0].Position = dimension.P1.Vector;
            vertices[1].Position = dimension.P2.Vector;
            vertices[2].Position = dimension.P3.Vector;

            vertexBuffer.Unlock();
        }

        private void SetVerticesTextureAllocation()
        {
            CustomVertex.PositionColoredTextured[] vertices = (CustomVertex.PositionColoredTextured[])vertexBuffer.Lock(0, 0);

            vertices[0].Tu = textureAllocation.T1.X;
            vertices[0].Tv = textureAllocation.T1.Y;
            vertices[1].Tu = textureAllocation.T2.X;
            vertices[1].Tv = textureAllocation.T2.Y;
            vertices[2].Tu = textureAllocation.T3.X;
            vertices[2].Tv = textureAllocation.T3.Y;

            vertexBuffer.Unlock();
        }

        private TransformedPoint[] GetTransformedPoints()
        {
            TransformedPoint[] tPoints = new TransformedPoint[3];

            Vector2 tPoint;
            float vec12Len, vec23Len;
            Vector2 vec12, vec23;
            Vector2 xVec = new Vector2(1f, 0f);

            vec12Len = Vector3.Length(dimension.P2.Vector - dimension.P1.Vector);
            vec23Len = Vector3.Length(dimension.P3.Vector - dimension.P2.Vector);

            vec12 = xVec * vec12Len;

            tPoint = new Vector2(100f, 100f);
            tPoints[0] = new TransformedPoint(dimension.P1, tPoint);

            tPoint += vec12;
            tPoints[1] = new TransformedPoint(dimension.P2, tPoint);

            vec12 = -vec12;

            Angle angle;
            angle = -Vector3Utils.AngleBetweenVectors(dimension.P1.Vector - dimension.P2.Vector, dimension.P3.Vector - dimension.P2.Vector);

            Matrix rotMat;
            rotMat = Matrix.RotationZ(angle.Radians);

            vec23 = Vector2.TransformCoordinate(vec12, rotMat);
            vec23 = Vector2.Normalize(vec23) * vec23Len;

            tPoint += vec23;
            tPoints[2] = new TransformedPoint(dimension.P3, tPoint);

            return tPoints;
        }

        #region Overriden Members

        protected override void CreateVertexData(object sender, EventArgs args)
        {
            SetVerticesPositions();
            SetVerticesTextureAllocation();
            SetVerticesColor();
        }

        protected override void CreateIndexData(object sender, EventArgs args)
        {
            short[] indices = (short[])indexBuffer.Lock(0, 0);
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indexBuffer.Unlock();
        }

        internal override void UpdatePosition()
        {
            CustomVertex.PositionColoredTextured[] vertices = (CustomVertex.PositionColoredTextured[])vertexBuffer.Lock(0, 0);

            vertices[0].Position = dimension.P1.Vector;
            vertices[1].Position = dimension.P2.Vector;
            vertices[2].Position = dimension.P3.Vector;

            vertexBuffer.Unlock();
        }

        internal override Vertex GetIntersectionPoint(Ray ray)
        {
            return ray.GetIntersectionPointWithTriangle(dimension.P1, dimension.P2, dimension.P3);
        }

        protected override void SetDefaultTextureAllocation()
        {
            textureAllocation = new TextureAllocation3(new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0.5f, 1f));
        }

        protected override TextureAllocation TextureAllocation
        {
            set { textureAllocation = value as TextureAllocation3; }
        }

        public override void SetTextureAllocation(TextureAllocation allocation)
        {
            this.textureAllocation = allocation as TextureAllocation3;
            SetVerticesTextureAllocation();
        }

        protected override TransformedPoint[] TransformedPoints
        {
            get { return GetTransformedPoints(); }
        }

        protected override void ApplyTextureCoordinates()
        {
            Vector2 t1, t2, t3;
            t1 = rotator.GetTextureCoordinatesForPoint(dimension.P1);
            t2 = rotator.GetTextureCoordinatesForPoint(dimension.P2);
            t3 = rotator.GetTextureCoordinatesForPoint(dimension.P3);

            SetTextureAllocation(new TextureAllocation3(t1, t2, t3));
        }

        #endregion
    }
}
