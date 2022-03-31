using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Windows.Settings;
using System.Windows.Forms;
using Gds.LiteConstruct.Rendering;
using Gds.Runtime;
using Gds.Runtime.Settings;

namespace Gds.LiteConstruct.Presentation.Settings
{
	public class SceneSettingsProvider : ISettingsControlProvider
	{
		public SettingsControlData CreateControl()
		{
			SettingsControlData data = new SettingsControlData();
			SceneSettingsControl control = new SceneSettingsControl();
			control.Initialize(Gds.Runtime.AppContext.Get<ISettingsContext>().GetSettingsCopy<SceneSettings>());
			data.Control = control;
			data.Title = "Scene";
			data.BindingSource = control.BindingSource;
			return data;
		}

		public void SaveData(object data)
		{
			Gds.Runtime.AppContext.Get<ISettingsContext>().SetSettings<SceneSettings>((SceneSettings)data);
		}
	}
}
