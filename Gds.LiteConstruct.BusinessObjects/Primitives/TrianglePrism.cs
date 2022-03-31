using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.Sides;
using Gds.LiteConstruct.BusinessObjects.Axises;

namespace Gds.LiteConstruct.BusinessObjects.Primitives
{
    [Serializable]
    public abstract class TrianglePrism : PrimitiveBase, ITrianglePrismSizable
    {
        protected Size4 size;
        protected Vector3 vectorA;
        protected Vector3 vectorB;
        protected Vector3 vectorC;

        protected abstract Vector3 Perpendicular { get; }
        protected abstract Vector3 Direction { get; }

        protected float SizeA
        {
            get { return size.A; }
            set
            {
                size.A = value;
                vectorA = Vector3Utils.SetLength(vectorA, value);
            }
        }

        protected float SizeB
        {
            get { return size.B; }
            set
            {
                size.B = value;
                vectorB = Vector3Utils.SetLength(vectorB, value);
            }
        }

        protected float SizeC
        {
            get { return size.C; }
            set
            {
                size.C = value;
                vectorC = Vector3Utils.SetLength(vectorC, value);
            }
        }

        protected float SizeZ
        {
            get { return size.Z; }
            set
            {
                size.Z = value;
            }
        }

        private Vector3 currentSizeAVector;
        private Vector3 currentSizeBVector;
        private Vector3 currentSizeCVector;

        private float AxisRadius
        {
            get { return 0.2f; }
        }

        [NonSerialized]
        private IRotationVectorLimitable rotationLimiter;
        [NonSerialized]
        private FreeBindingAxis[] bindingAxes;
        [NonSerialized]
        private int axesCnt;
        [NonSerialized]
        private Vector3 layerVec;

        protected TrianglePrism()
            : base()
        {
            InitCurrentSizeVectors();
        }

        private void InitCurrentSizeVectors()
        {
            currentSizeAVector = vectorA;
            currentSizeBVector = vectorB;
            currentSizeCVector = vectorC;

            layerVec = Perpendicular;
        }

        private void RotateSizeVectors()
        {
            Matrix rotationMat;
            rotationMat = rotation.GetRotationMatrix();

            currentSizeAVector = Vector3.TransformCoordinate(vectorA, rotationMat);
            currentSizeBVector = Vector3.TransformCoordinate(vectorB, rotationMat);
            currentSizeCVector = Vector3.TransformCoordinate(vectorC, rotationMat);

            layerVec = Vector3.TransformCoordinate(Perpendicular, rotationMat);
        }

        private void UpdateVertices()
        {
            Vector3 scaledPerp;
            scaledPerp = Vector3Utils.SetLength(Perpendicular, size.Z / 2f);
            vertices[0].Vector = vectorA + scaledPerp;
            vertices[1].Vector = vectorB + scaledPerp;
            vertices[2].Vector = vectorC + scaledPerp;

            vertices[3].Vector = vectorA - scaledPerp;
            vertices[4].Vector = vectorB - scaledPerp;
            vertices[5].Vector = vectorC - scaledPerp;
        }

        private void UpdateSizeVectorsLength()
        {
            RotateSizeVectors();
        }

        private Vector3 GoToLayer(Vector3 widthVec)
        {
            return position + widthVec;
        }

        private void CreateLayerAxes(Vector3 widthVec)
        {
            Vector3 position, direction;

            position = GoToLayer(widthVec) + currentSizeAVector;
            direction = (GoToLayer(widthVec) + currentSizeBVector) - position;
            bindingAxes[axesCnt] = new FreeBindingAxis(idsForAxises[axesCnt], this, position, direction, AxisRadius, rotationLimiter);
            axesCnt++;

            position = position + direction;
            direction = (GoToLayer(widthVec) + currentSizeCVector) - position;
            bindingAxes[axesCnt] = new FreeBindingAxis(idsForAxises[axesCnt], this, position, direction, AxisRadius, rotationLimiter);
            axesCnt++;

            position = position + direction;
            direction = (GoToLayer(widthVec) + currentSizeAVector) - position;
            bindingAxes[axesCnt] = new FreeBindingAxis(idsForAxises[axesCnt], this, position, direction, AxisRadius, rotationLimiter);
            axesCnt++;
        }

