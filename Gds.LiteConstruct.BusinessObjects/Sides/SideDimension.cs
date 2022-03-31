using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.Sides
{
    public abstract class SideDimension
    {
        protected Vertex p1;

        internal Vertex P1
        {
            get { return p1; }
            set { p1 = value; }
        }
        
        protected Vertex p2;

        internal Vertex P2
        {
            get { return p2; }
            set { p2 = value; }
        }

        protected Vertex p3;

        internal Vertex P3
        {
            get { return p3; }
            set { p3 = value; }
        }

        internal abstract SimpleSide CreateSide();


        protected SideDimension(Vertex p1, Vertex p2, Vertex p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }

        protected SideDimension(Vertex[] points, int index1, int index2, int index3)
        {
            this.p1 = points[index1];
            this.p2 = points[index2];
            this.p3 = points[index3];
        }
    }
}
