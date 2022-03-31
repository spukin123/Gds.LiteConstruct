using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Core.Controllers
{
    public interface IMainFormController
    {
        int FPS { get; }

        void CreateNewModel();
        void SaveModel(string fileName);
        void LoadModel(string fileName);
    }
}
