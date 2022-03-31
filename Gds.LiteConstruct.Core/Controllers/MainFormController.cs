using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Environment;

namespace Gds.LiteConstruct.Core.Controllers
{
    internal class MainFormController : IMainFormController
    {
        private readonly Core core;
        private readonly Workspace workspace;

        internal MainFormController(Core core)
        {
            this.core = core;
            workspace = core.Workspace;
        }

        public int FPS
        {
            get { return core.GraphicController.FPS; }
        }

        public void CreateNewModel()
        {
            core.PrimitiveManagerController.Selection.Clear();
            if (core.GraphicController.CurentRenderMode != core.SceneRenderMode)
            {
                core.RenderModeSwitcherController.SetSceneRenderMode();
            }
            core.SceneRenderMode.Model = workspace.CreateNewModel();

            if (ModelCreated != null)
                ModelCreated();
        }

        public void SaveModel(string fileName)
        {
            workspace.SaveModel(fileName);
        }

        public void LoadModel(string fileName)
        {
            core.PrimitiveManagerController.Selection.Clear();
            if (core.GraphicController.CurentRenderMode != core.SceneRenderMode)
            {
                core.RenderModeSwitcherController.SetSceneRenderMode();
            }
            workspace.LoadModel(fileName);
            core.SceneRenderMode.Model = workspace.Model;

            if (ModelCreated != null)
                ModelCreated();
        }

        public event ModelCreatedEventHandler ModelCreated;
    }
}
