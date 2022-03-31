using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.SizeTypes
{
    public interface IColumnSizeable
    {
        float Z { get; set; }
        float Radius { get; set; }
		void Scale(float factor);
    }
}
