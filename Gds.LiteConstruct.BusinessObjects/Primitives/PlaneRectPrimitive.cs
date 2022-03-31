using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;
using Gds.LiteConstruct.BusinessObjects.Axises;

namespace Gds.LiteConstruct.BusinessObjects.Primitives
{
    [Serializable]
    public class PlaneRectPrimitive : Parallelepiped
    {
        public PlaneRectPrimitive() : base()
        {
        }

        #region Overriden Members

        protected override Vector3 LayerWidthVec
        {
            get { return currentSizeZVec; }
        }

        protected override void CreateLayerAxes(Vector3 widthVec)
        {
            Vector3 position, direction;

            position = (this.position + widthVec) - currentSizeXVec - currentSizeYVec;
            direction = 2f * currentSizeXVec;
            bindingAxes[axesCnt] = new FreeBindingAxis(idsForAxises[axesCnt], this, position, direction, AxisRadius, rotationLimiter);
            axesCnt++;

            position = position + direction;
            direction = 2f * currentSizeYVec;
            bindingAxes[axesCnt] = new FreeBindingAxis(idsForAxises[axesCnt], this, position, direction, AxisRadius, rotationLimiter);
            axesCnt++;

            position = position + direction;
            direction = -2f * currentSizeXVec;
            bindingAxes[axesCnt] = new FreeBindingAxis(idsForAxises[axesCnt], this, position, direction, AxisRadius, rotationLimiter);
            axesCnt++;

            position = position + direction;
            direction = -2f * currentSizeYVec;
            bindingAxes[axesCnt] = new FreeBindingAxis(idsForAxises[axesCnt], this, position, direction, AxisRadius, rotationLimiter);
            axesCnt++;
        }

        protected override void SetDefaultSize()
        {
            size = new Size3(20, 20, 2);
        }

        public override bool CanRotateX
        {
            get { return true; }
        }

        public override bool CanRotateY
        {
            get { return true; }
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
