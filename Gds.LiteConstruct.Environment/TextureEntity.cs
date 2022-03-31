using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using Gds.LiteConstruct.BusinessObjects;

namespace Gds.LiteConstruct.Environment
{
    class TextureEntity
    {
        private string fileName;

        public string FileName
        {
            get { return fileName; }
        }

        private Texture texture;

        public Texture Texture
        {
            get { return texture; }
        }

        private int consumerCount;

        public int ConsumerCount
        {
            get { return consumerCount; }
            set { consumerCount = value; }
        }

        public TextureEntity(string fileName)
        {
            this.fileName = fileName;
            texture = TextureLoader.FromFile(DeviceObject.Device, fileName);
            consumerCount = 0;
        }
    }
}
