using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.Primitives
{
    [Serializable]
    public class DetailedHeightModeData : DetailedModeData
    {
        public DetailedHeightModeData(Size3 size, float stairHeight, Angle leftTopAngle, Angle rightTopAngle)
            : base(leftTopAngle, rightTopAngle)
        {
            this.size = size;
            this.stairHeight = stairHeight;

            UpdateAfterResizing();
        }

        public override float StairLength
        {
            set { }
        }

        public override float StairHeight
        {
            set 
            {
                stairHeight = value;
                UpdateAfterResizing();
            }
        }

        protected override void UpdateAfterResizing()
        {
            stairLength = stairHeight / (size.Z / size.Y);
            CalculateStairsNumber(size.Y / stairLength);

            CallDetailedModeSizeChanged();
        }
    }
}
