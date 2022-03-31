using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Gds.LiteConstruct.Windows.Openers
{
	public delegate TForm FormCreatorEventHandler<TForm, TArg>(TArg data, bool isNewItem) where TForm : class, IForm;

	public class SimilarFormsOpener<TForm, TArg> where TForm : class, IForm
	{
		private readonly Dictionary<Guid, TForm> forms = new Dictionary<Guid, TForm>();

		private readonly FormCreatorEventHandler<TForm, TArg> formCreator;

		
		public SimilarFormsOpener(FormCreatorEventHandler<TForm, TArg> formCreator)
		{
			this.formCreator = formCreator;
		}

		#region Public Methods

		public void ShowEdit(Guid id, TArg arg)
		{
			ShowForm(id, arg, false);
		}

		public void ShowNew(Guid id, TArg arg)
		{
			ShowForm(id, arg, true);
		}

		private void ShowFormEditItemExisting(Guid id)
		{
			if (forms.ContainsKey(id))
			{
				TForm openedForm = forms[id];
				openedForm.BringToFront();
			}
		}

		public bool IsOpened(Guid id)
		{
			return forms.ContainsKey(id);
		}

		public void CloseForm(Guid id)
		{
			if (forms.ContainsKey(id))
				forms[id].Close();
		}

		public TForm GetFormById(Guid id)
		{
			if (forms.ContainsKey(id))
				return forms[id];
			else
				return null;
		}

		#endregion

		#region Private Methods

		private void ShowForm(Guid id, TArg arg, bool isNewItem)
		{
			if (forms.ContainsKey(id))
			{
				ShowFormEditItemExisting(id);
			}
			else
			{
				TForm form = formCreator(arg, isNewItem);
				BeforeShowForm(id, form);
				form.Show();
				forms.Add(id, form);
				form.Closed += form_Closed;
			}
		}

		private void form_Closed(object sender, EventArgs e)
		{
			TForm form = (TForm)sender;

			if (forms.ContainsValue(form))
			{
				Guid id = GetFormId(form);
				AfterCloseForm(id, form);
				forms.Remove(id);
			}
		}

		private Guid GetFormId(TForm form)
		{
			foreach (KeyValuePair<Guid, TForm> item in forms)
			{
				if (item.Value == form)
					return item.Key;
			}
			return Guid.Empty;
		}

		#endregion

		#region Protected Virtual Methods

		protected virtual void BeforeShowForm(Guid id, TForm form)
		{
		}

		protected virtual void AfterCloseForm(Guid id, TForm form)
		{
		}

		#endregion
	}
}
