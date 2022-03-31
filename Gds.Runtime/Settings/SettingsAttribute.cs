using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.Runtime.Settings
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
	public class SettingsAttribute : Attribute
	{
		private string name;

		public string Name
		{
			get { return name; }
		}

		public SettingsAttribute(string name)
		{
			this.name = name;
		}
	}
}
