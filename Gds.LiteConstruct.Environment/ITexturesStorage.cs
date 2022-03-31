using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Environment
{
    internal interface ITexturesStorage
    {
        Guid DefaultTextureId { get; }
        TexturesCategory ModelTexturesCategory { get; set; }
        TextureInfo GetTextureById(Guid id, out string path);
        void Serialize();
    }
}
