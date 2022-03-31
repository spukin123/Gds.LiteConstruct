using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Runtime.Serialization;

namespace Gds.LiteConstruct.BusinessObjects.Sides
{
    [Serializable]
    public abstract class SideBase : IDisposable
    {
        [NonSerialized]
        protected bool selected = false;

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        [NonSerialized]
        protected bool transparent = false;

        protected bool Transparent
        {
            get { return transparent; }
            set { transparent = value; }
        }

        protected int Color
        {
            get
            {
                if (selected)
                {
                    return HighlightedColor;
                }
                else if (transparent)
                {
                    return TransparencyColor;
                }
                else
                {
                    return DefColor;
                }
            }
        }

        [NonSerialized]
        protected const int DefColor = unchecked((int)0xffffffff);
        [NonSerialized]
        protected const int HighlightedColor = unchecked((int)0xffffffa0);
        [NonSerialized]
        protected const int TransparencyColor = unchecked((int)0x55ffffff);

        [NonSerialized]
        protected Texture texture;

        protected Guid textureId;
        public Guid TextureId
        {
            get { return textureId; }
            set { textureId = value; }
        }

        private Angle textureRotationAngle = Angle.A0;
        public Angle TextureRotationAngle
        {
            get
            {
                return textureRotationAngle;
            }
        }

        [NonSerialized]
        protected ITextureRotatable rotator;

        [NonSerialized]
        protected ITextureProvider textureProvider;

        internal virtual ITextureProvider TextureProvider
        {
            set { textureProvider = value; }
        }

        public virtual void SetTexture(Guid newTextureId)
        {
            textureProvider.Detach(textureId);
            textureId = newTextureId;
            texture = textureProvider.Attach(newTextureId);
        }

        internal void SetDefaultTexture()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        internal virtual void InitializeTexture()
        {
            texture = textureProvider.Attach(textureId);
        }

        public void RotateTexture(Angle angle)
        {
            if (rotator != null)
            {
                textureRotationAngle = angle;
                rotator.Rotate(angle);
                ApplyTextureCoordinates();
            }
        }

        internal abstract Vertex GetIntersectionPoint(Ray ray);
        internal abstract void UpdatePosition();
        internal abstract void Render();
        public abstract void Select(bool select);
        internal abstract void MakeTransparent(bool transparent);
        protected abstract void ApplyTextureCoordinates();
        public abstract void UpdateTextureCoordinates();
        protected abstract TransformedPoint[] TransformedPoints { get; }

        #region IDisposable Members

        public abstract void Dispose();

        #endregion
    }
}
