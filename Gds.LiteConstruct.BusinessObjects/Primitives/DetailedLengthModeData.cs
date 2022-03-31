using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.Primitives
{
    [Serializable]
    public class DetailedLengthModeData : DetailedModeData
    {
        public DetailedLengthModeData(Size3 size, float stairLength, Angle leftTopAngle, Angle rightTopAngle)
            : base(leftTopAngle, rightTopAngle)
        {
            this.size = size;
            this.stairLength = stairLength;

            UpdateAfterResizing();
        }

        public override float StairLength
        {
            set
            {
                stairLength = value;
                UpdateAfterResizing();
            }
        }

        public override float StairHeight
        {
            set { }
        }

        protected override void UpdateAfterResizing()
        {
            stairHeight = stairLength * (size.Z / size.Y);
            CalculateStairsNumber(size.Z / stairHeight);

            CallDetailedModeSizeChanged();
        }
    }
}
