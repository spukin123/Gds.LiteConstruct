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
    public partial class ParallelepipedSizeControl : UserControl, IPrimitivePropertiesControl
    {
        protected IParallelepipedSizable primitive;

        public ParallelepipedSizeControl(IParallelepipedSizable primitive)
        {
            InitializeComponent();

            SetPrimitive(primitive);
        }
        
        public void SetPrimitive(object primitive)
        {
            this.primitive = primitive as IParallelepipedSizable;
            numericUpDownX.Value = (decimal)this.primitive.Size.X;
            numericUpDownY.Value = (decimal)this.primitive.Size.Y;
            numericUpDownZ.Value = (decimal)this.primitive.Size.Z;
        }

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            primitive.SetX((float)numericUpDownX.Value);
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            primitive.SetY((float)numericUpDownY.Value);
        }

        private void numericUpDownZ_ValueChanged(object sender, EventArgs e)
        {
            primitive.SetZ((float)numericUpDownZ.Value);
        }

        private void scaleControl_ButtonApplyClick(float scaleFactor)
        {
            primitive.Scale(scaleFactor);

            numericUpDownX.Value = (decimal)primitive.Size.X;
            numericUpDownY.Value = (decimal)primitive.Size.Y;
            numericUpDownZ.Value = (decimal)primitive.Size.Z;
        }
    }
}
