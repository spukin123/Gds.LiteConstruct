using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.GraphicManagers
{
    public abstract class Illuminator : ValueScroller
    {   
        public Illuminator()
        {

        }

        public Illuminator(float offset, float step, int sleep)
            : base(offset, step, sleep)
        {

        }

        protected void ValidateStopValue()
        {
            if (finalValue < 0f || finalValue > 255f)
            {
                throw new Exception("\"stopValue\" is out of range in \"ValidateStopValue()\" function");
            }
        }
    }
}
