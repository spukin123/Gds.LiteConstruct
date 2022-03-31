using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.Primitives
{
    [Serializable]
    public class SimpleModeData : StairsData
    {
        public override float X
        {
            set
            {
                size.X = value;
                FindBordersLength();

                CallSimpleModeSizeChanged();
            }
        }

        public override float Y
        {
            set
            {
                size.Y = value;
                FindBordersLength();
                UpdateAfterResizing();

                CallSimpleModeSizeChanged();
            }
        }

        public override float Z
        {
            set
            {
                size.Z = value;
                UpdateAfterResizing();

                CallSimpleModeSizeChanged();
            }
        }

        public override float StairHeight
        {
            set { }
        }

        public override float StairLength
        {
            set { }
        }

        public override int StairsNumber
        {
            set
            {
                stairsNum = value;
                UpdateAfterResizing();

                CallDetailedModeSizeChanged();
            }
        }

        public SimpleModeData(Size3 size, int stairsNum, Angle leftTopAngle, Angle rightTopAngle) 
            : base(leftTopAngle, rightTopAngle)
        {
            this.size = size;
            this.stairsNum = stairsNum;

            UpdateAfterResizing();
        }

        protected override void UpdateAfterResizing()
        {
            stairHeight = size.Z / stairsNum;
            stairLength = size.Y / stairsNum;
        }
    }
}
