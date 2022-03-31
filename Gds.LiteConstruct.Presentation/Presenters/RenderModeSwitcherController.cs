using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.Core.Presenters;
using Gds.LiteConstruct.Core.Controllers;
using Gds.LiteConstruct.Core;

namespace Gds.LiteConstruct.Presentation.Presenters
{
    public partial class RenderModeSwitcherController : UserControl, IRenderModeSwitcherPresenter
    {
        public RenderModeSwitcherController()
        {
            InitializeComponent();
			InitializeIcons();
        }

		private void InitializeIcons()
		{
			btnSceneMode.Image = Icons.App.Scene.ToBitmap32();
		}

        #region IRenderModeSwitcherPresenter Members

        private IRenderModeSwitcherController controller;

        public IRenderModeSwitcherController RenderModeController
        {
            get { return controller; }
            set { controller = value; }
        }
	
        public void EnableIndividualRenderModes(bool enabled)
        {
            btnTexturizeMode.Visible = enabled;
        }

        public void UpdateSceneRenderModeControl(RenderModeControlState state)
        {
            SetButtonState(btnSceneMode, state);
        }

        public void UpdateTexturingRenderModeControl(RenderModeControlState state)
        {
            SetButtonState(btnTexturizeMode, state);
        }

        public void UpdateDetailedRenderModeControl(RenderModeControlState state)
        {
            //SetButtonState(buttonDetailedMode, state);
        }

        #endregion

        private void SetButtonState(Button button, RenderModeControlState state)
        {
            switch (state)
            {
                case RenderModeControlState.Checked:
                    button.Enabled = false;
                    button.Visible = true;
                    break;
                case RenderModeControlState.Unchecked:
                    button.Enabled = true;
                    button.Visible = true;
                    break;
                case RenderModeControlState.Invisible:
                    button.Visible = false;
                    break;
                default:
                    throw new ApplicationException("Unknown render mode control state");
            }
        }

        private void btnSceneMode_Click(object sender, EventArgs e)
        {
            controller.SetSceneRenderMode();
        }

        private void btnTexturingMode_Click(object sender, EventArgs e)
        {
            controller.SetTexturingRenderMode();
        }
    }
}
