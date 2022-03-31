using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using Microsoft.DirectX;
using Gds.LiteConstruct.Rendering;
using Gds.LiteConstruct.BusinessObjects;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Gds.LiteConstruct.Core;
using Gds.LiteConstruct.Core.Presenters;
using Gds.LiteConstruct.Core.Controllers;
using Gds.LiteConstruct.Presentation.Presenters;
using Gds.Windows;
using Gds.LiteConstruct.Windows.Commands;
using Gds.LiteConstruct.Windows.WorkItems;

namespace Gds.LiteConstruct.Presentation
{
    public partial class MainForm : Form, IMainFormPresenter, ISynchronizeService, IWorkItemComponent
    {
        private CoreManager coreManager = null;

        private TexturingPresenter texturingPresenter = new TexturingPresenter();
        private UserControlSwitcher userControlSwitcher;
        private IManagerPresenter currentManagerPresenter;
        private bool waitingForManager = false;


        public MainForm()
        {
            InitializeComponent();
			InitializeIcons();
			this.Text = string.Format(titleFormat, defaultFileName);
            BusyProcessManager.Instance.View = new SimpleBusyProcessForm();
            BusyProcessManager.Instance.OwnerWindow = this;
			CommandsSynchronizer.SynchronizeService = this;
        }

		private void InitializeIcons()
		{
			miNew.Image = Icons.App.NewDocument.ToBitmap16();
			miOpen.Image = Icons.App.Open.ToBitmap16();
			miSave.Image = Icons.App.Save.ToBitmap16();

			miSettings.Image = Icons.App.Settings.ToBitmap16();
			miAbout.Image = Icons.App.About.ToBitmap16();
			miExit.Image = Icons.App.ExitApplication.ToBitmap16();
		}

		public void Invoke(SimpleHandler methodToExecute)
		{
			if (this.InvokeRequired)
				this.Invoke((Delegate)methodToExecute);
			else
				methodToExecute();
		}

        #region IMainFormPresenter Members

        private IMainFormController controller;

        public IMainFormController MainFormController
        {
            set { controller = value; }
        }

        public void SwitchRenderMode(IManagerPresenter managerPresenter)
        {
            if (!waitingForManager)
            {
                if (!userControlSwitcher.Processing)
                {
                    userControlSwitcher.Switch(currentManagerPresenter as UserControl, managerPresenter as UserControl);
                }
                else
                {
                    waitingForManager = true;
                }
            }
            currentManagerPresenter = managerPresenter;
        }

        #endregion

        private void ManagerSwitcher_Finished(object sender, EventArgs e)
        {
            if (waitingForManager)
            {
                UserControlSwitcherEventArgs args = e as UserControlSwitcherEventArgs;
                userControlSwitcher.Switch(args.Control, currentManagerPresenter as UserControl);
                waitingForManager = false;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            currentManagerPresenter = primitiveManagerPresenter;
            userControlSwitcher = new UserControlSwitcher(panel2, 15, 0, 30);
            userControlSwitcher.ProcessingFinished += new EventHandler(ManagerSwitcher_Finished);

            coreManager = new CoreManager(this,
                                          graphicWindowPresenter,
                                          primitiveManagerPresenter,
                                          texturingPresenter,
                                          primitivePropertiesPresenter,
                                          renderModeSwitcherController,
                                          cameraModeSwitcherController,
										  primitiveManagerPresenter);
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(UpdateFps);
            timer.Interval = 1000;
            timer.Start();
            labelFPS.Text = "";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            coreManager.Dispose();
        }

        private void UpdateFps(object sender, EventArgs e)
        {
            labelFPS.Text = controller.FPS.ToString();
        }
	}
}