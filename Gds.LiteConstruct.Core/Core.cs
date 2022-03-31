using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.Rendering;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Gds.LiteConstruct.Core.Controllers;
using Gds.LiteConstruct.Core.CameraModes;
using Gds.LiteConstruct.Core.Presenters;
using Microsoft.DirectX;
using System.Drawing;
using Gds.LiteConstruct.BusinessObjects;
using Gds.LiteConstruct.BusinessObjects.Sides;
using Gds.LiteConstruct.Environment;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;

namespace Gds.LiteConstruct.Core
{
    internal class Core : IDisposable
    {
        private Workspace workspace = new Workspace(AppDomain.CurrentDomain.BaseDirectory);
        public Workspace Workspace
        {
            get { return workspace; }
        }

        private GraphicDeviceController graphicController = null;
        public GraphicDeviceController GraphicController
        {
            get { return graphicController; }
        }

        private SceneRenderMode sceneRenderMode = null;
        public SceneRenderMode SceneRenderMode
        {
            get { return sceneRenderMode; }
        }

		private MouseActionMode mouseActionMode = MouseActionMode.CameraMode;
		public MouseActionMode MouseActionMode
		{
			get { return mouseActionMode; }
			set { mouseActionMode = value; }
		}

        #region Presenters
        private IMainFormPresenter mainFormPresenter;
        internal IMainFormPresenter MainFormPresenter
        {
            get { return mainFormPresenter; }
        }

        private IGraphicWindowPresenter graphicWindowPresenter;
        internal IGraphicWindowPresenter GraphicWindowPresenter
        {
            get { return graphicWindowPresenter; }
        }

        private IPrimitiveManagerPresenter primitiveManagerPresenter;
        internal IPrimitiveManagerPresenter PrimitiveManagerPresenter
        {
            get { return primitiveManagerPresenter; }
        }

        private ITexturingPresenter texturingManagerPresenter;
        internal ITexturingPresenter TexturingManagerPresenter
        {
            get { return texturingManagerPresenter; }
        }

        private IPrimitivePropertiesPresenter primitivePropertiesPresenter;
        internal IPrimitivePropertiesPresenter PrimitivePropertiesPresenter
        {
            get { return primitivePropertiesPresenter; }
        }

        private IRenderModeSwitcherPresenter renderModeSwitcherPresenter;
        internal IRenderModeSwitcherPresenter RenderModeSwitcherPresenter
        {
            get { return renderModeSwitcherPresenter; }
        }

        private ICameraSwitcherPresenter cameraSwitcherPresenter;
        internal ICameraSwitcherPresenter CameraSwitcherPresenter
        {
            get { return cameraSwitcherPresenter; }
        }

        private IPrimitiveEditModeSwitcherPresenter primitiveEditModeSwitcherPresenter;
        internal IPrimitiveEditModeSwitcherPresenter PrimitiveEditModeSwitcherPresenter
        {
            get { return primitiveEditModeSwitcherPresenter; }
        }
        #endregion

        #region Controllers
        
        private MainFormController mainFormController;
        internal MainFormController MainFormController
        {
            get { return mainFormController; }
        }

        private TexturingController texturingController;
        internal TexturingController TexturingController
        {
            get { return texturingController; }
        }

        private CameraSwitcherController cameraSwitcherController;
        internal CameraSwitcherController CameraSwitcherController
        {
            get { return cameraSwitcherController; }
        }
        
        private PrimitiveManagerController primitiveManagerController;
        public PrimitiveManagerController PrimitiveManagerController
        {
            get { return primitiveManagerController; }
        }
        
        private RenderModeSwitcherController renderModeSwitcherController;
        internal RenderModeSwitcherController RenderModeSwitcherController
        {
            get { return renderModeSwitcherController; }
        }
        
        private GraphicWindowController graphicWindowController;
        internal GraphicWindowController GraphicWindowController
        {
            get { return graphicWindowController; }
        }
        
        private PrimitiveEditModeSwitcherController primitiveEditModeSwitcherController;
        internal PrimitiveEditModeSwitcherController PrimitiveEditModeSwitcherController
        {
            get { return primitiveEditModeSwitcherController; }
        }
        
        private PrimitivePropertiesController primitivePropertiesController;
        internal PrimitivePropertiesController PrimitivePropertiesController
        {
            get { return primitivePropertiesController; }
        }
        #endregion

