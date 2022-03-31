using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.BusinessObjects;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.Presentation
{
    public partial class PrimitivePositionControl : UserControl, IPrimitivePropertiesControl
    {
        private IMovable primitive;

        public PrimitivePositionControl(IMovable primitive)
        {
            InitializeComponent();

            SetPrimitive(primitive);
        }

        public void SetPosition(float x, float y, float z)
        {
            numericUpDownX.Value = (decimal)x;
            numericUpDownY.Value = (decimal)y;
            numericUpDownZ.Value = (decimal)z;
        }

        public void SetPrimitive(object primitive)
        {
            this.primitive = primitive as IMovable;

            SetPosition(this.primitive.Position.X, this.primitive.Position.Y, this.primitive.Position.Z);
        }

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            primitive.MoveX((float)numericUpDownX.Value);
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            primitive.MoveY((float)numericUpDownY.Value);
        }

        private void numericUpDownZ_ValueChanged(object sender, EventArgs e)
        {
            primitive.MoveZ((float)numericUpDownZ.Value);
        }
    }
}
