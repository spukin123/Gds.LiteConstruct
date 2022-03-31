using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Windows.Commands
{
	public static class CommandsSynchronizer
	{
		private static ISynchronizeService syncService;
		public static ISynchronizeService SynchronizeService
		{
			get { return syncService; }
			set { syncService = value; }
		}
	}
}
