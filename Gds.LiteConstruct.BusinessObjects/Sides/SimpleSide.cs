using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;

namespace Gds.LiteConstruct.BusinessObjects.Sides
{
    [Serializable]
    public abstract class SimpleSide : SideBase
    {
        [NonSerialized]
        protected static Device device = DeviceObject.Device;
        [NonSerialized]
        protected VertexBuffer vertexBuffer;
        [NonSerialized]
        protected IndexBuffer indexBuffer;
        
        protected abstract int VerticesCount { get; }
        protected abstract int IndicesCount { get; }
        protected abstract void CreateVertexData(object sender, EventArgs args);
        protected abstract void CreateIndexData(object sender, EventArgs args);
        protected abstract SideDimension Dimension { set; }

        public SimpleSide(SideDimension dimension)
        {
            SetDefaultTextureAllocation();
            DoInitialize(dimension);
        }
        
        public SimpleSide(SideDimension dimension, TextureAllocation textureAllocation)
        {
            TextureAllocation = textureAllocation;
            DoInitialize(dimension);
        }

        //protected void SetVerticesColor(CustomVertex.PositionColoredTextured[] vertices, int color)
        //{
        //    int cnt1;
        //    for (cnt1 = 0; cnt1 < VerticesCount; cnt1++)
        //    {
        //        vertices[cnt1].Color = color;
        //    }
        //}

        protected void SetVerticesColor()
        {
            CustomVertex.PositionColoredTextured[] vertices = (CustomVertex.PositionColoredTextured[])vertexBuffer.Lock(0, 0);

            for (int cnt1 = 0; cnt1 < VerticesCount; cnt1++)
            {
                vertices[cnt1].Color = Color;
            }

            vertexBuffer.Unlock();
        }

        protected void InitializeVertexIndexData()
        {
            vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionColoredTextured), VerticesCount, device, Usage.Dynamic, CustomVertex.PositionColoredTextured.Format, Pool.Default);
            vertexBuffer.Created += new EventHandler(this.CreateVertexData);
            indexBuffer = new IndexBuffer(typeof(short), IndicesCount, device, Usage.WriteOnly, Pool.Default);
            indexBuffer.Created += new EventHandler(this.CreateIndexData);
            CreateVertexData(vertexBuffer, null);
            CreateIndexData(indexBuffer, null);
        }

        internal override void Render()
        {
            if (transparent)
            {
                device.RenderState.AlphaBlendEnable = true;
                //device.RenderState.SourceBlend = Blend.SourceColor;
                //device.RenderState.DestinationBlend = Blend.DestinationColor;
            }

            device.SetTexture(0, texture);
            device.VertexFormat = CustomVertex.PositionColoredTextured.Format;
            device.SetStreamSource(0, vertexBuffer, 0);
            device.Indices = indexBuffer;
            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, VerticesCount, 0, IndicesCount / 3);
            device.SetTexture(0, null);


            if (transparent)
            {
                device.RenderState.AlphaBlendEnable = false;
            }
        }

        public override void Select(bool select)
        {
            selected = select;
            SetVerticesColor();
        }

        internal override void MakeTransparent(bool transparent)
        {
            this.transparent = transparent;
            SetVerticesColor();
        }

        public override void UpdateTextureCoordinates()
        {
            rotator = new TextureRotator(TransformedPoints, TextureRotationType.Compress);
            rotator.Rotate(TextureRotationAngle);
            ApplyTextureCoordinates();
        }

        public void Initialize(SideDimension dimension)
        {
            SetDefaultTextureAllocation();
            DoInitialize(dimension);
        }

        protected virtual void DoInitialize(SideDimension dimension)
        {
            Dimension = dimension;
            InitializeVertexIndexData();

            UpdateTextureCoordinates();
        }

        protected abstract void SetDefaultTextureAllocation();
        protected abstract TextureAllocation TextureAllocation { set; }

        public abstract void SetTextureAllocation(TextureAllocation allocation);

        #region IDisposable Members
        
        public override void Dispose()
        {
            textureProvider.Detach(TextureId);

            vertexBuffer.Dispose();
            indexBuffer.Dispose();
        }

        #endregion
    }
}
