using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Windows.WorkItems;

namespace Gds.LiteConstruct.Windows.Commands
{
    public class CommandHandlerDependency : IDependency
    {
        public CommandHandlerDependency(Command command, VoidHandler handlerDelegate)
        {
            this.command = command;
            this.handlerDelegate = handlerDelegate;
        }

        private Command command;
        private VoidHandler handlerDelegate;

        public void RemoveDependent()
        {
            this.command.Execute -= handlerDelegate;
        }

    }
}
