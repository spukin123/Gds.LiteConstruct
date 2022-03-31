using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gds.LiteConstruct.Environment
{
    [Serializable]
    public class TexturesCategory
    {
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                texturesStorage.Serialize();
            }
        }

        protected List<TextureInfo> textures = new List<TextureInfo>();

        [NonSerialized]
        internal ITexturesStorage texturesStorage;

        public TextureInfo[] Textures
        {
            get { return textures.ToArray(); }
        }

        private string folderName;

        internal string FolderName
        {
            get { return folderName; }
        }
#warning create category folder name, which is normalized name

        public virtual bool AllowTexturesAdding
        {
            get { return true; }
        }

        public virtual bool AllowTexturesRemoving
        {
            get { return true; }
        }

        public virtual bool AllowTexturesEditing
        {
            get { return true; }
        }

        public virtual bool AllowEditing
        {
            get { return true; }
        }

        public virtual bool AllowRemoving
        {
            get { return true; }
        }

        internal TexturesCategory(string name)
        {
            this.name = name;
            this.folderName = name;
            string categoryPath = Path.Combine(WorkspaceData.TexturesDirectory, folderName);
            if (!Directory.Exists(categoryPath))
                Directory.CreateDirectory(categoryPath);
        }

        internal void InitializeTexturesEnvironment(ITexturesStorage environment)
        {
            this.texturesStorage = environment;
        }

        internal void AddExisting(string fileName)
        {
            textures.Add(new TextureInfo(fileName));
            texturesStorage.Serialize();
        }

        internal void Add(TextureInfo texture)
        {
            textures.Add(texture);
            texturesStorage.Serialize();
        }

        public virtual void Add(string fileName)
        {
            string newFileName = Path.GetFileName(fileName);
            string texturesDir = DirectoryPath;
            if (File.Exists(Path.Combine(texturesDir, newFileName)))
                throw new FileExistsException();
            
            File.Copy(fileName, Path.Combine(texturesDir, newFileName));
            textures.Add(new TextureInfo(newFileName));
            texturesStorage.Serialize();
        }

        public virtual void Remove(TextureInfo texture)
        {
            if (textures.Contains(texture) == false)
                throw new FileNotInListException();
			if (texture.Id == texturesStorage.DefaultTextureId)
				throw new InvalidOperationException("Texture cannot be deleted.\n\nThis is a default texture.");

            File.Delete(Path.Combine(DirectoryPath, texture.FileName));
            textures.Remove(texture);
            texturesStorage.Serialize();
        }

        internal void Clear()
        {
            textures.Clear();
        }

        public void Update(TextureInfo oldTexture, TextureInfo newTexture)
        {
            int index = textures.IndexOf(oldTexture);
            textures.RemoveAt(index);
            textures.Insert(index, newTexture);
            texturesStorage.Serialize();
        }

        public string DirectoryPath
        {
            get { return Path.Combine(WorkspaceData.TexturesDirectory, folderName); }
        }

        internal bool ContainsId(Guid id)
        {
            foreach (TextureInfo texture in textures)
            {
                if (texture.Id == id)
                    return true;
            }
            return false;
        }

        internal TextureInfo GetTextureById(Guid id)
        {
            foreach (TextureInfo texture in textures)
            {
                if (texture.Id == id)
                    return texture;
            }
            return null;
        }
    }

    public class FileExistsException : Exception {}

    public class FileNotInListException : Exception { }
}
