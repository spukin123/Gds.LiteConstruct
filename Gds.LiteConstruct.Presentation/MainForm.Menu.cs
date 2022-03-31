using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Gds.Windows;
using Gds.LiteConstruct.Windows.WorkItems;
using Gds.Runtime;
using Gds.LiteConstruct.Windows.Settings;
using System.IO;

namespace Gds.LiteConstruct.Presentation
{
    public partial class MainForm
    {
		#region IWorkItemComponent Members

		private WorkItem workItem;

		public void AddedToWorkItem(WorkItem workItem)
		{
			this.workItem = workItem;
			workItem.Commands[PresentationConstants.Commands.ShowSettings].AddInvoker(miSettings);
			workItem.Commands[PresentationConstants.Commands.ShowAbout].AddInvoker(miAbout);

			workItem.Commands[PresentationConstants.Commands.ShowSettings].Execute += ShowSettings;
			workItem.Commands[PresentationConstants.Commands.ShowAbout].Execute += ShowAbout;

			workItem.AddItem(texturingPresenter);
		}

		public void RemovedFromWorkItem(WorkItem workItem)
		{
		}

		#endregion

        #region File Menu Items

		private const string titleFormat = "{0} - Lite Construct";
		private const string defaultFileName = "Untitled";
		private string currentFile;

		private string CurrentFile
		{
			get
			{
				return currentFile;
			}
			set
			{
				currentFile = value;
				if (string.IsNullOrEmpty(currentFile))
				{
					this.Text = string.Format(titleFormat, defaultFileName);
				}
				else
				{
					this.Text = string.Format(titleFormat, Path.GetFileNameWithoutExtension(value));
				}
			}
		}

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentFile = null;
            controller.CreateNewModel();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(openFileDialog.FileName))
                {
                    BusyProcessManager.Instance.ShowLoading();
                    controller.LoadModel(openFileDialog.FileName);
                    CurrentFile = openFileDialog.FileName;
                    BusyProcessManager.Instance.Hide();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentFile) == false)
            {
                controller.SaveModel(CurrentFile);
            }
            else
            {
                SaveFileAs();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void SaveFileAs()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(saveFileDialog.FileName))
                {
                    BusyProcessManager.Instance.ShowSaving();
                    controller.SaveModel(saveFileDialog.FileName);
                    CurrentFile = saveFileDialog.FileName;
                    BusyProcessManager.Instance.Hide();
                }
            }
        }

		#endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void ShowSettings()
		{
			Gds.Runtime.AppContext.Get<ISettingsFormService>().Show();
		}

		private void ShowAbout()
		{
			AboutBox box = new AboutBox();
			box.ShowDialog();
		}

		//private void inverseXToolStripMenuItem_Click(object sender, EventArgs e)
		//{
		//    //inverseXToolStripMenuItem.Checked = !inverseXToolStripMenuItem.Checked;
		//    //core.SceneRenderMode.Camera.InverseHorizontally = inverseXToolStripMenuItem.Checked;
		//}

		//private void inverseYToolStripMenuItem_Click(object sender, EventArgs e)
		//{
		//    //inverseYToolStripMenuItem.Checked = !inverseYToolStripMenuItem.Checked;
		//    //core.SceneRenderMode.Camera.InverseVertically = inverseYToolStripMenuItem.Checked;
		//}
    }
}
