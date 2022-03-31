using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.Core.Presenters;
using Gds.LiteConstruct.Core.Controllers;
using Gds.LiteConstruct.BusinessObjects.Primitives;

namespace Gds.LiteConstruct.Presentation.Presenters
{
	public partial class PrimitiveManagerPresenter : UserControl, IPrimitiveManagerPresenter, IPrimitiveEditModeSwitcherPresenter
    {
        public PrimitiveManagerPresenter()
        {
            InitializeComponent();
			InitializeIcons();

            buttonWallRectangle.Click += new EventHandler(button_Click);
            buttonWallTriangle.Click += new EventHandler(button_Click);
            buttonPlaneRectangle.Click += new EventHandler(button_Click);
            buttonPlaneTriangle.Click += new EventHandler(button_Click);
			buttonStairs.Click += new EventHandler(button_Click);
			buttonColumn.Click += new EventHandler(button_Click);
			btnMove.Click += new EventHandler(button_Click);
			btnRotate.Click += new EventHandler(button_Click);
        }

		private void InitializeIcons()
		{
			btnMove.Image = Icons.Actions.Move.ToBitmap32();
			btnRotate.Image = Icons.Actions.Rotate.ToBitmap32();
			btnDelete.Image = Icons.Actions.Delete.ToBitmap32();
            buttonBind.Image = Icons.Actions.Bind.ToBitmap32();
		}

        private void buttonWallRectangle_Click(object sender, EventArgs e)
        {
            controller.PreAddPrimitive(new WallRectPrimitive());
        }

        private void buttonWallTriangle_Click(object sender, EventArgs e)
        {
            controller.PreAddPrimitive(new WallTrianglePrimitive());
        }

        private void buttonPlaneRectangle_Click(object sender, EventArgs e)
        {
            controller.PreAddPrimitive(new PlaneRectPrimitive());
        }

        private void buttonPlaneTriangle_Click(object sender, EventArgs e)
        {
            controller.PreAddPrimitive(new PlaneTrianglePrimitive());
        }

        private void buttonStairs_Click(object sender, EventArgs e)
        {
            controller.PreAddPrimitive(new StairsPrimitive());
        }
        
        private void buttonColumn_Click(object sender, EventArgs e)
        {
            controller.PreAddPrimitive(new ColumnPrimitive());
        }

        private void EnableAllButtons(bool enabled)
        {
            buttonWallRectangle.Enabled = enabled;
            buttonWallTriangle.Enabled = enabled;
            buttonPlaneRectangle.Enabled = enabled;
            buttonPlaneTriangle.Enabled = enabled;
			buttonStairs.Enabled = enabled;
			buttonColumn.Enabled = enabled;
			if (!enabled)
			{
				btnMove.Enabled = enabled;
				btnRotate.Enabled = enabled;
			}
        }

        private void button_Click(object sender, EventArgs e)
        {
            EnableAllButtons(true);
            (sender as Button).Enabled = false;
        }

        private void buttonDeletePrimitive_Click(object sender, EventArgs e)
        {
            controller.DeleteSelection();
        }

        private void btnMove_Click(object sender, EventArgs e)
		{
			modeSwitcherController.BeginTranslation();
			btnRotate.Enabled = true;
		}

		private void btnRotate_Click(object sender, EventArgs e)
		{
			modeSwitcherController.BeginRotation();
			btnMove.Enabled = true;
		}

        private void buttonBind_Click(object sender, EventArgs e)
        {
            modeSwitcherController.BeginBinding();
        }

        #region IPrimitiveEditModeSwitcherPresenter Members

        private IPrimitiveEditModeSwitcherController modeSwitcherController;

        public IPrimitiveEditModeSwitcherController PrimitiveEditModeSwitcherController
        {
            set { modeSwitcherController = value; }
        }

        public void Show(bool show)
        {
            btnMove.Enabled = show;
            btnRotate.Enabled = show;
        }

        public void EnableBinding(bool state)
        {
            buttonBind.Enabled = state;
        }

        #endregion

        #region IPrimitiveManagerPresenter Members

        private IPrimitiveManagerController controller;

        public IPrimitiveManagerController PrimitiveManagerController
        {
            get { return controller; }
            set { controller = value; }
        }

        public void PrimitiveAdded()
        {
            EnableAllButtons(true);
        }

        public bool DeletePrimitiveEnabled
        {
            set { btnDelete.Enabled = value; }
        }

        public void PrimitiveAddingCancelled()
        {
            EnableAllButtons(true);
        }

        #endregion
    }
}
