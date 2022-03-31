using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.GraphicManagers;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using System.Drawing;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.TransformationControllers
{
    public abstract class TransformationController : ITransformationControllerPresenter, IColorable, ITranspareable
    {
        protected Device device;

        protected BrightnessManager bManager;
        protected TransparencyManager tManager;
        protected ScaleManager sManager;

        protected Color color;

        public TransformationController(Color color)
        {
            this.color = color;
        }
        
        protected abstract void CreateInteractors();

        #region ITransformationControllerPresenter Members

        public abstract void MakePrepared();
        public abstract void MakeUnprepared();
        public abstract void MakeActive();
        public abstract void MakeUnactive();
        public abstract bool InteractedWithMouse(System.Drawing.Point mousePos);
        public abstract Vector3 InteractionPoint { get; }
        public abstract void Render();
        public abstract void Accept(IResizableVisitorPresenter visitor);
        public virtual void Dispose()
        {
            if (tManager != null)
            {
                tManager.StopAction();
            }
            if (bManager != null)
            {
                bManager.StopAction();
            }
            if (sManager != null)
            {
                sManager.StopAction();
            }
        }

        #endregion

        #region IColorable Members

        public abstract void SetColor(System.Drawing.Color color);

        #endregion

        #region ITranspareable Members

        public abstract void SetTransparency(int transparency);

        #endregion
    }
}
