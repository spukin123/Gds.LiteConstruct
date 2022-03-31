using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public abstract class TextureAllocation
    {
        protected Vector2[] coordinates;

        public TextureAllocation()
        {
            CreateCoordinatesStorage();
        }

        protected abstract void CreateCoordinatesStorage();
    }
}
