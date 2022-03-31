using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.Sides
{
    public class Side4Dimension : SideDimension
    {
        protected Vertex p4;

        public Vertex P4
        {
            get { return p4; }
            set { p4 = value; }
        }

        #region Constructors

        public Side4Dimension(Vertex p1, Vertex p2, Vertex p3, Vertex p4)
            : base(p1, p2, p3)
        {
            this.p4 = p4;
        }

        public Side4Dimension(Vertex[] points, int index1, int index2, int index3, int index4)
            : base(points, index1, index2, index3)
        {
            this.p4 = points[index4];
        }

        #endregion

        internal override SimpleSide CreateSide()
        {
            return new Side4(this);
        }
    }
}
