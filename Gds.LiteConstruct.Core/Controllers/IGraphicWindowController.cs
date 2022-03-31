using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Primitives;

namespace Gds.LiteConstruct.Core.Controllers
{
    public interface IGraphicWindowController
    {
        void SelectEntity(int x, int y);
        void FreeMouseMove(int x, int y);
        void MousePrimaryClick(int x, int y);
        void MouseSecondaryClick(int x, int y);
        void SetTexturizeRenderMode();
        void SetDetailedRenderMode();
        void SetNextCameraMode();
        void SetPrevCameraMode();
        void MouseDown();
        void DeltaClampedMouseMove(int mx, int my);
        void ClampedMouseMove(int x, int y);
        void MouseUp();
    }
}
