using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.TransformationControllers
{
    public abstract class BufferedTransformationController : TransformationController
    {
        protected VertexBuffer vertexBuffer;
        protected IndexBuffer indexBuffer;

        public BufferedTransformationController(Color color) : base(color)
        {
        }

        protected virtual void InitBuffers()
        {
            vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionColored), VerticesCount, device, Usage.Dynamic, CustomVertex.PositionColored.Format, Pool.Default);
            vertexBuffer.Created += new EventHandler(CreateVertexData);
            indexBuffer = new IndexBuffer(typeof(short), IndicesCount, device, Usage.WriteOnly, Pool.Default);
            indexBuffer.Created += new EventHandler(CreateIndexData);

            CreateVertexData(vertexBuffer, new EventArgs());
            CreateIndexData(indexBuffer, new EventArgs());
        }

        public override void Dispose()
        {
            base.Dispose();
            vertexBuffer.Dispose();
            indexBuffer.Dispose();
        }

        protected abstract void CreateVertexData(object sender, EventArgs e);
        protected abstract void CreateIndexData(object sender, EventArgs e);
        protected abstract void ApplyColorToVertices(Color color);
        protected abstract void ApplyTransparencyToVertices(int transparency);
        protected abstract int VerticesCount { get; }
        protected abstract int IndicesCount { get; }
    }
}
