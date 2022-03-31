using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Axises;
using System.Drawing;
using Gds.LiteConstruct.PrimitivesManagement.AxisBindings.BindingAxisControllerManagement;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.GraphicManagers;

namespace PrimitivesManagement.AxisBindings.BindingAxisControllerManagement
{
    internal class AssociatedBindingAxisController : BindingAxisController
    {
        private const int SidesNumber = 15;

        public AssociatedBindingAxis AssociatedBindingAxis
        {
            get { return axis as AssociatedBindingAxis; }
        }

        public AssociatedBindingAxisController(AssociatedBindingAxis axis)
            : base(axis, SidesNumber, Color.FromArgb(0, 0, 200))
        {
        }

        protected override BrightnessManager CreateBrightnessManager()
        {
            return new BrightnessManager(color, 50f, 11f, 10);
        }
    }
}
