using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using System.Drawing;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.TransformationControllers;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation
{
    public class MouseRotation : MouseTransformation, IBillboard
    {
        public Vector3 Look
        {
            set
            {
                foreach (RotationController controller in controllers)
                {
                    controller.Look = value;
                }
            }
        }

        private float Radius
        {
            get { return primitiveInteractor.ActivePrimitive.FarestPointDistance + 1f; }
        }

        public MouseRotation(PrimitiveBase activePrimitive, Vector3 look)
            : base(activePrimitive)
        {
            isBillboard = true;
            Look = look;
        }

        private void MouseRotation_OnMouseUp(object sender, EventArgs e)
        {
            (activeController as RotationController).RestoreInteractors();
        }

        private void CreateControllers()
        {
            controllers = new ITransformationControllerPresenter[primitiveInteractor.RotationRingsNum];

            int cnt1 = 0;
            if (primitiveInteractor.CanRotateX)
            {
                controllers[cnt1] = new RotationController(Vector3Utils.AlignedXVector, Color.FromArgb(150, 0, 0), Radius, primitiveInteractor.Position);
                cnt1++;
            }
            if (primitiveInteractor.CanRotateY)
            {
                controllers[cnt1] = new RotationController(Vector3Utils.AlignedYVector, Color.FromArgb(0, 150, 0), Radius, primitiveInteractor.Position);
                cnt1++;
            }
            if (primitiveInteractor.CanRotateZ)
            {
                controllers[cnt1] = new RotationController(Vector3Utils.AlignedZVector, Color.FromArgb(0, 0, 150), Radius, primitiveInteractor.Position);
                cnt1++;
            }
        }

        private void ActivePrimitive_PositionChanged(float x, float y, float z)
        {
            Vector3 position = new Vector3(x, y, z);
            foreach (IMovable controller in controllers)
            {
                controller.MoveBy(position - controller.Position);
            }
        }

        private void ActivePrimitive_SizeChanged(object sender, EventArgs e)
        {
            foreach (RotationController controller in controllers)
            {
                controller.Radius = Radius;
            }
        }

        #region Overriden Memders

        protected override void PrimitiveInteractorReloaded()
        {
            CreateControllers();
            BindToPrimitive();
        }

        protected override void DoProcessWithMouseClamped(Point position)
        {
            curMousePos = position;

            Point vector = new Point(curMousePos.X - prevMousePos.X, curMousePos.Y - prevMousePos.Y);
            //RotationVector rotation = (activeController as RotationController).GetRotation(prevMousePos, vector);
            AxisAngle rotation = (activeController as RotationController).GetRotation(prevMousePos, vector);
            primitiveInteractor.Rotate(rotation);

            prevMousePos = curMousePos;
        }

        public override void BindToPrimitive()
        {
            primitiveInteractor.ActivePrimitive.PositionChanged += ActivePrimitive_PositionChanged;
            primitiveInteractor.ActivePrimitive.SizeChanged += ActivePrimitive_SizeChanged;
        }
 
        public override void UnbindFromPrimitive()
        {
            primitiveInteractor.ActivePrimitive.PositionChanged -= ActivePrimitive_PositionChanged;
            primitiveInteractor.ActivePrimitive.SizeChanged -= ActivePrimitive_SizeChanged;
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
