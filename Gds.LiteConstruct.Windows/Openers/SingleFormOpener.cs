using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gds.LiteConstruct.Windows.Openers
{
	public class SingleFormOpener : SingleFormOpener<Form>
	{
	}

	public class SingleFormOpener<T> where T : Form
	{
		protected T form;

		public T Form
		{
			get { return form; }
		}

		protected bool isOpened = false;

		public bool IsOpened
		{
			get { return isOpened; }
		}

		public void Show(T form)
		{
			if (!IsOpened)
			{
				isOpened = true;
				this.form = form;
				form.Closed += new EventHandler(form_Closed);
				BeforeShowForm();
				form.Show();
			}
			else
			{
				this.form.BringToFront();
			}
		}

		public void ShowExisting()
		{
			if (IsOpened)
				form.BringToFront();
		}

		private void form_Closed(object sender, EventArgs e)
		{
			isOpened = false;
			AflerCloseForm();
		}

		protected virtual void BeforeShowForm()
		{
		}

		protected virtual void AflerCloseForm()
		{
		}
	}
}
