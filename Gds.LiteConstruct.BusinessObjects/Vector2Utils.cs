using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public static class Vector2Utils
    {
        public static Vector2 ProjectPointOnVector(Vector2 point, Vector2 vector)
        {
            return vector * (Vector2.Dot(point, vector) / (float)Math.Sqrt(vector.X) + (float)Math.Sqrt(vector.Y));
        }

        public static Vector2 LinesCross(Vector2 line1Point, Vector2 line1Direction, Vector2 line2Point, Vector2 line2Direction)
        {
            Vector2 a, b, a1, b1;
            a = line1Point;
            b = line2Point;
            a1 = Vector2.Normalize(line1Direction);
            b1 = Vector2.Normalize(line2Direction);

            if (Vector2Utils.VectorAlignedByAxis(a1) && Vector2Utils.VectorAlignedByAxis(b1))
			{
#warning Handle exception situations //jack986: Ahuitelnoe soobschenie ob oshibke
				throw new Exception("Vector2Utils");
            }

            float kA, kB;
            float numer, denom;
            numer = a1.X * b.Y - a1.X * a.Y - a1.Y * b.X + a1.Y * a.X;
            denom = a1.Y * b1.X - a1.X * b1.Y;

            kB = numer / denom;
            kA = (b.X + b1.X * kB - a.X) / a1.X;

            return line1Point + a1 * kA;
        }

        public static bool VectorAlignedByAxis(Vector2 vector)
        {
            return vector.X == 0f || vector.Y == 0f;
        }

        public static Vector2 GetPerpendicularTo(Vector2 vector)
        {
            Vector3 vectorV3, perpV3;
            vectorV3 = new Vector3(vector.X, vector.Y, 0f);
            perpV3 = Vector3.TransformCoordinate(vectorV3, Matrix.RotationZ(Angle.A90.Radians));
            return new Vector2(perpV3.X, perpV3.Y);
        }

        public static Vector2 ProjectPointOnLine(Vector2 point, Vector2 lineStartPoint, Vector2 lineDirection)
        {
            Vector2 perp;
            perp = Vector2Utils.GetPerpendicularTo(lineDirection);

            return Line2D.Cross(new Line2D(point, point + perp), new Line2D(lineStartPoint, lineStartPoint + lineDirection));
            //return Vector2Utils.LinesCross(point, perp, lineStartPoint, lineDirection);
        }
    }
}
