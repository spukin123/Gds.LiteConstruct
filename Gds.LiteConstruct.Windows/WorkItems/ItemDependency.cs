using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Windows.WorkItems
{
    public class ItemDependency : IDependency
    {
        private object dependentItem;
        private WorkItem workItem;

        public ItemDependency(object dependentItem, WorkItem workItem)
        {
            this.dependentItem = dependentItem;
            this.workItem = workItem;
        }

        public void RemoveDependent()
        {
            workItem.RemoveItem(dependentItem);
        }
    }
}
