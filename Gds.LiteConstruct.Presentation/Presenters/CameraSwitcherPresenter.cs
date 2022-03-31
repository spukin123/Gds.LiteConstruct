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

namespace Gds.LiteConstruct.Presentation.Presenters
{
    public partial class CameraSwitcherPresenter : UserControl, ICameraSwitcherPresenter
    {
        public CameraSwitcherPresenter()
        {
            InitializeComponent();

            radioButtonCameraModeRotate.CheckedChanged += new EventHandler(radioButtonCameraMode_CheckedChanged);
            radioButtonCameraModeMove.CheckedChanged += new EventHandler(radioButtonCameraMode_CheckedChanged);
            radioButtonCameraModeZoom.CheckedChanged += new EventHandler(radioButtonCameraMode_CheckedChanged);
        }

        #region ICameraModeSwitcherPresenter Members

        private ICameraSwitcherController controller;

        public void SelectNextCameraMode()
        {
            if (radioButtonCameraModeRotate.Checked)
            {
                radioButtonCameraModeRotate.Checked = false;
                radioButtonCameraModeMove.Checked = true;
            }
            else if (radioButtonCameraModeMove.Checked)
            {
                radioButtonCameraModeMove.Checked = false;
                radioButtonCameraModeZoom.Checked = true;
            }
            else if (radioButtonCameraModeZoom.Checked)
            {
                radioButtonCameraModeZoom.Checked = false;
                radioButtonCameraModeRotate.Checked = true;
            }
        }

        public void SelectPrevCameraMode()
        {
            if (radioButtonCameraModeRotate.Checked)
            {
                radioButtonCameraModeRotate.Checked = false;
                radioButtonCameraModeZoom.Checked = true;
            }
            else if (radioButtonCameraModeMove.Checked)
            {
                radioButtonCameraModeMove.Checked = false;
                radioButtonCameraModeRotate.Checked = true;
            }
            else if (radioButtonCameraModeZoom.Checked)
            {
                radioButtonCameraModeZoom.Checked = false;
                radioButtonCameraModeMove.Checked = true;
            }
        }

        public ICameraSwitcherController CameraSwitcherController
        {
            get { return controller; }
            set { controller = value; }
        }

        public void Show(bool show)
        {
            whiteContainer.Show(show);
        }

        public void UpdateCameraMode(bool canMove)
        {
            if (radioButtonCameraModeMove.Checked)
            {
                controller.SetNextMode();
                radioButtonCameraModeZoom.Checked = true;
            }
            radioButtonCameraModeMove.Enabled = canMove;
        }

        #endregion

        private void radioButtonCameraModeRotate_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                controller.SetRotateMode();
            }
        }

        private void radioButtonCameraModeMove_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                controller.SetMoveMode();
            }
        }

        private void radioButtonCameraModeZoom_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                controller.SetZoomMode();
            }
        }

        private void radioButtonCameraMode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked == true)
            {
                //radio.ForeColor = Color.FromArgb(38, 150, 35);
                //radio.ForeColor = Color.FromArgb(0, 70, 213);
                radio.ForeColor = Color.FromArgb(30, 130, 30);
            }
            else
            {
                radio.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            }
        }

        private void whiteContainer_Header_Click(object sender, EventArgs e)
        {
            controller.Activate();
        }
    }
}
