using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class Vertex
    {
        protected Vector3 vector;

        public float X
        {
            get { return vector.X; }
            set { vector.X = value; }
        }

        public float Y
        {
            get { return vector.Y; }
            set { vector.Y = value; }
        }

        public float Z
        {
            get { return vector.Z; }
            set { vector.Z = value; }
        }

        public Vector3 Vector
        {
            get { return vector; }
            set { vector = value; }
        }

        public Vertex()
        {
        }

        public Vertex(float x, float y, float z)
        {
            vector = new Vector3(x, y, z);
        }

        public Vertex(Vector3 vector)
        {
            this.vector = vector;
        }

        public static Vertex FromVector3(Vector3 vector)
        {
            return new Vertex(vector);
        }
    }
}
