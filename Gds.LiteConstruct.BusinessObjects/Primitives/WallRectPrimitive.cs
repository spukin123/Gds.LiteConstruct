using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;
using Gds.LiteConstruct.BusinessObjects.Axises;

namespace Gds.LiteConstruct.BusinessObjects.Primitives
{
    [Serializable]
    public class WallRectPrimitive : Parallelepiped
    {
        public WallRectPrimitive() : base()
        {
        }

        #region Overriden Members

        protected override Vector3 LayerWidthVec
        {
            get { return currentSizeYVec; }
        }

        protected override void CreateLayerAxes(Vector3 widthVec)
        {
            Vector3 position, direction;

            position = (this.position + widthVec) - currentSizeXVec + currentSizeZVec;
            direction = 2f * currentSizeXVec;
            bindingAxes[axesCnt] = new FreeBindingAxis(idsForAxises[axesCnt], this, position, direction, AxisRadius, rotationLimiter);
            axesCnt++;

            position = position + direction;
            direction = -2f * currentSizeZVec;
            bindingAxes[axesCnt] = new FreeBindingAxis(idsForAxises[axesCnt], this, position, direction, AxisRadius, rotationLimiter);
            axesCnt++;

            position = position + direction;
            direction = -2f * currentSizeXVec;
            bindingAxes[axesCnt] = new FreeBindingAxis(idsForAxises[axesCnt], this, position, direction, AxisRadius, rotationLimiter);
            axesCnt++;

            position = position + direction;
            direction = 2f * currentSizeZVec;
            bindingAxes[axesCnt] = new FreeBindingAxis(idsForAxises[axesCnt], this, position, direction, AxisRadius, rotationLimiter);
            axesCnt++;
        }

        protected override void SetDefaultSize()
        {
            size = new Size3(20f, 2f, 20f);
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

        public override void Accept(ISizableVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Accept(IAdditionalVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion
    }
}
