using System;

namespace Gds.LiteConstruct.Environment
{
    interface IModelTexturesCategory
    {
        TextureInfo GetTextureById(Guid id);
    }
}
