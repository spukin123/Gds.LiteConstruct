using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects.Axises
{
    [Serializable]
    public class Axis
    {
        protected Ray axis;
        private float radius;

        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public Vector3 Origin
        {
            get { return axis.Position; }
            set { axis.Position = value; }
        }

        public Vector3 Body
        {
            get { return axis.Direction; }
            set { axis.Direction = value; }
        }

        public Axis(Vector3 origin, Vector3 body, float radius)
        {
            axis = new Ray(origin, body);
            this.radius = radius;
        }
    }
}
