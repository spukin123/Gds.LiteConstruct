using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Core.Controllers;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects;

namespace Gds.LiteConstruct.Core.Presenters
{
    public interface IPrimitivePropertiesPresenter
    {
        IPrimitivePropertiesController PrimitivePropertiesController { set; }
        void ShowPrimitiveProperties(PrimitiveBase primitive);
        void OnPrimitivePositionChanged(float x, float y, float z);
        void OnPrimitiveRotationChanged(RotationVector vector);
        void ShowPosition(bool state);
        void ShowSize(bool state);
        void ShowRotation(bool state);
        void ShowAdditional(bool state);
    }
}
