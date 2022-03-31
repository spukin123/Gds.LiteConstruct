using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class LinesParallelException : Exception { }
    
    public class Line2D
    {
        private Vector2 point1;

		public Vector2 Point1
        {
            get { return point1; }
            set { point1 = value; }
        }

		private Vector2 point2;

        public Vector2 Point2
        {
            get { return point2; }
            set { point2 = value; }
        }

        public Line2D()
        {
        }

        public Line2D(Vector2 point1, Vector2 point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        public Vector2 CrossWith(Line2D line)
        {
            if (AreLinesParallel(this, line))
            {
                throw new LinesParallelException();
            }
            else if (AreLinesPerpendicular(this, line))
            {
                if (IsLineHorizontal(this) && IsLineVertical(line))
                {
                    return new Vector2(line.Point1.X, this.Point1.Y);
                }
                else
                {
                    return new Vector2(this.Point1.X, line.Point1.Y);
                }
            }

            float x, y;
            if (IsLineHorizontal(this))
            {
                y = this.Point1.Y;
                x = (y - FindB(line)) / FindK(line);

                return new Vector2(x, y);
            }
            else if (IsLineHorizontal(line))
            {
                y = line.Point1.Y;
                x = (y - FindB(this)) / FindK(this);

                return new Vector2(x, y);
            }
            else if (IsLineVertical(this))
            {
                x = this.Point1.X;
                y = FindK(line) * x + FindB(line);

                return new Vector2(x, y);
            }
            else if (IsLineVertical(line))
            {
                x = line.Point1.X;
                y = FindK(this) * x + FindB(this);

                return new Vector2(x, y);
            }
            else
            {
                x = (FindB(line) - FindB(this)) / (FindK(this) - FindK(line));
                y = FindK(this) * x + FindB(this);

                return new Vector2(x, y);
            }
        }

        #region Static Members
        
        public static Vector2 Cross(Line2D line1, Line2D line2)
        {
            return line1.CrossWith(line2);
        }

        public static Line2D BuildXAlignedLine(float yValue)
        {
            return new Line2D(new Vector2(-1f, yValue), new Vector2(1f, yValue));
        }

        public static Line2D BuildYAlignedLine(float xValue)
        {
            return new Line2D(new Vector2(xValue, -1f), new Vector2(xValue, 1f));
        }

        public static float FindK(Line2D line)
        {
            if (line.Point2.X - line.Point1.X == 0f)
            {
                throw new DivideByZeroException();
            }
            else
            {
                return (line.Point2.Y - line.Point1.Y) / (line.Point2.X - line.Point1.X);
            }
        }

        public static float FindB(Line2D line)
        {
            return line.Point2.Y - FindK(line) * line.Point2.X;
        }

        public static bool AreLinesParallel(Line2D line1, Line2D line2)
        {
            if (IsLineHorizontal(line1))
            {
                return IsLineHorizontal(line2);
            }
            else if (IsLineHorizontal(line2))
            {
                return IsLineHorizontal(line1);
            }
            else if (IsLineVertical(line1))
            {
                return IsLineVertical(line2);
            }
            else if (IsLineVertical(line2))
            {
                return IsLineVertical(line1);
            }
            else
            {
                float line1K, line2K;
                line1K = FindK(line1);
                line2K = FindK(line2);

                return ValuesComparer.FloatValuesEqual(line1K, line2K, 0f);
            }
        }

        public static bool AreLinesPerpendicular(Line2D line1, Line2D line2)
        {
            if (IsLineHorizontal(line1))
            {
                return IsLineVertical(line2);
            }
            else if (IsLineHorizontal(line2))
            {
                return IsLineVertical(line1);
            }
            else
            {
                Vector2 line1Vec, line2Vec;

                line1Vec = line1.Point2 - line1.Point1;
                line2Vec = line2.Point2 - line2.Point1;

                return ValuesComparer.FloatValuesEqual(Vector2.Dot(line1Vec, line2Vec), 0f, 0f);
            }
        }

        public static bool IsLineVertical(Line2D line)
        {
            return ValuesComparer.FloatValuesEqual(line.Point2.X, line.Point1.X, 0f);
        }

        public static bool IsLineHorizontal(Line2D line)
        {
            return ValuesComparer.FloatValuesEqual(line.Point2.Y, line.Point1.Y, 0f);
        }

        #endregion
    }
}
