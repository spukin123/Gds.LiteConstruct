using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Gds.Windows
{
    public partial class WhiteContainer : Panel
    {
        [Browsable(true)]
        public string TitleText
        {
            get { return this.gradientWhitePanel.TitleText; }
            set { this.gradientWhitePanel.TitleText = value; }
        }

        [Browsable(true)]
        public int TitleIndent
        {
            get { return gradientWhitePanel.TextIndent; }
            set { gradientWhitePanel.TextIndent = value; }
        }

        [Browsable(true)]
        public event EventHandler Header_Click
        {
            add { this.gradientWhitePanel.Click += value; }
            remove { this.gradientWhitePanel.Click -= value; }
        }

        private int showColorValue = 250;
        private int hideColorValue = 220;
        private int currentColorValue;
        private int colorValueStep = 2;
        
        private bool showed = true;

        public bool Showed
        {
            get { return showed; }
        }

        private bool processing = false;

        [Browsable(false)]
        public bool Processing
        {
            get { return processing; }
            set { processing = value; }
        }

        private Timer showHideTimer = new Timer();
        private int showHideInterval = 30;
        
        public void Show(bool show)
        {
            if (showed == show) return;

            showed = show;
            foreach (Control control in this.Controls)
            {
                if (control != gradientWhitePanel)
                    control.Enabled = showed;
            }
            if (processing == false)
            {
                processing = true;
                currentColorValue = (showed) ? hideColorValue : showColorValue;
                showHideTimer.Start();
            }
        }

        private void showHideTimer_Tick(object sender, EventArgs e)
        {
            currentColorValue += colorValueStep * ((showed) ? 1 : -1);
            this.BackColor = Color.FromArgb(currentColorValue, currentColorValue, currentColorValue);
            if (currentColorValue == ((showed) ? showColorValue : hideColorValue))
            {
                (sender as Timer).Stop();
                processing = false;
            }
        }

        public WhiteContainer()
        {
            InitializeComponent();
            showHideTimer.Interval = showHideInterval;
            showHideTimer.Tick += new EventHandler(showHideTimer_Tick);
        }
    }
}
