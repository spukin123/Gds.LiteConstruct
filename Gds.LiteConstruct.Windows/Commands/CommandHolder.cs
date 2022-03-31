using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Windows.WorkItems;

namespace Gds.LiteConstruct.Windows.Commands
{
    public class CommandHolder
    {
        public CommandHolder(WorkItem workItem)
        {
            this.workItem = workItem;
        }

        private WorkItem workItem;
        
        Dictionary<string, Command> commands = new Dictionary<string, Command>();

        public Command this[string key]
        {
            get
            {
                Command result;
                if (!commands.TryGetValue(key, out result))
                {
                    result = new Command(this.workItem);
                    commands.Add(key, result);
                }
                return result;
            }
        }
    }
}
