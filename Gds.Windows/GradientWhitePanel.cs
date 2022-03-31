using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gds.Windows
{
    public partial class GradientWhitePanel : PictureBox
    {
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = new Size(value.Width, 15); }
        }

        public new int Height
        {
            get { return base.Height; }
            set { base.Height = 15; }
        }

        /*public new Size PreferredSize
        {
            get { return new Size(300, 15); }
        }*/

        [Browsable(true)]
        public string TitleText
        {
            get { return labelTitle.Text; }
            set { labelTitle.Text = value; }
        }

        [Browsable(true)]
        public new event EventHandler Click
        {
            add
            {
                base.Click += value;
                this.labelTitle.Click += value;
            }
            remove
            {
                base.Click -= value;
                this.labelTitle.Click += value;
            }
        }

        [Browsable(true)]
        public int TextIndent
        {
            get { return labelTitle.Location.X; }
            set { labelTitle.Location = new Point(value, labelTitle.Location.Y); }
        }

        public GradientWhitePanel()
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
