using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Windows.WorkItems;

namespace Gds.LiteConstruct.Windows.Commands
{
    public class CommandInvokerDependency : IDependency
    {
        private Command command;
        private object invoker;

        public CommandInvokerDependency(Command command, object invoker)
        {
            this.command = command;
            this.invoker = invoker;
        }

        public void RemoveDependent()
        {
            command.RemoveInvoker(invoker);
        }
    }
}
