using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Core.Controllers;

namespace Gds.LiteConstruct.Core.Presenters
{
    public interface IPrimitiveEditModeSwitcherPresenter
    {
        IPrimitiveEditModeSwitcherController PrimitiveEditModeSwitcherController { set; }
        void Show(bool show);
        void EnableBinding(bool state);
    }
}
