using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Core.Presenters;

namespace Gds.LiteConstruct.Core
{
    public class CoreManager : IDisposable
    {
        private Core core;

        public CoreManager(IMainFormPresenter mainFormPresenter,
                           IGraphicWindowPresenter graphicWindowPresenter,
                           IPrimitiveManagerPresenter primitiveManagerPresenter,
                           ITexturingPresenter texturizeManagerPresenter,
                           IPrimitivePropertiesPresenter primitivePropertiesPresenter,
                           IRenderModeSwitcherPresenter renderModeSwitcherPresenter,
                           ICameraSwitcherPresenter cameraModeSwitcherPresenter,
                           IPrimitiveEditModeSwitcherPresenter primitiveEditModeSwitcherPresenter)
        {
            core = new Core(mainFormPresenter, graphicWindowPresenter,
                            primitiveManagerPresenter, texturizeManagerPresenter,
                            primitivePropertiesPresenter, renderModeSwitcherPresenter,
                            cameraModeSwitcherPresenter, primitiveEditModeSwitcherPresenter);
        }

        public void Dispose()
        {
            core.Dispose();
        }
    }
}
