using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.BusinessObjects;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Gds.Windows;

namespace Gds.LiteConstruct.Presentation
{
    public partial class TrianglePrismSizeControl : UserControl, IPrimitivePropertiesControl
    {
        protected ITrianglePrismSizable primitive;

        public TrianglePrismSizeControl(ITrianglePrismSizable primitive)
        {
            InitializeComponent();

            SetPrimitive(primitive);
        }

        public void SetPrimitive(object primitive)
        {
            this.primitive = primitive as ITrianglePrismSizable;
            numericUpDownA.Value = (decimal)this.primitive.Size.A;
            numericUpDownB.Value = (decimal)this.primitive.Size.B;
            numericUpDownC.Value = (decimal)this.primitive.Size.C;
            numericUpDownZ.Value = (decimal)this.primitive.Size.Z;
        }

        private void numericUpDownA_ValueChanged(object sender, EventArgs e)
        {
            primitive.SetA((float)numericUpDownA.Value);
        }

        private void numericUpDownB_ValueChanged(object sender, EventArgs e)
        {
            primitive.SetB((float)numericUpDownB.Value);
        }

        private void numericUpDownC_ValueChanged(object sender, EventArgs e)
        {
            primitive.SetC((float)numericUpDownC.Value);
        }

        private void numericUpDownZ_ValueChanged(object sender, EventArgs e)
        {
            primitive.SetZ((float)numericUpDownZ.Value);
        }

        private void buttonEquilateral_Click(object sender, EventArgs e)
        {
            primitive.MakeEquilateral();
            numericUpDownA.Value = (decimal)primitive.Size.A;
            numericUpDownB.Value = (decimal)primitive.Size.B;
            numericUpDownC.Value = (decimal)primitive.Size.C;
            numericUpDownZ.Value = (decimal)primitive.Size.Z;
        }

        private void scaleControl_ButtonApplyClick(float scaleFactor)
        {
            primitive.Scale(scaleFactor);

            numericUpDownA.Value = (decimal)primitive.Size.A;
            numericUpDownB.Value = (decimal)primitive.Size.B;
            numericUpDownC.Value = (decimal)primitive.Size.C;
            numericUpDownZ.Value = (decimal)primitive.Size.Z;
        }
    }
}
