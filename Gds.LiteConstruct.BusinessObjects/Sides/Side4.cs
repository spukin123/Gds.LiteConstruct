using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;

namespace Gds.LiteConstruct.BusinessObjects.Sides
{
    [Serializable]
    internal class Side4 : SimpleSide
    {
        [NonSerialized]
        private Side4Dimension dimension;
        [NonSerialized]
        private TextureAllocation4 textureAllocation;

        protected override int VerticesCount
        {
            get { return 4; }
        }

        protected override int IndicesCount
        {
            get { return 6; }
        }

        protected override SideDimension Dimension
        {
            set { dimension = value as Side4Dimension; }
        }

        private float P1P2Length
        {
            get { return Vector3.Length(dimension.P2.Vector - dimension.P1.Vector); }
        }

        private float P2P3Length
        {
            get { return Vector3.Length(dimension.P3.Vector - dimension.P2.Vector); }
        }

        private float P3P4Length
        {
            get { return Vector3.Length(dimension.P4.Vector - dimension.P3.Vector); }
        }

        private Vector2 P2P1Vec
        {
            get 
            { 
                return new Vector2(1f, 0f); 
            }
        }

        private Vector2 P3P2Vec
        {
            get 
            {
                Angle angle;
                angle = Vector3Utils.AngleBetweenVectors(dimension.P2.Vector - dimension.P1.Vector, dimension.P3.Vector - dimension.P2.Vector);

                Matrix rotMat;
                rotMat = Matrix.RotationZ((Angle.A180 - angle).Radians);

                return Vector2.TransformCoordinate(P2P1Vec, rotMat);
            }
        }

        private Vector2 P4P3Vec
        {
            get
            {
                Angle angle;
                angle = Vector3Utils.AngleBetweenVectors(dimension.P4.Vector - dimension.P3.Vector, dimension.P3.Vector - dimension.P2.Vector);

                Matrix rotMat;
                rotMat = Matrix.RotationZ((Angle.A180 - angle).Radians);

                return Vector2.TransformCoordinate(P3P2Vec, rotMat);
            }
        }

        internal Side4(SideDimension dimension)
            : base(dimension)
        {
        }

        internal Side4(SideDimension dimension, TextureAllocation textureAllocation)
            : base(dimension, textureAllocation)
        {
        }

        private void SetVerticesPositions()
        {
            CustomVertex.PositionColoredTextured[] vertices = (CustomVertex.PositionColoredTextured[])vertexBuffer.Lock(0, 0);

            vertices[0].Position = dimension.P1.Vector;
            vertices[1].Position = dimension.P2.Vector;
            vertices[2].Position = dimension.P3.Vector;
            vertices[3].Position = dimension.P4.Vector;

            vertexBuffer.Unlock();
        }

        private void SetVerticesTextureAllocation()
        {
            CustomVertex.PositionColoredTextured[] vertices = (CustomVertex.PositionColoredTextured[])vertexBuffer.Lock(0, 0);
#warning Bug here when resizing
            vertices[0].Tu = textureAllocation.T1.X;
            vertices[0].Tv = textureAllocation.T1.Y;
            vertices[1].Tu = textureAllocation.T2.X;
            vertices[1].Tv = textureAllocation.T2.Y;
            vertices[2].Tu = textureAllocation.T3.X;
            vertices[2].Tv = textureAllocation.T3.Y;
            vertices[3].Tu = textureAllocation.T4.X;
            vertices[3].Tv = textureAllocation.T4.Y;

            vertexBuffer.Unlock();
        }

        private TransformedPoint[] GetTransformedPoints()
        {
            TransformedPoint[] tPoints = new TransformedPoint[4];
            Vector2 tPoint;
            
            tPoint = new Vector2(100f, 100f);
            tPoints[0] = new TransformedPoint(dimension.P1, tPoint);

            tPoint += P2P1Vec * P1P2Length;
            tPoints[1] = new TransformedPoint(dimension.P2, tPoint);

            tPoint += P3P2Vec * P2P3Length;
            tPoints[2] = new TransformedPoint(dimension.P3, tPoint);

            tPoint += P4P3Vec * P3P4Length;
            tPoints[3] = new TransformedPoint(dimension.P4, tPoint);

            return tPoints;
        }

        #region Overriden Members

        protected override void CreateVertexData(object sender, EventArgs args)
        {
            SetVerticesPositions();
            SetVerticesTextureAllocation();
            SetVerticesColor();
        }

        public override void SetTextureAllocation(TextureAllocation allocation)
        {
            this.textureAllocation = allocation as TextureAllocation4;
            SetVerticesTextureAllocation();
        }

        protected override void CreateIndexData(object sender, EventArgs args)
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

        internal override void UpdatePosition()
        {
            CustomVertex.PositionColoredTextured[] vertices = (CustomVertex.PositionColoredTextured[])vertexBuffer.Lock(0, 0);

            vertices[0].Position = dimension.P1.Vector;
            vertices[1].Position = dimension.P2.Vector;
            vertices[2].Position = dimension.P3.Vector;
            vertices[3].Position = dimension.P4.Vector;

            vertexBuffer.Unlock();
        }

        internal override Vertex GetIntersectionPoint(Ray ray)
        {
            Vertex point = ray.GetIntersectionPointWithTriangle(dimension.P1, dimension.P2, dimension.P3);
            if (point != null)
            {
                return point;
            }
            return ray.GetIntersectionPointWithTriangle(dimension.P3, dimension.P4, dimension.P1);
        }

        protected override void SetDefaultTextureAllocation()
        {
            textureAllocation = new TextureAllocation4(new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(0f, 1f));
        }

        protected override TextureAllocation TextureAllocation
        {
            set { textureAllocation = value as TextureAllocation4; }
        }

        protected override TransformedPoint[] TransformedPoints
        {
            get { return GetTransformedPoints(); }
        }

        protected override void ApplyTextureCoordinates()
        {
            Vector2 t1, t2, t3, t4;
            t1 = rotator.GetTextureCoordinatesForPoint(dimension.P1);
            t2 = rotator.GetTextureCoordinatesForPoint(dimension.P2);
            t3 = rotator.GetTextureCoordinatesForPoint(dimension.P3);
            t4 = rotator.GetTextureCoordinatesForPoint(dimension.P4);

            SetTextureAllocation(new TextureAllocation4(t1, t2, t3, t4));
        }

        #endregion
    }
}
