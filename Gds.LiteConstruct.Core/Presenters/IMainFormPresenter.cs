using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Core.Controllers;

namespace Gds.LiteConstruct.Core.Presenters
{
    public interface IMainFormPresenter
    {
        IMainFormController MainFormController { set; }
        void SwitchRenderMode(IManagerPresenter managerPresenter);
    }
}
