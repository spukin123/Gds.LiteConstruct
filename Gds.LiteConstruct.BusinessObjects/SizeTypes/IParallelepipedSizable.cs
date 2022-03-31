using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
    public interface IParallelepipedSizable
    {
        Size3 Size { get; }
        void SetX(float x);
        void SetY(float y);
        void SetZ(float z);
        void Scale(float factor);
    }
}
