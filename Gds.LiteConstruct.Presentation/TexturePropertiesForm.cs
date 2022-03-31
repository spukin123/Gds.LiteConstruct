using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.Environment;

namespace Gds.LiteConstruct.Presentation
{
    public partial class TexturePropertiesForm : Form
    {
        private TextureInfo texture;

        public TextureInfo Texture
        {
            get { return texture; }
        }

        public TexturePropertiesForm(TextureInfo texture)
        {
            InitializeComponent();
			InitializeIcons();

			this.Text = string.Format("{0} - Texture properties", texture.Name);
            this.texture = texture.Clone();
            textureBindingSource.DataSource = this.texture;
        }

		private void InitializeIcons()
		{
			this.Icon = Icons.Files.Properties.Icon;
		}
    }
}