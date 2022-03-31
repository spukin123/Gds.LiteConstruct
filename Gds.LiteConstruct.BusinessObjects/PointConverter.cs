using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public static class PointConverter
    {
        public static Point Vector2ToPoint(Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }

        public static Vector2 PointToVector2(Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Vector2 Vector3ToVectorXY(Vector3 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        public static Vector3 PointXYToVector3(Vector2 point, float z)
        {
            return new Vector3(point.X, point.Y, z);
        }
    }
}
