using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Windows.Commands;
using Gds.Runtime;

namespace Gds.LiteConstruct.Windows.Controlling
{
	internal class CommandsAccessibility : ICommandsAccessibility
	{
		private ItemCommandsAccessibility itemBaseCommands;
		private ItemCommandsAccessibility directoryBaseCommands;
		private readonly Dictionary<string, ItemCommandsAccessibility> itemCommands = new Dictionary<string, ItemCommandsAccessibility>();
		private readonly Dictionary<string, ItemCommandsAccessibility> directoryCommands = new Dictionary<string, ItemCommandsAccessibility>();
		private readonly CommandHolder commands;

		public CommandsAccessibility(CommandHolder commands)
		{
			Guard.ArgumentNotNull(commands, "commands");
			this.commands = commands;
		}

		#region ICommandsAccessibility Members

		public void RegisterItemCommandsAccessibilty(string typeName, ItemCommandsAccessibility commandsAccessibility)
		{
			Guard.ArgumentNotNullOrEmptyString(typeName, "typeName");
			Guard.ArgumentNotNull(commandsAccessibility, "commandsAccessibility");
			itemCommands.Add(typeName, commandsAccessibility);
		}

		public void RegisterDirectoryCommandsAccessibilty(string typeName, ItemCommandsAccessibility commandsAccessibility)
		{
			Guard.ArgumentNotNullOrEmptyString(typeName, "typeName");
			Guard.ArgumentNotNull(commandsAccessibility, "commandsAccessibility");
			directoryCommands.Add(typeName, commandsAccessibility);
		}

		public void RegisterItemCommandsBaseAccessibilty(ItemCommandsAccessibility commandsAccessibility)
		{
			itemBaseCommands = commandsAccessibility;
		}

		public void RegisterDirectoryCommandsBaseAccessibilty(ItemCommandsAccessibility commandsAccessibility)
		{
			directoryBaseCommands = commandsAccessibility;
		}

		public void SetItemCommandsAccessibility(ITypeItem typeItem)
		{
			if (typeItem == null)
			{
				if (itemBaseCommands != null)
					itemBaseCommands.ExecuteForNullable(commands);
				foreach (ItemCommandsAccessibility accessibility in itemCommands.Values)
				{
					accessibility.ExecuteForNullable(commands);
				}
				return;
			}
			
			if (itemBaseCommands != null)
				itemBaseCommands.Execute(commands);
			if (itemCommands.ContainsKey(typeItem.TypeName))
				itemCommands[typeItem.TypeName].Execute(commands);
		}

		public void SetDirectoryCommandsAccessibility(ITypeItem typeItem)
		{
			if (typeItem == null)
			{
				if (directoryBaseCommands != null)
					directoryBaseCommands.ExecuteForNullable(commands);
				foreach (ItemCommandsAccessibility accessibility in directoryCommands.Values)
				{
					accessibility.ExecuteForNullable(commands);
				}
				return;
			}
			
			if (directoryBaseCommands != null)
				directoryBaseCommands.Execute(commands);
			if (directoryCommands.ContainsKey(typeItem.TypeName))
				directoryCommands[typeItem.TypeName].Execute(commands);
		}

		#endregion
	}
}
