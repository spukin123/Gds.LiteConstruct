using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace Gds.Windows
{
    public class BusyProcessManager
    {
        private IBusyProcessView view;

        public IBusyProcessView View
        {
            set { view = value; }
        }

        private Form ownerWindow;

        public Form OwnerWindow
        {
            set { ownerWindow = value; }
        }

        private bool isShown = false;
        private Thread thread;

        public static BusyProcessManager Instance = new BusyProcessManager();

        private BusyProcessManager()
        {
        }

        public void Show(string message)
        {
            if (view == null) throw new ApplicationException("View is not initialized");
            if (isShown) throw new ApplicationException("Busy process window is currently shown");
            isShown = true;

            ownerWindow.Refresh();
            view.ProcessMessage = string.Format("{0}...", message);

            Point point = new Point();
            point.X = (ownerWindow.Size.Width - view.Size.Width) / 2;
            point.Y = (ownerWindow.Size.Height - view.Size.Height) / 2;
            view.Location = ownerWindow.Location + new Size(point);
            thread = new Thread(Show);
			thread.IsBackground = true;
            thread.Start();
        }

        private void Show()
        {
            view.ShowDialog();
        }

        public void ShowLoading()
        {
            Show("Loading");
        }

        public void ShowSaving()
        {
            Show("Saving");
        }

        public void Hide()
        {
            if (view == null) throw new ApplicationException("View is not initialized");
            if (!isShown) throw new ApplicationException("Busy process window it is not shown");
            thread.Abort();
            try
            {
                view.Close();
            }
            catch { }
            isShown = false;
        }

        public void SetProcessMessage(string message)
        {
            if (view == null) throw new ApplicationException("View is not initialized");
            if (!isShown) throw new ApplicationException("Busy process window it is not shown");
            try
            {
                view.ProcessMessage = message;
            }
            catch { }
        }
    }
}
