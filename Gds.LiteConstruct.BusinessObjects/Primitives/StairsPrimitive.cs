using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.Sides;

namespace Gds.LiteConstruct.BusinessObjects.Primitives
{
    [Serializable]
    public class StairsPrimitive : PrimitiveBase, IStairsSizable, IStairsExtendable
    {
        private StairsData data;

        [NonSerialized]
        private int vertexCnt;

        private int SidePartVertNum
        {
            get { return data.GetStairsNumber() * 4; }
        }

        private int CentralPartVertNum
        {
            get { return data.GetStairsNumber() * 8; }
        }

        private int SidePartDimNum
        {
            get { return SidePartVertNum / 4; }
        }

        private int CentralPartDimNum
        {
            get { return CentralPartVertNum / 4; }
        }

        private Angle LeftTopAngleLocal
        {
            get
            {
                return Vector3Utils.AngleBetweenVectors(leftSideLengthVec, yVec);
            }
        }

        private Angle RightTopAngleLocal
        {
            get
            {
                return Vector3Utils.AngleBetweenVectors(rightSideLengthVec, yVec);
            }
        }

        [NonSerialized]
        private Vector3 leftSideLengthVec, rightSideLengthVec;
        [NonSerialized]
        private Vector3 leftStartPoint, rightStartPoint;
        [NonSerialized]
        private Vector3 firstStairHeightVec, stairHeightVec;
        [NonSerialized]
        private Vector3 firstLeftSideLengthVec, firstRightSideLengthVec;
        [NonSerialized]
        private Line2D leftBorder, rightBorder;
        [NonSerialized]
        private Vector3 stairsWidthVec;
        [NonSerialized]
        private float firstStairLength;
        [NonSerialized]
        private Vector3 yVec;

        private bool simpleMode = true;
        private bool stairHeightMode = false;
        private bool stairLengthMode = false;

        public StairsPrimitive()
            : base()
        {
        }

        private void CreateVertices()
        {
            vertices = new Vertex[CentralPartVertNum + 2 * SidePartVertNum + 8];
            for (int cnt1 = 0; cnt1 < vertices.Length; cnt1++)
            {
                vertices[cnt1] = new Vertex();
            }
        }

        private void UpdateVectices()
        {
            Size3 size = data.GetStairsSize();

            leftBorder = data.GetLeftBorder();
            rightBorder = data.GetRightBorder();
            leftStartPoint = PointConverter.PointXYToVector3(leftBorder.Point2, -size.Z / 2f);
            rightStartPoint = PointConverter.PointXYToVector3(rightBorder.Point2, -size.Z / 2f);

            stairsWidthVec = new Vector3(size.X, 0f, 0f);
            stairHeightVec = Vector3Utils.AlignedZVector * data.GetStairHeight();

            leftSideLengthVec = PointConverter.PointXYToVector3(leftBorder.Point2 - leftBorder.Point1, 0f);
            rightSideLengthVec = PointConverter.PointXYToVector3(rightBorder.Point2 - rightBorder.Point1, 0f);
            yVec = Vector3Utils.AlignedYVector;

            FindFirstStairParams();
            FindFollowStairsParams();

            vertexCnt = 0;
            InitLeftPart();
            InitTopPart();
            InitRightPart();
            InitFloorPart();
            InitRarePart();
        }

        private void InitLeftPart()
        {
            int prevSideVertexIndex;

            vertices[vertexCnt].Vector = leftStartPoint - firstLeftSideLengthVec + firstStairHeightVec;
            prevSideVertexIndex = vertexCnt;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector + firstLeftSideLengthVec;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - firstStairHeightVec;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - firstLeftSideLengthVec;
            vertexCnt++;

            for (int cnt1 = 2; cnt1 <= data.GetStairsNumber(); cnt1++)
            {
                vertices[vertexCnt].Vector = vertices[prevSideVertexIndex].Vector - leftSideLengthVec + stairHeightVec;
                vertexCnt++;
                vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector + leftSideLengthVec;
                vertexCnt++;
                vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - (cnt1 - 1) * stairHeightVec - firstStairHeightVec;
                vertexCnt++;
                vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - leftSideLengthVec;
                vertexCnt++;

                prevSideVertexIndex += 4;
            }
        }

