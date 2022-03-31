using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gds.Windows
{
    public partial class SimpleBusyProcessForm : Form, IBusyProcessView
    {
        public SimpleBusyProcessForm()
        {
            InitializeComponent();
        }

        #region IBusyProcessView Members

        public string ProcessMessage
        {
            set { labelMessage.Text = value; }
        }

        #endregion
    }
}