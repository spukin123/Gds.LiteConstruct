using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.Sides;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;

namespace Gds.LiteConstruct.BusinessObjects.Primitives
{
    [Serializable]
    public class WallTrianglePrimitive : TrianglePrism
    {
        protected override Vector3 Perpendicular
        {
            get { return Vector3Utils.AlignedYVector; }
        }

        protected override Vector3 Direction
        {
            get { return Vector3Utils.AlignedZVector; }
        }

        public override bool CanRotateX
        {
            get { return false; }
        }

        public override bool CanRotateY
        {
            get { return false; }
        }

        public override bool CanRotateZ
        {
            get { return true; }
        }

        public WallTrianglePrimitive()
            : base()
        {
        }

        protected override void SetSideDimensions()
        {
            simpleSideDimensions = new SideDimension[5];
            simpleSideDimensions[0] = new Side3Dimension(vertices, 0, 1, 2);
            simpleSideDimensions[1] = new Side3Dimension(vertices, 3, 5, 4);
            simpleSideDimensions[2] = new Side4Dimension(vertices, 0, 3, 4, 1);
            simpleSideDimensions[3] = new Side4Dimension(vertices, 1, 4, 5, 2);
            simpleSideDimensions[4] = new Side4Dimension(vertices, 3, 0, 2, 5);
        }

        public override void Accept(ISizableVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Accept(IAdditionalVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
