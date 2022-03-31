using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class Triangle
    {
        protected Vector3 point1;
        
        public Vector3 Point1
        {
            get { return point1; }
            set { point1 = value; }
        }

		protected Vector3 point2;

        public Vector3 Point2
        {
            get { return point2; }
            set { point2 = value; }
        }

		protected Vector3 point3;

        public Vector3 Point3
        {
            get { return point3; }
            set { point3 = value; }
        }

        public Triangle()
        {
        }

        public Triangle(Vector3 point1, Vector3 point2, Vector3 point3)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
        }

        public Triangle Transform(Matrix transformationMatrix)
        {
            Vector3 tPoint1, tPoint2, tPoint3;
            tPoint1 = Vector3.TransformCoordinate(point1, transformationMatrix);
            tPoint2 = Vector3.TransformCoordinate(point2, transformationMatrix);
            tPoint3 = Vector3.TransformCoordinate(point3, transformationMatrix);

            return new Triangle(tPoint1, tPoint2, tPoint3);
        }
    }
}
