using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects.Sides
{
    [Serializable]
    internal class ColumnStubeSide : MasterSide<Side3Dimension>
    {
        [NonSerialized]
        private Side3Dimension[] dimensions;

        public ColumnStubeSide(SideDimension[] dimensions)
            : base(dimensions)
        {
        }

        private TransformedPoint[] GetTransformedPoints()
        {
            List<TransformedPoint> tPoints = new List<TransformedPoint>();
            TransformedPoint item;
            for (int cnt = 0; cnt < dimensions.Length; cnt++)
            {
                item = new TransformedPoint(dimensions[cnt].P1, PointConverter.Vector3ToVectorXY(dimensions[cnt].P1.Vector));
                tPoints.Add(item);

                item = new TransformedPoint(dimensions[cnt].P2, PointConverter.Vector3ToVectorXY(dimensions[cnt].P2.Vector));
                tPoints.Add(item);

                item = new TransformedPoint(dimensions[cnt].P3, PointConverter.Vector3ToVectorXY(dimensions[cnt].P3.Vector));
                tPoints.Add(item);
            }

            return tPoints.ToArray();
        }

        #region Overriden Members

        protected override int DimensionsNumber
        {
            get { return dimensions.Length; }
        }

        protected override TransformedPoint[] TransformedPoints
        {
            get { return GetTransformedPoints(); }
        }

        protected override void DoInitialize(Side3Dimension[] dimensions)
        {
            this.dimensions = dimensions;

            CreateChildren();
        }

        protected override void CreateSide(int index)
        {
            children[index] = new Side3(dimensions[index]);
        }

        protected override void ApplyTextureCoordinates()
        {
            for (int cnt = 0; cnt < dimensions.Length; cnt++)
            {
                Vector2 t1, t2, t3;
                t1 = rotator.GetTextureCoordinatesForPoint(dimensions[cnt].P1);
                t2 = rotator.GetTextureCoordinatesForPoint(dimensions[cnt].P2);
                t3 = rotator.GetTextureCoordinatesForPoint(dimensions[cnt].P3);

                children[cnt].SetTextureAllocation(new TextureAllocation3(t1, t2, t3));
            }
        }

        //protected override TextureAllocation GetTextureCoordinatesFromDimension(int dimensionIndex)
        //{
        //    Vector2 center = new Vector2(0.5f, 0.5f);
        //    Vector2 t1, t2, t3;
        //    Vector2 v1, v2, v3;

        //    v1 = Vector2.Normalize(PointConverter.Vector3ToVectorXY(dimensions[dimensionIndex].P1.Vector));
        //    v2 = Vector2.Normalize(PointConverter.Vector3ToVectorXY(dimensions[dimensionIndex].P2.Vector));
        //    v3 = Vector2.Normalize(PointConverter.Vector3ToVectorXY(dimensions[dimensionIndex].P3.Vector));

        //    t1 = center + v1 * 0.5f;
        //    t2 = center + v2 * 0.5f;
        //    t3 = center + v3 * 0.5f;

        //    return new TextureAllocation3(t1, t2, t3);
        //}

        //protected override void PrepareTextureGeneration()
        //{
        //}

        //protected override void CreateSide(int index, TextureAllocation allocation)
        //{
        //    children[index] = new Side3(dimensions[index], allocation as TextureAllocation3);
        //}

        #endregion
    }
}
