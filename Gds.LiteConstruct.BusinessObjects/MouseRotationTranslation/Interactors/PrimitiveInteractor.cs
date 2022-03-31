using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interactors
{
    public class PrimitiveInteractor
    {
        private PrimitiveBase activePrimitive;
        private AxisAngle curRotation;

        public PrimitiveBase ActivePrimitive
        {
            get { return activePrimitive; }
            set 
            { 
                activePrimitive = value;
            }
        }

        public Vector3 Position
        {
            get { return activePrimitive.Position; }
        }

        public int RotationRingsNum
        {
            get
            {
                int cnt1 = 0;
                if (activePrimitive.CanRotateX)
                {
                    cnt1++;
                }
                if (activePrimitive.CanRotateY)
                {
                    cnt1++;
                }
                if (activePrimitive.CanRotateZ)
                {
                    cnt1++;
                }

                return cnt1;
            }
        }

        public bool CanRotateX
        {
            get { return activePrimitive.CanRotateX; }
        }
        
        public bool CanRotateY
        {
            get { return activePrimitive.CanRotateY; }
        }

        public bool CanRotateZ
        {
            get { return activePrimitive.CanRotateZ; }
        }

        public PrimitiveInteractor()
        {
        }

        public void MoveBy(Vector3 moveVec)
        {
            activePrimitive.MoveBy(moveVec);
        }

        public void Rotate(AxisAngle rotation)
        {
            //Quaternions
            if (activePrimitive.Rotation.HasRotation)
            {
                curRotation = activePrimitive.Rotation.ToQuaternion();
            }
            else
            {
                curRotation = new AxisAngle(Quaternion.Identity);
            }

            curRotation = curRotation * rotation;
            curRotation.Normalize();
            activePrimitive.Rotation = curRotation.ToRotationVector();

            //Euler
            //RotationVector curRotation;
            //curRotation = activePrimitive.Rotation + rotation;
            //curRotation.Normalize();
            //ApplyRotationToPrimitive(curRotation);
        }

        private void ApplyRotationToPrimitive(RotationVector rotation)
        {
            //if (!activePrimitive.CanRotateX)
            //{
            //    rotation.X = Angle.A0;
            //}
            //if (!activePrimitive.CanRotateY)
            //{
            //    rotation.Y = Angle.A0;
            //}
            //if (!activePrimitive.CanRotateZ)
            //{
            //    rotation.Z = Angle.A0;
            //}

            activePrimitive.Rotation = rotation;
        }

        public float FarestPointDistance
        {
            get { return activePrimitive.FarestPointDistance; }
        }
    }
}
