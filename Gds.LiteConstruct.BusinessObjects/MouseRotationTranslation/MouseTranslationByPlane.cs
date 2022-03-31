using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using System.Drawing;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interactors;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.TransformationControllers;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation
{
    public class MouseTranslationByPlane : MouseTranslation
    {
        public MouseTranslationByPlane(PrimitiveBase activePrimitive) 
            : base(activePrimitive)
        {
        }

        protected override void PrimitiveInteractorReloaded()
        {
            controllers = new ITransformationControllerPresenter[3];

            controllers[0] = new TranslationPlaneController(primitiveInteractor.Position, PlaneSize, new RotationVector(Angle.A0, Angle.A0, Angle.A90), Vector3Utils.AlignedXVector, Color.FromArgb(150, 0, 0));
            controllers[1] = new TranslationPlaneController(primitiveInteractor.Position, PlaneSize, new RotationVector(Angle.A0, Angle.A0, Angle.A0), Vector3Utils.AlignedYVector, Color.FromArgb(0, 150, 0));
            controllers[2] = new TranslationPlaneController(primitiveInteractor.Position, PlaneSize, new RotationVector(Angle.A90, Angle.A0, Angle.A0), Vector3Utils.AlignedZVector, Color.FromArgb(0, 0, 150));
        }

        public override void Dispose()
        {
            throw new Exception("Not implemented!");
        }
    }
}
