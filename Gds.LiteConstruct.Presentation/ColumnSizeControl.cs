using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;

namespace Gds.LiteConstruct.Presentation
{
    public partial class ColumnSizeControl : UserControl, IPrimitivePropertiesControl
    {
        private IColumnSizeable primitive;
        private bool binded = true;

        public ColumnSizeControl(IColumnSizeable primitive)
        {
            InitializeComponent();

            this.primitive = primitive;
            
            LoadAll();
        }

        private void Bind()
        {
            binded = true;
        }

        private void Unbind()
        {
            binded = false;
        }

        private void LoadAll()
        {
            Unbind();

            numericUpDownZ.Value = (decimal)primitive.Z;
            numericUpDownRadius.Value = (decimal)primitive.Radius;

            Bind();
        }

        private void numericUpDownZ_ValueChanged(object sender, EventArgs e)
        {
            if (binded)
            {
                primitive.Z = (float)numericUpDownZ.Value;
            }
        }

        private void numericUpDownRadius_ValueChanged(object sender, EventArgs e)
        {
            if (binded)
            {
                primitive.Radius = (float)numericUpDownRadius.Value;
            }
        }
        
        private void ScaleControl_Apply(float scaleFactor)
        {
			primitive.Scale(scaleFactor);

			Unbind();
			numericUpDownRadius.Value = (decimal)primitive.Radius;
			numericUpDownZ.Value = (decimal)primitive.Z;
			Bind();
        }

        #region IPrimitivePropertiesControl Members

        public void SetPrimitive(object primitive)
        {
            this.primitive = primitive as IColumnSizeable;
        }

        #endregion
    }
}
