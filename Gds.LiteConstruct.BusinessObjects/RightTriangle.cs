using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class AngleIsOutOfRange : Exception 
    {
        public AngleIsOutOfRange(string message) : base(message)
        {
        }
    }

    public class RightTriangle
    {
        private float adjacent, opposite, hypotenuse;
        
        public float Adjacent
        {
            get { return adjacent; }
        }

        public float Opposite
        {
            get { return opposite; }
        }

        public float Hypotenuse
        {
            get { return hypotenuse; }
        }
        
        private Angle alpha;
        
        public Angle Alpha
        {
            get { return alpha; }
        }

        private RightTriangle(float adjacent, float opposite, float hypotenuse, Angle alpha)
        {
            this.adjacent = adjacent;
            this.opposite = opposite;
            this.hypotenuse = hypotenuse;
            this.alpha = alpha;
        }

        #region Static interface

        private static void TestAlpha(Angle alpha)
        {
            if (alpha < Angle.A0 || alpha >= Angle.A90)
            {
                throw new AngleIsOutOfRange("Angle must be between 0 and 90.");
            }
        }

        public static RightTriangle FromAdjacentAndAlpha(float adjacent, Angle alpha)
        {
            TestAlpha(alpha);

            float hypotenuse, opposite;
            hypotenuse = adjacent / (float)Math.Cos(alpha.Radians);
            opposite = hypotenuse * (float)Math.Sin(alpha.Radians);

            return new RightTriangle(adjacent, opposite, hypotenuse, alpha);
        }

        public static RightTriangle FromOppositeAndAlpha(float opposite, Angle alpha)
        {
            TestAlpha(alpha);

            float hypotenuse, adjacent;
            hypotenuse = opposite / (float)Math.Sin(alpha.Radians);
            adjacent = hypotenuse * (float)Math.Cos(alpha.Radians);

            return new RightTriangle(adjacent, opposite, hypotenuse, alpha);
        }

        public static RightTriangle FromHypotenuseAndAlpha(float hypotenuse, Angle alpha)
        {
            TestAlpha(alpha);

            float adjacent, opposite;
            adjacent = hypotenuse * (float)Math.Cos(alpha.Radians);
            opposite = hypotenuse * (float)Math.Sin(alpha.Radians);

            return new RightTriangle(adjacent, opposite, hypotenuse, alpha);
        }

        public static RightTriangle FromAdjacentAndOpposite(float adjacent, float opposite)
        {
            float hypotenuse;
            Angle alpha;
            alpha = Angle.FromRadians((float)Math.Atan(opposite / adjacent));
            hypotenuse = adjacent / (float)Math.Cos(alpha.Radians);

            return new RightTriangle(adjacent, opposite, hypotenuse, alpha);
        }

        public static RightTriangle FromAdjacentAndHypotenuse(float adjacent, float hypotenuse)
        {
            float opposite;
            Angle alpha;
            alpha = Angle.FromRadians((float)Math.Acos(adjacent / hypotenuse));
            opposite = hypotenuse * (float)Math.Sin(alpha.Radians);

            return new RightTriangle(adjacent, opposite, hypotenuse, alpha);
        }

        public static RightTriangle FromOppositeAndHypotenuse(float opposite, float hypotenuse)
        {
            float adjacent;
            Angle alpha;
            alpha = Angle.FromRadians((float)Math.Asin(opposite / hypotenuse));
            adjacent = hypotenuse * (float)Math.Cos(alpha.Radians);

            return new RightTriangle(adjacent, opposite, hypotenuse, alpha);
        }

        #endregion
    }
}
