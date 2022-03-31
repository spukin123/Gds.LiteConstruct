using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Gds.LiteConstruct.Windows.WorkItems;
using Gds.Runtime;

namespace Gds.LiteConstruct.Windows.Commands
{
    public enum CommandStatus
    {
        Enabled,
        Disabled
    }

    public delegate void CommandHandler(Command command);
    public delegate void VoidHandler();

    public class Command : IDisposable
    {
        public Command(WorkItem workItem)
        {
            this.workItem = workItem;
        }

        private WorkItem workItem;

        public WorkItem WorkItem
        {
            get { return workItem; }
        }

        private object userData;

        public object UserData
        {
            get { return userData; }
            set
			{
				if (userData != null ? !userData.Equals(value) : value != null)
				{
					userData = value;
					if (UserDataChanged != null)
					{
						UserDataChanged();
					}
				}
			}
        }

		private Dictionary<object, ICommandAdapter> adapters = new Dictionary<object, ICommandAdapter>();

        public void AddInvoker(object invoker, ICommandAdapter adapter)
        {
            Guard.ArgumentNotNull(invoker, "invoker");
            Guard.ArgumentNotNull(adapter, "adapter");
            adapter.SetInvoker(invoker);
            adapter.SetCommand(this);
            adapters.Add(invoker, adapter);
            
            if (workItem.ItemInProgress != null)
            {
                workItem.SetDependency(workItem.ItemInProgress, new CommandInvokerDependency(this, invoker));
            }
        }

        public void AddInvoker(object invoker)
        {
            Guard.ArgumentNotNull(invoker, "invoker");
            AddInvoker(invoker, CommandAdapterFactoryContainer.CreateAdapter(invoker));
        }

        public void RemoveInvoker(object invoker)
        {
            Guard.ArgumentNotNull(invoker, "invoker");
            ICommandAdapter adapter;
            if (!adapters.TryGetValue(invoker, out adapter))
            {
                throw new InvalidOperationException(Gds.LiteConstruct.Windows.Properties.Resources.InvokerIsNotRegistered);
            }
            adapter.UnsetCommand();
            adapters.Remove(invoker);
        }

        private CommandStatus status = CommandStatus.Enabled;
        public CommandStatus Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    if (StatusChanged != null)
                    {
                        StatusChanged(this);
                    }
                }
            }
        }

		public bool Enabled
		{
			get { return Status == CommandStatus.Enabled; }
			set { Status = (value) ? CommandStatus.Enabled : CommandStatus.Disabled; }
		}

        public event CommandHandler StatusChanged;

        private event VoidHandler executeEvent;

        public event VoidHandler Execute
        {
            add
            {
                executeEvent += value;
                if (workItem.ItemInProgress != null)
                {
                    workItem.SetDependency(workItem.ItemInProgress,
                        new CommandHandlerDependency(this, value));
                }
            }
            remove
            {
                executeEvent -= value;
            }
        }

        public void Fire()
        {
            if (executeEvent != null && status == CommandStatus.Enabled)
            {
                executeEvent();
            }
        }

		public event VoidHandler UserDataChanged;

		public void FireUserDataChanged()
		{
			if (UserDataChanged != null)
			{
				UserDataChanged();
			}
		}

        #region IDisposable Members

        public void Dispose()
        {
            foreach (ICommandAdapter adapter in adapters.Values)
            {
                adapter.Dispose();
            }
        }

        #endregion
    }
}
