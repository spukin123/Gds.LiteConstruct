using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Windows.Commands.Adapters
{
    public class ButtonAdapterFactory : ICommandAdapterFactory
    {
        #region ICommandAdapterFactory Members

        public ICommandAdapter Create()
        {
            return new ButtonAdapter();
        }

        #endregion
    }
}
