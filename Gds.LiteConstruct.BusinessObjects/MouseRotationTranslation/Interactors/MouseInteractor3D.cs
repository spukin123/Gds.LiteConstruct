using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interactors
{
    public abstract class MouseInteractor3D : IRenderable, IMovable, IDisposable
    {
        //struct here
        protected Triangle[] etalonIS;
        protected Triangle[] currentIS;

        protected Device device;

        protected abstract int VerticesCount { get; }
        protected abstract int IndicesCount { get; }
        protected abstract Vector3 Origin { get; set; }

        protected VertexBuffer vertexBuffer;
        protected IndexBuffer indexBuffer;

        protected Texture texture;
        private Bitmap textureBitmap;
        public Bitmap TextureBitmap
        {
            get { return textureBitmap; }
            set 
            { 
                textureBitmap = value;
                texture = Texture.FromBitmap(DeviceObject.Device, textureBitmap, Usage.Dynamic, Pool.Default);
            }
        }

        protected Color defaultColor;
        public Color DefaultColor
        {
            get { return defaultColor; }
            set { defaultColor = value; } 
        }

        protected Vector3 interactionPoint;
        public Vector3 InteractionPoint
        {
            get { return interactionPoint; }
            set { interactionPoint = value; }
        }

        protected bool disposed = false;
        public bool Disposed
        {
            get { return disposed; } 
        }

        public MouseInteractor3D(Color color, Bitmap textureBitmap)
        {
            device = DeviceObject.Device;

            this.defaultColor = color;
            this.TextureBitmap = textureBitmap;
        }

        public MouseInteractor3D(Color color)
        {
            device = DeviceObject.Device;

            this.defaultColor = color;
        }

        protected virtual void ConnectToBuffersEvents()
        {
            vertexBuffer.Created += new EventHandler(CreateVertexData);
            indexBuffer.Created += new EventHandler(CreateIndexData);
        }

        protected virtual void InitBuffers()
        {
            vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionColoredTextured), VerticesCount, device, Usage.Dynamic, CustomVertex.PositionColoredTextured.Format, Pool.Default);
            indexBuffer = new IndexBuffer(typeof(short), IndicesCount, device, Usage.WriteOnly, Pool.Default);

            ConnectToBuffersEvents();

            CreateVertexData(vertexBuffer, null);
            CreateIndexData(indexBuffer, null);
        }

        protected void LoadBuffers()
        {
#warning Maximize/Minimize bug here. //jack986: Sho za nahuy?
            //Maximize/Minimize buh here, because texture is disposing
            //device.SetTexture(0, texture);
            //
            if (texture.Disposed)
            {
                texture = Texture.FromBitmap(DeviceObject.Device, textureBitmap, Usage.Dynamic, Pool.Default);
            }
            device.SetTexture(0, texture);

            device.VertexFormat = CustomVertex.PositionColoredTextured.Format;
            device.SetStreamSource(0, vertexBuffer, 0);
            device.Indices = indexBuffer;
#warning Texture = null? //jack986: Snova huynya kakayato, pisets...
			device.SetTexture(0, null);
        }

        public abstract bool Interacted(Point mouseCoords);

        protected abstract void CreateVertexData(object sender, EventArgs e);
        protected abstract void CreateIndexData(object sender, EventArgs e);
        protected abstract void InitStruct();
        protected abstract void ApplyChangesToStruct();

        #region IRenderable Members

        public abstract void Render();

        #endregion

        #region IMovable Members

        public void MoveTo(Vector3 pos)
        {
            Origin = pos;
        }

        public void MoveBy(Vector3 mpos)
        {
            Origin += mpos;
        }

        public void MoveByX(float mx)
        {
            Origin += new Vector3(mx, 0f, 0f);
        }

        public void MoveByY(float my)
        {
            Origin += new Vector3(0f, my, 0f);
        }

        public void MoveByZ(float mz)
        {
            Origin += new Vector3(0f, 0f, mz);
        }

        public void MoveX(float x)
        {
            Origin = new Vector3(x, 0f, 0f);
        }

        public void MoveY(float y)
        {
            Origin = new Vector3(0f, y, 0f);
        }

        public void MoveZ(float z)
        {
            Origin = new Vector3(0f, 0f, z);
        }

        public Vector3 Position
        {
            get { return Origin; }
            set { Origin = value; }
        }

        #endregion

        #region IDisposable Members

        public virtual void Dispose()
        {
            disposed = true;
            vertexBuffer.Dispose();
            indexBuffer.Dispose();
        }

        #endregion
    }
}
