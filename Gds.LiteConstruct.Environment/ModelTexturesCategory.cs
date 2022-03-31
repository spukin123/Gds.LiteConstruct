using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Gds.LiteConstruct.Environment
{
    [Serializable]
    class ModelTexturesCategory : TexturesCategory, IModelTexturesCategory
    {
        public override bool AllowTexturesAdding
        {
            get { return false; }
        }

        public override bool AllowTexturesRemoving
        {
            get { return false; }
        }

        public override bool AllowTexturesEditing
        {
            get { return false; }
        }

        public override bool AllowEditing
        {
            get { return false; }
        }

        public override bool AllowRemoving
        {
            get { return false; }
        }

        [NonSerialized]
        private string texturesDirectory;


        internal ModelTexturesCategory()
            : base("[Current model]")
        {
        }

        internal void Initialize(ITexturesStorage texturesStorage, string texturesDirectory)
        {
            base.texturesStorage = texturesStorage;
            this.texturesDirectory = texturesDirectory;
            base.texturesStorage.ModelTexturesCategory = this;
        }

        TextureInfo IModelTexturesCategory.GetTextureById(Guid id)
        {
            TextureInfo texture = GetTextureInfoById(id);
            if (texture != null)
            {
                return texture;
            }
            else
            {
                string path;
                TextureInfo envTexture = texturesStorage.GetTextureById(id, out path);
                if (envTexture == null)
                    return null;

                TextureInfo textureClone = envTexture.CloneWithUniqueFileName();
                base.Add(textureClone);
                File.Copy(path, Path.Combine(texturesDirectory, textureClone.FileName));
                return textureClone;
            }
        }

        private TextureInfo GetTextureInfoById(Guid id)
        {
            foreach (TextureInfo texture in textures)
            {
                if (texture.Id == id)
                    return texture;
            }
            return null;
        }
    }
}
