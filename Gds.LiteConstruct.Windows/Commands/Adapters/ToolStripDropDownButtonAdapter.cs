using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gds.LiteConstruct.Windows.Commands.Adapters
{
	public class ToolStripDropDownButtonAdapter : CommandAdapterBase<ToolStripDropDownButton>
    {
        public override void OnSetCommand(Command command)
        {
        }

        public override void OnUnsetCommand()
        {
        }

        public override void OnStatusChanged(CommandStatus status)
        {
            invoker.Enabled = status == CommandStatus.Enabled;
        }
    }
}
