using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.BusinessObjects;

namespace Gds.LiteConstruct.Presentation
{
    public partial class PrimitiveRotationControl : UserControl, IPrimitivePropertiesControl
    {
        IRotatable primitive;

        public PrimitiveRotationControl(IRotatable primitive)
        {
            InitializeComponent();

            SetPrimitive(primitive);
        }

        public void SetRotation(RotationVector rotation)
        {
            numericUpDownX.Value = (decimal)rotation.X.Degrees;
            numericUpDownY.Value = (decimal)rotation.Y.Degrees;
            numericUpDownZ.Value = (decimal)rotation.Z.Degrees;
        }

        public void SetPrimitive(object primitive)
        {
            this.primitive = primitive as IRotatable;

            if (this.primitive.CanRotateX == false)
                numericUpDownX.Enabled = false;
            if (this.primitive.CanRotateY == false) 
                numericUpDownY.Enabled = false;
            if (this.primitive.CanRotateZ == false)
                numericUpDownZ.Enabled = false;

            SetRotation(this.primitive.Rotation);
        }

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            decimal degree = ConvDegrees(numericUpDownX.Value);
            if (degree != numericUpDownX.Value)
                numericUpDownX.Value = degree;
            primitive.RotateX(Angle.FromDegrees((float)degree));
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            decimal degree = ConvDegrees(numericUpDownY.Value);
            if (degree != numericUpDownY.Value)
                numericUpDownY.Value = degree;
            primitive.RotateY(Angle.FromDegrees((float)degree));
        }

        private void numericUpDownZ_ValueChanged(object sender, EventArgs e)
        {
            decimal degree = ConvDegrees(numericUpDownZ.Value);
            if (degree != numericUpDownZ.Value)
                numericUpDownZ.Value = degree;
            primitive.RotateZ(Angle.FromDegrees((float)degree));
        }

        private decimal ConvDegrees(decimal val)
        {
            if (val >= 181)
                val = -179 + (val - 181);
            else
                if (val <= -180)
                    val = 180 + (val + 180);
                else
                    return val;
            return val;
        }
    }
}
