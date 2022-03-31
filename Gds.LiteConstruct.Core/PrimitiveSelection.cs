using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using System.Collections;

namespace Gds.LiteConstruct.Core
{
	public sealed class PrimitiveSelection : IEnumerable<PrimitiveBase>, ICollection<PrimitiveBase>
	{
		private List<PrimitiveBase> items = new List<PrimitiveBase>();

		internal PrimitiveSelection()
		{
		}

		public PrimitiveBase this[int index]
		{
			get { return items[index]; }
		}

		public PrimitiveBase First
		{
			get { return items[0]; }
		}

		public PrimitiveBase Last
		{
			get { return items[items.Count - 1]; }
		}

        public PrimitiveBase[] Items
        {
            get { return items.ToArray(); }
        }

		private List<PrimitiveBase> oldItems = new List<PrimitiveBase>();

		private void BeforeChangeSelection()
		{
			oldItems.Clear();
			oldItems.AddRange(items.ToArray());
		}

		private void AfterChangeSelection()
		{
			if (oldItems.Count == 1 && items.Count == 1)
			{
				if (SingleSelectionChanged != null)
					SingleSelectionChanged(oldItems[0], items[0]);
			}
			if (oldItems.Count == 1 && items.Count != 1)
			{
				if (SingleSelectionLost != null)
					SingleSelectionLost(oldItems[0]);
			}
			if (oldItems.Count <= 0 && items.Count > 0)
			{
				if (NoneSelectionLost != null)
					NoneSelectionLost();
			}

			if (oldItems.Count > 0 && items.Count <= 0)
			{
				if (NoneSelectionEntered != null)
					NoneSelectionEntered();
			}
			if (oldItems.Count != 1 && items.Count == 1)
			{
				if (SingleSelectionEntered != null)
					SingleSelectionEntered(items[0]);
			}
			if (oldItems.Count <= 1 && items.Count > 1)
			{
				if (MultipleSelectionEntered != null)
					MultipleSelectionEntered(items.ToArray());
			}
		}

		private void NotifyItemsAdded(PrimitiveBase primitive)
		{
			if (ItemsAdded != null)
				ItemsAdded(new PrimitiveBase[] { primitive });
		}

		private void NotifyItemsAdded(IEnumerable<PrimitiveBase> primitives)
		{
			if (ItemsAdded != null)
			{
				List<PrimitiveBase> list = new List<PrimitiveBase>(primitives);
				ItemsAdded(list.ToArray());
			}
		}

		private void NotifyItemsRemoved(PrimitiveBase primitive)
		{
			if (ItemsRemoved != null)
				ItemsRemoved(new PrimitiveBase[] { primitive });
		}

		private void NotifyItemsRemoved(IEnumerable<PrimitiveBase> primitives)
		{
			if (ItemsRemoved != null)
			{
				List<PrimitiveBase> list = new List<PrimitiveBase>(primitives);
				ItemsRemoved(list.ToArray());
			}
		}

		public void Set(PrimitiveBase primitive)
		{
			BeforeChangeSelection();
			NotifyItemsRemoved(items);

			items.Clear();
			items.Add(primitive);

			NotifyItemsAdded(primitive);
			AfterChangeSelection();
		}

		public void SetRange(IEnumerable<PrimitiveBase> range)
		{
			BeforeChangeSelection();
			NotifyItemsRemoved(items);

			items.Clear();
			items.AddRange(range);

			NotifyItemsAdded(range);
			AfterChangeSelection();
		}

		public void Add(PrimitiveBase primitive)
		{
			BeforeChangeSelection();

			items.Add(primitive);

			NotifyItemsAdded(primitive);
			AfterChangeSelection();
		}

		public void AddRange(IEnumerable<PrimitiveBase> range)
		{
			BeforeChangeSelection();

			items.AddRange(range);

			NotifyItemsAdded(range);
			AfterChangeSelection();
		}

		public bool Remove(PrimitiveBase primitive)
		{
			BeforeChangeSelection();

			bool result = items.Remove(primitive);
			if (result == true)
				NotifyItemsRemoved(primitive);

			AfterChangeSelection();
			return result;
		}

		public void Clear()
		{
			BeforeChangeSelection();
			NotifyItemsRemoved(items);

			items.Clear();

			AfterChangeSelection();
		}

		public bool IsEmpty
		{
			get { return items.Count <= 0; }
		}

		public bool IsSingle
		{
			get { return items.Count == 1; }
		}

		public bool IsMultiple
		{
			get { return items.Count > 1; }
		}

		public SelectionType Type
		{
			get
			{
				if (IsEmpty)
					return SelectionType.None;
				else if (IsSingle)
					return SelectionType.Single;
				else
					return SelectionType.Multiple;
			}
		}

		#region IEnumerable Members

		public IEnumerator<PrimitiveBase> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}

		#endregion

		#region ICollection<PrimitiveBase> Members

		public bool Contains(PrimitiveBase item)
		{
			return items.Contains(item);
		}

		public int Count
		{
			get { return items.Count; }
		}

		void ICollection<PrimitiveBase>.CopyTo(PrimitiveBase[] array, int arrayIndex)
		{
			items.CopyTo(array, arrayIndex);
		}

		bool ICollection<PrimitiveBase>.IsReadOnly
		{
			get { return (items as ICollection<PrimitiveBase>).IsReadOnly; }
		}

		#endregion

		public event ItemsAddedEventHandler ItemsAdded;
		public event ItemsRemovedEventHandler ItemsRemoved;

		public event NoneSelectionEnteredEventHandler NoneSelectionEntered;
		public event NoneSelectionLostEventHandler NoneSelectionLost;

		public event SingleSelectionEnteredEventHandler SingleSelectionEntered;
		public event SingleSelectionLostEventHandler SingleSelectionLost;
		public event SingleSelectionChangedEventHandler SingleSelectionChanged;

		public event MultipleSelectionEnteredEventHandler MultipleSelectionEntered;
		//public event MultipleSelectionLostEventHandler MultipleSelectionLost;
	}

	public enum SelectionType
	{
		None,
		Single,
		Multiple
	}

	public delegate void ItemsAddedEventHandler(PrimitiveBase[] items);
	public delegate void ItemsRemovedEventHandler(PrimitiveBase[] items);

	public delegate void NoneSelectionEnteredEventHandler();
	public delegate void NoneSelectionLostEventHandler();

	public delegate void SingleSelectionEnteredEventHandler(PrimitiveBase item);
	public delegate void SingleSelectionLostEventHandler(PrimitiveBase item);
	public delegate void SingleSelectionChangedEventHandler(PrimitiveBase oldItem, PrimitiveBase newItem);

	public delegate void MultipleSelectionEnteredEventHandler(PrimitiveBase[] items);
	public delegate void MultipleSelectionLostEventHandler(PrimitiveBase[] items);
}
