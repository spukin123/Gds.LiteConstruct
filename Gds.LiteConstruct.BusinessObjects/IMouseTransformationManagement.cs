using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public interface IMouseTransformationManagement
    {
        bool MouseTransformationOn { get; }
        void SetMouseTranslationByPlaneMode();
        void SetMouseTranslatonByAxisMode(Vector3 look);
        void SetMouseRotationMode(Vector3 look);
        void SetMouseTranslationMode(Vector3 look);
        void TurnOffMouseTransformation();
        void HideMouseTransformation();
        void ShowMouseTransformation();
        bool MouseTranslationIsOn { get; }
        bool MouseRotationIsOn { get; }
    }
}
