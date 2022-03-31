using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Core.Controllers;
using Gds.LiteConstruct.Environment;
using Gds.LiteConstruct.BusinessObjects;

namespace Gds.LiteConstruct.Core.Presenters
{
    public interface ITexturingPresenter : IManagerPresenter
    {
        ITexturingController TexturizeManagerController { set; }
        void EnableTexturing(bool enable);
        TexturesEnvironment TexturesEnvironment { set; }
        void UpdateTexturesCategories();
        void EnableRotationControl(bool state);
        Angle RotationAngle { set; } 
    }
}
