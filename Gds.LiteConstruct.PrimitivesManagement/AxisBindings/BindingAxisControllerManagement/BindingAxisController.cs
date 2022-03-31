using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Axises;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interactors;
using Gds.LiteConstruct.BusinessObjects;
using System.Drawing;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.GraphicManagers;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.PrimitivesManagement.AxisBindings.BindingAxisControllerManagement
{
    internal abstract class BindingAxisController : IBindingAxisControllerPresenter, IColorable, IScalable, ITranspareable
    {
        protected Axis axis;

        public Axis BindingAxis
        {
            get { return axis; }
        }

        //private CylinderMouseInteractor3D interactor;
        private ArrowMouseInteractor interactor;

        private ScaleManager scaleManager;
        private BrightnessManager brightnessManager;
        private TransparencyManager transparencyManager;

        protected Color color;
        private bool transactionMode = false;
        private bool hidden = false;
        
        private const float LengthAdd = 0.1f;
        private const float RadiusAdd = 0.15f;
        private bool disposed = false;

        protected float Radius
        {
            get { return axis.Radius + RadiusAdd; }
        }

        protected Vector3 Origin
        {
            get 
            {
                return axis.Origin - Vector3Utils.SetLength(axis.Body, LengthAdd);
            }
        }

        protected Vector3 Body
        {
            get 
            {
                float bodyLength, newBodyLength;
                bodyLength = axis.Body.Length();
                newBodyLength = bodyLength + 2 * LengthAdd;

                return Vector3Utils.SetLength(axis.Body, newBodyLength);
            }
        }

        public BindingAxisController(Axis axis, int sidesNumber, Color color)
        {
            this.axis = axis;
            this.color = color;

            //interactor = new CylinderMouseInteractor3D(new Ray(Origin, Body), sidesNumber, Radius, color);
            interactor = new ArrowMouseInteractor(new Ray(Origin, Body), sidesNumber, Radius, 2.5f, Angle.A45, color);
        }

        private void TransparencyManager_RewindFinished(object sender, EventArgs e)
        {
            interactor.Transparent = false;
            transparencyManager.RewindFinished -= TransparencyManager_RewindFinished;
            transactionMode = false;
            hidden = false;
        }

        private void BrightnessManager_RewindFinished(object sender, EventArgs e)
        {
            interactor.SetColor(color);
            brightnessManager.RewindFinished -= BrightnessManager_RewindFinished;
            transactionMode = false;
        }

        private void BrightnessManager_PrimaryFinished(object sender, EventArgs e)
        {
            brightnessManager.PrimaryFinished -= BrightnessManager_PrimaryFinished;
            transactionMode = false;
        }

        private void TransparencyManager_PrimaryFinished(object sender, EventArgs e)
        {
            transparencyManager.PrimaryFinished -= TransparencyManager_PrimaryFinished;
            transactionMode = false;
        }

        protected abstract BrightnessManager CreateBrightnessManager();

        #region IBindingAxisControllerPresenter Members

        public bool Interacted(int x, int y)
        {
            if (transactionMode || hidden)
            {
                return false;
            }

            return interactor.Interacted(new Point(x, y));;
        }

        public void MakeActive()
        {
            if (brightnessManager != null)
            {
                brightnessManager.StopAction();
            }

            brightnessManager = CreateBrightnessManager();
            brightnessManager.ConnectTo(new IColorable[1] { this });
            brightnessManager.PrimaryFinished += BrightnessManager_PrimaryFinished;
            brightnessManager.StartAction();

            //scaleManager = new ScaleManager(1f, 0.1f, 0.017f, 10);
            //scaleManager.ConnectTo(new IScalable[1] { this });
            //scaleManager.StartAction();
        }

        public void MakeUnactive()
        {
            brightnessManager.StopAction();
            //scaleManager.StopAction();

            brightnessManager.RewindFinished += BrightnessManager_RewindFinished;
            brightnessManager.Rewind();
            //scaleManager.Rewind();
        }

        public void Hide()
        {
            if (transparencyManager != null)
            {
                transparencyManager.StopAction();
            }

            transparencyManager = new TransparencyManager(255, -100, 5.7f, 10);
            transparencyManager.ConnectTo(new ITranspareable[1] { this });
            transparencyManager.PrimaryFinished += TransparencyManager_PrimaryFinished;
            hidden = true;
            interactor.Transparent = true;
            transparencyManager.StartAction();
        }

        public void Show()
        {
            transparencyManager.StopAction();
            transparencyManager.RewindFinished += TransparencyManager_RewindFinished;
            transparencyManager.Rewind();
        }

        public void Render()
        {
            if (!disposed)
            {
                interactor.Render();
            }
        }

        public void Dispose()
        {
            disposed = true;
            if (brightnessManager != null)
            {
                brightnessManager.StopAction();
            }
            if (transparencyManager != null)
            {
                transparencyManager.StopAction();
            }
            interactor.Dispose();
        }

        public void EnableTransactionMode()
        {
            transactionMode = true;
        }

        #endregion

        #region IColorable Members

        public void SetColor(Color color)
        {
            if (!disposed)
            {
                interactor.SetColor(color);
            }
        }

        #endregion

        #region IScalable Members

        public void SetScale(float factor)
        {
            if (!disposed)
            {
                interactor.SetScale(factor);
            }
        }

        #endregion

        #region ITranspareable Members

        public void SetTransparency(int transparency)
        {
            if (!disposed)
            {
                interactor.SetTransparency(transparency);
            }
        }

        #endregion
    }
}
