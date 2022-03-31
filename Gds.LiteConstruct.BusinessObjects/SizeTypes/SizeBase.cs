using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
    [Serializable]
    abstract public class SizeBase
    {
        protected float p1;
        protected float p2;

        protected SizeBase()
        {
        }

        protected SizeBase(float p1, float p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
    }
}
