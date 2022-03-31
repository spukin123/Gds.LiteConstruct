using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gds.LiteConstruct.Windows.Commands.Adapters
{
    public class ToolStripButtonAdapter : CommandAdapterBase<ToolStripButton>
    {
        public override void OnSetCommand(Command command)
        {
            invoker.Click += invoker_Click;
        }

        void invoker_Click(object sender, EventArgs e)
        {
            command.Fire();
        }

        public override void OnUnsetCommand()
        {
            invoker.Click -= invoker_Click;
        }

        public override void OnStatusChanged(CommandStatus status)
        {
            invoker.Enabled = status == CommandStatus.Enabled;
        }
    }
}
