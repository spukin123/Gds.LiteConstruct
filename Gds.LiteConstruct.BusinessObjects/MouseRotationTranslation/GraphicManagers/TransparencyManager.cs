using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.GraphicManagers
{
    public class TransparencyManager : Illuminator, IConnectableToTranspareable
    {
        protected ITranspareable[] transpareableObjects;
        
        protected int startTransparency;
        public int StartTransparency
        {
            get { return startTransparency; }
            set { startTransparency = value; }
        }

        protected int curTransparecy;

        public TransparencyManager()
        {

        }

        public TransparencyManager(int startTransparency, int offset, float step, int sleep) : base(offset, step, sleep)
        {
            StartTransparency = startTransparency;

            Apply();
        }

        protected void InitCurTransparecy()
        {
            curTransparecy = startTransparency;
        }

        protected override void InitStopValue()
        {
            finalValue = startTransparency + offset;

            ValidateStopValue();
        }

        protected override void Apply()
        {
            InitCurTransparecy();
            InitDirectionSign();
            InitStopValue();

            InitTimer();
        }

        protected override void MakeIteration()
        {
            curTransparecy += (int)(step * directionSign);
        }

        protected override bool CheckStop()
        {
            if (directionSign > 0)
            {
                if (curTransparecy > finalValue)
                {
                    StopAction();

                    return true;
                }
            }
            else
            {
                if (curTransparecy < finalValue)
                {
                    StopAction();

                    return true;
                }
            }

            return false;
        }

        protected override void SendToClient()
        {
            foreach (ITranspareable transpareableObject in transpareableObjects)
            {
                transpareableObject.SetTransparency(curTransparecy); 
            }
        }

        public override void Rewind()
        {
            startTransparency = (int)finalValue;
            Offset *= -1f;

            Apply();

            startedByRewind = true;
            StartAction();
        }

        #region IConnectableToTranspareable Members

        public void ConnectTo(ITranspareable[] transpareableObjects)
        {
            this.transpareableObjects = transpareableObjects;
        }

        #endregion

        #region IAction Members

        public override void StopAction()
        {
            base.StopAction();
            curTransparecy = startTransparency;
        }

        #endregion
    }
}