        private void InitTopPart()
        {
            int prevSideVertexIndex;
            prevSideVertexIndex = vertexCnt;

            float stairLengthSum = 0f;
            stairsWidthVec = new Vector3(1f, 0f, 0f);

            FindStairsWidthVec(stairLengthSum);

            vertices[vertexCnt].Vector = leftStartPoint;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector + firstStairHeightVec;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector + stairsWidthVec;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - firstStairHeightVec;
            vertexCnt++;

            stairLengthSum += firstStairLength;
            FindStairsWidthVec(stairLengthSum);

            vertices[vertexCnt].Vector = leftStartPoint + firstStairHeightVec;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - firstLeftSideLengthVec;
            prevSideVertexIndex = vertexCnt;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector + stairsWidthVec;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector + firstRightSideLengthVec;
            vertexCnt++;

            for (int cnt1 = 2; cnt1 <= data.GetStairsNumber(); cnt1++)
            {
                vertices[vertexCnt].Vector = vertices[prevSideVertexIndex].Vector;
                vertexCnt++;
                vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector + stairHeightVec;
                vertexCnt++;
                vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector + stairsWidthVec;
                vertexCnt++;
                vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - stairHeightVec;
                vertexCnt++;

                stairLengthSum += data.GetStairLength();
                FindStairsWidthVec(stairLengthSum);

                vertices[vertexCnt].Vector = vertices[prevSideVertexIndex].Vector + stairHeightVec;
                vertexCnt++;
                vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - leftSideLengthVec;
                vertexCnt++;
                vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector + stairsWidthVec;
                vertexCnt++;
                vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector + rightSideLengthVec;
                vertexCnt++;

                prevSideVertexIndex += 8;
            }
        }

        private void InitRightPart()
        {
            int prevSideVertexIndex;

            vertices[vertexCnt].Vector = rightStartPoint + firstStairHeightVec;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - firstRightSideLengthVec;
            prevSideVertexIndex = vertexCnt;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - firstStairHeightVec;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector + firstRightSideLengthVec;
            vertexCnt++;

            for (int cnt1 = 2; cnt1 <= data.GetStairsNumber(); cnt1++)
            {
                vertices[vertexCnt].Vector = vertices[prevSideVertexIndex].Vector + stairHeightVec;
                vertexCnt++;
                vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - rightSideLengthVec;
                vertexCnt++;
                vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - (cnt1 - 1) * stairHeightVec - firstStairHeightVec;
                vertexCnt++;
                vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector + rightSideLengthVec;
                vertexCnt++;

                prevSideVertexIndex += 4;
            }
        }

        private void InitFloorPart()
        {
            Vector3 topVec, bottomVec;
            Vector3 rightVec;

            topVec = PointConverter.PointXYToVector3(rightBorder.Point1 - leftBorder.Point1, 0f);
            bottomVec = PointConverter.PointXYToVector3(rightBorder.Point2 - leftBorder.Point2, 0f);
            rightVec = Vector3Utils.SetLength(rightSideLengthVec, RightTriangle.FromAdjacentAndAlpha(data.GetStairsSize().Y, RightTopAngleLocal).Hypotenuse);

            vertices[vertexCnt].Vector = leftStartPoint;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector + bottomVec;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - rightVec;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - topVec;
            vertexCnt++;
        }

        private void InitRarePart()
        {
            Vector3 rightVec;
            Vector3 topVec;
            Vector3 lenVec;

            rightVec = Vector3Utils.AlignedZVector * data.GetStairsSize().Z;
            topVec = Vector3Utils.AlignedXVector * data.GetStairsSize().X;
            lenVec = Vector3Utils.SetLength(rightSideLengthVec, RightTriangle.FromAdjacentAndAlpha(data.GetStairsSize().Y, RightTopAngleLocal).Hypotenuse);

            vertices[vertexCnt].Vector = rightStartPoint - lenVec + rightVec;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - topVec;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector - rightVec;
            vertexCnt++;
            vertices[vertexCnt].Vector = vertices[vertexCnt - 1].Vector + topVec;
            vertexCnt++;
        }

        private void ProcessAfterResizing()
        {
            UpdateVectices();
            UpdateSidesPositions();
            UpdateTextureCoordinates();

            RaiseSizeChangedEvent();
        }

        private void ProcessAfterAdditionalResizing()
        {
            SetVertices();
            SetSideDimensions();

            SavePreviousSides();
            CreateSimpleSides();
            CreateMasterSides();

            ApplyPreviousSidesState();
            ObtainAllSides();
            ClearPreviousSides();

            RaiseSizeChangedEvent();
            RaiseSelectedSideChangedEvent();
        }

