using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.SizeTypes
{
    public interface IAdditionalVisitor
    {
        void Visit(IStairsExtendable primitive);
        void Visit(IColumnExtendable primitive);
        void Visit(object primitive);
    }
}
