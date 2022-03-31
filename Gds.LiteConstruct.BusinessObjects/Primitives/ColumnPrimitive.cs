using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.Sides;

namespace Gds.LiteConstruct.BusinessObjects.Primitives
{
    [Serializable]
    public class ColumnPrimitive : PrimitiveBase, IColumnSizeable, IColumnExtendable
    {
        private ColumnData data;

        private int BodyPartVerticesNumber
        {
            get { return data.AnglesNumber * 4; }
        }

        private int StubPartVerticesNumber
        {
            get { return data.AnglesNumber * 3; }
        }

        private int StubPartDimensionsNumber
        {
            get { return StubPartVerticesNumber / 3; }
        }

        private int BodyPartDimensionsNumber
        {
            get { return BodyPartVerticesNumber / 4; }
        }

        private int vertexCnt;
        private Vector3 stubStartVector;
        private Angle deltaAngle;

        public ColumnPrimitive() : base()
        {
        }

        private void CreateVertices()
        {
            vertices = new Vertex[BodyPartVerticesNumber + 2 * StubPartVerticesNumber];
            for (int cnt = 0; cnt < vertices.Length; cnt++)
            {
                vertices[cnt] = new Vertex();
            }
        }

        private void UpdateVertices()
        {
            vertexCnt = 0;

            deltaAngle = new Angle(2f * Angle.Pi / data.AnglesNumber);

            InitDownStub();
            InitBodyPart();
            InitTopStub();
        }

        private void InitDownStub()
        {
            Angle curAngle;
            Matrix mat = Matrix.Identity;

            curAngle = Angle.A0;
            stubStartVector = new Vector3(data.Radius, 0f, -data.Z / 2f);

            for (int cnt = 1; cnt <= data.AnglesNumber; cnt++)
            {
                mat.RotateZ(curAngle.Radians);
                vertices[vertexCnt].Vector = Vector3.TransformCoordinate(stubStartVector, mat);
                vertexCnt++;
                
                vertices[vertexCnt].Vector = new Vector3(0f, 0f, -data.Z / 2f);
                vertexCnt++;

                mat.RotateZ(curAngle.Radians + deltaAngle.Radians);
                vertices[vertexCnt].Vector = Vector3.TransformCoordinate(stubStartVector, mat);
                vertexCnt++;

                curAngle += deltaAngle;
            }
        }

        private void InitBodyPart()
        {
            Vector3 p1, p2;
            Vector3 zVec = Vector3Utils.AlignedZVector;

            for (int cnt = 2; cnt <= (data.AnglesNumber - 1) * 3 + 2; cnt += 3)
            {
                p1 = vertices[cnt].Vector;
                p2 = vertices[cnt - 2].Vector;

                vertices[vertexCnt].Vector = p1 + data.Z * zVec;
                vertexCnt++;

                vertices[vertexCnt].Vector = p2 + data.Z * zVec;
                vertexCnt++;

                vertices[vertexCnt].Vector = p2;
                vertexCnt++;

                vertices[vertexCnt].Vector = p1;
                vertexCnt++;
            }
        }

        private void InitTopStub()
        {
            Angle curAngle;
            Matrix mat = Matrix.Identity;

            curAngle = Angle.A0;
            stubStartVector = new Vector3(data.Radius, 0f, data.Z / 2f);

            for (int cnt = 1; cnt <= data.AnglesNumber; cnt++)
            {
                mat.RotateZ(curAngle.Radians);
                vertices[vertexCnt].Vector = Vector3.TransformCoordinate(stubStartVector, mat);
                vertexCnt++;

                mat.RotateZ(curAngle.Radians + deltaAngle.Radians);
                vertices[vertexCnt].Vector = Vector3.TransformCoordinate(stubStartVector, mat);
                vertexCnt++;

                vertices[vertexCnt].Vector = new Vector3(0f, 0f, data.Z / 2f);
                vertexCnt++;

                curAngle += deltaAngle;
            }
        }

        private void UpdateAfterResizing()
        {
            UpdateVertices();
            UpdateSidesPositions();
            UpdateTextureCoordinates();

            RaiseSizeChangedEvent();
        }

        private void UpdateAfterRebuilding()
        {
            SetVertices();
            SetSideDimensions();

            SavePreviousSides();
            CreateMasterSides();

            ApplyPreviousSidesState();
            ObtainAllSides();
            ClearPreviousSides();

            RaiseSizeChangedEvent();
            RaiseSelectedSideChangedEvent();
        }

        #region Overriden Members

        protected override int AxesNumber
        {
            get { return 0; }
        }

        protected override void SetDefaultSize()
        {
            data = new ColumnData(20f, 2f, 15);
        }

        protected override void SetVertices()
        {
            CreateVertices();
            UpdateVertices();
        }

        protected override void SetSideDimensions()
        {
            int cnt2 = 0;
            masterSideDimensions = new SideDimension[3][];

            masterSideDimensions[0] = new Side3Dimension[StubPartDimensionsNumber];
            for (int cnt = 0; cnt < StubPartDimensionsNumber; cnt++, cnt2 += 3)
            {
                masterSideDimensions[0][cnt] = new Side3Dimension(vertices, cnt2, cnt2 + 1, cnt2 + 2);
            }

            masterSideDimensions[1] = new Side4Dimension[BodyPartDimensionsNumber];
            for (int cnt = 0; cnt < BodyPartDimensionsNumber; cnt++, cnt2 += 4)
            {
                masterSideDimensions[1][cnt] = new Side4Dimension(vertices, cnt2, cnt2 + 1, cnt2 + 2, cnt2 + 3);
            }

            masterSideDimensions[2] = new Side3Dimension[StubPartDimensionsNumber];
            for (int cnt = 0; cnt < StubPartDimensionsNumber; cnt++, cnt2 += 3)
            {
                masterSideDimensions[2][cnt] = new Side3Dimension(vertices, cnt2, cnt2 + 1, cnt2 + 2);
            }
        }

        protected override void CreateMasterSides()
        {
            masterSides = new MasterSide[3];
            masterSides[0] = new ColumnStubeSide(masterSideDimensions[0]);
            masterSides[1] = new ColumnBodySide(masterSideDimensions[1]);
            masterSides[2] = new ColumnStubeSide(masterSideDimensions[2]);
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

        #endregion

        #region IColumnSizeable Members

        public float Z
        {
            get { return data.Z; }
            set 
            {
                data.Z = value;
                UpdateAfterResizing();
            }
        }

        public float Radius
        {
            get { return data.Radius; }
            set
            {
                data.Radius = value;
                UpdateAfterResizing();
            }
        }

		public void Scale(float factor)
		{
			data.Radius *= factor;
			data.Z *= factor;
			UpdateAfterResizing();
		}

        #endregion

        #region IColumnExtendable Members

        public int AnglesNumber
        {
            get { return data.AnglesNumber; }
            set 
            {
                data.AnglesNumber = value;
                UpdateAfterRebuilding();
            }
        }

        #endregion
    }
}
