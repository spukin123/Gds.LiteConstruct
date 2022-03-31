using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.Sides
{
    class Side3Dimension : SideDimension
    {
        #region Constructors

        public Side3Dimension(Vertex p1, Vertex p2, Vertex p3)
            : base(p1, p2, p3)
        {
        }

        public Side3Dimension(Vertex[] points, int index1, int index2, int index3)
            : base(points, index1, index2, index3)
        {
        }

        #endregion

        internal override SimpleSide CreateSide()
        {
            return new Side3(this);
        }
    }
}
