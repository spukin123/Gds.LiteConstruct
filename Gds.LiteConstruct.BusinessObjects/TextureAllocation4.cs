using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class TextureAllocation4 : TextureAllocation3
    {
        public Vector2 T4
        {
            get { return coordinates[3]; }
        }
        
        public TextureAllocation4(Vector2 t1, Vector2 t2, Vector2 t3, Vector2 t4) 
            : base(t1, t2, t3)
        {
            coordinates[3] = t4;
        }

        protected override void CreateCoordinatesStorage()
        {
            coordinates = new Vector2[4];
        }
    }
}
