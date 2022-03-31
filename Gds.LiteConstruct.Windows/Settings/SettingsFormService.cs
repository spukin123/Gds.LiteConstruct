using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Windows.Openers;
using System.Drawing;
using System.Windows.Forms;

namespace Gds.LiteConstruct.Windows.Settings
{
	public class SettingsFormService : ISettingsFormService
	{
		public void Show()
		{
			SettingsForm form = new SettingsForm();
			form.Icon = formIcon;
			form.Text = formTitle;
			form.StartPosition = formStartPosition;
			if (formSize != Size.Empty)
				form.Size = formSize;

			form.Initialize(providers.ToArray());
			form.ShowDialog();
		}

		private List<ISettingsControlProvider> providers = new List<ISettingsControlProvider>();

		public void RegisterProvider(ISettingsControlProvider provider)
		{
			providers.Add(provider);
		}

		private Size formSize = Size.Empty;

		public Size FormSize
		{
			get { return formSize; }
			set { formSize = value; }
		}

		private string formTitle = "Settings";

		public string FormTitle
		{
			get { return formTitle; }
			set { formTitle = value; }
		}

		private Icon formIcon;

		public Icon FormIcon
		{
			get { return formIcon; }
			set { formIcon = value; }
		}

		private FormStartPosition formStartPosition = FormStartPosition.CenterParent;

		public FormStartPosition FormStartPosition
		{
			get { return formStartPosition; }
			set { formStartPosition = value; }
		}
	}
}
