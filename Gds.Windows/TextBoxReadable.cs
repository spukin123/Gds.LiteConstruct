using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gds.Windows
{
    public partial class TextBoxReadable : TextBox
    {
        public TextBoxReadable()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        public new bool ReadOnly
        {
            get { return base.ReadOnly; }
            set { }
        }

        [Browsable(false)]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set { }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }
    }
}
