using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Windows.WorkItems
{
    public interface IWorkItemComponent
    {
        void AddedToWorkItem(WorkItem workItem);
        void RemovedFromWorkItem(WorkItem workItem);
    }
}
