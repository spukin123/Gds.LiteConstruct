using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class TextureAllocation3 : TextureAllocation
    {
        public Vector2 T1
        {
            get { return coordinates[0]; }
        }

        public Vector2 T2
        {
            get { return coordinates[1]; }
        }

        public Vector2 T3
        {
            get { return coordinates[2]; }
        }

        public TextureAllocation3(Vector2 t1, Vector2 t2, Vector2 t3) : base()
        {
            coordinates[0] = t1;
            coordinates[1] = t2;
            coordinates[2] = t3;
        }

        protected override void CreateCoordinatesStorage()
        {
            coordinates = new Vector2[3];
        }
    }
}
