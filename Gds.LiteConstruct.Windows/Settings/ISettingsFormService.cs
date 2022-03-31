using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gds.LiteConstruct.Windows.Settings
{
	public interface ISettingsFormService
	{
		Size FormSize { get; set; }
		FormStartPosition FormStartPosition { get; set; }
		string FormTitle { get; set; }
		Icon FormIcon { get; set; }
		void RegisterProvider(ISettingsControlProvider provider);
		void Show();
	}
}
