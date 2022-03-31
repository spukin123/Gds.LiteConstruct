using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gds.LiteConstruct.Windows.Commands.Adapters
{
    public class ChekedItemAdapter<T> : CommandAdapterBase<T> where T : ToolStripItem
    {
        public override void OnSetCommand(Command command)
        {
            this.invoker.Click += new EventHandler(invoker_Click);
            GetCheckedDelegates();
            UpdateCheckedState(command.UserData);
        }

        private void GetCheckedDelegates()
        {
            GetChecked = (GetCheckedDelegate)Delegate.CreateDelegate(typeof(GetCheckedDelegate), this.invoker, "get_Checked");
            SetChecked = (SetCheckedDelegate)Delegate.CreateDelegate(typeof(SetCheckedDelegate), this.invoker, "set_Checked");
        }

        GetCheckedDelegate GetChecked;
        SetCheckedDelegate SetChecked;

        delegate bool GetCheckedDelegate();
        delegate void SetCheckedDelegate(bool value);

        void invoker_Click(object sender, EventArgs e)
        {
            command.UserData = GetChecked();
            command.Fire();
        }

        public override void OnUnsetCommand()
        {
            this.invoker.Click -= invoker_Click;
        }

        public override void OnStatusChanged(CommandStatus status)
        {
            this.invoker.Enabled = (status == CommandStatus.Enabled);
        }

        public override void OnUserDataChanged(object userData)
        {
            UpdateCheckedState(userData);
        }

        public void UpdateCheckedState(object userData)
        {
            if (userData != null && userData is bool)
            {
                SetChecked((bool)userData);
            }
        }
    }
}
