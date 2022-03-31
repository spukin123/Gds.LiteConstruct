using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interactors;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation
{
    public abstract class MouseTransformation : IRenderable, IDisposable
    {
        protected event EventHandler OnMouseUp;
        
        protected PrimitiveInteractor primitiveInteractor = new PrimitiveInteractor();

        protected ITransformationControllerPresenter[] controllers;

        protected Point prevMousePos;
        protected Point curMousePos;

        protected bool controllerPrepared = false;
        protected ITransformationControllerPresenter preparedController;
        protected ITransformationControllerPresenter activeController;

        public PrimitiveBase ActivePrimitive
        {
            set 
            { 
                primitiveInteractor.ActivePrimitive = value;
                PrimitiveInteractorReloaded();
            }
        }

        protected bool isBillboard = false;
        public bool IsBillboard
        {
            get { return isBillboard; }
        }

        public MouseTransformation(PrimitiveBase activePrimitive)
        {
            ActivePrimitive = activePrimitive;
        }

        public void ProcessWithMousePosition(Point position)
        {
            curMousePos = position;
            
            if (controllerPrepared == false)
            {
                ITransformationControllerPresenter controller;
                controller = GetInteractedController();
                if (controller != null)
                {
                    controllerPrepared = true;
                    preparedController = controller;
                    preparedController.MakePrepared();
                }
            }
            else
            {
                if (PreparedControllerLostFocus())
                {
                    preparedController.MakeUnprepared();
                    preparedController = null;
                    controllerPrepared = false;
                }
            }

            prevMousePos = curMousePos;
        }

        public void ProcessWithMouseDown()
        {
            if (preparedController != null)
            {
                activeController = preparedController;

                foreach (ITransformationControllerPresenter controller in controllers)
                {
                    if (controller != preparedController)
                    {
                        controller.MakeUnactive();
                    }
                }
            }
        }

        public void ProcessWithMouseUp()
        {
            if (activeController != null)
            {
                RaiseOnMouseUpEvent();
                foreach (ITransformationControllerPresenter controller in controllers)
                {
                    if (controller != activeController)
                    {
                        controller.MakeActive();
                    }
                }
                
                activeController = null;
            }
        }

        private bool PreparedControllerLostFocus()
        {
            Ray ray;
            ray = Ray.GetRayFromScreenCoordinates(curMousePos.X, curMousePos.Y);
            
            if (!preparedController.InteractedWithMouse(curMousePos))
            {
                return true;
            }

            float preparedControllerLen;
            preparedControllerLen = Vector3.Length(preparedController.InteractionPoint - ray.Position);

            foreach (ITransformationControllerPresenter controller in controllers)
            {
                if (controller.InteractedWithMouse(curMousePos))
                {
                    float tempLen;
                    tempLen = Vector3.Length(controller.InteractionPoint - ray.Position);
                    if (tempLen < preparedControllerLen)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        protected ITransformationControllerPresenter GetInteractedController()
        {
            Ray ray = Ray.GetRayFromScreenCoordinates(curMousePos.X, curMousePos.Y);

            int closestIndex = -1;
            float minLen = 1000000f;
            for (int cnt1 = 0; cnt1 < controllers.Length; cnt1++)
            {
                if (controllers[cnt1].InteractedWithMouse(curMousePos))
                {
                    float tempLen = Vector3.Length(controllers[cnt1].InteractionPoint - ray.Position);
                    if (tempLen < minLen)
                    {
                        closestIndex = cnt1;
                        minLen = tempLen;
                    }
                }
            }
            return (closestIndex >= 0) ? controllers[closestIndex] : null;
        }

        private void RaiseOnMouseUpEvent()
        {
            if (OnMouseUp != null)
            {
                OnMouseUp(this, null);
            }
        }

        public void ProcessWithMouseClamped(Point position)
        {
            if (activeController != null)
            {
                DoProcessWithMouseClamped(position);
            }
        }

        public abstract void BindToPrimitive();
        public abstract void UnbindFromPrimitive();
        protected abstract void DoProcessWithMouseClamped(Point position);
        protected abstract void PrimitiveInteractorReloaded();

        #region IRenderable Members

        public void Render()
        {
            foreach (ITransformationControllerPresenter controller in controllers)
            {
                controller.Render();
            }
        }

        #endregion

        #region IDisposable Members

        public abstract void Dispose();

        #endregion
    }
}
