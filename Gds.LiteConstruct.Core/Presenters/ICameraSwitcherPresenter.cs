using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Core.Controllers;

namespace Gds.LiteConstruct.Core.Presenters
{
    public interface ICameraSwitcherPresenter
    {
        ICameraSwitcherController CameraSwitcherController { set; }
        void Show(bool show);
        void UpdateCameraMode(bool canMove);
        void SelectNextCameraMode();
        void SelectPrevCameraMode();
    }
}
