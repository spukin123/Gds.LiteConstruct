using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Gds.LiteConstruct.Environment
{
    [Serializable]
    public class TextureInfo : ICloneable
    {
        private Guid id;

        public Guid Id
        {
            get { return id; }
        }

        private string fileName;

        public string FileName
        {
            get { return fileName; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public TextureInfo(string fileName)
        {
            this.id = Guid.NewGuid();
            this.fileName = fileName;
            this.name = Path.GetFileNameWithoutExtension(fileName);
        }

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        public TextureInfo Clone()
        {
            return this.MemberwiseClone() as TextureInfo;
        }

        public TextureInfo CloneWithUniqueFileName()
        {
            TextureInfo clone = this.MemberwiseClone() as TextureInfo;
            clone.fileName = id.ToString() + Path.GetExtension(fileName);
            return clone;
        }

        #endregion
    }
}
