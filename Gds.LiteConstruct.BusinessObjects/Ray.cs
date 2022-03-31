using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using System.Drawing;

namespace Gds.LiteConstruct.BusinessObjects
{
    [Serializable]
    public class Ray
    {
        protected Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        protected Vector3 direction;

        public Vector3 Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public Ray()
        {
        }

        public Ray(Vector3 position, Vector3 direction)
        {
            this.position = position;
            this.direction = direction;
        }

        public Point[] ProjectToScreenSpace()
        {
            Point[] transformed;
            transformed = Vector3Utils.ProjectToScreen(new Vector3[2] { position, position + direction }, DeviceObject.Device.Transform.View, DeviceObject.Device.Transform.Projection, DeviceObject.Device.Viewport.Width, DeviceObject.Device.Viewport.Height);

            return transformed;
        }

        public Vertex GetIntersectionPointWithTriangle(Triangle triangle)
        {
            return GetIntersectionPointWithTriangle(Vertex.FromVector3(triangle.Point1), Vertex.FromVector3(triangle.Point2), Vertex.FromVector3(triangle.Point3));
        }

        public Vertex GetIntersectionPointWithTriangle(Vertex p1, Vertex p2, Vertex p3)
        {   
            Plane plane = Plane.FromPoints(p1.Vector, p2.Vector, p3.Vector);
            Vertex p = new Vertex(Plane.IntersectLine(plane, this.position, this.position + this.direction));
            if (p.Vector == Vector3.Empty)
            {
                return null;
            }

            float j1, j2, j3;

            if (plane.B == 0f && plane.C == 0f)
            {
                j1 = ((p.Y - p1.Y) * (p2.Z - p1.Z)) - ((p.Z - p1.Z) * (p2.Y - p1.Y));
                j2 = ((p.Y - p2.Y) * (p3.Z - p2.Z)) - ((p.Z - p2.Z) * (p3.Y - p2.Y));
                j3 = ((p.Y - p3.Y) * (p1.Z - p3.Z)) - ((p.Z - p3.Z) * (p1.Y - p3.Y));
            }
            else if (plane.C == 0f)
            {
                j1 = ((p.Z - p1.Z) * (p2.X - p1.X)) - ((p.X - p1.X) * (p2.Z - p1.Z));
                j2 = ((p.Z - p2.Z) * (p3.X - p2.X)) - ((p.X - p2.X) * (p3.Z - p2.Z));
                j3 = ((p.Z - p3.Z) * (p1.X - p3.X)) - ((p.X - p3.X) * (p1.Z - p3.Z));
            }
            else
            {
                j1 = ((p.Y - p1.Y) * (p2.X - p1.X)) - ((p.X - p1.X) * (p2.Y - p1.Y));
                j2 = ((p.Y - p2.Y) * (p3.X - p2.X)) - ((p.X - p2.X) * (p3.Y - p2.Y));
                j3 = ((p.Y - p3.Y) * (p1.X - p3.X)) - ((p.X - p3.X) * (p1.Y - p3.Y));
            }

            if ((j1 >= 0f && j2 >= 0f && j3 >= 0f) || (j1 <= 0f && j2 <= 0f && j3 <= 0f))
            {
                return p;
            }
            return null;
        }

        public static Ray GetRayFromScreenCoordinates(float x, float y)
        {
            Matrix matProj;
            matProj = DeviceObject.Device.Transform.Projection;

            // Compute the vector of the pick ray in screen space
            Vector3 vec;
            //vec.X = (((2f * x) / ScreenWidth) - 1) / matProj.M11;
            //vec.Y = -(((2f * y) / ScreenHeight) - 1) / matProj.M22;
            vec.X = (((2f * x) / DeviceObject.Device.Viewport.Width) - 1) / matProj.M11;
            vec.Y = -(((2f * y) / DeviceObject.Device.Viewport.Height) - 1) / matProj.M22;
            vec.Z = 1f;

            // Get the inverse view matrix
            Matrix matView;
            matView = DeviceObject.Device.Transform.View;
            matView.Invert();

            // Transform the screen space pick ray into 3D space
            Vector3 vecPosition, vecDirection;

            vecDirection.X = vec.X * matView.M11 + vec.Y * matView.M21 + vec.Z * matView.M31;
            vecDirection.Y = vec.X * matView.M12 + vec.Y * matView.M22 + vec.Z * matView.M32;
            vecDirection.Z = vec.X * matView.M13 + vec.Y * matView.M23 + vec.Z * matView.M33;
            vecPosition.X = matView.M41;
            vecPosition.Y = matView.M42;
            vecPosition.Z = matView.M43;

            return new Ray(vecPosition, vecDirection);
        }

        public static Ray GetRayFromScreenCoordinates(int x, int y)
        {
            Matrix matProj;
            matProj = DeviceObject.Device.Transform.Projection;

            // Compute the vector of the pick ray in screen space
            Vector3 vec;
            //vec.X = (((2f * x) / ScreenWidth) - 1) / matProj.M11;
            //vec.Y = -(((2f * y) / ScreenHeight) - 1) / matProj.M22;
            vec.X = (((2f * x) / DeviceObject.Device.Viewport.Width) - 1) / matProj.M11;
            vec.Y = -(((2f * y) / DeviceObject.Device.Viewport.Height) - 1) / matProj.M22;
            vec.Z = 1f;

            // Get the inverse view matrix
            Matrix matView;
            matView = DeviceObject.Device.Transform.View;
            matView.Invert();

            // Transform the screen space pick ray into 3D space
            Vector3 vecPosition, vecDirection;

            vecDirection.X = vec.X * matView.M11 + vec.Y * matView.M21 + vec.Z * matView.M31;
            vecDirection.Y = vec.X * matView.M12 + vec.Y * matView.M22 + vec.Z * matView.M32;
            vecDirection.Z = vec.X * matView.M13 + vec.Y * matView.M23 + vec.Z * matView.M33;
            vecPosition.X = matView.M41;
            vecPosition.Y = matView.M42;
            vecPosition.Z = matView.M43;

            return new Ray(vecPosition, vecDirection);
        }

        public float CrossWith(Ray ray)
        {
            return Ray.GetRayCross(this, ray).X;
        }

        // <return> 
        // Vector2.x - len of first ray
        // Vector2.y - len of second ray
        public static Vector2 GetRayCross(Ray ray1, Ray ray2)
        {
            Vector3 u, v, w0;
            u = Vector3.Normalize(ray1.Direction);
            v = Vector3.Normalize(ray2.Direction);
            w0 = ray1.Position - ray2.Position;

            float len1, len2, denom;
            denom = Vector3.Dot(u, u) * Vector3.Dot(v, v) - Vector3.Dot(u, v) * Vector3.Dot(u, v);
            len1 = (Vector3.Dot(u, v) * Vector3.Dot(v, w0) - Vector3.Dot(v, v) * Vector3.Dot(u, w0)) / denom;
            len2 = (Vector3.Dot(u, u) * Vector3.Dot(v, w0) - Vector3.Dot(u, v) * Vector3.Dot(u, w0)) / denom;

            return new Vector2(len1, len2);
        }
    }
}
