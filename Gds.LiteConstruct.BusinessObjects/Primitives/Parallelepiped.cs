using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.Sides;
using Gds.LiteConstruct.BusinessObjects.Axises;

namespace Gds.LiteConstruct.BusinessObjects.Primitives
{
    [Serializable]
    public abstract class Parallelepiped : PrimitiveBase, IParallelepipedSizable
    {
        protected Size3 size;
        
        protected Vector3 sizeXVec;
        protected Vector3 sizeYVec;
        protected Vector3 sizeZVec;

        protected Vector3 currentSizeXVec;
        protected Vector3 currentSizeYVec;
        protected Vector3 currentSizeZVec;

        protected IRotationVectorLimitable rotationLimiter;
        protected FreeBindingAxis[] bindingAxes;

        protected float AxisRadius
        {
            get { return 0.2f; }
        }
        protected int axesCnt;

        protected Parallelepiped()
            : base()
        {
            InitSizeVectors();
            InitCurrentSizeVectors();
        }

        private void InitCurrentSizeVectors()
        {
            currentSizeXVec = sizeXVec;
            currentSizeYVec = sizeYVec;
            currentSizeZVec = sizeZVec;
        }

        private void InitSizeVectors()
        {
            sizeXVec = Vector3Utils.AlignedXVector * size.X * 0.5f;
            sizeYVec = Vector3Utils.AlignedYVector * size.Y * 0.5f;
            sizeZVec = Vector3Utils.AlignedZVector * size.Z * 0.5f;
        }

        private void UpdateSizeVectorsLength()
        {
            InitSizeVectors();
            RotateSizeVectors();
        }

        private void RotateSizeVectors()
        {
            Matrix rotationMat;
            rotationMat = rotation.GetRotationMatrix();

            currentSizeXVec = Vector3.TransformCoordinate(sizeXVec, rotationMat);
            currentSizeYVec = Vector3.TransformCoordinate(sizeYVec, rotationMat);
            currentSizeZVec = Vector3.TransformCoordinate(sizeZVec, rotationMat);
        }

        private void UpdateVertices()
        {
            float mx, my, mz;
            mx = size.X / 2f;
            my = size.Y / 2f;
            mz = size.Z / 2f;

            vertices[0].Vector = new Vector3(mx, my, mz);
            vertices[1].Vector = new Vector3(-mx, my, mz);
            vertices[2].Vector = new Vector3(-mx, -my, mz);
            vertices[3].Vector = new Vector3(mx, -my, mz);

            vertices[4].Vector = new Vector3(mx, my, -mz);
            vertices[5].Vector = new Vector3(-mx, my, -mz);
            vertices[6].Vector = new Vector3(-mx, -my, -mz);
            vertices[7].Vector = new Vector3(mx, -my, -mz);
        }

        private void ProcessAfterResizing()
        {
            UpdateVertices();
            UpdateSidesPositions();
            UpdateTextureCoordinates();

            RaiseSizeChangedEvent();
        }

        protected abstract void CreateLayerAxes(Vector3 widthVec);

        protected abstract Vector3 LayerWidthVec { get; }

        #region Overriden Members

        protected override int AxesNumber
        {
            get { return 8; }
        }

        public override FreeBindingAxis[] GetAxes()
        {
            rotationLimiter = new RotationVectorLimiter(CanRotateX, CanRotateY, CanRotateZ);
            bindingAxes = new FreeBindingAxis[AxesNumber];

            CreateLayerAxes(LayerWidthVec);
            //CreateLayerAxes(Vector3Utils.ZeroVector);
            CreateLayerAxes(-LayerWidthVec);

            axesCnt = 0;
            return bindingAxes;
        }

        protected override void SetVertices()
        {
            vertices = new Vertex[8];
            for (int cnt1 = 0; cnt1 < 8; cnt1++)
            {
                vertices[cnt1] = new Vertex();
            }
            UpdateVertices();
        }

        protected override void SetSideDimensions()
        {
            simpleSideDimensions = new SideDimension[6];
            simpleSideDimensions[0] = new Side4Dimension(vertices, 0, 1, 2, 3);
            simpleSideDimensions[1] = new Side4Dimension(vertices, 1, 0, 4, 5);
            simpleSideDimensions[2] = new Side4Dimension(vertices, 2, 1, 5, 6);
            simpleSideDimensions[3] = new Side4Dimension(vertices, 3, 2, 6, 7);
            simpleSideDimensions[4] = new Side4Dimension(vertices, 0, 3, 7, 4);
            simpleSideDimensions[5] = new Side4Dimension(vertices, 5, 4, 7, 6);
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

        #region IParallelepipedSizable Members

        public Size3 Size
        {
            get { return size; }
        }
        
        public void SetX(float x)
        {
            size.X = x;
            ProcessAfterResizing();
            UpdateSizeVectorsLength();
        }

        public void SetY(float y)
        {
            size.Y = y;
            ProcessAfterResizing();
            UpdateSizeVectorsLength();
        }

        public void SetZ(float z)
        {
            size.Z = z;
            ProcessAfterResizing();
            UpdateSizeVectorsLength();
        }

        public void Scale(float scaleFactor)
        {
            size.X *= scaleFactor;
            size.Y *= scaleFactor;
            size.Z *= scaleFactor;

            ProcessAfterResizing();
            UpdateSizeVectorsLength();
        }

        #endregion
    }
}
