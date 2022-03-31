using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Windows.Commands.Adapters
{
    public class CheckedToolStripButtonAdapterFactory : ICommandAdapterFactory
    {
        #region ICommandAdapterFactory Members

        public ICommandAdapter Create()
        {
            return new CheckedToolStripButtonAdapter();
        }

        #endregion
    }
}
