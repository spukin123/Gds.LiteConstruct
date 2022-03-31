using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.Windows.WorkItems;

namespace Gds.LiteConstruct.Windows.Openers
{
	public class SimilarWIFormsOpener<TForm, TArg> : SimilarFormsOpener<TForm, TArg> where TForm : class, IForm, IWorkItemComponent
	{
		private readonly Dictionary<Guid, WorkItem> workItems = new Dictionary<Guid, WorkItem>();

		public SimilarWIFormsOpener(FormCreatorEventHandler<TForm, TArg> formCreator)
			: base(formCreator)
		{
		}

		protected override void BeforeShowForm(Guid id, TForm form)
		{
			WorkItem wi = new WorkItem();
			wi.AddItem(form);
			workItems.Add(id, wi);
		}

		protected override void AfterCloseForm(Guid id, TForm form)
		{
			if (workItems.ContainsKey(id))
			{
				workItems[id].Dispose();
				workItems.Remove(id);
			}
		}
	}
}
