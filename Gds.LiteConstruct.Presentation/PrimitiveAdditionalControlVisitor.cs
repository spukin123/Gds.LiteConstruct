using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;

namespace Gds.LiteConstruct.Presentation
{
    public class PrimitiveAdditionalControlVisitor : IAdditionalVisitor
    {
        private IPrimitivePropertiesControl result = null;

        public IPrimitivePropertiesControl Result
        {
            get { return result; }
        }

        #region IAdditionalVisitor Members

        public void Visit(object primitve)
        {
            result = null;
        }

        public void Visit(IStairsExtendable primitive)
        {
            result = new StairsAdditionalControl(primitive);
        }

        public void Visit(IColumnExtendable primitive)
        {
            result = new ColumnAdditionalControl(primitive);
        }

        #endregion
    }
}
