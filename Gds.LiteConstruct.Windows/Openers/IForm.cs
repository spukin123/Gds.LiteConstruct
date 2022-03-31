using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Windows.Openers
{
	public interface IForm
	{
		void Show();
		void Close();
		void BringToFront();
		event EventHandler Closed;
	}
}
