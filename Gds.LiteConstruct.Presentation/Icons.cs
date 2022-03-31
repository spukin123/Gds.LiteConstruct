using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Gds.LiteConstruct.Presentation.Properties;

namespace Gds.LiteConstruct.Presentation
{
	public static class Icons
	{
		public static class App
		{
			public static readonly IconEntity ShellIcon = new IconEntity(Resources.OBIcon);
			public static readonly IconEntity NewDocument = new IconEntity(Resources.NewDocument);
			public static readonly IconEntity Open = new IconEntity(Resources.Open);
			public static readonly IconEntity Save = new IconEntity(Resources.Save);
			public static readonly IconEntity ExitApplication = new IconEntity(Resources.ExitApplication);
			public static readonly IconEntity Settings = new IconEntity(Resources.Settings);
			public static readonly IconEntity About = new IconEntity(Resources.About);
			public static readonly IconEntity Scene = new IconEntity(Resources.Home);
		}

		public static class Actions
		{
			public static readonly IconEntity Delete = new IconEntity(Resources.Delete);
			public static readonly IconEntity Rotate = new IconEntity(Resources.Rotate);
			public static readonly IconEntity Move = new IconEntity(Resources.Move);
			public static readonly IconEntity Tick = new IconEntity(Resources.Tick);
            public static readonly IconEntity Bind = new IconEntity(Resources.Bind);
			public static readonly IconEntity Unbind = new IconEntity(Resources.Unbind);
		}

		public static class Folders
		{
			public static readonly IconEntity New = new IconEntity(Resources.NewFolder);
			public static readonly IconEntity Edit = new IconEntity(Resources.EditFolder);
			public static readonly IconEntity Properties = new IconEntity(Resources.FolderProperties);
			public static readonly IconEntity Delete = new IconEntity(Resources.DeleteFolder);
		}

		public static class Files
		{
			public static readonly IconEntity New = new IconEntity(Resources.NewObject);
			public static readonly IconEntity Edit = new IconEntity(Resources.EditObject);
			public static readonly IconEntity Properties = new IconEntity(Resources.ObjectProperties);
			public static readonly IconEntity Preview = new IconEntity(Resources.Preview);
			public static readonly IconEntity Delete = new IconEntity(Resources.DeleteObject);
			public static readonly IconEntity Multiple = new IconEntity(Resources.MultipleObjects);
			public static readonly IconEntity Multiple_Star = new IconEntity(Resources.MultipleObjects_Star);
			public static readonly IconEntity Check = new IconEntity(Resources.Check);
		}
	}

	public class IconEntity
	{
		private Icon icon;

		public Icon Icon
		{
			get { return icon; }
		}

		public Image ToBitmap16()
		{
			Icon copy = new Icon(icon, new Size(16, 16));
			return copy.ToBitmap();
		}

		public Image ToBitmap24()
		{
			Icon copy = new Icon(icon, new Size(24, 24));
			return copy.ToBitmap();
		}

		public Image ToBitmap32()
		{
			Icon copy = new Icon(icon, new Size(32, 32));
			return copy.ToBitmap();
		}

		public IconEntity(Icon icon)
		{
			this.icon = icon;
		}
	}
}
