using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Windows.Commands;
using System.Threading;
using Gds.Runtime;

namespace Gds.LiteConstruct.Windows.WorkItems
{
    public class WorkItem : IDisposable, IWorkItemComponent
    {
        public WorkItem()
        {
            creatorThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        private int creatorThreadId;

        private CommandHolder commands;

        public CommandHolder Commands
        {
            get { return commands ?? (commands = new CommandHolder(this)); }
        }

        private WorkItem parentWorkItem;
        public WorkItem ParentWorkItem
        {
            get { return parentWorkItem; }
        }

        private object itemInProgress = null;

        public object ItemInProgress
        {
            get 
            {
                CheckCalledInCreatorThread();
                return itemInProgress; 
            }
        }

        private void CheckCalledInCreatorThread()
        {
            if (creatorThreadId != Thread.CurrentThread.ManagedThreadId)
            {
                throw new InvalidOperationException(
                    Gds.LiteConstruct.Windows.Properties.Resources.ItemInProgressCanBeAccessedFromThreadWhereWorkItemWasCreated);
            }
        }

        private List<object> items = new List<object>();

        private Dictionary<object, List<IDependency>> dependencies = new Dictionary<object, List<IDependency>>();

        public void SetDependency(object item, IDependency dependency)
        {
            Guard.ArgumentNotNull(item, "item");
            Guard.ArgumentNotNull(dependency, "dependency");

            List<IDependency> objectDependencies;
            if (!dependencies.TryGetValue(item, out objectDependencies))
            {
                objectDependencies = new List<IDependency>();
                dependencies.Add(item, objectDependencies);
            }
            objectDependencies.Add(dependency);
        }

        public virtual void AddItem(object item)
        {
            Guard.ArgumentNotNull(item, "item");
            if (items.Contains(item))
            {
                throw new InvalidOperationException(Gds.LiteConstruct.Windows.Properties.Resources.ItemIsAlreadyAddedToWorkitem);
            }

            if (itemInProgress != null)
            {
                SetDependency(itemInProgress, new ItemDependency(item, this));
            }
            IWorkItemComponent component = item as IWorkItemComponent;
            if (component != null)
            {
                try
                {
                    itemInProgress = item;
                    component.AddedToWorkItem(this);
                }
                finally
                {
                    itemInProgress = null;
                }
            }
            items.Add(item);
        }

        public virtual void RemoveItem(object item)
        {
            Guard.ArgumentNotNull(item, "item");
            if (!items.Contains(item))
            {
                throw new InvalidOperationException(Gds.LiteConstruct.Windows.Properties.Resources.ItemIsNotAddedToWorkitem);
            }
            IWorkItemComponent component = item as IWorkItemComponent;
            if (component != null)
            {
                component.RemovedFromWorkItem(this);
            }
            items.Remove(item);

            if (!disposing)
            {
                List<IDependency> objectDependencies;
                if (dependencies.TryGetValue(item, out objectDependencies))
                {
                    foreach (IDependency dependency in objectDependencies)
                    {
                        dependency.RemoveDependent();
                    }
                    dependencies.Remove(item);
                }
            }
        }

        private bool disposing = false;

        public void Dispose()
        {
            disposing = true;
            foreach (object item in items.ToArray())
            {
                RemoveItem(item);
            }
            
            if (this.parentWorkItem != null)
            {
                parentWorkItem.RemoveItem(this);
            }
        }

        void IWorkItemComponent.AddedToWorkItem(WorkItem workItem)
        {
            this.parentWorkItem = workItem;
        }

        void IWorkItemComponent.RemovedFromWorkItem(WorkItem workItem)
        {
            if (this.parentWorkItem != workItem)
            {
                throw new ArgumentException(Gds.LiteConstruct.Windows.Properties.Resources.WorkitemCanBeRemovedOnlyFromParentWorkitem, "workItem");
            }
            this.parentWorkItem = null;
        }

        public IDisposable Dependency(object item)
        {
            this.itemInProgress = item;
            return new DependencyMarker(this);
        }

        private class DependencyMarker : IDisposable
        {
            private WorkItem workItem;

            public DependencyMarker(WorkItem workItem)
            {
                this.workItem = workItem;
            }

            void IDisposable.Dispose()
            {
                this.workItem.itemInProgress = null;
            }
        }
    }
}
