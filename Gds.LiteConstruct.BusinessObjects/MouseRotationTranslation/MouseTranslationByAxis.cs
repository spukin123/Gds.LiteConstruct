using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using System.Drawing;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.TransformationControllers;
using Gds.LiteConstruct.BusinessObjects.Primitives;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation
{
    public class MouseTranslationByAxis : MouseTranslation
    {
        public MouseTranslationByAxis(PrimitiveBase activePrimitive, Vector3 look)
            : base(activePrimitive, look)
        {
        }

        public override Vector3 Look
        {
            set
            {
                foreach (TranslationAxisController controller in controllers)
                {
                    controller.Look = value;
                }
            }
        }

        protected override void PrimitiveInteractorReloaded()
        {
            isBillboard = true;

            controllers = new ITransformationControllerPresenter[3];

            controllers[0] = new TranslationAxisController(new Ray(primitiveInteractor.Position, Vector3Utils.AlignedXVector * AxisLen), Color.FromArgb(150, 0, 0));
            controllers[1] = new TranslationAxisController(new Ray(primitiveInteractor.Position, Vector3Utils.AlignedYVector * AxisLen), Color.FromArgb(0, 150, 0));
            controllers[2] = new TranslationAxisController(new Ray(primitiveInteractor.Position, Vector3Utils.AlignedZVector * AxisLen), Color.FromArgb(0, 0, 150));
        }

        public override void Dispose()
        {
            throw new Exception("Not implemented!");
        }
    }
}
