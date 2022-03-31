using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gds.LiteConstruct.Windows.Controlling
{
	public interface IShellUIManager : IShellUIManagerBase
	{
		void AddMenuItem(ToolStripMenuItem menuItem, string location, MenuPosition position);
	}
}
