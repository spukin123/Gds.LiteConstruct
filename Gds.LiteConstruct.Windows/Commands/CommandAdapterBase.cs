using System;
using System.Collections.Generic;
using System.Text;
using Gds.Runtime;

namespace Gds.LiteConstruct.Windows.Commands
{
    public abstract class CommandAdapterBase<T> : ICommandAdapter where T: class
    {
        protected Command command = null;
        protected T invoker;

        void ICommandAdapter.SetInvoker(object invoker)
        {
            try
            {
                SetInvoker((T)invoker);
            }
            catch(InvalidCastException)
            {
                throw new ArgumentException(Gds.LiteConstruct.Windows.Properties.Resources.InvokerHasIncorrectType, "invoker");
            }
        }

        private void SetInvoker(T invoker)
        {
            Guard.ArgumentNotNull(invoker, "invoker");
            this.invoker = invoker;
        }

        public void SetCommand(Command command)
        {
            Guard.ArgumentNotNull(command, "command");
            if (this.command != null)
            {
                throw new InvalidOperationException(Gds.LiteConstruct.Windows.Properties.Resources.CommandIsAlreadySet);
            }
            OnSetCommand(command);
            this.command = command;
            command.StatusChanged += new CommandHandler(command_StatusChanged);
			command.UserDataChanged += new VoidHandler(command_UserDataChanged);
            OnStatusChanged(command.Status);
        }

		public void UnsetCommand()
        {
            OnUnsetCommand();
            command.StatusChanged -= command_StatusChanged;
			command.UserDataChanged -= command_UserDataChanged;
            command = null;
        }

        public abstract void OnSetCommand(Command command);
        public abstract void OnUnsetCommand();
        public abstract void OnStatusChanged(CommandStatus status);
		public virtual void OnUserDataChanged(object userData) { }

        private void command_StatusChanged(Command command)
        {
            ISynchronizeService syncService = CommandsSynchronizer.SynchronizeService;

			if (syncService != null && syncService.InvokeRequired)
			{
				syncService.Invoke(new SimpleHandler(
					delegate()
					{
						OnStatusChanged(command.Status);
					}));
			}
			else
			{
				OnStatusChanged(command.Status);
			}
        }

		private void command_UserDataChanged()
		{
			OnUserDataChanged(command.UserData);
		}

        public void Dispose()
        {
            UnsetCommand();
        }
    }
}
