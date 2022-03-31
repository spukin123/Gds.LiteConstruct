using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects;
using Microsoft.DirectX;
using System.Windows.Forms;

namespace Gds.LiteConstruct.PrimitivesManagement
{
    public class TransformationSmoother
    {
        private IRotatable rotatableEntity;
        private IMovable movableEntity;
        private Vector3 translation;
        private AxisAngle rotation;
        private int transformationTime;

        private const int StepTime = 15;

        private int timeFactorMainPart;
        private float timeFactorFinalPart;

        private int iterationCount = 1;
        private Timer timer;

        private Vector3 moveByVector;
        private Angle rotateByAngle;

        public TransformationSmoother(IRotatable rotatableEntity, IMovable movableEntity, Vector3 translation, AxisAngle rotation, int transformationTime)
        {
            this.rotatableEntity = rotatableEntity;
            this.movableEntity = movableEntity;
            this.translation = translation;
            this.rotation = rotation;
            this.transformationTime = transformationTime;
        }

        private void Timer_TickMainStage(object sender, EventArgs e) 
        {
            RotateObject();
            TranslateObject();

            if (iterationCount == timeFactorMainPart)
            {
                SetFinalStage();
            }
            else
            {
                iterationCount++;
            }
        }

        private void Timer_TickFinalStage(object sender, EventArgs e)
        {
            float newLength;
            newLength = moveByVector.Length() * timeFactorFinalPart;
            moveByVector = Vector3Utils.SetLength(translation, newLength);

            rotateByAngle = new Angle(rotateByAngle.Radians * timeFactorFinalPart);

            RotateObject();
            TranslateObject();

            Cleanup();
        }

        private void SetFinalStage()
        {
            timer.Stop();
            timer.Tick -= Timer_TickMainStage;

            timer = new Timer();
            
            int finalInterval = (int)(StepTime * timeFactorFinalPart);
            if (finalInterval == 0)
            {
                timer.Interval = 1;
            }
            else
            {
                timer.Interval = finalInterval;
            }

            timer.Tick += Timer_TickFinalStage;

            timer.Start();
        }

        private void RotateObject()
        {
            AxisAngle objectRotation;
            objectRotation = rotatableEntity.Rotation.ToQuaternion();

            AxisAngle deltaRotation;
            deltaRotation = new AxisAngle(rotation.Axis, rotateByAngle);

            AxisAngle combination;
            combination = objectRotation * deltaRotation;

            rotatableEntity.Rotation = combination.ToRotationVector();
        }

        private void TranslateObject()
        {
            movableEntity.MoveBy(moveByVector);
        }

        #region Private Utils

        private void Cleanup()
        {
            timer.Stop();
            timer.Tick -= Timer_TickFinalStage;

            RaiseSmoothFinished();
        }

        private void RaiseSmoothFinished()
        {
            if (SmoothFinished != null)
            {
                SmoothFinished();
            }
        }

        private void InitSmoothParams()
        {
            float k;
            k = (float)StepTime / transformationTime;

            float deltaMove, deltaRotate;
            deltaMove = k * translation.Length();
            deltaRotate = k * rotation.RotationAngle.Radians;

            moveByVector = Vector3Utils.SetLength(translation, deltaMove);
            rotateByAngle = new Angle(deltaRotate);

            float timeFactor;
            timeFactor = (float)transformationTime / StepTime;

            timeFactorMainPart = (int)timeFactor;
            timeFactorFinalPart = timeFactor - (float)timeFactorMainPart;
        }

        private void InitTimer()
        {
            timer = new Timer();
            timer.Interval = StepTime;
            timer.Tick += Timer_TickMainStage;
        }

        private void Start()
        {
            timer.Start();
        }

        #endregion

        public void Smooth()
        {
            InitSmoothParams();
            InitTimer();
            Start();
        }

        public event NotifyHandler SmoothFinished;
    }
}
