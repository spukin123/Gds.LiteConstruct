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
    public partial class PrimitiveScaleControl : UserControl
    {
        public PrimitiveScaleControl()
        {
            InitializeComponent();
            numericScaleFactor.Value = (decimal)1f;
        }

        public event ScaleEventHandler ButtonApplyClick;

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (ButtonApplyClick != null)
            {
                ButtonApplyClick((float)numericScaleFactor.Value);
                numericScaleFactor.Value = (decimal)1f;
            }
        }
    }
}