        #region Overriden Members

        public override FreeBindingAxis[] GetAxes()
        {
            rotationLimiter = new RotationVectorLimiter(CanRotateX, CanRotateY, CanRotateZ);
            bindingAxes = new FreeBindingAxis[AxesNumber];

            CreateLayerAxes(layerVec * size.Z * 0.5f);
            //CreateLayerAxes(Vector3Utils.ZeroVector);
            CreateLayerAxes(-layerVec * size.Z * 0.5f);

            axesCnt = 0;

            return bindingAxes;
        }

        protected override int AxesNumber
        {
            get { return 6; }
        }

        protected override void PreInit()
        {
            vectorA = Direction;
            vectorB = Vector3Utils.RotateVectorByAxis(vectorA, Perpendicular, Angle.A120);
            vectorC = Vector3Utils.RotateVectorByAxis(vectorA, Perpendicular, -Angle.A120);

            vectorA = Vector3Utils.SetLength(vectorA, SizeA);
            vectorB = Vector3Utils.SetLength(vectorB, SizeB);
            vectorC = Vector3Utils.SetLength(vectorC, SizeC);
        }

        protected override void SetDefaultSize()
        {
            size = new Size4(10f, 10f, 10f, 2f);
        }

        protected override void SetVertices()
        {
            vertices = new Vertex[6];
            for (int cnt1 = 0; cnt1 < 6; cnt1++)
            {
                vertices[cnt1] = new Vertex();
            }
            UpdateVertices();
        }

        protected override void DoRotateX(Angle angle)
        {
            base.DoRotateX(angle);
            RotateSizeVectors();
        }

        protected override void DoRotateY(Angle angle)
        {
            base.DoRotateY(angle);
            RotateSizeVectors();
        }

        protected override void DoRotateZ(Angle angle)
        {
            base.DoRotateZ(angle);
            RotateSizeVectors();
        }

        protected override void DoRotateXYZ(RotationVector vector)
        {
            base.DoRotateXYZ(vector);
            RotateSizeVectors();
        }

        #endregion

        #region ITrianglePrismSizable Members

        public Size4 Size
        {
            get { return size; }
        }

        public void SetA(float a)
        {
            SizeA = a;
            UpdateVertices();
            UpdateSidesPositions();
            UpdateSizeVectorsLength();
            RaiseSizeChangedEvent();
        }

        public void SetB(float b)
        {
            SizeB = b;
            UpdateVertices();
            UpdateSidesPositions();
            UpdateSizeVectorsLength();
            RaiseSizeChangedEvent();
        }

        public void SetC(float c)
        {
            SizeC = c;
            UpdateVertices();
            UpdateSidesPositions();
            UpdateSizeVectorsLength();
            RaiseSizeChangedEvent();
        }

        public void SetZ(float z)
        {
            size.Z = z;
            UpdateVertices();
            UpdateSidesPositions();
            UpdateSizeVectorsLength();
            RaiseSizeChangedEvent();
        }

        public void Scale(float factor)
        {
            SizeA *= factor;
            SizeB *= factor;
            SizeC *= factor;
            SizeZ *= factor;

            UpdateVertices();
            UpdateSidesPositions();
            UpdateSizeVectorsLength();
            RaiseSizeChangedEvent();
        }

        public void MakeEquilateral()
        {
            float arithmeticMean;
            arithmeticMean = (vectorA.Length() + vectorB.Length() + vectorC.Length()) / 3f;
            SizeA = SizeB = SizeC = arithmeticMean;

            UpdateVertices();
            UpdateSidesPositions();
            UpdateSizeVectorsLength();
            RaiseSizeChangedEvent();
        }

        #endregion
    }
}
