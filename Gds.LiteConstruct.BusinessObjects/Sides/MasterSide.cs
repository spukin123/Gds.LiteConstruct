using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects.Sides
{
    [Serializable]
    public abstract class MasterSide<TDimension> : MasterSide where TDimension : SideDimension
    {
        public MasterSide(SideDimension[] dimensions)
            : base(dimensions)
        {
        }

        public override void Initialize(SideDimension[] dimensions)
        {
            TDimension[] concreteDimensions = new TDimension[dimensions.Length];
            for (int i = 0; i < dimensions.Length; i++)
            {
                concreteDimensions[i] = (TDimension)dimensions[i];
            }
            DoInitialize(concreteDimensions);
        }

        protected abstract void DoInitialize(TDimension[] dimensions);
    }

    [Serializable]
    public abstract class MasterSide : SideBase
    {
        protected SimpleSide[] children;
        [NonSerialized]
        protected float sideHeight, sideWidth;
        
        protected abstract int DimensionsNumber { get; }

        public MasterSide(SideDimension[] dimensions)
        {
            Initialize(dimensions);
        }

        protected void CreateChildren()
        {
            children = new SimpleSide[DimensionsNumber];
            for (int cnt = 0; cnt < DimensionsNumber; cnt++)
            {
                CreateSide(cnt);
            }

            UpdateTextureCoordinates();
        }

        #region Overriden Members

        internal override Vertex GetIntersectionPoint(Ray ray)
        {
            foreach (SimpleSide child in children)
            {
                Vertex point = child.GetIntersectionPoint(ray);
                if (point != null)
                {
                    return point;
                }
            }

            return null;
        }

        internal override void UpdatePosition()
        {
            foreach (SimpleSide child in children)
            {
                child.UpdatePosition();
            }
        }

        internal override void Render()
        {
            foreach (SimpleSide child in children)
            {
                child.Render();
            }
        }

        public override void Select(bool select)
        {
            selected = select;
            foreach (SimpleSide child in children)
            {
                child.Select(select);
            }
        }

        internal override void MakeTransparent(bool transparent)
        {
            foreach (SimpleSide child in children)
            {
                child.MakeTransparent(transparent);
            }
        }

        public abstract void Initialize(SideDimension[] dimensions);

        public override void SetTexture(Guid newTextureId)
        {
            textureId = newTextureId;
            foreach (SimpleSide child in children)
            {
                child.SetTexture(newTextureId);
            }
        }

        internal override ITextureProvider TextureProvider
        {
            set
            {
                base.TextureProvider = value;
                foreach (SimpleSide child in children)
                {
                    child.TextureProvider = value;
                }
            }
        }

        internal override void InitializeTexture()
        {
            base.InitializeTexture();
            foreach (SimpleSide child in children)
            {
                child.SetTexture(TextureId);
            }
        }

        public override void Dispose()
        {
            //textureProvider.Detach(TextureId);
            foreach (SimpleSide child in children)
            {
                child.Dispose();
            }
        }

        public override void UpdateTextureCoordinates()
        {
            rotator = new TextureRotator(TransformedPoints, TextureRotationType.Compress);
            rotator.Rotate(TextureRotationAngle);
            ApplyTextureCoordinates();
        }

        #endregion

        protected abstract void CreateSide(int index);
    }
}
