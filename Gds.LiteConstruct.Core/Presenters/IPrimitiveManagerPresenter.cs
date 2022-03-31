using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Core.Controllers;

namespace Gds.LiteConstruct.Core.Presenters
{
    public interface IPrimitiveManagerPresenter : IManagerPresenter
    {
        IPrimitiveManagerController PrimitiveManagerController { set; }
        void PrimitiveAdded();
        bool DeletePrimitiveEnabled { set; }
        void PrimitiveAddingCancelled();
    }
}
