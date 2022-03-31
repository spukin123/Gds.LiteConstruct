using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.TransformationControllers;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation
{
    public class MouseTranslation : MouseTransformation, IBillboard
    {
        public virtual Vector3 Look
        {
            set
            {
                foreach (ITranslationControllerPresenter controller in controllers)
                {
                    if (controller.IsBillboard)
                    {
                        (controller as TranslationAxisController).Look = value;
                    }
                }
            }
        }
        
        public MouseTranslation(PrimitiveBase activePrimitive, Vector3 look)
            : base(activePrimitive)
        {
            isBillboard = true;
            Look = look;
        }

        public MouseTranslation(PrimitiveBase activePrimitive) : base(activePrimitive)
        {
        }

        private void MoveControllersByPrimitivePosition(float x, float y, float z)
        {
            Vector3 position = new Vector3(x, y, z);
            foreach (ITranslationControllerPresenter controller in controllers)
            {
                controller.MoveBy(position - controller.Position);
            }
        }

        protected float AxisLen
        {
            get { return primitiveInteractor.FarestPointDistance + 7f; }
        }

        protected Size2 PlaneSize
        {
            get { return new Size2(AxisLen - 6f, AxisLen - 6f); }
        }

        private void ChangeControllersSize(object sender, EventArgs e)
        {
            ResizableVisitor visitor = new ResizableVisitor();
            visitor.AxisLen = AxisLen;
            visitor.PlaneSize = PlaneSize;

            controllers[0].Accept(visitor);
            controllers[1].Accept(visitor);
            controllers[2].Accept(visitor);
            controllers[3].Accept(visitor);
            controllers[4].Accept(visitor);
            controllers[5].Accept(visitor);
        }

        private void CreateControllers()
        {
            controllers = new ITransformationControllerPresenter[6];

            controllers[0] = new TranslationAxisController(new Ray(primitiveInteractor.Position, Vector3Utils.AlignedXVector * AxisLen), Color.FromArgb(150, 0, 0));
            controllers[1] = new TranslationAxisController(new Ray(primitiveInteractor.Position, Vector3Utils.AlignedYVector * AxisLen), Color.FromArgb(0, 150, 0));
            controllers[2] = new TranslationAxisController(new Ray(primitiveInteractor.Position, Vector3Utils.AlignedZVector * AxisLen), Color.FromArgb(0, 0, 150));

            controllers[3] = new TranslationPlaneController(primitiveInteractor.Position, PlaneSize, new RotationVector(Angle.A0, Angle.A0, Angle.A90), Vector3Utils.AlignedXVector, Color.FromArgb(150, 0, 0));
            controllers[4] = new TranslationPlaneController(primitiveInteractor.Position, PlaneSize, new RotationVector(Angle.A0, Angle.A0, Angle.A0), Vector3Utils.AlignedYVector, Color.FromArgb(0, 150, 0));
            controllers[5] = new TranslationPlaneController(primitiveInteractor.Position, PlaneSize, new RotationVector(Angle.A90, Angle.A0, Angle.A0), Vector3Utils.AlignedZVector, Color.FromArgb(0, 0, 150));
        }

        #region Overriden Members
        
        protected override void DoProcessWithMouseClamped(Point position)
        {
            curMousePos = position;

            Vector3 translation = (activeController as ITranslationControllerPresenter).GetTranslationVector(prevMousePos, new Point(curMousePos.X - prevMousePos.X, curMousePos.Y - prevMousePos.Y));
            primitiveInteractor.MoveBy(translation);

            prevMousePos = curMousePos;
        }

        protected override void PrimitiveInteractorReloaded()
        {
            CreateControllers();
            BindToPrimitive();
        }

        public override void BindToPrimitive()
        {
            primitiveInteractor.ActivePrimitive.PositionChanged += MoveControllersByPrimitivePosition;
            primitiveInteractor.ActivePrimitive.SizeChanged += ChangeControllersSize;
        }

        public override void UnbindFromPrimitive()
        {
            primitiveInteractor.ActivePrimitive.PositionChanged -= MoveControllersByPrimitivePosition;
            primitiveInteractor.ActivePrimitive.SizeChanged -= ChangeControllersSize;
        }

        #endregion

        #region IDisposable Members

        public override void Dispose()
        {
            UnbindFromPrimitive();
            foreach (ITransformationControllerPresenter controller in controllers)
            {
                controller.Dispose();
            }
        }

        #endregion
    }
}
