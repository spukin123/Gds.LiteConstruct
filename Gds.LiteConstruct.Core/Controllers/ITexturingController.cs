using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects;

namespace Gds.LiteConstruct.Core.Controllers
{
    public interface ITexturingController
    {
        void ApplyTextureToSelectedSide(Guid textureId);
        void CreateTexturesCategory(string name);
        void RotateTextureOnSelectedSide(Angle angle);
        void ApplyTextureToAllSides(Guid textureId);
    }
}
