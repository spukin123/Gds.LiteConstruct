using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;

namespace Gds.LiteConstruct.BusinessObjects.Sides
{
    [Serializable]
    public class StairsTopSide : MasterSide<Side4Dimension>
    {
        [NonSerialized]
        private Side4Dimension[] dimensions;

        [NonSerialized]
        private float curTY;

        public StairsTopSide(SideDimension[] dimensions)
            : base(dimensions)
        {
        }

        private float GetChildLengthPerpZ(Side4Dimension dimension)
        {
            return (float)Math.Abs(dimension.P1.Y - dimension.P2.Y);
        }

        private float GetChildLengthPerpY(Side4Dimension dimension)
        {
            return (float)Math.Abs(dimension.P1.Z - dimension.P2.Z);
        }

        private float GetChildHeight(Side4Dimension dimension)
        {
            return (float)Math.Abs(dimension.P1.X - dimension.P4.X);
        }

        private void FindSideSize()
        {
            sideHeight = 0f;
            sideWidth = 0f;

            sideHeight = GetChildHeight(dimensions[0]);
            for (int cnt = 0; cnt < dimensions.Length; cnt++)
            {
                if (cnt % 2 == 0)
                {
                    sideWidth += GetChildLengthPerpY(dimensions[cnt]);
                }
                else
                {
                    sideWidth += GetChildLengthPerpZ(dimensions[cnt]);
                }
            }
        }

        private TransformedPoint[] GenerateTransformedPoints()
        {
            List<TransformedPoint> newPoints = new List<TransformedPoint>();
            
            TransformedPoint newItem;
            Vector2 projectedPoint;
            Vector2 yVec = new Vector2(0f, -1f);

            Vector2 currentLeftPoint, currentRightPoint;
            currentLeftPoint = new Vector2(dimensions[0].P1.X, dimensions[0].P1.Y);
            currentRightPoint = new Vector2(dimensions[0].P4.X, dimensions[0].P4.Y);

            for (int cnt = 0; cnt < dimensions.Length; cnt++)
            {
                if (cnt % 2 == 0)
                {
                    newItem = new TransformedPoint(dimensions[cnt].P1, currentLeftPoint);
                    newPoints.Add(newItem);

                    newItem = new TransformedPoint(dimensions[cnt].P4, currentRightPoint);
                    newPoints.Add(newItem);

                    currentLeftPoint += yVec * GetChildLengthPerpY(dimensions[cnt]);
                    newItem = new TransformedPoint(dimensions[cnt].P2, currentLeftPoint);
                    newPoints.Add(newItem);

                    currentRightPoint += yVec * GetChildLengthPerpY(dimensions[cnt]);
                    newItem = new TransformedPoint(dimensions[cnt].P3, currentRightPoint);
                    newPoints.Add(newItem);
                }
                else
                {
                    newItem = new TransformedPoint(dimensions[cnt].P1, currentLeftPoint);
                    newPoints.Add(newItem);

                    newItem = new TransformedPoint(dimensions[cnt].P4, currentRightPoint);
                    newPoints.Add(newItem);

                    //--
                    currentLeftPoint += GetLeftBorderVector(dimensions[cnt]);
                    newItem = new TransformedPoint(dimensions[cnt].P2, currentLeftPoint);
                    newPoints.Add(newItem);

                    currentRightPoint += GetRightBorderVector(dimensions[cnt]);
                    newItem = new TransformedPoint(dimensions[cnt].P3, currentRightPoint);
                    newPoints.Add(newItem);
                }
            }

            return newPoints.ToArray();
        }

        private Vector2 GetLeftBorderVector(Side4Dimension dimension)
        {
            Vector3 result;
            result = dimension.P2.Vector - dimension.P1.Vector;

            return PointConverter.Vector3ToVectorXY(result);
        }

        private Vector2 GetRightBorderVector(Side4Dimension dimension)
        {
            Vector3 result;
            result = dimension.P3.Vector - dimension.P4.Vector;

            return PointConverter.Vector3ToVectorXY(result);
        }

        #region OverridenMembers

        protected override int DimensionsNumber
        {
            get { return dimensions.Length; }
        }

        protected override TransformedPoint[] TransformedPoints
        {
            get { return GenerateTransformedPoints(); }
        }

        protected override void DoInitialize(Side4Dimension[] dimensions)
        {
            this.dimensions = dimensions;

            CreateChildren();
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
        //    curTY = 1f;
        //    FindSideSize();
        //}

        //protected override TextureAllocation GetTextureCoordinatesFromDimension(int dimensionIndex)
        //{
        //    float curTX;
        //    float deltaTX, deltaTY;
        //    Vector2 t1, t2, t3, t4;

        //    deltaTX = 1f;
        //    curTX = 0f;

        //    if (dimensionIndex % 2 == 0)
        //    {
        //        deltaTY = GetChildLengthPerpY(dimensions[dimensionIndex]) / sideWidth;
        //    }
        //    else
        //    {
        //        deltaTY = GetChildLengthPerpZ(dimensions[dimensionIndex]) / sideWidth;
        //    }

        //    t1 = new Vector2(curTX, curTY);

        //    curTY -= deltaTY;
        //    t2 = new Vector2(curTX, curTY);

        //    curTX += deltaTX;
        //    t3 = new Vector2(curTX, curTY);

        //    curTY += deltaTY;
        //    t4 = new Vector2(curTX, curTY);

        //    curTY -= deltaTY;

        //    return new TextureAllocation4(t1, t2, t3, t4);
        //}

        protected override void CreateSide(int index)
        {
            children[index] = new Side4(dimensions[index]);
        }

        #endregion
    }
}
