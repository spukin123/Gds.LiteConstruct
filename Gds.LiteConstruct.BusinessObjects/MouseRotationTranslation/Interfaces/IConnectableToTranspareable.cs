using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces
{
    public interface IConnectableToTranspareable
    {
        void ConnectTo(ITranspareable[] transpareableObjects);
    }
}
