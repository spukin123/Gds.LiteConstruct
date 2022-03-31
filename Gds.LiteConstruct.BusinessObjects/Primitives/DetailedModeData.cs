using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.Primitives
{
    [Serializable]
    public abstract class DetailedModeData : StairsData
    {
        public override float X
        {
            set
            {
                size.X = value;
                FindBordersLength();

                UpdateAfterResizing();
            }
        }

        public override float Y
        {
            set
            {
                size.Y = value;
                FindBordersLength();

                UpdateAfterResizing();
            }
        }

        public override float Z
        {
            set
            {
                size.Z = value;
                UpdateAfterResizing();
            }
        }
        
        public override int StairsNumber
        {
            set { }
        }

        public DetailedModeData(Angle leftTopAngle, Angle rightTopAngle)
            : base(leftTopAngle, rightTopAngle)
        {
        }

        protected void CalculateStairsNumber(float res)
        {
            if (res - (int)res != 0f)
            {
                stairsNum = (int)res + 1;
            }
            else
            {
                stairsNum = (int)res;
            }
        }
    }
}
