using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Gds.LiteConstruct.BusinessObjects;

namespace Gds.LiteConstruct.PrimitivesManagement
{
    public abstract class BindingsManager : IMouseInteractable, IRenderable
    {
        protected PrimitiveBase primitive1;
        protected PrimitiveBase primitive2;

        protected BindingsManager(PrimitiveBase primitive1, PrimitiveBase primitive2)
        {
            this.primitive1 = primitive1;
            this.primitive2 = primitive2;
        }

        protected void RaiseBindingFinished()
        {
            if (BindingFinished != null)
            {
                BindingFinished();
            }
        }

        public abstract void StartBinding();
        public abstract void StopBinding();

        #region IMouseInteractable Members

        public abstract void FreeMouseMove(int x, int y);
        public abstract void ClampedMouseMove(int x, int y);
        public abstract void PrimaryMouseClick(int x, int y);
        public abstract void SecondaryMouseClick(int x, int y);

        #endregion

        #region IRenderable Members

        public abstract void Render();

        #endregion

        public event NotifyHandler BindingFinished;
    }
}
