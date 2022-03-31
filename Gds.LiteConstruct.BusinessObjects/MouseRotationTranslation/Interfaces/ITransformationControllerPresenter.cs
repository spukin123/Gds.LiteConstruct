using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces
{
    public interface ITransformationControllerPresenter : IRenderable, IDisposable
    {
        void MakePrepared();
        void MakeUnprepared();
        void MakeActive();
        void MakeUnactive();
        bool InteractedWithMouse(Point mousePos);
        Vector3 InteractionPoint { get; }
        void Accept(IResizableVisitorPresenter visitor);
    }
}
