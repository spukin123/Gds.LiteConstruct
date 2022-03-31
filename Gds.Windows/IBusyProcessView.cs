using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Gds.Windows
{
    public interface IBusyProcessView
    {
        void Show();
        DialogResult ShowDialog();
        void Close();
        string ProcessMessage { set; }
        Point Location { get; set; }
        Size Size { get; set; }
    }
}
