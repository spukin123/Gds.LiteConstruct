using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gds.LiteConstruct.Windows.Commands.Adapters
{
    public abstract class CheckedItemAdapter<T> : CommandAdapterBase<T> where T : ToolStripItem
    {
        protected abstract void AddInvokerClickHandler(EventHandler eventHandler);
        protected abstract void RemoveInvokerClickHandler(EventHandler eventHandler);

        protected abstract bool InvokerChecked
        {
            get;
            set;
        }

        public override void OnSetCommand(Command command)
        {
            AddInvokerClickHandler(invoker_Click);
            UpdateCheckedState(command.UserData);
        }

        void invoker_Click(object sender, EventArgs e)
        {
            command.UserData = this.InvokerChecked;
            command.Fire();
        }

        public override void OnUnsetCommand()
        {
            RemoveInvokerClickHandler(invoker_Click);
        }

        public override void OnStatusChanged(CommandStatus status)
        {
            this.invoker.Enabled = (status == CommandStatus.Enabled);
        }

        public override void OnUserDataChanged(object userData)
        {
            UpdateCheckedState(userData);
        }

        private void UpdateCheckedState(object userData)
        {
            if (userData != null && userData is bool)
            {
                this.InvokerChecked = (bool)userData;
            }
        }
    }
}
