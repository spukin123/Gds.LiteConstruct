using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.Primitives
{
	[Serializable]
    public class ColumnData
    {
        private float z;
        public float Z
        {
            get { return z; }
            set { z = value; }
        }
        
        private float radius;
        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }
        
        private int anglesNumber;
        public int AnglesNumber
        {
            get { return anglesNumber; }
            set { anglesNumber = value; }
        }

        public ColumnData(float z, float radius, int anglesNumber)
        {
            this.z = z;
            this.radius = radius;
            this.anglesNumber = anglesNumber;
        }
    }
}
