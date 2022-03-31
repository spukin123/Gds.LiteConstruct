using System;
using System.Collections.Generic;
using System.Text;
using Gds.Windows;

namespace Gds.LiteConstruct.Presentation.Services
{
	public class TextureEditService : ITextureEditService
	{
		public void Preview(string name, string location)
		{
			try
			{
				TexturePreviewForm form = new TexturePreviewForm(name, location);
				form.ShowDialog();
			}
			catch
			{
				MessageWindow.Error("Failed to open texture file.");
			}
		}
	}
}
