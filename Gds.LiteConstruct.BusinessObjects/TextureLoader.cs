using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using System.IO;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class TexturesLoader
    {
        public static Texture GetSystemTexture(Bitmap bitmap)
        {
            return Texture.FromBitmap(DeviceObject.Device, bitmap, Usage.Dynamic, Pool.Default);
        }

        public static Texture GetTexture(string fullPath)
        {
            return TextureLoader.FromFile(DeviceObject.Device, fullPath);
        }
    }
}
