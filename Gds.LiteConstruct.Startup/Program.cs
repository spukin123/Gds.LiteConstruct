using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Gds.LiteConstruct.Presentation;
using Gds.Runtime;
using Gds.LiteConstruct.Windows.WorkItems;
using Gds.LiteConstruct.Windows.Settings;
using Gds.LiteConstruct.Presentation.Settings;
using System.Drawing;
using Gds.Runtime.Settings;
using System.IO;
using Gds.LiteConstruct.Presentation.Services;

namespace Gds.LiteConstruct.Startup
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			RegisterServices();
			RegisterSettingsContext();

			using (WorkItem workItem = new WorkItem())
			{
				MainForm form = new MainForm();
				workItem.AddItem(form);

				Application.Run(form);
			}
		}

		private static void RegisterServices()
		{
			ISettingsFormService settingsFormService = new SettingsFormService();
			settingsFormService.FormIcon = Icons.App.Settings.Icon;
			settingsFormService.RegisterProvider(new SceneSettingsProvider());
			Gds.Runtime.AppContext.Set<ISettingsFormService>(settingsFormService);

			Gds.Runtime.AppContext.Set<ITextureEditService>(new TextureEditService());
		}

		private static void RegisterSettingsContext()
		{
			string appFolder = AppDomain.CurrentDomain.BaseDirectory;
			SettingsContext settingsContext = new SettingsContext(Path.Combine(appFolder, "Settings"));
			Gds.Runtime.AppContext.Set<ISettingsContext>(settingsContext);
		}
	}
}