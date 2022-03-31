using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Gds.Runtime.Settings;

namespace Gds.LiteConstruct.Rendering
{
	[Settings("scene"), Serializable]
	public class SceneSettings : ICloneable
	{
		private float cellLength = 10f;

		public float CellLength
		{
			get { return cellLength; }
			set { cellLength = value; }
		}

		private int cellCount = 10;

		public int CellCount
		{
			get { return cellCount; }
			set { cellCount = value; }
		}

		private Color gridColor = Color.FromArgb(0, 255, 0);

		public Color GridColor
		{
			get { return gridColor; }
			set { gridColor = value; }
		}

		private Color backgroundColor = Color.FromArgb(140, 140, 140);

		public Color BackgroundColor
		{
			get { return backgroundColor; }
			set { backgroundColor = value; }
		}

		#region ICloneable Members

		public object Clone()
		{
			return this.MemberwiseClone();
		}

		#endregion
	}
}
