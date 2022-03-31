using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Core.Controllers;

namespace Gds.LiteConstruct.Core.Presenters
{
    public interface IRenderModeSwitcherPresenter
    {
        IRenderModeSwitcherController RenderModeController { set; }
        void UpdateSceneRenderModeControl(RenderModeControlState state);
        void UpdateTexturingRenderModeControl(RenderModeControlState state);
        void UpdateDetailedRenderModeControl(RenderModeControlState state);
    }
}
