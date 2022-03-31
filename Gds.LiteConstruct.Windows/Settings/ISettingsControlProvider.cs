using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Windows.Settings
{
	public interface ISettingsControlProvider
	{
		SettingsControlData CreateControl();
		void SaveData(object data);
	}
}
