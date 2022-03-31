using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gds.LiteConstruct.Windows.Commands.Adapters
{
    public class CheckedToolStripMenuItemAdapter : CheckedItemAdapter<ToolStripMenuItem>
    {
        protected override void AddInvokerClickHandler(EventHandler eventHandler)
        {
            this.invoker.Click += eventHandler;
        }

        protected override void RemoveInvokerClickHandler(EventHandler eventHandler)
        {
            this.invoker.Click -= eventHandler;
        }

        protected override bool InvokerChecked
        {
            get
            {
                return this.invoker.Checked;
            }
            set
            {
                this.invoker.Checked = value;
            }
        }
    }
}
