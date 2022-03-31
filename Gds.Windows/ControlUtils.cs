using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Gds.Windows
{
    static public class ControlUtils
    {
        static public void ShowUC(Panel panel, UserControl control)
        {
            panel.SuspendLayout();
            panel.Controls.Clear();
            panel.Controls.Add(control);
            control.Dock = DockStyle.Fill;
            panel.ResumeLayout();
            control.Show();
        }

        static public void SwitchControls(Panel panel, UserControl oldControl, UserControl newControl)
        {
            panel.SuspendLayout();
            int index = panel.Controls.IndexOf(oldControl);
            Point location = panel.Controls[index].Location;
            panel.Controls.Remove(oldControl);
            newControl.Location = location;
            panel.Controls.Add(newControl);
            panel.ResumeLayout();
            newControl.Show();
        }

        static public void ClearPanel(Panel panel)
        {
            //panel.SuspendLayout();
            panel.Controls.Clear();
            //panel.ResumeLayout();
        }
    }
}
