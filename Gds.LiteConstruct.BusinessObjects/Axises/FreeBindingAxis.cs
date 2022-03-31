using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.Primitives;

namespace Gds.LiteConstruct.BusinessObjects.Axises
{
	[Serializable]
    public class FreeBindingAxis : Axis, IRotationVectorLimitable
    {
        private IRotationVectorLimitable rotationLimiter;
        private PrimitiveBase container;
        private Guid id;

        public PrimitiveBase Container
        {
            get { return container; }
        }

        public Guid Id
        {
            get { return id; }
        }

        public FreeBindingAxis(Guid id, PrimitiveBase container, Vector3 origin, Vector3 body, float radius, IRotationVectorLimitable rotationLimiter)
            : base(origin, body, radius)
        {
            this.id = id;
            this.container = container;
            this.rotationLimiter = rotationLimiter;
        }

        public static FreeBindingAxis Empty
        {
            get { return new FreeBindingAxis(Guid.Empty, null, Vector3Utils.ZeroVector, Vector3Utils.ZeroVector, 0f, new RotationVectorLimiter(false, false, false)); }
        }

        #region IRotationVectorLimitable Members

        public bool CanRotateX
        {
            get { return rotationLimiter.CanRotateX; }
        }

        public bool CanRotateY
        {
            get { return rotationLimiter.CanRotateY; }
        }

        public bool CanRotateZ
        {
            get { return rotationLimiter.CanRotateZ; }
        }

        #endregion
    }
}
