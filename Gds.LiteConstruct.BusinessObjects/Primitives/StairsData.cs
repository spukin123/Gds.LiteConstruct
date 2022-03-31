using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects.Primitives
{
    [Serializable]
    public abstract class StairsData
    {
        protected Size3 size;
        protected float stairHeight;
        protected float stairLength;
        protected int stairsNum;

        private Angle leftTopAngle;
        public Angle LeftTopAngle
        {
            get { return leftTopAngle; }
            set
            {
                leftTopAngle = value;
                FindBordersLength();

                CallSimpleModeSizeChanged();
            }
        }

        private Angle rightTopAngle;
        public Angle RightTopAngle
        {
            get { return rightTopAngle; }
            set
            {
                rightTopAngle = value;
                FindBordersLength();

                CallSimpleModeSizeChanged();
            }
        }

        [NonSerialized]
        private float topBorderLength;
        public float TopBorderLength
        {
            get { return topBorderLength; }
        }

        [NonSerialized]
        private float bottomBorderLength;
        public float BottomBorderLength
        {
            get { return bottomBorderLength; }
        }

        public abstract float X { set; }
        public abstract float Y { set; }
        public abstract float Z { set; }

        public abstract float StairHeight { set; }
        public abstract float StairLength { set; }
        public abstract int StairsNumber { set; }

        [NonSerialized]
        public NotifyHandler SimpleModeSizeChanged;
        [NonSerialized]
        public NotifyHandler DetailedModeSizeChanged;

        protected bool changesApplied = true;
        public bool ChangesApplied
        {
            get { return changesApplied; }
            set { changesApplied = value; }
        }

        public StairsData(Angle leftTopAngle, Angle rightTopAngle)
        {
            this.leftTopAngle = leftTopAngle;
            this.rightTopAngle = rightTopAngle;
        }

        protected void FindBordersLength()
        {
            topBorderLength = size.X;

            Line2D rightLine, leftLine;
            leftLine = GetLeftBorder();
            rightLine = GetRightBorder();
            bottomBorderLength = Vector2.Length(rightLine.Point2 - leftLine.Point2);
        }

        public Line2D GetLeftBorder()
        {
            Vector3 rightVec, yVec, leftVec;
            rightVec = new Vector3(size.X / 2f, 0f, 0f);
            leftVec = new Vector3(-size.X / 2f, 0f, 0f);
            yVec = new Vector3(0f, -size.Y / 2f, 0f);

            Matrix rightRotation;
            rightRotation = Matrix.RotationZ(leftTopAngle.Radians);

            Vector3 leftBorder;
            leftBorder = Vector3.TransformCoordinate(rightVec, rightRotation);

            Vector3 leftTopPoint;
            leftTopPoint = yVec + leftVec;

            Line2D bottomLine, leftLine;
            Vector2 cross;
            bottomLine = Line2D.BuildXAlignedLine(size.Y / 2f);
            leftLine = new Line2D(PointConverter.Vector3ToVectorXY(leftTopPoint), PointConverter.Vector3ToVectorXY(leftTopPoint + leftBorder));
            cross = leftLine.CrossWith(bottomLine);
            leftLine.Point2 = cross;

            return leftLine;
        }

        public Line2D GetRightBorder()
        {
            Vector3 leftVec, rightVec, yVec;
            leftVec = new Vector3(-size.X / 2f, 0f, 0f);
            rightVec = new Vector3(size.X / 2f, 0f, 0f);
            yVec = new Vector3(0f, -size.Y / 2f, 0f);

            Matrix leftRotation;
            leftRotation = Matrix.RotationZ(-rightTopAngle.Radians);

            Vector3 rightBorder;
            rightBorder = Vector3.TransformCoordinate(leftVec, leftRotation);

            Vector3 rightTopPoint;
            rightTopPoint = yVec + rightVec;

            Line2D bottomLine, rightLine;
            Vector2 cross;
            bottomLine = Line2D.BuildXAlignedLine(size.Y / 2f);
            rightLine = new Line2D(PointConverter.Vector3ToVectorXY(rightTopPoint), PointConverter.Vector3ToVectorXY(rightTopPoint + rightBorder));
            cross = rightLine.CrossWith(bottomLine);
            rightLine.Point2 = cross;

            return rightLine;
        }

        protected void CallSimpleModeSizeChanged()
        {
            if (SimpleModeSizeChanged != null && changesApplied)
            {
                SimpleModeSizeChanged();
            }
        }

        protected void CallDetailedModeSizeChanged()
        {
            if (DetailedModeSizeChanged != null && changesApplied)
            {
                DetailedModeSizeChanged();
            }
        }

        public int GetStairsNumber()
        {
            return stairsNum;
        }

        public float GetStairHeight()
        {
            return stairHeight;
        }

        public float GetStairLength()
        {
            return stairLength;
        }

        public Size3 GetStairsSize()
        {
            return size;
        }

        public void ClearHandlers()
        {
            SimpleModeSizeChanged = null;
            DetailedModeSizeChanged = null;
        }

        protected abstract void UpdateAfterResizing();
    }
}
