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
    public partial class ColumnAdditionalControl : UserControl, IPrimitivePropertiesControl
    {
        private IColumnExtendable primitive;
        private bool binded = true;

        public ColumnAdditionalControl(IColumnExtendable primitive)
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

            numericUpDownAnglesNumber.Value = (decimal)primitive.AnglesNumber;

            Bind();
        }

        private void numericUpDownAnglesNumber_ValueChanged(object sender, EventArgs e)
        {
            if (binded)
            {
                primitive.AnglesNumber = (int)numericUpDownAnglesNumber.Value;
            }
        }

        #region IPrimitivePropertiesControl Members

        public void SetPrimitive(object primitive)
        {
            this.primitive = primitive as IColumnExtendable;
        }

        #endregion
    }
}
