using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.PrimitivesManagement.AxisBindings.BindingAxisControllerManagement;
using Gds.LiteConstruct.BusinessObjects.Axises;
using System.Drawing;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.GraphicManagers;

namespace PrimitivesManagement.AxisBindings.BindingAxisControllerManagement
{
    internal class FreeBindingAxisController : BindingAxisController
    {
        private const int SidesNumber = 15;

        public FreeBindingAxis FreeBindingAxis
        {
            get { return axis as FreeBindingAxis; }
        }

        public FreeBindingAxisController(FreeBindingAxis axis)
            : base(axis, SidesNumber, Color.FromArgb(160, 0, 0))
        {
        }

        protected override BrightnessManager CreateBrightnessManager()
        {
            return new BrightnessManager(color, 90f, 11f, 10);
        }
    }
}
