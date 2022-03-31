using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.BusinessObjects;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;

namespace Gds.LiteConstruct.Presentation
{
    class PrimitiveSizableControlVisitor : ISizableVisitor
    {
        private IPrimitivePropertiesControl result = null;

        public IPrimitivePropertiesControl Result
        {
            get { return result; }
        }

        #region ISizableVisitor Members

        public void Visit(IParallelepipedSizable primitive)
        {
            result = new ParallelepipedSizeControl(primitive);
        }

        public void Visit(ITrianglePrismSizable primitive)
        {
            result = new TrianglePrismSizeControl(primitive);
        }

        public void Visit(IStairsSizable primitive)
        {
            result = new StairsSizeControl(primitive);
        }

        public void Visit(IColumnSizeable primitive)
        {
            result = new ColumnSizeControl(primitive);
        }

        #endregion
    }
}
