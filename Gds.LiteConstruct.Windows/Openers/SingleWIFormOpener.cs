using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Windows.WorkItems;
using System.Windows.Forms;

namespace Gds.LiteConstruct.Windows.Openers
{
	public class SingleWIFormOpener<T> : SingleFormOpener<T> where T : Form, IWorkItemComponent
	{
		private WorkItem workItem;

		protected override void BeforeShowForm()
		{
			workItem = new WorkItem();
			workItem.AddItem(form);
		}

		protected override void AflerCloseForm()
		{
			workItem.Dispose();
		}
	}
}
