using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Windows.WorkItems;

namespace Gds.LiteConstruct.Windows.Controlling
{
	public interface IShellUIElementsView<T> : IDisposable, IWorkItemComponent where T : IShellUIManagerBase
	{
		IShellUIElementsView<T> GetNewInstance();
		void RegisterElements(T manager);
	}
}
