using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Core.Controllers
{
    public interface IPrimitiveEditModeSwitcherController
    {
        void BeginTranslation();
        void BeginRotation();
        void BeginBinding();
    }
}
