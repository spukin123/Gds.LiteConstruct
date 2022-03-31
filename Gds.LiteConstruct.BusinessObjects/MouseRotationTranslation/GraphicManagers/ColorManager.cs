using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.GraphicManagers
{
    public class ColorManager : IConnectableToColorable, IActionable
    {
        protected IColorable[] colorableObjects;

        #region IConnectableToColorable Members

        public void ConnectTo(IColorable[] colorableObjects)
        {
            this.colorableObjects = colorableObjects;
        }

        #endregion

        #region IAction Members

        public void StartAction()
        {

        }

        public void StopAction()
        {

        }

        #endregion
    }
}
