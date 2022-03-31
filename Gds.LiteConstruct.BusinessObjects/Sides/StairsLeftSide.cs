using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects.Sides
{
    [Serializable]
    public class StairsLeftSide : StairsSide
    {
        //[NonSerialized]
        //private float curTX;

        public StairsLeftSide(SideDimension[] dimensions)
            : base(dimensions)
        {
        }

        private TransformedPoint[] GetTransformedPoints()
        {
            List<TransformedPoint> tPoints = new List<TransformedPoint>();
            
            TransformedPoint item;
            Vector2 mainPoint;
            Vector2 xVec = new Vector2(-1f, 0f);
            Vector2 yVec = new Vector2(0f, -1f);
            Vector2 transformedPoint;

            mainPoint = new Vector2(100f, 100f);
            for (int cnt = 0; cnt < dimensions.Length; cnt++)
            {
                transformedPoint = mainPoint + GetChildLength(dimensions[cnt]) * xVec + GetChildHeight(dimensions[cnt]) * yVec;
                item = new TransformedPoint(dimensions[cnt].P1, transformedPoint);
                tPoints.Add(item);

                transformedPoint = mainPoint + GetChildHeight(dimensions[cnt]) * yVec;
                item = new TransformedPoint(dimensions[cnt].P2, transformedPoint);
                tPoints.Add(item);

                transformedPoint = mainPoint;
                item = new TransformedPoint(dimensions[cnt].P3, transformedPoint);
                tPoints.Add(item);

                transformedPoint = mainPoint + GetChildLength(dimensions[cnt]) * xVec;
                item = new TransformedPoint(dimensions[cnt].P4, transformedPoint);
                tPoints.Add(item);

                mainPoint += GetChildLength(dimensions[cnt]) * xVec;
            }

            return tPoints.ToArray();
        }

        #region Overriden Members

        protected override TransformedPoint[] TransformedPoints
        {
            get { return GetTransformedPoints(); }
        }

        protected override int DimensionsNumber
        {
            get { return dimensions.Length; }
        }

        protected override void DoInitialize(Side4Dimension[] dimensions)
        {
            this.dimensions = dimensions;
            
            CreateChildren();
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

        //protected override void CreateSide(int index, TextureAllocation allocation)
        //{
        //    children[index] = new Side4(dimensions[index], allocation as TextureAllocation4);
        //}

        

        //protected override void PrepareTextureGeneration()
        //{
        //    FindSideSize();
        //    curTX = 1;
        //}

        //protected override TextureAllocation GetTextureCoordinatesFromDimension(int dimensionIndex)
        //{
        //    float curTY;
        //    float deltaTX, deltaTY;
        //    Vector2 t1, t2, t3, t4;

        //    #region Old algorithm
        //    //for (cnt = 0; cnt < dimensions.Length; cnt++)
        //    //{
        //    //    deltaTX = GetChildLength(dimensions[cnt]) / sideLength;
        //    //    deltaTY = GetChildHeight(dimensions[cnt]) / sideHeight;

        //    //    curTX = deltaTX * (cnt + 1);
        //    //    curTY = 1 - deltaTY;

        //    //    t1 = new Vector2(curTX, curTY);

        //    //    curTX -= deltaTX;
        //    //    t2 = new Vector2(curTX, curTY);

        //    //    curTY += deltaTY;
        //    //    t3 = new Vector2(curTX, curTY);

        //    //    curTX += deltaTX;
        //    //    t4 = new Vector2(curTX, curTY);

        //    //    children[cnt] = new Side4(dimensions[cnt], new TextureAllocation4(t1, t2, t3, t4));
        //    //}
        //    #endregion

        //    deltaTX = GetChildLength(dimensions[dimensionIndex]) / sideWidth;
        //    deltaTY = GetChildHeight(dimensions[dimensionIndex]) / sideHeight;

        //    curTY = 1 - deltaTY;
        //    curTX -= deltaTX;

        //    t1 = new Vector2(curTX, curTY);

        //    curTX += deltaTX;
        //    t2 = new Vector2(curTX, curTY);

        //    curTY += deltaTY;
        //    t3 = new Vector2(curTX, curTY);

        //    curTX -= deltaTX;
        //    t4 = new Vector2(curTX, curTY);

        //    return new TextureAllocation4(t1, t2, t3, t4);
        //}

        #endregion
    }
}
