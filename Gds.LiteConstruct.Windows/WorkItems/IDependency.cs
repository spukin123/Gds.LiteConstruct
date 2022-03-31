using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Windows.WorkItems
{
    public interface IDependency
    {
        void RemoveDependent();
    }
}
