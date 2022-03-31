using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
    public interface ITrianglePrismSizable
    {
        Size4 Size { get; }
        void SetA(float a);
        void SetB(float b);
        void SetC(float c);
        void SetZ(float z);
        void Scale(float factor);
        void MakeEquilateral();
    }
}
