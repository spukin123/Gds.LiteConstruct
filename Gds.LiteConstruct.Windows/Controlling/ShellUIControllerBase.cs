using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Windows.WorkItems;
using Gds.LiteConstruct.Windows.Controlling;
using Gds.LiteConstruct.Windows.Commands;
using Gds.Runtime;

namespace Gds.LiteConstruct.Windows.Controlling
{
	public abstract class ShellUIControllerBase<T> : IShellUIController<T>, IWorkItemComponent where T : IShellUIManagerBase
	{
		private SelectedItem item;
		private SelectedItem directory;
		private readonly List<IShellUIElementsView<T>> views = new List<IShellUIElementsView<T>>();
		private readonly ICommandsAccessibility commandsAccessibility;
		private readonly WorkItem workItem;
		private bool activated = false;

		public ICommandsAccessibility CommandsAccessibility
		{
			get { return commandsAccessibility; }
		}

		protected SelectedItem CurrentItem
		{
			get { return item; }
		}

		protected SelectedItem CurrentDirectory
		{
			get { return directory; }
		}

		protected CommandHolder Commands
		{
			get { return workItem.Commands; }
		}

		
		protected ShellUIControllerBase(WorkItem workItem)
		{
			Guard.ArgumentNotNull(workItem, "workItem");

			this.workItem = workItem;
			commandsAccessibility = new CommandsAccessibility(workItem.Commands);
		}

		public void AddView(IShellUIElementsView<T> view)
		{
			Guard.ArgumentNotNull(view, "view");
			views.Add(view);
		}

		public void Activate(T manager)
		{
			Guard.ArgumentNotNull(manager, "manager");
			if (activated)
			{
				Deactivate();
			}
			activated = true;
			workItem.AddItem(this);
			
			for (int i = 0; i < views.Count; i++)
			{
				views[i] = views[i].GetNewInstance();
				views[i].RegisterElements(manager);
				workItem.AddItem(views[i]);
			}
			commandsAccessibility.SetDirectoryCommandsAccessibility(directory);
			commandsAccessibility.SetItemCommandsAccessibility(item);
		}

		public void Deactivate()
		{
			if (!activated)
				return;
			workItem.RemoveItem(this);
			foreach (IShellUIElementsView<T> view in views)
			{
				workItem.RemoveItem(view);
				view.Dispose();
			}
			activated = false;
		}

		public void SetCurrentItem(SelectedItem item)
		{
			if (!activated)
				return;
				//throw new InvalidOperationException("Action can not be executed. Controller is not activated.");
			this.item = item;
			commandsAccessibility.SetItemCommandsAccessibility(item);
		}

		public void SetCurentDirectory(SelectedItem directory)
		{
			if (!activated)
				return;
				//throw new InvalidOperationException("Action can not be executed. Controller is not activated.");
			this.directory = directory;
			commandsAccessibility.SetDirectoryCommandsAccessibility(directory);
		}

		protected abstract void AddCommandsExecuters(CommandHolder commands);

		protected virtual void RemoveCommandsExecuters(CommandHolder commands) { }

		#region IWorkItemComponent Members

		void IWorkItemComponent.AddedToWorkItem(WorkItem workItem)
		{
			AddCommandsExecuters(workItem.Commands);
		}

		void IWorkItemComponent.RemovedFromWorkItem(WorkItem workItem)
		{
			RemoveCommandsExecuters(workItem.Commands);
		}

		#endregion
	}
}
