using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.SizeTypes
{
    public interface IStairsExtendable : IStairsConstraint, IStairsApplyable
    {
        int StairsNumber { get; set; }
        float StairLength { get; set; }
        float StairHeight { get; set; }
        Angle LeftTopAngle { get; set; }
        Angle RightTopAngle { get; set; }
        float BottomBorderLength { get; }

        bool SimpleMode { get; }
        bool StairHeightMode { get; }
        bool StairLengthMode { get; }

        void EnableSimpleMode();
        void EnableDetailedHeightMode();
        void EnableDetailedLengthMode();
    }
}
