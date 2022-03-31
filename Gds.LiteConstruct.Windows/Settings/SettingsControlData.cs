using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gds.LiteConstruct.Windows.Settings
{
	public class SettingsControlData : ICloneable
	{
		private string title;

		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		private UserControl control;

		public UserControl Control
		{
			get { return control; }
			set { control = value; }
		}

		private BindingSource bindingSource;

		public BindingSource BindingSource
		{
			get { return bindingSource; }
			set { bindingSource = value; }
		}

		#region ICloneable Members

		public object Clone()
		{
			return this.MemberwiseClone();
		}

		#endregion
	}
}
