using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class ProjectionPlane : IRotatable, IMovable
    {
        protected Plane plane;
        protected Vector3 planeNormalVec;
        protected Vector3 m0;

        public ProjectionPlane(Vector3 planeNormalVec, Vector3 passPoint)
        {
            plane = new Plane();
            
            this.planeNormalVec = planeNormalVec;
            UpdatePlaneABC();
            
            m0 = passPoint;
            UpdatePlaneD();
        }

        public Vector3 MakeProjection(Ray projRay)
        {
            return Plane.IntersectLine(plane, projRay.Position, projRay.Position + projRay.Direction);
        }

        protected void UpdatePlaneABC()
        {
            plane.A = planeNormalVec.X;
            plane.B = planeNormalVec.Y;
            plane.C = planeNormalVec.Z;
        }

        protected void UpdatePlaneD()
        {
            plane.D = -plane.A * m0.X - plane.B * m0.Y - plane.C * m0.Z;
        }

        #region IRotatable Members

        public bool CanRotateX
        {
            get { return true; }
        }

        public bool CanRotateY
        {
            get { return true; }
        }

        public bool CanRotateZ
        {
            get { return true; }
        }

        public void RotateX(Angle angle)
        {
            Matrix rotMat = Matrix.Identity;
            rotMat.RotateX(angle.Radians);
            
            planeNormalVec.TransformCoordinate(rotMat);
            UpdatePlaneABC();
        }

        public void RotateY(Angle angle)
        {
            Matrix rotMat = Matrix.Identity;
            rotMat.RotateY(angle.Radians);

            planeNormalVec.TransformCoordinate(rotMat);
            UpdatePlaneABC();
        }

        public void RotateZ(Angle angle)
        {
            Matrix rotMat = Matrix.Identity;
            rotMat.RotateZ(angle.Radians);

            planeNormalVec.TransformCoordinate(rotMat);
            UpdatePlaneABC();
        }

        public RotationVector Rotation
        {
            get { return null; }
            set 
            {
                RotateX(value.X);
                RotateY(value.Y);
                RotateZ(value.Z);
            }
        }

        #endregion

        #region IMovable Members

        public void MoveTo(Vector3 pos)
        {
            m0 = pos;
            UpdatePlaneD();
        }

        public void MoveBy(Vector3 mpos)
        {
            m0 += mpos;
            UpdatePlaneD();
        }

        public void MoveByX(float mx)
        {
            m0.X += mx;
            UpdatePlaneD();
        }

        public void MoveByY(float my)
        {
            m0.Y += my;
            UpdatePlaneD();
        }

        public void MoveByZ(float mz)
        {
            m0.Z += mz;
            UpdatePlaneD();
        }

        public void MoveX(float x)
        {
            m0.X = x;
            UpdatePlaneD();
        }

        public void MoveY(float y)
        {
            m0.Y = y;
            UpdatePlaneD();
        }

        public void MoveZ(float z)
        {
            m0.Z = z;
            UpdatePlaneD();
        }

        public Vector3 Position
        {
            get
            {
                return m0;
            }
            set
            {
                m0 = value;
                UpdatePlaneD();
            }
        }

        #endregion
    }
}
