using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Core.Controllers;
using System.Windows.Forms;

namespace Gds.LiteConstruct.Core.Presenters
{
    public interface IGraphicWindowPresenter
    {
        IGraphicWindowController GraphicWindowController { set; }
        IPrimitiveManagerController PrimitiveManagerController { set; }
		ICameraSwitcherController CameraSwitcherController { set; }
		IPrimitiveEditModeSwitcherController PrimitiveEditModeSwitcherController { set; }
        Control OutputGraphicControl { get; }
        void ShowGroundContextMenu();
        void ShowPrimitiveContextMenu();
    }
}
