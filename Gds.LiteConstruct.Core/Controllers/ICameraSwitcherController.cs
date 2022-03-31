using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Core.Controllers
{
    public interface ICameraSwitcherController
    {
        void Activate();
        void SetRotateMode();
        void SetMoveMode();
        void SetZoomMode();
        void SetNextMode();
    }
}
