using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using System.Drawing;
using Microsoft.DirectX.Direct3D;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interactors
{
    public class TranslationPlaneInteractor : RectangleMouseInteractor3D
    {
        private RotationVector rotation;
        protected RotationVector Rotation
        {
            set 
            { 
                rotation = value; 
                SetRotation();
                ApplyChangesToStruct();
            }
        }

        public TranslationPlaneInteractor(Vector3 origin, Size2 size, RotationVector rotation, Bitmap textureBitmap, Color color)
            : base(size, color, textureBitmap, origin)
        {
            Rotation = rotation;

            InitBuffers();
        }

        public TranslationPlaneInteractor(Vector3 origin, Size2 size, Matrix rotation, Bitmap textureBitmap, Color color)
            : base(size, color, textureBitmap, origin)
        {
            rotationMat = rotation;

            InitBuffers();
        }

        protected void SetRotation()
        {
            rotationMat.RotateYawPitchRoll(rotation.Y.Radians, rotation.X.Radians, rotation.Z.Radians);
        }
    }
}
