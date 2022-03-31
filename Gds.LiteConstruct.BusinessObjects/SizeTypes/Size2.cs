using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.SizeTypes
{
    public class Size2 : SizeBase
    {
        public float Height
        {
            get { return p1; }
            set { p1 = value; }
        }

        public float Width
        {
            get { return p2; }
            set { p2 = value; }
        }

        public Size2()
        {
        }

        public Size2(float width, float height)
            : base(width, height)
        {

        }
    }
}
