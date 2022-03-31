using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.Core;
using Gds.LiteConstruct.Core.Presenters;
using Gds.LiteConstruct.Core.Controllers;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Gds.LiteConstruct.Presentation.Properties;

namespace Gds.LiteConstruct.Presentation.Presenters
{
    public partial class GraphicWindowPresenter : UserControl, IGraphicWindowPresenter
    {
        protected bool sceneLButtonDown = false;
        protected Point cursorLocation;
        protected bool[] objectsButtonsStates = new bool[6];

        public GraphicWindowPresenter()
        {
            InitializeComponent();
			InitializeIcons();
        }

		private void InitializeIcons()
		{
			miDelete.Image = Icons.Actions.Delete.ToBitmap16();
			miClone.Image = Icons.Files.Multiple.ToBitmap16();
			miCloneAndMove.Image = Icons.Files.Multiple_Star.ToBitmap16();
			miMove.Image = Icons.Actions.Move.ToBitmap16();
			miRotate.Image = Icons.Actions.Rotate.ToBitmap16();
			miUnbind.Image = Icons.Actions.Unbind.ToBitmap16();
		}

        private void pictureBoxScene_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                graphicWindowController.SetPrevCameraMode();
            else
                graphicWindowController.SetNextCameraMode();
        }

        private void pictureBoxScene_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                sceneLButtonDown = true;
                cursorLocation = e.Location;
            }

            graphicWindowController.MouseDown();
        }

        private void pictureBoxScene_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                sceneLButtonDown = false;
            }

            graphicWindowController.MouseUp();
        }

        private void pictureBoxScene_MouseMove(object sender, MouseEventArgs e)
        {
            if (sceneLButtonDown == true)
            {
                graphicWindowController.DeltaClampedMouseMove(e.Location.X - cursorLocation.X, cursorLocation.Y - e.Location.Y);
                graphicWindowController.ClampedMouseMove(e.Location.X, e.Location.Y);

                cursorLocation = e.Location;
#warning call camera mode method execute
            }
            else
            {
                graphicWindowController.FreeMouseMove(e.X, e.Y);
            }
        }

        private void pictureBoxScene_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                graphicWindowController.MousePrimaryClick(e.X, e.Y);
            }
            else if (e.Button == MouseButtons.Right)
            {
                graphicWindowController.MouseSecondaryClick(e.X, e.Y);
            }
            else if (e.Button == MouseButtons.Middle)
            {
                //controller.SetNextCameraMode();
            }
        }

        private void pictureBoxScene_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs)e;
            if (args.Button == MouseButtons.Left)
            {
                graphicWindowController.SelectEntity(args.X, args.Y);
            }
            else if (args.Button == MouseButtons.Middle)
            {
                //controller.SetNextCameraMode();
            }
        }

        private void miWallRectangle_Click(object sender, EventArgs e)
        {
            primitiveManagerController.AddPrimitive(new WallRectPrimitive());
        }

        private void miWallTriangle_Click(object sender, EventArgs e)
        {
            primitiveManagerController.AddPrimitive(new WallTrianglePrimitive());
        }

        private void miPlaneRectangle_Click(object sender, EventArgs e)
        {
            primitiveManagerController.AddPrimitive(new PlaneRectPrimitive());
        }

        private void miPlaneTriangle_Click(object sender, EventArgs e)
        {
            primitiveManagerController.AddPrimitive(new PlaneTrianglePrimitive());
        }

		private void miStairs_Click(object sender, EventArgs e)
		{
            primitiveManagerController.AddPrimitive(new StairsPrimitive());
		}

        private void texturingModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphicWindowController.SetTexturizeRenderMode();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            primitiveManagerController.DeleteSelection();
        }

        private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            primitiveManagerController.CloneSelectedPrimitive();
        }

		private void miRotateCamera_Click(object sender, EventArgs e)
		{
			cameraSwitcherController.SetRotateMode();
		}

		private void miMoveCamera_Click(object sender, EventArgs e)
		{
			cameraSwitcherController.SetMoveMode();
		}

		private void miZoomCamera_Click(object sender, EventArgs e)
		{
			cameraSwitcherController.SetZoomMode();
		}

		private void miMove_Click(object sender, EventArgs e)
		{
			primitiveEditModeSwitcherController.BeginTranslation();
		}

		private void miRotate_Click(object sender, EventArgs e)
		{
			primitiveEditModeSwitcherController.BeginRotation();
		}

		private void miCloneAndMove_Click(object sender, EventArgs e)
		{
			primitiveManagerController.CloneSelectedPrimitive();
			primitiveEditModeSwitcherController.BeginTranslation();
		}

        #region IGraphicWindowPresenter Members

        private IGraphicWindowController graphicWindowController;
        
        public IPrimitiveManagerController PrimitiveManagerController
        {
            set { primitiveManagerController = value; }
        }

		private IPrimitiveManagerController primitiveManagerController;

        public IGraphicWindowController GraphicWindowController
        {
            set { graphicWindowController = value; }
        }

		private ICameraSwitcherController cameraSwitcherController;

		public ICameraSwitcherController CameraSwitcherController
		{
			set { cameraSwitcherController = value; }
		}

		private IPrimitiveEditModeSwitcherController primitiveEditModeSwitcherController;

		public IPrimitiveEditModeSwitcherController PrimitiveEditModeSwitcherController
		{
			set { primitiveEditModeSwitcherController = value; }
		}

        public Control OutputGraphicControl
        {
            get { return graphicWindow; }
        }

        public void ShowGroundContextMenu()
        {
            contextMenuOnEmpty.Show(MainForm.MousePosition);
        }

        public void ShowPrimitiveContextMenu()
        {
            contextMenuOnPrimitive.Show(MainForm.MousePosition);
        }

        #endregion
	}
}
