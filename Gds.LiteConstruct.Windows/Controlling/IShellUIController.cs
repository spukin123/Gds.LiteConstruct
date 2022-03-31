using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Windows.Controlling
{
	public interface IShellUIController<T> where T : IShellUIManagerBase
	{
		void AddView(IShellUIElementsView<T> view);
		void Activate(T manager);
		void Deactivate();
		void SetCurrentItem(SelectedItem item);
		void SetCurentDirectory(SelectedItem directory);
	}
}
