using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using Microsoft.DirectX;
using System.Drawing;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interactors;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.GraphicManagers;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;
using Gds.LiteConstruct.BusinessObjects.Properties;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.TransformationControllers
{
    public class TranslationPlaneController : TransformationController, ITranslationControllerPresenter, IMovable, IScalable
    {
        protected TranslationPlaneInteractor interactor;
        protected ProjectionPlane projPlane;

        protected Size2 size;
        protected RotationVector startRotation;
        protected Vector3 position;
        private Vector3 projPlaneNormal;

        private bool CanMakeUnactive
        {
            get 
            {
                if (tManager == null)
                {
                    return true;
                }
                else
                {
                    return !tManager.IsBusy;
                }
            }
        }

        public TranslationPlaneController(Vector3 position, Size2 size, RotationVector startRotation, Vector3 projPlaneNormal, Color color)
            : base(color)
        {
            this.size = size;
            this.startRotation = startRotation;
            this.position = position;
            this.projPlaneNormal = projPlaneNormal;

            CreateInteractors();
            InitProjectionPlane();
        }

        private void InitProjectionPlane()
        {
            projPlane = new ProjectionPlane(projPlaneNormal, position);
        }

        private void RewindStopped(object sender, EventArgs e)
        {
            interactor.Transparent = false;
            tManager.RewindFinished -= RewindStopped;
        }

        public Size2 Size
        {
            get { return interactor.Size; }
            set { interactor.Size = value; }
        }

        #region Overriden Members

        protected override void CreateInteractors()
        {
            interactor = new TranslationPlaneInteractor(position, size, startRotation, Resources.Interactor_TPlane, color);
        }

        #endregion

        #region ITranslationControllerPresenter Members

        public Vector3 GetTranslationVector(Point point, Point vector)
        {
            Ray ray1, ray2;
            ray1 = Ray.GetRayFromScreenCoordinates(point.X, point.Y);
            ray2 = Ray.GetRayFromScreenCoordinates(point.X + vector.X, point.Y + vector.Y);

            Vector3 startVec, endVec;
            startVec = projPlane.MakeProjection(ray1) - position;
            endVec = projPlane.MakeProjection(ray2) - position;

            return endVec - startVec;
        }

        public bool IsBillboard
        {
            get { return false; }
        }

        #endregion

        #region IControllerPresenter Members

        private const float DeltaMove = 2f;

        private float Step
        {
            get
            {
                return 1f / (float)(Math.Pow((size.Width + size.Height) / 2f, 1.3f));
            }
        }

        public override void MakePrepared()
        {
            bManager = new BrightnessManager(color, 100f, 9.5f, 10);
            bManager.ConnectTo(new IColorable[1] { this });
            bManager.StartAction();

            float endScaleFactor;
            endScaleFactor = (size.Height + DeltaMove) / size.Height;

            sManager = new ScaleManager(1f, endScaleFactor - 1f, Step, 10);
            sManager.ConnectTo(new IScalable[1] { this });
            sManager.StartAction();
        }

        public override void MakeUnprepared()
        {
            bManager.StopAction();
            sManager.StopAction();

            bManager.Rewind();
            sManager.Rewind();
        }

        public override void MakeActive()
        {
            tManager.StopAction();

            tManager.RewindFinished += RewindStopped;
            tManager.Rewind();
        }

        public override void MakeUnactive()
        {
            if (CanMakeUnactive)
            {
                tManager = new TransparencyManager(255, -100, 5.7f, 10);
                tManager.ConnectTo(new ITranspareable[1] { this });
                tManager.StartAction();

                interactor.Transparent = true;
            }
        }

        public override bool InteractedWithMouse(Point mousePos)
        {
            return interactor.Interacted(mousePos);
        }

        public override Vector3 InteractionPoint
        {
            get { return interactor.InteractionPoint; }
        }

        public override void Render()
        {
            interactor.Render();
        }

        public override void Accept(IResizableVisitorPresenter visitor)
        {
            visitor.Visit(this);
        }

        public override void Dispose()
        {
            base.Dispose();
            interactor.Dispose();
        }

        #endregion

        #region IColorable Members

        public override void SetColor(Color color)
        {
            interactor.SetColor(color);
        }

        #endregion

        #region ITranspareable Members

        public override void SetTransparency(int transparency)
        {
            interactor.SetTransparency(transparency);
        }

        #endregion

        #region IMovable Members

        public void MoveTo(Vector3 pos)
        {
            position = pos;
            interactor.Position = position;
            projPlane.Position = position;
        }

        public void MoveBy(Vector3 mpos)
        {
            position += mpos;
            interactor.MoveBy(mpos);
            projPlane.MoveBy(mpos);
        }

        public void MoveByX(float mx)
        {
            position += new Vector3(mx, 0f, 0f);
            interactor.MoveByX(mx);
            projPlane.MoveByX(mx);
        }

        public void MoveByY(float my)
        {
            position += new Vector3(0f, my, 0f);
            interactor.MoveByY(my);
            projPlane.MoveByY(my);
        }

        public void MoveByZ(float mz)
        {
            position += new Vector3(0f, 0f, mz);
            interactor.MoveByZ(mz);
            projPlane.MoveByZ(mz);
        }

        public void MoveX(float x)
        {
            position = new Vector3(x, 0f, 0f);
            interactor.MoveX(x);
            projPlane.MoveX(x);
        }

        public void MoveY(float y)
        {
            position = new Vector3(0f, y, 0f);
            interactor.MoveY(y);
            projPlane.MoveY(y);
        }

        public void MoveZ(float z)
        {
            position = new Vector3(0f, 0f, z);
            interactor.MoveZ(z);
            projPlane.MoveZ(z);
        }

        public Vector3 Position
        {
            get { return position; }
            set { MoveTo(value); }
        }


        #endregion

        #region IScalable Members

        public void SetScale(float scaleFactor)
        {
            interactor.SetScale(scaleFactor);
        }

        #endregion
    }
}
