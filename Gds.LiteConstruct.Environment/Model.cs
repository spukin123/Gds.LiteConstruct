using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation;
using Gds.LiteConstruct.BusinessObjects.Axises;

namespace Gds.LiteConstruct.Environment
{
    [Serializable]
    public class Model : IDeserializationCallback, IDisposable
    {
        private List<PrimitiveBase> primitiveList = new List<PrimitiveBase>();
        public List<PrimitiveBase> Primitives
        {
            get { return primitiveList; }
        }

        private List<AssociatedBindingAxis> associatedBindingAxes = new List<AssociatedBindingAxis>();
        public List<AssociatedBindingAxis> AssociatedBindingAxes
        {
            get { return associatedBindingAxes; }
        }

        [NonSerialized]
        private string workPath;

        internal string WorkPath
        {
            get { return workPath; }
        }

        [NonSerialized]
        private ITexturesStorage texturesStorage;

        private ModelTexturesCategory textureInfoCollection;

        [NonSerialized]
        private TextureProvider textureProvider;

        internal void Initialize(ITexturesStorage texturesStorage, string workPath)
        {
            this.texturesStorage = texturesStorage;
            this.workPath = workPath;
            if (Directory.Exists(workPath) == false)
                Directory.CreateDirectory(workPath);
            string texturesDirectory = Path.Combine(workPath, WorkspaceData.ModelTexturesFolder);
            if (Directory.Exists(texturesDirectory) == false)
                Directory.CreateDirectory(texturesDirectory);
            textureInfoCollection = new ModelTexturesCategory();
            textureInfoCollection.Initialize(texturesStorage, texturesDirectory);
            textureProvider = new TextureProvider(textureInfoCollection, texturesDirectory);
        }

        public void AddAssociatedBindingAxis(AssociatedBindingAxis axis)
        {
            associatedBindingAxes.Add(axis);
        }

        public void AddPrimitive(PrimitiveBase primitive)
        {
            primitive.SetTextureProvider(textureProvider);
            primitive.SetTexture(texturesStorage.DefaultTextureId);
            primitiveList.Add(primitive);
        }

        public void AddClonedPrimitive(PrimitiveBase primitive)
        {
            primitiveList.Add(primitive);
        }

        public void RemovePrimitive(PrimitiveBase primitive)
        {
            primitive.Dispose();
            primitiveList.Remove(primitive);
        }

        public void RemoveAllPrimitives()
        {
            foreach (PrimitiveBase primitive in primitiveList.ToArray())
            {
                primitive.Dispose();
            }
            primitiveList.Clear();
        }

        internal void Serialize(string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Close();
        }

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
#warning This must be overwritten
        private static ITexturesStorage tempTexturesStorage;
        private static string tempWorkPath;
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        internal static Model Deserialize(string fileName, ITexturesStorage texturesStorage, string workPath)
        {
            tempTexturesStorage = texturesStorage;
            tempWorkPath = workPath;

            FileStream stream = new FileStream(fileName, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            Model data = formatter.Deserialize(stream) as Model;
            stream.Close();
            return data;
        }

        void IDeserializationCallback.OnDeserialization(object sender)
        {
            this.texturesStorage = tempTexturesStorage;
            this.workPath = tempWorkPath;
            string texturesDirectory = Path.Combine(workPath, WorkspaceData.ModelTexturesFolder);
            textureInfoCollection.Initialize(texturesStorage, texturesDirectory);
            textureProvider = new TextureProvider(textureInfoCollection, texturesDirectory);

            foreach (PrimitiveBase primitive in primitiveList)
            {
                primitive.Initialize(textureProvider);
            }
        }

        public void Dispose()
        {
            RemoveAllPrimitives();
            if (Directory.Exists(workPath))
                Directory.Delete(workPath, true);
        }
    }
}
