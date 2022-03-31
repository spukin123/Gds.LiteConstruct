using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Primitives;

namespace Gds.LiteConstruct.Core.Controllers
{
    public interface IPrimitiveManagerController
    {
        void PreAddPrimitive(PrimitiveBase primitive);
        void DeleteSelection();
        void CloneSelectedPrimitive();
        void AddPrimitive(PrimitiveBase primitive);
    }
}
