using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.SizeTypes
{
    public interface IStairsConstraint
    {
        float MinStairHeight { get; }
        float MaxStairHeight { get; }
        
        float MinStairLength { get; }
        float MaxStairLength { get; }

        int MinStairsNumber { get; }
        int MaxStairsNumber { get; }

        float MinStairsX { get; }
    }
}
