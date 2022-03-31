using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.Rendering;

namespace Gds.LiteConstruct.Presentation.Settings
{
	public partial class SceneSettingsControl : UserControl
	{
		public BindingSource BindingSource
		{
			get { return bindingSource; }
		}

		private SceneSettings settings;

		public SceneSettingsControl()
		{
			InitializeComponent();
		}

		public void Initialize(SceneSettings settings)
		{
			this.settings = settings;
			bindingSource.DataSource = settings;
			pbGridColor.BackColor = settings.GridColor;
			pbBackColor.BackColor = settings.BackgroundColor;
		}

		private void btnChooseGridColor_Click(object sender, EventArgs e)
		{
			settings.GridColor = ChooseColor(pbGridColor, settings.GridColor);
		}

		private void btChooseBackColor_Click(object sender, EventArgs e)
		{
			settings.BackgroundColor = ChooseColor(pbBackColor, settings.BackgroundColor);
		}

		private Color ChooseColor(Control control, Color color)
		{
			colorDialog.Color = color;
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				control.BackColor = colorDialog.Color;
				bindingSource.ResetBindings(false);
				return colorDialog.Color;
			}
			else
			{
				return color;
			}
		}
	}
}