        private void FindFirstStairParams()
        {
            float h;
            Size3 size = data.GetStairsSize();
            
            h = data.GetStairHeight() - (data.GetStairsNumber() * data.GetStairHeight() - size.Z);
            firstStairLength = data.GetStairLength() - (data.GetStairsNumber() * data.GetStairLength() - size.Y);

            firstStairHeightVec = Vector3Utils.AlignedZVector * h;

            firstLeftSideLengthVec = Vector3Utils.SetLength(leftSideLengthVec, RightTriangle.FromAdjacentAndAlpha(firstStairLength, LeftTopAngleLocal).Hypotenuse);
            firstRightSideLengthVec = Vector3Utils.SetLength(rightSideLengthVec, RightTriangle.FromAdjacentAndAlpha(firstStairLength, RightTopAngleLocal).Hypotenuse);
        }

        private float FindXStep(float yValue)
        {
            Line2D xLine;
            xLine = Line2D.BuildXAlignedLine(yValue);

            Vector2 leftPoint, rightPoint;
            leftPoint = leftBorder.CrossWith(xLine);
            rightPoint = rightBorder.CrossWith(xLine);

            return Vector2.Length(rightPoint - leftPoint);
        }

        private void FindStairsWidthVec(float stairLengthSum)
        {
            stairsWidthVec = Vector3Utils.SetLength(stairsWidthVec, FindXStep(data.GetStairsSize().Y / 2f - stairLengthSum));
        }

        private void FindFollowStairsParams()
        {
            leftSideLengthVec = Vector3Utils.SetLength(leftSideLengthVec, RightTriangle.FromAdjacentAndAlpha(data.GetStairLength(), LeftTopAngleLocal).Hypotenuse);
            rightSideLengthVec = Vector3Utils.SetLength(rightSideLengthVec, RightTriangle.FromAdjacentAndAlpha(data.GetStairLength(), RightTopAngleLocal).Hypotenuse);
        }

        #region OverridenMembers

        protected override int AxesNumber
        {
            get { return 0; }
        }

        protected override void SetDefaultSize()
        {
            data = new SimpleModeData(new Size3(10f, 15f, 10f), 5, Angle.A90, Angle.A90);
            data.SimpleModeSizeChanged += new NotifyHandler(ProcessAfterResizing);
        }

        protected override void SetVertices()
        {
            CreateVertices();
            UpdateVectices();
        }

        protected override void SetSideDimensions()
        {
            masterSideDimensions = new SideDimension[3][];
            int cnt1, cnt2;
            int stop;

            masterSideDimensions[0] = new Side4Dimension[SidePartDimNum];
            stop = SidePartDimNum;
            for (cnt1 = 0, cnt2 = 0; cnt1 < stop; cnt1++, cnt2 += 4)
            {
                masterSideDimensions[0][cnt1] = new Side4Dimension(vertices, cnt2, cnt2 + 1, cnt2 + 2, cnt2 + 3);
            }

            masterSideDimensions[1] = new Side4Dimension[CentralPartDimNum];
            stop = CentralPartDimNum;
            for (cnt1 = 0; cnt1 < stop; cnt1++, cnt2 += 4)
            {
                masterSideDimensions[1][cnt1] = new Side4Dimension(vertices, cnt2, cnt2 + 1, cnt2 + 2, cnt2 + 3);
            }

            masterSideDimensions[2] = new Side4Dimension[SidePartDimNum];
            stop = SidePartDimNum;
            for (cnt1 = 0; cnt1 < stop; cnt1++, cnt2 += 4)
            {
                masterSideDimensions[2][cnt1] = new Side4Dimension(vertices, cnt2, cnt2 + 1, cnt2 + 2, cnt2 + 3);
            }

            simpleSideDimensions = new SideDimension[2];

            simpleSideDimensions[0] = new Side4Dimension(vertices, cnt2, cnt2 + 1, cnt2 + 2, cnt2 + 3);
            cnt2 += 4;
            simpleSideDimensions[1] = new Side4Dimension(vertices, cnt2, cnt2 + 1, cnt2 + 2, cnt2 + 3);
        }

        protected override void CreateMasterSides()
        {
            masterSides = new MasterSide[3];
            masterSides[0] = new StairsLeftSide(masterSideDimensions[0]);
            masterSides[1] = new StairsTopSide(masterSideDimensions[1]);
            masterSides[2] = new StairsRightSide(masterSideDimensions[2]);
        }

