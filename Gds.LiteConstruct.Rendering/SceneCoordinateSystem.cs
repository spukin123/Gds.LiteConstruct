using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using Gds.Runtime;
using Gds.Runtime.Settings;

namespace Gds.LiteConstruct.Rendering
{
    public class SceneCoordinateSystem : IRenderable, IDisposable
    {
        protected Device device = null;
        protected VertexBuffer axisesVB = null;
        protected int axisesVC = 12;
        protected int axisColorX = unchecked((int)0xffff0000);
        protected int axisColorY = unchecked((int)0xffffff00);
        protected int axisColorZ = unchecked((int)0xff0000ff);
        protected int axisColorX2 = unchecked((int)0xffEAB9BB);
        protected int axisColorY2 = unchecked((int)0xffFEFFDD);
        protected int axisColorZ2 = unchecked((int)0xffBAC6FF);
        protected float axisLength = 100.0f;

        protected bool gridEnabled;
        protected VertexBuffer gridVB = null;
        protected int gridColor;
        protected float cellLength = 10f;
        protected int gridVC;


        public SceneCoordinateSystem(Device device, bool gridEnabled)
        {
            this.device = device;
            this.gridEnabled = gridEnabled;
			Initialize();
        }

		private void Initialize()
		{
			if (gridEnabled)
			{
				ISettingsContext settingsContext = Gds.Runtime.AppContext.Get<ISettingsContext>();
				SceneSettings settings = settingsContext.GetSettingsCopy<SceneSettings>();
				ApplySettings(settings);
				InitializeGridData();
				settingsContext.SubscribeToSettingsUpdate<SceneSettings>(SettingsChanged);
			}
			InitializeAxisesData();
		}

		private void InitializeGridData()
        {
            int cellDirCount = (int)(axisLength / cellLength);
            this.gridVC = cellDirCount * 2 * 4;
            
            gridVB = new VertexBuffer(typeof(CustomVertex.PositionColored), gridVC, device, Usage.WriteOnly, CustomVertex.PositionColored.Format, Pool.Default);
            gridVB.Created += this.RestoreGridData;
            RestoreGridData(gridVB, null);
        }

		private void RestoreGridData(object sender, EventArgs e)
        {
            int cellDirCount = (int)(axisLength / cellLength);
            int cnt1, index;
            float cellPos;

            CustomVertex.PositionColored[] vertices = (CustomVertex.PositionColored[])gridVB.Lock(0, 0);

            index = 0;
            for (cnt1 = 0, cellPos = cellLength; cnt1 < cellDirCount; cnt1++, cellPos += cellLength, index += 2)
            {
                vertices[index].Position = new Vector3(cellPos, axisLength, 0.0f);
                vertices[index + 1].Position = new Vector3(cellPos, -axisLength, 0.0f);
            }
            for (cnt1 = 0, cellPos = -cellLength; cnt1 < cellDirCount; cnt1++, cellPos -= cellLength, index += 2)
            {
                vertices[index].Position = new Vector3(cellPos, axisLength, 0.0f);
                vertices[index + 1].Position = new Vector3(cellPos, -axisLength, 0.0f);
            }
            for (cnt1 = 0, cellPos = cellLength; cnt1 < cellDirCount; cnt1++, cellPos += cellLength, index += 2)
            {
                vertices[index].Position = new Vector3(axisLength, cellPos, 0.0f);
                vertices[index + 1].Position = new Vector3(-axisLength, cellPos, 0.0f);
            }
            for (cnt1 = 0, cellPos = -cellLength; cnt1 < cellDirCount; cnt1++, cellPos -= cellLength, index += 2)
            {
                vertices[index].Position = new Vector3(axisLength, cellPos, 0.0f);
                vertices[index + 1].Position = new Vector3(-axisLength, cellPos, 0.0f);
            }
            for (index = 0; index < gridVC; index++)
            {
                vertices[index].Color = gridColor;
            }

            gridVB.Unlock();
        }

		private void InitializeAxisesData()
        {
            axisesVB = new VertexBuffer(typeof(CustomVertex.PositionColored), axisesVC, device, Usage.WriteOnly, CustomVertex.PositionColored.Format, Pool.Default);
            axisesVB.Created += this.CreateAxisesData;
            CreateAxisesData(axisesVB, null);
        }

		private void CreateAxisesData(object sender, EventArgs args)
        {
            CustomVertex.PositionColored[] vertices = (CustomVertex.PositionColored[])axisesVB.Lock(0, 0);

            Vector3 zeroVec = new Vector3(0.0f, 0.0f, 0.0f);
            vertices[0] = new CustomVertex.PositionColored(zeroVec, axisColorX);
            vertices[1] = new CustomVertex.PositionColored(new Vector3(axisLength, 0.0f, 0.0f), axisColorX);
            vertices[2] = new CustomVertex.PositionColored(zeroVec, axisColorY);
            vertices[3] = new CustomVertex.PositionColored(new Vector3(0.0f, axisLength, 0.0f), axisColorY);
            vertices[4] = new CustomVertex.PositionColored(zeroVec, axisColorZ);
            vertices[5] = new CustomVertex.PositionColored(new Vector3(0.0f, 0.0f, axisLength), axisColorZ);

            vertices[6] = new CustomVertex.PositionColored(zeroVec, axisColorX2);
            vertices[7] = new CustomVertex.PositionColored(new Vector3(-axisLength, 0.0f, 0.0f), axisColorX2);
            vertices[8] = new CustomVertex.PositionColored(zeroVec, axisColorY2);
            vertices[9] = new CustomVertex.PositionColored(new Vector3(0.0f, -axisLength, 0.0f), axisColorY2);
            vertices[10] = new CustomVertex.PositionColored(zeroVec, axisColorZ2);
            vertices[11] = new CustomVertex.PositionColored(new Vector3(0.0f, 0.0f, -axisLength), axisColorZ2);

            axisesVB.Unlock();
        }

        #region IRenderable Members

        public void Render()
        {
            device.SetTexture(0, null);
            device.VertexFormat = CustomVertex.PositionColored.Format;

            device.SetStreamSource(0, axisesVB, 0);
            device.DrawPrimitives(PrimitiveType.LineList, 0, axisesVC / 2);

            if (gridEnabled)
            {
                device.SetStreamSource(0, gridVB, 0);
                device.DrawPrimitives(PrimitiveType.LineList, 0, gridVC / 2);
            }
        }

        #endregion

		#region IDisposable Members

		public void Dispose()
		{
			axisesVB.Dispose();
			if (gridEnabled)
				gridVB.Dispose();
		}

		#endregion

		private void ApplySettings(SceneSettings settings)
		{
			cellLength = settings.CellLength;
			axisLength = settings.CellLength * (float)settings.CellCount;
			gridColor = settings.GridColor.ToArgb();
		}

		private void SettingsChanged(SceneSettings settings)
		{
			Dispose();
			ApplySettings(settings);
			InitializeAxisesData();
			if (gridEnabled)
			{
				InitializeGridData();
			}
		}
	}
}
