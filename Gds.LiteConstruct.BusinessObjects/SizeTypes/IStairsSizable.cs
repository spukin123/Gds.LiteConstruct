using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.SizeTypes
{
    public interface IStairsSizable : IStairsConstraint, IStairsApplyable
    {
        float X { get; set; }
        float Y { get; set; }
        float Z { get; set; }

        void Scale(float scaleFactor);
    }
}
