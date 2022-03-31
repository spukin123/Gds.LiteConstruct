using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Windows.Controlling
{
	public class SelectedItem : ITypeItem
	{
		private Guid id;

		public Guid Id
		{
			get { return id; }
		}

		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		private string typeName;

		public string TypeName
		{
			get { return typeName; }
		}

		public SelectedItem(Guid id, string name, string typeName)
		{
			this.id = id;
			this.name = name;
			this.typeName = typeName;
		}
	}

	public delegate void SelectedItemChanged(SelectedItem selectedItem);
}
