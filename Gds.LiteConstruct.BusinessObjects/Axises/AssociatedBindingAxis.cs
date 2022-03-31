using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects.Axises
{
    [Serializable]
    public class AssociatedBindingAxis : Axis
    {
        private List<Guid> connectedFreeAxes = new List<Guid>();
        private List<Guid> connectedPrimitives = new List<Guid>();

        public AssociatedBindingAxis(Vector3 origin, Vector3 body, float radius, Guid primitive1, Guid axis1, Guid primitive2, Guid axis2)
            : base(origin, body, radius)
        {
            connectedPrimitives.Add(primitive1);
            connectedFreeAxes.Add(axis1);
            connectedPrimitives.Add(primitive2);
            connectedFreeAxes.Add(axis2);
        }

        public void ConnectFreeAxis(Guid id)
        {
            connectedFreeAxes.Add(id);
        }

        public void ConnectPrimitive(Guid id)
        {
            connectedPrimitives.Add(id);
        }

        public bool IsPrimitiveConnected(Guid id)
        {
            return connectedPrimitives.Contains(id);
        }

        public bool IsFreeAxisConnected(Guid id)
        {
            return connectedFreeAxes.Contains(id);
        }
    }
}