        public Core(IMainFormPresenter mainFormPresenter,
                    IGraphicWindowPresenter graphicWindowPresenter,
                    IPrimitiveManagerPresenter primitiveManagerPresenter,
                    ITexturingPresenter texturingPresenter,
                    IPrimitivePropertiesPresenter primitivePropertiesPresenter,
                    IRenderModeSwitcherPresenter renderModeSwitcherPresenter,
                    ICameraSwitcherPresenter cameraSwitcherPresenter,
                    IPrimitiveEditModeSwitcherPresenter primitiveEditModeSwitcherPresenter)
        {
            //Controllers
			primitiveManagerController = new PrimitiveManagerController(this);
            renderModeSwitcherController = new RenderModeSwitcherController(this);
            mainFormController = new MainFormController(this);
            texturingController = new TexturingController(this);
			cameraSwitcherController = new CameraSwitcherController(this);
            graphicWindowController = new GraphicWindowController(this);
            primitiveEditModeSwitcherController = new PrimitiveEditModeSwitcherController(this);
            primitivePropertiesController = new PrimitivePropertiesController(this);

            //Presenters
            this.mainFormPresenter = mainFormPresenter;
            this.graphicWindowPresenter = graphicWindowPresenter;
            this.primitiveManagerPresenter = primitiveManagerPresenter;
            this.texturingManagerPresenter = texturingPresenter;
            this.primitivePropertiesPresenter = primitivePropertiesPresenter;
            this.renderModeSwitcherPresenter = renderModeSwitcherPresenter;
            this.cameraSwitcherPresenter = cameraSwitcherPresenter;
            this.primitiveEditModeSwitcherPresenter = primitiveEditModeSwitcherPresenter;

            //Presenters to Controllers adjustment
            this.mainFormPresenter.MainFormController = mainFormController;
			this.graphicWindowPresenter.GraphicWindowController = graphicWindowController;
            this.graphicWindowPresenter.PrimitiveManagerController = primitiveManagerController;
			this.graphicWindowPresenter.CameraSwitcherController = cameraSwitcherController;
			this.graphicWindowPresenter.PrimitiveEditModeSwitcherController = primitiveEditModeSwitcherController;
            this.primitiveManagerPresenter.PrimitiveManagerController = primitiveManagerController;
            this.texturingManagerPresenter.TexturizeManagerController = texturingController;
            this.primitivePropertiesPresenter.PrimitivePropertiesController = primitivePropertiesController;
            this.renderModeSwitcherPresenter.RenderModeController = renderModeSwitcherController;
            this.cameraSwitcherPresenter.CameraSwitcherController = cameraSwitcherController;
            this.primitiveEditModeSwitcherPresenter.PrimitiveEditModeSwitcherController = primitiveEditModeSwitcherController;
            
            //Initialization
            graphicController = new GraphicDeviceController(graphicWindowPresenter.OutputGraphicControl);
            sceneRenderMode = new SceneRenderMode(workspace.Model);
            graphicController.SetRenderMode(sceneRenderMode);

            //--Axis binding test
            //workspace.Model.AddPrimitive(new PlaneRectPrimitive());
            //workspace.Model.AddPrimitive(new PlaneRectPrimitive());
            //workspace.Model.Primitives[0].MoveBy(new Vector3(0f, 0f, 0f));
            //workspace.Model.Primitives[1].MoveBy(new Vector3(30f, 0f, 0f));
            //workspace.Model.Primitives[0].RotateX(-Angle.A60);
            //workspace.Model.Primitives[0].RotateZ(Angle.A30);

            //workspace.Model.AddPrimitive(new WallRectPrimitive());
            //workspace.Model.AddPrimitive(new WallRectPrimitive());
            //workspace.Model.Primitives[0].MoveBy(new Vector3(0f, 0f, 0f));
            //workspace.Model.Primitives[1].MoveBy(new Vector3(30f, 0f, 0f));

            //primitiveManagerController.AddPrimitive(null);
            //workspace.Model.AddPrimitive(new PlaneRectPrimitive());
            //workspace.Model.AddPrimitive(new WallRectPrimitive());
            //workspace.Model.Primitives[0].MoveBy(new Vector3(0f, 0f, 0f));
            //workspace.Model.Primitives[1].MoveBy(new Vector3(30f, 0f, 0f));
            //workspace.Model.Primitives[0].RotateX(-Angle.A60);
            //workspace.Model.Primitives[0].RotateZ(Angle.A30);
            //(workspace.Model.Primitives[0] as PlaneRectPrimitive).SetZ(1f);
            //(workspace.Model.Primitives[1] as WallRectPrimitive).SetY(1f);

            renderModeSwitcherPresenter.UpdateSceneRenderModeControl(RenderModeControlState.Checked);
            renderModeSwitcherPresenter.UpdateTexturingRenderModeControl(RenderModeControlState.Invisible);
            renderModeSwitcherPresenter.UpdateDetailedRenderModeControl(RenderModeControlState.Invisible);

            this.texturingManagerPresenter.TexturesEnvironment = workspace.TexturesEnvironment;
            this.renderModeSwitcherPresenter.RenderModeController = null;
            this.primitiveEditModeSwitcherPresenter.Show(false);
			this.primitiveManagerPresenter.DeletePrimitiveEnabled = false;
            
			mainFormController.ModelCreated += texturingPresenter.UpdateTexturesCategories;

            workspace.BeforeSave += primitiveManagerController.UnbindFromSelectedPrimitive;
            workspace.AfterSave += primitiveManagerController.BindToSelectedPrimitive;
		}
        
        #region IDisposable Members

        public void Dispose()
        {
            graphicController.StopRendering();
            workspace.Dispose();
        }

        #endregion
    }
}
