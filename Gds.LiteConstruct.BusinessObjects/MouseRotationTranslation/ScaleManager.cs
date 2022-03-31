using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation
{
    public class ScaleManager : ValueScroller, IConnectableToScalable
    {
        protected IScalable[] scalableObjects;

        protected float startScaleFactor;
        public float StartScaleFactor
        {
            get { return startScaleFactor; }
            set { startScaleFactor = value; }
        }

        protected float curScaleFactor;

        public ScaleManager(float startScaleFactor, float offset, float step, int sleep) : base(offset, step, sleep)
        {
            StartScaleFactor = startScaleFactor;

            Apply();
        }

        public override void Rewind()
        {
            if (passedOffset != 0f)
            {
                StartScaleFactor = finalValue;
                Offset = -passedOffset;

                Apply();

                StartAction();
            }
        }

        protected void InitCurScaleFactor()
        {
            curScaleFactor = StartScaleFactor;
        }

        protected override void InitStopValue()
        {
            finalValue = startScaleFactor + offset;
        }

        protected override void MakeIteration()
        {
            float delta = step * directionSign;
            curScaleFactor += delta;
            passedOffset += delta;
        }

        protected override bool CheckStop()
        {
            if (directionSign > 0f)
            {
                if (curScaleFactor > finalValue)
                {
                    StopAction();

                    return true;
                }
            }
            else
            {
                if (curScaleFactor < finalValue)
                {
                    StopAction();

                    return true;
                }
            }

            return false;
        }

        protected override void SendToClient()
        {
            foreach (IScalable scalableObject in scalableObjects)
            {
                scalableObject.SetScale(curScaleFactor); 
            }
        }

        #region IActionable Members
        
        public override void StopAction()
        {
            base.StopAction();
            finalValue = curScaleFactor;
        }
        
        #endregion

        #region IApplyable Members
        
        protected override void Apply()
        {
            InitCurScaleFactor();
            InitDirectionSign();
            InitStopValue();

            InitTimer();
        }

        #endregion

        #region IConnectableToScalable Members

        public void ConnectTo(IScalable[] scalableObjects)
        {
            this.scalableObjects = scalableObjects;
        }

        #endregion
    }
}
