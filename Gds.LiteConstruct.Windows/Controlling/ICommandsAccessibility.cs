using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Windows.Controlling
{
	public interface ICommandsAccessibility
	{
		void RegisterItemCommandsAccessibilty(string typeName, ItemCommandsAccessibility commandsAccessibility);
		void RegisterDirectoryCommandsAccessibilty(string typeName, ItemCommandsAccessibility commandsAccessibility);
		void RegisterItemCommandsBaseAccessibilty(ItemCommandsAccessibility commandsAccessibility);
		void RegisterDirectoryCommandsBaseAccessibilty(ItemCommandsAccessibility commandsAccessibility);
		void SetItemCommandsAccessibility(ITypeItem typeItem);
		void SetDirectoryCommandsAccessibility(ITypeItem typeItem);
	}
}
