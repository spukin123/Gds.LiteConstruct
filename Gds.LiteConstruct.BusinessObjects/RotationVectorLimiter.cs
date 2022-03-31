using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects
{
	[Serializable]
    public class RotationVectorLimiter : IRotationVectorLimitable
    {
        private bool canRotateX;
        private bool canRotateY;
        private bool canRotateZ;

        public RotationVectorLimiter(bool canRotateX, bool canRotateY, bool canRotateZ)
        {
            this.canRotateX = canRotateX;
            this.canRotateY = canRotateY;
            this.canRotateZ = canRotateZ;
        }

        #region IRotationVectorLimitable Members

        public bool CanRotateX
        {
            get { return canRotateX; }
        }

        public bool CanRotateY
        {
            get { return canRotateY; }
        }

        public bool CanRotateZ
        {
            get { return canRotateZ; }
        }

        #endregion
    }
}
