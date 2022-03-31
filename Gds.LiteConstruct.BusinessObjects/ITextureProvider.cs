using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;

namespace Gds.LiteConstruct.BusinessObjects
{
    public interface ITextureProvider
    {
        Texture Attach(Guid textureId);
        void Detach(Guid textureId);
    }
}
