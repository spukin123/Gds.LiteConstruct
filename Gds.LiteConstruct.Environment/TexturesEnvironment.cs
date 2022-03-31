using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gds.LiteConstruct.Environment
{
    [Serializable]
    public class TexturesEnvironment : ITexturesStorage
    {
        private List<TexturesCategory> categories = new List<TexturesCategory>();

        public TexturesCategory[] Categories
        {
            get
            {
                List<TexturesCategory> resultCategories = new List<TexturesCategory>(categories);
                resultCategories.Add(modelTexturesCategory);
                return resultCategories.ToArray();
            }
        }

        [NonSerialized]
        private TexturesCategory modelTexturesCategory;

        TexturesCategory ITexturesStorage.ModelTexturesCategory
        {
            get { return modelTexturesCategory; }
            set { modelTexturesCategory = value; }
        }

        private Guid defaultTextureId;

        public Guid DefaultTextureId
        {
            get { return defaultTextureId; }
        }

        public string DefaultTextureName
        {
            get
            {
                string path;
                return GetTextureById(defaultTextureId, out path).Name;
            }
        }

        private void Initialize()
        {
            TexturesCategory standartCategory = new TexturesCategory("Standart");
            standartCategory.InitializeTexturesEnvironment(this);
            categories.Add(standartCategory);
            standartCategory.AddExisting("default.png");
            defaultTextureId = standartCategory.Textures[0].Id;
        }

        public void AddCategory(string name)
        {
            TexturesCategory category = new TexturesCategory(name);
            category.InitializeTexturesEnvironment(this);
            categories.Add(category);
            Serialize();
        }

        public void RemoveCategory(TexturesCategory category)
        {
            foreach (TextureInfo texture in category.Textures)
            {
                if (texture.Id == defaultTextureId)
                    throw new InvalidOperationException("Category cannot be deleted.\n\nCurrent category contains default texture.");
            }
            categories.Remove(category);
            this.Serialize();
        }

        public void MakeDefaultTexture(TextureInfo texture)
        {
            defaultTextureId = texture.Id;
            Serialize();
        }

        private void Serialize()
        {
            (this as ITexturesStorage).Serialize();
        }

        void ITexturesStorage.Serialize()
        {
            FileStream stream = new FileStream(Path.Combine(WorkspaceData.WorkDirectory, WorkspaceData.TexturesFile), FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Close();
        }

        internal static TexturesEnvironment Deserialize()
        {
            TexturesEnvironment environment = null;
            FileStream stream = null;
            try
            {
                stream = new FileStream(Path.Combine(WorkspaceData.WorkDirectory, WorkspaceData.TexturesFile), FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                environment = formatter.Deserialize(stream) as TexturesEnvironment;
            }
            catch { }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            if (environment == null)
            {
                environment = new TexturesEnvironment();
                environment.Initialize();
            }
            else
            {
                foreach (TexturesCategory category in environment.categories)
                {
                    category.InitializeTexturesEnvironment(environment);
                }
            }
            return environment;
        }

        private string GetTextureFullPath(TexturesCategory category, TextureInfo texture)
        {
            return Path.Combine(Path.Combine(WorkspaceData.TexturesDirectory, category.FolderName), texture.FileName);
        }

        public TextureInfo GetTextureById(Guid id, out string path)
        {
            foreach (TexturesCategory category in categories)
            {
                if (category.ContainsId(id))
                {
                    TextureInfo texture = category.GetTextureById(id);
                    path = GetTextureFullPath(category, texture);
                    return texture;
                }
            }
            path = null;
            return null;
        }

		public string GetTexturePhysicalPath(Guid id)
		{
			foreach (TexturesCategory category in categories)
			{
				if (category.ContainsId(id))
				{
					TextureInfo texture = category.GetTextureById(id);
					return GetTextureFullPath(category, texture);
				}
			}
			return null;
		}
    }
}