        public override void Accept(ISizableVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Accept(IAdditionalVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override bool CanRotateX
        {
            get { return false; }
        }

        public override bool CanRotateY
        {
            get { return false; }
        }

        public override bool CanRotateZ
        {
            get { return true; }
        }

        public override void Initialize(ITextureProvider textureProvider)
        {
            base.Initialize(textureProvider);
            data.SimpleModeSizeChanged = new NotifyHandler(ProcessAfterResizing);
            data.DetailedModeSizeChanged = new NotifyHandler(ProcessAfterAdditionalResizing);
        }

        #endregion

        #region IStairsExtendable Members

        public int StairsNumber
        {
            get { return data.GetStairsNumber(); }
            set
            {
                data.StairsNumber = value;
                //ProcessAfterAdditionalResizing();
            }
        }

        public float StairLength
        {
            get { return data.GetStairLength(); }
            set
            {
                data.StairLength = value;
                //ProcessAfterAdditionalResizing();
            }
        }

        public float StairHeight
        {
            get { return data.GetStairHeight(); }
            set
            {
                data.StairHeight = value;
                //ProcessAfterAdditionalResizing();
            }
        }

        public bool SimpleMode
        {
            get { return simpleMode; }
        }

        public bool StairHeightMode
        {
            get { return stairHeightMode; }
        }

        public bool StairLengthMode
        {
            get { return stairLengthMode; }
        }
        
        public void EnableSimpleMode()
        {
            data.ClearHandlers();
            data = new SimpleModeData(data.GetStairsSize(), data.GetStairsNumber(), data.LeftTopAngle, data.RightTopAngle);
            data.SimpleModeSizeChanged = new NotifyHandler(ProcessAfterResizing);
            data.DetailedModeSizeChanged = new NotifyHandler(ProcessAfterAdditionalResizing);

            simpleMode = true;
            stairHeightMode = false;
            stairLengthMode = false;
        }

        public void EnableDetailedHeightMode()
        {
            data.ClearHandlers();
            data = new DetailedHeightModeData(data.GetStairsSize(), data.GetStairHeight(), data.LeftTopAngle, data.RightTopAngle);
            data.SimpleModeSizeChanged = new NotifyHandler(ProcessAfterResizing);
            data.DetailedModeSizeChanged = new NotifyHandler(ProcessAfterAdditionalResizing);
            
            simpleMode = false;
            stairHeightMode = true;
            stairLengthMode = false;
        }

        public void EnableDetailedLengthMode()
        {
            data.ClearHandlers();
            data = new DetailedLengthModeData(data.GetStairsSize(), data.GetStairLength(), data.LeftTopAngle, data.RightTopAngle);
            data.SimpleModeSizeChanged = new NotifyHandler(ProcessAfterResizing);
            data.DetailedModeSizeChanged = new NotifyHandler(ProcessAfterAdditionalResizing);
            
            simpleMode = false;
            stairHeightMode = false;
            stairLengthMode = true;
        }

        public Angle LeftTopAngle
        {
            get { return data.LeftTopAngle; }
            set { data.LeftTopAngle = value; }
        }

        public Angle RightTopAngle
        {
            get { return data.RightTopAngle; }
            set { data.RightTopAngle = value; }
        }

        public float BottomBorderLength
        {
            get { return data.BottomBorderLength; }
        }

        #endregion

        #region IStairsConstraint Members

        public float MinStairHeight
        {
            get { return 1f; }
        }

        public float MaxStairHeight
        {
            get { return 100f; }
        }

        public float MinStairLength
        {
            get { return 1f; }
        }

        public float MaxStairLength
        {
            get { return 100f; }
        }

        public int MinStairsNumber
        {
            get { return 2; }
        }

        public int MaxStairsNumber
        {
            get { return 30; }
        }

        public float MinStairsX
        {
            get { return 1f; }
        }

        #endregion

        #region IStairsSizable Members

        public float X
        {
            get { return data.GetStairsSize().X; }
            set { data.X = value; }
        }

        public float Y
        {
            get { return data.GetStairsSize().Y; }
            set { data.Y = value; }
        }

        public float Z
        {
            get { return data.GetStairsSize().Z; }
            set { data.Z = value; }
        }

        public void Scale(float factor)
        {
            Size3 size = data.GetStairsSize();
            data.X = size.X * factor;
            data.Y = size.Y * factor;
            data.Z = size.Z * factor;
        }

        #endregion

        #region IStairsApplyable Members
        
        public bool ChangesApplied
        {
            get { return data.ChangesApplied; }
            set { data.ChangesApplied = value; }
        }

        #endregion
    }
}
