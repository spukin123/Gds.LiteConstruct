using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
    [Serializable]
    public class Size3 : SizeBase
    {
        protected float p3;

        public Size3(float x, float y, float z)
            : base(x, y)
        {
            this.p3 = z;
        }

        public Size3()
        {
        }

        public float X
        {
            get { return p1; }
            set { p1 = value; }
        }

        public float Y
        {
            get { return p2; }
            set { p2 = value; }
        }

        public float Z
        {
            get { return p3; }
            set { p3 = value; }
        }
    }
}
