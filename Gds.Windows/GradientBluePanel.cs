using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gds.Windows
{
    public partial class GradientBluePanel : PictureBox
    {
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = new Size(value.Width, 24); }
        }

        public new int Height
        {
            get { return base.Height; }
            set { base.Height = 24; }
        }

        public new Size PreferredSize
        {
            get { return new Size(300, 24); }
        }

        [Browsable(true)]
        public string TitleText
        {
            get { return labelTitle.Text; }
            set { labelTitle.Text = value; }
        }

        public GradientBluePanel()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }
    }
}
