using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class GeometricalFigure
    {
        protected ObjectsBuffer<Vector3> points;
        protected ObjectsBuffer<short> indices;

        public ObjectsBuffer<Vector3> Points
        {
            get { return points; }
        }

        public ObjectsBuffer<short> Indices
        {
            get { return indices; }
        }

        public int PointsCount
        {
            get { return points.ItemsCount; }
        }

        public int IndicesCount
        {
            get { return indices.ItemsCount; }
        }

        public GeometricalFigure()
        {
            points = new ObjectsBuffer<Vector3>();
            indices = new ObjectsBuffer<short>();
        }

        public GeometricalFigure(ObjectsBuffer<Vector3> points, ObjectsBuffer<short> indices)
        {
            this.points = points;
            this.indices = indices;
        }

        public void TranslateBy(Vector3 vector)
        {
            for (int cnt = 0; cnt < points.ItemsCount; cnt++)
			{
                points[cnt] += vector;
			}
        }

        public static GeometricalFigure Merge(GeometricalFigure figure1, GeometricalFigure figure2)
        {
            ObjectsBuffer<Vector3> mergedPoints = new ObjectsBuffer<Vector3>();
            ObjectsBuffer<short> mergedIndices = new ObjectsBuffer<short>();

            for (int cnt = 0; cnt < figure1.PointsCount; cnt++)
            {
                mergedPoints.AddItem(figure1.Points[cnt]);
            }

            for (int cnt = 0; cnt < figure2.PointsCount; cnt++)
            {
                mergedPoints.AddItem(figure2.Points[cnt]);
            }

            for (int cnt = 0; cnt < figure1.IndicesCount; cnt++)
            {
                mergedIndices.AddItem(figure1.Indices[cnt]);
            }

            int startVertex = figure1.PointsCount;
            for (int cnt = 0; cnt < figure2.IndicesCount; cnt++)
            {
                mergedIndices.AddItem((short)(figure2.Indices[cnt] + startVertex));
            }

            return new GeometricalFigure(mergedPoints, mergedIndices);
        }
    }
}
