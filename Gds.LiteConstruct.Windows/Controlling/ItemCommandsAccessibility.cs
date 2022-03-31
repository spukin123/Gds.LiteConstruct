using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Windows.Commands;

namespace Gds.LiteConstruct.Windows.Controlling
{
	public abstract class ItemCommandsAccessibility
	{
		private List<string> enabled = new List<string>();
		private List<string> disabled = new List<string>();

		protected void AddEnabled(string commandName)
		{
			enabled.Add(commandName);
		}

		protected void AddDisabled(string commandName)
		{
			disabled.Add(commandName);
		}

		public void Execute(CommandHolder commands)
		{
			foreach (string command in enabled)
			{
				commands[command].Status = CommandStatus.Enabled;
			}
			foreach (string command in disabled)
			{
				commands[command].Status = CommandStatus.Disabled;
			}
		}

		public void ExecuteForNullable(CommandHolder commands)
		{
			foreach (string command in enabled)
			{
				commands[command].Status = CommandStatus.Disabled;
			}
			foreach (string command in disabled)
			{
				commands[command].Status = CommandStatus.Disabled;
			}
		}
	}
}
