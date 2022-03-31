using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces
{
    public interface IConnectableToColorable
    {
        void ConnectTo(IColorable[] colorableObjects);
    }
}
