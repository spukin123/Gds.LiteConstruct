using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
    public interface IMouseInteractable
    {
        void FreeMouseMove(int x, int y);
        void ClampedMouseMove(int x, int y);
        void PrimaryMouseClick(int x, int y);
        void SecondaryMouseClick(int x, int y);
    }
}
