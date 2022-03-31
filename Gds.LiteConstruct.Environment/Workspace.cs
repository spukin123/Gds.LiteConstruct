using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Gds.LiteConstruct.Environment
{
    public class Workspace : IDisposable
    {
        private Model model;

        public Model Model
        {
            get { return model; }
        }

        private TexturesEnvironment texturesEnvironment;

        public TexturesEnvironment TexturesEnvironment
        {
            get { return texturesEnvironment; }
        }

        public event EventHandler BeforeSave;
        public event EventHandler AfterSave;

        public Workspace(string workDirectory)
        {
            WorkspaceData.SetWorkDirectory(workDirectory);
            texturesEnvironment = TexturesEnvironment.Deserialize();
            model = new Model();
            model.Initialize(texturesEnvironment, GetModelWorkPath());
        }

        public void SaveModel(string fileName)
        {
            if (BeforeSave != null)
                BeforeSave(this, null);
            
            model.Serialize(ModelDataFilePath);
            PackModel(fileName);

            if (AfterSave != null)
                AfterSave(this, null);
        }

        public void LoadModel(string fileName)
        {
            model.Dispose();
            string modelWorkPath = GetModelWorkPath();
            try
            {
                UnpackModel(fileName, modelWorkPath);
            }
            finally
            {
                //model = new Model();
            }
            string modelDataFilePath = Path.Combine(modelWorkPath, WorkspaceData.ModelDataFile);
            model = Model.Deserialize(modelDataFilePath, texturesEnvironment, modelWorkPath);
            //model.Initialize(texturesEnvironment, modelWorkPath);
        }

        private void UnpackModel(string fileName, string destinationPath)
        {
            FastZip zip = new FastZip();
            zip.ExtractZip(fileName, destinationPath, null);
        }

        private void PackModel(string fileName)
        {
            FastZip zip = new FastZip();
            zip.CreateZip(fileName, model.WorkPath, true, null);
        }

        private string GetModelWorkPath()
        {
            return Path.Combine(WorkspaceData.WorkDirectory, Guid.NewGuid().ToString());
        }

        private string ModelDataFilePath
        {
            get { return Path.Combine(model.WorkPath, WorkspaceData.ModelDataFile); }
        }

        public Model CreateNewModel()
        {
            model.Dispose();
            model = new Model();
            model.Initialize(texturesEnvironment, GetModelWorkPath());
            return model;
        }

        #region IDisposable Members

        public void Dispose()
        {
            model.Dispose();
        }

        #endregion
    }
}
