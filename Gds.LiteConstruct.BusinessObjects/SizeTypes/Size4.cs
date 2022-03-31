using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
    [Serializable]
    public class Size4 : SizeBase
    {
        protected float p3;
        protected float p4;

        public Size4(float a, float b, float c, float z)
            : base(a, b)
        {
            this.p3 = c;
            this.p4 = z;
        }

        public float A
        {
            get { return p1; }
            set { p1 = value; }
        }

        public float B
        {
            get { return p2; }
            set { p2 = value; }
        }

        public float C
        {
            get { return p3; }
            set { p3 = value; }
        }

        public float Z
        {
            get { return p4; }
            set { p4 = value; }
        }
    }
}
