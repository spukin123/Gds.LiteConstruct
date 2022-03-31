using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects.Sides
{
    [Serializable]
    public abstract class StairsSide : MasterSide<Side4Dimension>
    {
        [NonSerialized]
        protected Side4Dimension[] dimensions;

        public StairsSide(SideDimension[] dimensions)
            : base(dimensions)
        {
        }
        
        protected float GetChildHeight(Side4Dimension dimension)
        {
            return Math.Abs(dimension.P1.Z - dimension.P4.Z);
        }

        protected float GetChildLength(Side4Dimension dimension)
        {
            return Math.Abs(dimension.P1.Y - dimension.P2.Y);
        }

        protected void FindSideSize()
        {
            sideHeight = GetChildHeight(dimensions[dimensions.Length - 1]);
            sideWidth = 0f;
            foreach (Side4Dimension dimension in dimensions)
            {
                sideWidth += GetChildLength(dimension);
            }
        }

        #region Overriden Members

        protected override int DimensionsNumber
        {
            get { return dimensions.Length; }
        }

        #endregion
    }
}
