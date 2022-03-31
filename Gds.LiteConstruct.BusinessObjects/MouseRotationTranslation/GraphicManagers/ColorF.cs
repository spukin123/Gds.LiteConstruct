using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.GraphicManagers
{
    public class ColorF
    {
        protected float r, g, b;
        public float R
        {
            get { return r; }
            set { r = value; }
        }
        public float G
        {
            get { return g; }
            set { g = value; }
        }
        public float B
        {
            get { return b; }
            set { b = value; }
        }

        public ColorF()
        {

        }

        public ColorF(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public void MakeValid()
        {
            if (r < 0f)
            {
                r = 0f;
            }
            else if (r > 255f)
            {
                r = 255f;
            }

            if (g < 0f)
            {
                g = 0f;
            }
            else if (g > 255f)
            {
                g = 255f;
            }

            if (b < 0f)
            {
                b = 0f;
            }
            else if (b > 255f)
            {
                b = 255f;
            }
        }

        public static ColorF FromColor(Color color)
        {
            return new ColorF((float)color.R, (float)color.G, (float)color.B);
        }

        public Color ToColor()
        {
            return Color.FromArgb((byte)r, (byte)g, (byte)b);
        }

        public override string ToString()
        {
            return string.Format("R:{0},G:{1},B:{2}", r, g, b);
        }
    }
}
