using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects.Sides
{
    [Serializable]
    internal class ColumnBodySide : MasterSide<Side4Dimension>
    {
        [NonSerialized]
        private Side4Dimension[] dimensions;
        [NonSerialized]
        private float curTX;

        public ColumnBodySide(SideDimension[] dimensions)
            : base(dimensions)
        {
        }

        private void FindSideSize()
        {
            sideHeight = GetChildHeight(dimensions[0]);
            sideWidth = 0f;
            for (int cnt = 0; cnt < DimensionsNumber; cnt++)
            {
                sideWidth += GetChildWidth(dimensions[cnt]);
            }
        }

        private float GetChildHeight(Side4Dimension dimension)
        {
            return dimension.P1.Z - dimension.P4.Z;
        }

        private float GetChildWidth(Side4Dimension dimension)
        {
            return Vector3.Length(dimension.P2.Vector - dimension.P1.Vector);
        }

        private TransformedPoint[] GetTransformedPoints()
        {
            List<TransformedPoint> tPoints = new List<TransformedPoint>();
            
            TransformedPoint item;
            Vector2 xVec = new Vector2(-1f, 0f);
            Vector2 yVec = new Vector2(0f, -1f);
            Vector2 farCurrentPoint, nearCurrentPoint;

            nearCurrentPoint = new Vector2(100f, 100f);
            farCurrentPoint = nearCurrentPoint + yVec * GetChildHeight(dimensions[0]);

            for (int cnt = 0; cnt < dimensions.Length; cnt++)
            {
                item = new TransformedPoint(dimensions[cnt].P2, farCurrentPoint);
                tPoints.Add(item);

                item = new TransformedPoint(dimensions[cnt].P3, nearCurrentPoint);
                tPoints.Add(item);

                farCurrentPoint += xVec * GetChildWidth(dimensions[cnt]);
                item = new TransformedPoint(dimensions[cnt].P1, farCurrentPoint);
                tPoints.Add(item);

                nearCurrentPoint += xVec * GetChildWidth(dimensions[cnt]);
                item = new TransformedPoint(dimensions[cnt].P4, nearCurrentPoint);
                tPoints.Add(item);
            }

            return tPoints.ToArray();
        }

        #region Overriden Members

        protected override void DoInitialize(Side4Dimension[] dimensions)
        {
            this.dimensions = dimensions;
            CreateChildren();
        }

        protected override int DimensionsNumber
        {
            get { return dimensions.Length; }
        }

        protected override TransformedPoint[] TransformedPoints
        {
            get { return GetTransformedPoints(); }
        }

        protected override void CreateSide(int index)
        {
            children[index] = new Side4(dimensions[index]);
        }

        protected override void ApplyTextureCoordinates()
        {
            for (int cnt = 0; cnt < dimensions.Length; cnt++)
            {
                Vector2 t1, t2, t3, t4;
                t1 = rotator.GetTextureCoordinatesForPoint(dimensions[cnt].P1);
                t2 = rotator.GetTextureCoordinatesForPoint(dimensions[cnt].P2);
                t3 = rotator.GetTextureCoordinatesForPoint(dimensions[cnt].P3);
                t4 = rotator.GetTextureCoordinatesForPoint(dimensions[cnt].P4);

                children[cnt].SetTextureAllocation(new TextureAllocation4(t1, t2, t3, t4));
            }
        }

        //protected override void PrepareTextureGeneration()
        //{
        //    FindSideSize();
        //    curTX = 1f;
        //}

        //protected override TextureAllocation GetTextureCoordinatesFromDimension(int dimensionIndex)
        //{
        //    Vector2 t1, t2, t3, t4;
        //    float curTY;
        //    float deltaTX, deltaTY;

        //    deltaTX = GetChildWidth(dimensions[dimensionIndex]) / sideWidth;
        //    deltaTY = GetChildHeight(dimensions[dimensionIndex]) / sideHeight;

        //    curTX -= deltaTX;
        //    curTY = 0f;
        //    t1 = new Vector2(curTX, curTY);

        //    curTX += deltaTX;
        //    t2 = new Vector2(curTX, curTY);

        //    curTY += deltaTY;
        //    t3 = new Vector2(curTX, curTY);

        //    curTX -= deltaTX;
        //    t4 = new Vector2(curTX, curTY);

        //    return new TextureAllocation4(t1, t2, t3, t4);
        //}

        //protected override void CreateSide(int index, TextureAllocation allocation)
        //{
        //    children[index] = new Side4(dimensions[index], allocation as TextureAllocation4);
        //}

        #endregion
    }
}
