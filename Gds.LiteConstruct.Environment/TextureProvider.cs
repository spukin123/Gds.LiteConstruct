using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Gds.LiteConstruct.BusinessObjects;
using Microsoft.DirectX.Direct3D;

namespace Gds.LiteConstruct.Environment
{
    internal class TextureProvider : ITextureProvider
    {
        private Dictionary<Guid, TextureEntity> entities = new Dictionary<Guid, TextureEntity>();

        private IModelTexturesCategory textureCollection;

        private string texturesDirectory;
        
        public TextureProvider(IModelTexturesCategory textureCollection, string texturesDirectory)
        {
            this.textureCollection = textureCollection;
            this.texturesDirectory = texturesDirectory;
        }

        public Texture Attach(Guid textureId)
        {
            TextureEntity entity;
            if (entities.ContainsKey(textureId))
            {
                entity = entities[textureId];
            }
            else
            {
                TextureInfo textureInfo = textureCollection.GetTextureById(textureId);
                if (textureInfo == null) throw new ApplicationException("Texture is not found");
                entity = new TextureEntity(Path.Combine(texturesDirectory, textureInfo.FileName));
                entities.Add(textureId, entity);
            }
            entity.ConsumerCount++;
            return entity.Texture;
        }

        public void Detach(Guid textureId)
        {
            if (entities.ContainsKey(textureId))
            {
                TextureEntity entity;
                entity = entities[textureId];
                entity.ConsumerCount--;
                if (entity.ConsumerCount == 0)
                {
                    entities.Remove(textureId);
                }
            }
        }

        public bool Contains(Guid textureId)
        {
            return entities.ContainsKey(textureId);
        }
    }
}
