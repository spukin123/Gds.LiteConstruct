using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Primitives;

namespace Gds.LiteConstruct.Core.Controllers
{
    public interface IPrimitivePropertiesController
    {
        PrimitiveBase SelectedPrimitive { get; }
    }
}
