using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.TransformationControllers;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces
{
    public interface IResizableVisitorPresenter
    {
        void Visit(TranslationAxisController controller);
        void Visit(TranslationPlaneController controller);
        void Visit(RotationController controller);
    }
}
