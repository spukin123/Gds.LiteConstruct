using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gds.LiteConstruct.Windows.Commands.Adapters
{
    public class ButtonAdapter : CommandAdapterBase<Button>
    {
        public override void OnSetCommand(Command command)
        {
            this.invoker.Click += new EventHandler(invoker_Click);
        }

        void invoker_Click(object sender, EventArgs e)
        {
            this.command.Fire();
        }

        public override void OnUnsetCommand()
        {
            this.invoker.Click -= invoker_Click;
        }

        public override void OnStatusChanged(CommandStatus status)
        {
            this.invoker.Enabled = (status == CommandStatus.Enabled);
        }
    }
}
