using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public class Cylinder : GeometricalFigure
    {
        private Ray axis;
        private int sidesNumber;
        private float radius;

        private Vector3 HalfDirection
        {
            get { return axis.Direction * 0.5f; }
        }

        private int BottomStartIndex
        {
            get { return 2; }
        }

        private int TopStartIndex
        {
            get { return BottomStartIndex + sidesNumber; }
        }

        public Cylinder(Ray axis, int sidesNumber, float radius)
            : base()
        {
            Init(axis, sidesNumber, radius);
            CreatePoints();
            CreateIndices();
        }

        private void Init(Ray axis, int sidesNumber, float radius)
        {
            this.axis = axis;
            this.sidesNumber = sidesNumber;
            this.radius = radius;
        }

        private void CreateTopPartIndices()
        {
            int cnt;
            for (cnt = TopStartIndex; cnt <= TopStartIndex + (sidesNumber - 2); cnt++)
            {
                indices.AddItem((short)(cnt));
                indices.AddItem((short)(1));
                indices.AddItem((short)(cnt + 1));
            }

            indices.AddItem((short)(cnt));
            indices.AddItem((short)(1));
            indices.AddItem((short)(TopStartIndex));
        }

        private void CreateSidePartIndices()
        {
            int cnt;
            for (cnt = BottomStartIndex; cnt < TopStartIndex - 1; cnt++)
            {
                indices.AddItem((short)(cnt));
                indices.AddItem((short)(cnt + sidesNumber));
                indices.AddItem((short)(cnt + sidesNumber + 1));

                indices.AddItem((short)(cnt));
                indices.AddItem((short)(cnt + sidesNumber + 1));
                indices.AddItem((short)(cnt + 1));
            }

            indices.AddItem((short)(cnt));
            indices.AddItem((short)(cnt + sidesNumber));
            indices.AddItem((short)(TopStartIndex));

            indices.AddItem((short)(cnt));
            indices.AddItem((short)(TopStartIndex));
            indices.AddItem((short)(BottomStartIndex));
        }

        private void CreateBottomPartIndices()
        {
            int cnt;
            for (cnt = BottomStartIndex; cnt <= BottomStartIndex + (sidesNumber - 2); cnt++)
            {
                indices.AddItem((short)(cnt));
                indices.AddItem((short)(0));
                indices.AddItem((short)(cnt + 1));
            }

            indices.AddItem((short)(cnt));
            indices.AddItem((short)(0));
            indices.AddItem((short)(BottomStartIndex));
        }

        private void CreatePoints()
        {
            Vector3 zStep = Vector3Utils.AlignedZVector * Vector3.Length(HalfDirection);

            points.AddItem(-zStep);
            points.AddItem(zStep);

            Matrix rotation = Matrix.RotationZ(-Angle.FromDegrees(360f / sidesNumber).Radians);
            Vector3 perp = Vector3Utils.AlignedXVector * radius;
            Vector3[] basePoints = new Vector3[sidesNumber];

            for (int cnt = 0; cnt < sidesNumber; cnt++)
            {
                basePoints[cnt] = perp;

                perp = Vector3.TransformCoordinate(perp, rotation);
            }

            for (int cnt = BottomStartIndex; cnt <= BottomStartIndex + (sidesNumber - 1); cnt++)
            {
                points.AddItem(basePoints[cnt - BottomStartIndex] - zStep);
            }

            for (int cnt = TopStartIndex; cnt <= TopStartIndex + (sidesNumber - 1); cnt++)
            {
                points.AddItem(basePoints[cnt - TopStartIndex] + zStep);
            }
        }

        private void CreateIndices()
        {
            CreateTopPartIndices();
            CreateSidePartIndices();
            CreateBottomPartIndices();
        }
    }
}
