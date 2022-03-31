using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;

namespace Gds.LiteConstruct.BusinessObjects
{
    public interface ISizableVisitor
    {
        void Visit(IParallelepipedSizable primitive);
        void Visit(ITrianglePrismSizable primitive);
        void Visit(IStairsSizable primitive);
        void Visit(IColumnSizeable primitive);
    }
}
