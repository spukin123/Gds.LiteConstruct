using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
    public interface ISelectiveScalable
    {
        void ScaleWidth(float factor);
        void ScaleLength(float factor);
        void ScaleHeight(float factor);
    }
}
