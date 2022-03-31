using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.TransformationControllers;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation
{
    public class ResizableVisitor : IResizableVisitorPresenter
    {
        private float axisLen;
        public float AxisLen
        {
            get { return axisLen; }
            set { axisLen = value; }
        }

        private Size2 planeSize;
        public Size2 PlaneSize
        {
            get { return planeSize; }
            set { planeSize = value; }
        } 
        
        public ResizableVisitor()
        {
        }

        #region IResizableVisitorPresenter Members

        public void Visit(TranslationAxisController controller)
        {
            controller.AxisLen = axisLen;
        }

        public void Visit(TranslationPlaneController controller)
        {
            controller.Size = planeSize;
        }

        public void Visit(RotationController controller)
        {
            throw new Exception("ResizableVisitor.Visit() - Not implemented");
        }

        #endregion
    }
}
