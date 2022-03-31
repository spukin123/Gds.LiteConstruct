using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Windows.Commands
{
    public delegate void SimpleHandler();

    public interface ISynchronizeService
    {
        object Invoke(Delegate method, params object[] args);
        void Invoke(SimpleHandler methodToExecute);
        bool InvokeRequired { get; }
    }
}
