using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Windows.Commands.Adapters
{
    public class ToolStripButtonAdapterFactory : ICommandAdapterFactory
    {
        public ICommandAdapter Create()
        {
            return new CheckedToolStripButtonAdapter();
        }
    }
}
