using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Windows.Commands
{
    public interface ICommandAdapter : IDisposable
    {
        void SetInvoker(object adaptee);
        void SetCommand(Command command);
        void UnsetCommand();
    }
}
