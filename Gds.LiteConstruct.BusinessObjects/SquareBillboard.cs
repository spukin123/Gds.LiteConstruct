using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class SquareBillboard
    {
        private Vector3 upVec, sideVec;

        public Vector3 UpVec
        {
            get { return upVec; }
        }

        public Vector3 SideVec
        {
            get { return sideVec; }
        }

        public SquareBillboard(Vector3 upVec, Vector3 sideVec)
        {
            this.upVec = upVec;
            this.sideVec = sideVec;
        }

        public static SquareBillboard FromNormalVector(Vector3 vector)
        {
            Vector3 rotAxis;

            rotAxis = Vector3.Cross(vector, Vector3Utils.AlignedZVector);

            Matrix mat;
            mat = Matrix.RotationAxis(rotAxis, Angle.A90.Radians);

            Vector3 upVec, sideVec;
            upVec = Vector3.Normalize(Vector3.TransformCoordinate(vector, mat));
            sideVec = Vector3.Normalize(rotAxis);

            return new SquareBillboard(upVec, sideVec);
        }
    }
}
