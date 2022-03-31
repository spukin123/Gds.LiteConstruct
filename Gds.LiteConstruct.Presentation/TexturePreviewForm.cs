using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Gds.LiteConstruct.Presentation
{
	public partial class TexturePreviewForm : Form
	{
		public TexturePreviewForm(string name, string location)
		{
			InitializeComponent();
			InitializeIcons();
			InitializeData(location);
			this.Text = string.Format("{0} - Texture preview", name);
		}

		private void InitializeIcons()
		{
			this.Icon = Icons.Files.Preview.Icon;
		}

		private void InitializeData(string location)
		{
			Image image = Bitmap.FromFile(location);
			pictureBox.Image = image;
			lblType.Text = Path.GetExtension(location).Replace(".", "").ToUpper();
			lblSize.Text = string.Format("{0}x{1}", image.Width, image.Height);
			lblFileName.Text = Path.GetFileName(location);
		}
	}
}