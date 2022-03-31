using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Windows.Commands
{
    public interface ICommandAdapterFactory
    {
        ICommandAdapter Create();
    }
}
