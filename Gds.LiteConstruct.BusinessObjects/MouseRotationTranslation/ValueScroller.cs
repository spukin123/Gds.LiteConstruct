using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using System.Windows.Forms;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation
{
    public abstract class ValueScroller : IActionable
    {
        protected float offset;
        public float Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        protected float step;
        public float Step
        {
            get { return step; }
            set { step = value; }
        }

        protected int sleep;
        public int Sleep
        {
            get { return sleep; }
            set { sleep = value; }
        }

        protected Timer timer;
        protected float directionSign;
        protected float finalValue;
        protected float passedOffset = 0f;
        protected bool startedByRewind = false;
        private bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
        }

        public event EventHandler RewindFinished;
        public event EventHandler PrimaryFinished;

        public ValueScroller()
        {

        }

        public ValueScroller(float offset, float step, int sleep)
        {
            Offset = offset;
            Step = step;
            Sleep = sleep;
        }

        protected void InitDirectionSign()
        {
            directionSign = offset / Math.Abs(offset);
        }

        protected void InitTimer()
        {
            timer = new Timer();
            timer.Interval = Sleep;
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void ProcessStop()
        {
            isBusy = false;
            if (startedByRewind)
            {
                if (RewindFinished != null)
                {
                    RewindFinished(this, null);
                }
                startedByRewind = false;
            }
            else
            {
                if (PrimaryFinished != null)
                {
                    PrimaryFinished(this, null);
                }
            }
        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            MakeIteration();
            if (CheckStop() == false)
            {
                SendToClient();
            }
            else
            {
                ProcessStop();   
            }
        }

        public abstract void Rewind();

        protected abstract void MakeIteration();
        protected abstract bool CheckStop();
        protected abstract void SendToClient();
        protected abstract void InitStopValue();
        protected abstract void Apply();

        #region IActionable Members

        public void StartAction()
        {
            timer.Start();
            isBusy = true;
        }

        public virtual void StopAction()
        {
            timer.Stop();
            isBusy = false;
        }

        #endregion
    }
}
