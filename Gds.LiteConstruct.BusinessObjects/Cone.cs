using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class Cone : GeometricalFigure
    {
        private float height;
        private float radius;
        private int sidesNumber;

        private float HalfHeight
        {
            get { return height / 2f; }
        }

        private Vector3 TipVector
        {
            get { return Vector3Utils.AlignedZVector * HalfHeight; }
        }

        private Vector3 BaseVector
        {
            get { return -Vector3Utils.AlignedZVector * HalfHeight; }
        }

        public Cone(float height, float radius, int sidesNumber)
        {
            Init(height, radius, sidesNumber);
            CreatePoints();
            CreateIndices();
        }

        private void Init(float height, float radius, int sidesNumber)
        {
            this.height = height;
            this.radius = radius;
            this.sidesNumber = sidesNumber;
        }

        private void CreatePoints()
        {
            points.AddItem(TipVector);
            points.AddItem(BaseVector);

            Vector3 startVector = new Vector3(0f, -1f, 0f);
            Matrix rotation = Matrix.RotationZ(2f * Angle.Pi / sidesNumber);
            for (int cnt = 0; cnt < sidesNumber; cnt++)
            {
                points.AddItem(startVector * radius + BaseVector);
                startVector = Vector3.TransformCoordinate(startVector, rotation);
            }
        }

        private void CreateIndices()
        {
            CreateTopPartIndices();
            CreateBottomPartIndices();
        }

        private void CreateBottomPartIndices()
        {
            for (int cnt = 0; cnt < sidesNumber - 1; cnt++)
            {
                indices.AddItem((short)(1));
                indices.AddItem((short)(cnt + 2));
                indices.AddItem((short)(cnt + 3));
            }

            indices.AddItem((short)(1));
            indices.AddItem((short)(sidesNumber + 1));
            indices.AddItem((short)(2));
        }

        private void CreateTopPartIndices()
        {
            for (int cnt = 0; cnt < sidesNumber - 1; cnt++)
            {
                indices.AddItem((short)(0));
                indices.AddItem((short)(cnt + 2));
                indices.AddItem((short)(cnt + 3));
            }

            indices.AddItem((short)(0));
            indices.AddItem((short)(sidesNumber + 1));
            indices.AddItem((short)(2));
        }
    }
}
