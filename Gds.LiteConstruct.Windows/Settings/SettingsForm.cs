using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Gds.Windows;

namespace Gds.LiteConstruct.Windows.Settings
{
	public partial class SettingsForm : Form
	{
		private ISettingsControlProvider[] providers;
		private List<SettingsControlData> controlsData;

		public SettingsForm()
		{
			InitializeComponent();
		}

		public void Initialize(ISettingsControlProvider[] providers)
		{
			this.providers = providers;
			controlsData = new List<SettingsControlData>();

			foreach (ISettingsControlProvider provider in providers)
			{
				SettingsControlData  data = provider.CreateControl();
				controlsData.Add(data);

				TabPage page = new TabPage();
				page.Text = data.Title;
				page.UseVisualStyleBackColor = true;

				page.Controls.Add(data.Control);
				data.Control.Dock = DockStyle.Fill;
				data.Control.BackColor = Color.Transparent;
				
				tabControl.TabPages.Add(page);
				data.BindingSource.CurrentItemChanged += DataChanged;
			}
			btnApply.Enabled = false;
		}

		private void DataChanged(object sender, EventArgs e)
		{
			btnApply.Enabled = true;
		}

		private void SaveData()
		{
			for (int i = 0; i < providers.Length; i++)
			{
				providers[i].SaveData(controlsData[i].BindingSource.Current);
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			SaveData();
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			SaveData();
			btnApply.Enabled = false;
		}
	}
}