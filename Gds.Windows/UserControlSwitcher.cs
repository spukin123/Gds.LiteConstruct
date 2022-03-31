using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Gds.Windows
{
    public class UserControlSwitcher
    {
        private Panel panel;
        private UserControl oldControl;
        private UserControl newControl;
        private int shiftX;
        private int shiftY;
        private int timePeriod;
        private int positionX;
        private int positionY;
        private Timer hideTimer;
        private Timer showTimer;
        private HideCondition hideCondition;

        private EventHandler processingFinished;

        public EventHandler ProcessingFinished
        {
            get { return processingFinished; }
            set { processingFinished = value; }
        }

        private bool working = false;

        public bool Processing
        {
            get { return working; }
        }


        public UserControlSwitcher(Panel panel, int shiftX, int shiftY, int timePeriod)
        {
            this.panel = panel;
            this.shiftX = shiftX;
            this.shiftY = shiftY;
            this.timePeriod = timePeriod;

            hideTimer = new Timer();
            hideTimer.Interval = timePeriod;
            hideTimer.Tick += new EventHandler(UCHideTick);

            showTimer = new Timer();
            showTimer.Interval = timePeriod;
            showTimer.Tick += new EventHandler(UCShowTick);

            SetHideCondition();
        }


        private void SetHideCondition()
        {
            if (shiftX > 0)
            {
                hideCondition += new HideCondition(MoveRigth);
            }
            else if (shiftX < 0)
            {
                hideCondition += new HideCondition(MoveLeft);
            }
            else if (shiftY > 0)
            {
                hideCondition += new HideCondition(MoveBottom);
            }
            else if (shiftY < 0)
            {
                hideCondition += new HideCondition(MoveTop);
            }
        }

        private bool MoveRigth()
        {
            return (oldControl.Location.X >= panel.Size.Width) ? true : false;
        }

        private bool MoveLeft()
        {
            return (oldControl.Location.X + oldControl.Size.Width <= 0) ? true : false;
        }

        private bool MoveBottom()
        {
            return (oldControl.Location.Y >= panel.Size.Height) ? true : false;
        }

        private bool MoveTop()
        {
            return (oldControl.Location.Y + oldControl.Size.Height <= 0) ? true : false;
        }

        public void Switch(UserControl oldControl, UserControl newControl)
        {
            working = true;
            
            this.oldControl = oldControl;
            this.newControl = newControl;
            this.positionX = oldControl.Location.X;
            this.positionY = oldControl.Location.Y;

            newControl.Height = panel.Height;
            newControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;

            hideTimer.Start();
        }

        private void UCHideTick(object sender, EventArgs e)
        {
            oldControl.Location = new Point(oldControl.Location.X + shiftX, oldControl.Location.Y + shiftY);
            if (hideCondition())
            {
                hideTimer.Stop();
                SwitchControl();
            }
        }

        private void SwitchControl()
        {
            ControlUtils.SwitchControls(panel, oldControl, newControl);
            showTimer.Start();
        }

        private void UCShowTick(object sender, EventArgs e)
        {
            newControl.Location = new Point(newControl.Location.X - shiftX, newControl.Location.Y - shiftY);
            if (newControl.Location.X == positionX && newControl.Location.Y == positionY)
            {
                showTimer.Stop();
                working = false;
                processingFinished(this, new UserControlSwitcherEventArgs(newControl));
            }
        }

        private delegate bool HideCondition();
    }

    public class UserControlSwitcherEventArgs : EventArgs
    {
        private UserControl control;

        public UserControl Control
        {
            get { return control; }
        }

        public UserControlSwitcherEventArgs(UserControl control)
        {
            this.control = control;
        }
    }
}
