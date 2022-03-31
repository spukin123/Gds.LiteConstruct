using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.GraphicManagers
{
    public class BrightnessManager : Illuminator, IConnectableToColorable
    {
        protected IColorable[] colorableObjects;

        protected Color startColor;
        public Color StartColor
        {
            get { return startColor; }
            set
            {
                startColor = value;
            }
        }

        protected ColorF curColor;
        protected float[] ks = new float[3];
        protected int mainChannelIndex = -1;

        protected ColorF finalColor;
        public ColorF StopColor
        {
            get { return finalColor; }
        }

        public BrightnessManager()
        {
        }

        public BrightnessManager(Color startColor, float offset, float step, int sleep)
            : base(offset, step, sleep)
        {
            StartColor = startColor;

            Apply();
        }

        protected override void Apply()
        {
            InitCurColor();
            InitDirectionSign();
            InitKS();
            InitStopValue();

            InitTimer();
        }

        protected void InitCurColor()
        {
            curColor = ColorF.FromColor(startColor);
        }

        protected override void InitStopValue()
        {
            finalValue = GetChannelValueByIndexF(ColorF.FromColor(startColor), mainChannelIndex) + offset;
            ValidateStopValue();
        }

        protected void InitKS()
        {
            const int maxC = 3;

            int[] rgb = new int[maxC];
            rgb[0] = startColor.R;
            rgb[1] = startColor.G;
            rgb[2] = startColor.B;

            int max = -1;
            for (int cnt1 = 0; cnt1 < maxC; cnt1++)
            {
                if (rgb[cnt1] > max)
                {
                    max = rgb[cnt1];
                    mainChannelIndex = cnt1;
                }
            }

            for (int cnt1 = 0; cnt1 < maxC; cnt1++)
            {
                if (cnt1 != mainChannelIndex)
                {
                    ks[cnt1] = (float)rgb[cnt1] / (float)rgb[mainChannelIndex];
                }
                else
                {
                    ks[cnt1] = 1f;
                }

                ks[cnt1] *= (float)step * directionSign;
            }
        }

        protected override void MakeIteration()
        {
            curColor.R += ks[0];
            curColor.G += ks[1];
            curColor.B += ks[2];

            passedOffset += ks[mainChannelIndex];
        }

        protected override bool CheckStop()
        {
            if (directionSign > 0)
            {
                if (GetChannelValueByIndexF(curColor, mainChannelIndex) > finalValue)
                {
                    StopAction();

                    return true;
                }
            }
            else
            {
                if (GetChannelValueByIndexF(curColor, mainChannelIndex) < finalValue)
                {
                    StopAction();

                    return true;
                }
            }

            curColor.MakeValid();

            return false;
        }

        protected override void SendToClient()
        {
            foreach (IColorable colorableObject in colorableObjects)
            {
                colorableObject.SetColor(curColor.ToColor());
            }
        }

        protected float GetChannelValueByIndexF(ColorF color, int index)
        {
            if (index == 0)
            {
                return color.R;
            }
            else if (index == 1)
            {
                return color.G;
            }
            else if (index == 2)
            {
                return color.B;
            }

            throw new IndexOutOfRangeException("Index out of range in GetChannelValueByIndex(...)");
        }
        
        public override void Rewind()
        {
            if (passedOffset != 0f)
            {
                StartColor = finalColor.ToColor();
                Offset = -passedOffset;
                passedOffset = 0f;

                Apply();

                startedByRewind = true;
                StartAction();
            }
        }

        #region IConnectableToColorable Members

        public void ConnectTo(IColorable[] colorableObjects)
        {
            this.colorableObjects = colorableObjects;
        }

        #endregion

        #region IAction Members

        public override void StopAction()
        {
            base.StopAction();
            curColor.MakeValid();
            finalColor = curColor;
        }

        #endregion
    }
}
