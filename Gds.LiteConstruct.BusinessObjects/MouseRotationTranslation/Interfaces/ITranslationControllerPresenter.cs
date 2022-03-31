using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using System.Drawing;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces
{
    public interface ITranslationControllerPresenter : IMovable
    {
        Vector3 GetTranslationVector(Point point, Point vector);
        bool IsBillboard { get; }
    }
}
