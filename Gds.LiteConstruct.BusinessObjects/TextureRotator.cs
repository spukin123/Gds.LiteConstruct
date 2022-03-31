using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Gds.LiteConstruct.BusinessObjects
{
    public enum TextureRotationType { Cut, Compress }

    public class TransformedPoint
    {
        private Vertex originalPosition;
        public Vertex OriginalPosition
        {
            get { return originalPosition; }
            set { originalPosition = value; }
        }
        
        private Vector2 transformedPosition;
        public Vector2 TransformedPosition
        {
          get { return transformedPosition; }
          set { transformedPosition = value; }
        }

        public TransformedPoint()
        {
        }

        public TransformedPoint(Vertex originalPosition, Vector2 transformedPosition)
        {
            this.originalPosition = originalPosition;
            this.transformedPosition = transformedPosition;
        }
    }

    public class TargetPoint
    {
        private TransformedPoint transformedPoint;
        public TransformedPoint TransformedPointProp
        {
            get { return transformedPoint; }
            set { transformedPoint = value; }
        }

        private float tU;
        public float TU
        {
            get { return tU; }
            set { tU = value; }
        }

        private float tV;
        public float TV
        {
            get { return tV; }
            set { tV = value; }
        }

        public TargetPoint()
        {
        }

        public TargetPoint(TransformedPoint transformedPoint)
        {
            this.transformedPoint = transformedPoint;
        }
    }

    public class TextureRotator : ITextureRotatable
    {
        private TargetPoint[] targetPoints;
        private Dictionary<Vertex, TargetPoint> output;
        
        private Vector2 leftTop;
        private Angle angle;
        private float minX, maxX;
        private float minY, maxY;
        private float geometryWidth;
        private float geometryHeight;
        private Angle curAngle = Angle.A0;

        public TextureRotator(TransformedPoint[] points, TextureRotationType rotationType)
        {
            ConvertToTargetArray(points);
            Centralize();
            
            if (rotationType == TextureRotationType.Compress)
            {
                Scale();
            }

            GenerateOutput();
            Rotate(Angle.A0);
        }

        private void Scale()
        {
            float width, height;
            width = maxX - minX;
            height = maxY - minY;

            float yFactor;
            yFactor = width / height;

            Vector2 position;
            for (int cnt = 0; cnt < targetPoints.Length; cnt++)
            {
                position = targetPoints[cnt].TransformedPointProp.TransformedPosition;
                targetPoints[cnt].TransformedPointProp.TransformedPosition = new Vector2(position.X, position.Y * yFactor);
            }
        }

        private void ConvertToTargetArray(TransformedPoint[] points)
        {
            targetPoints = new TargetPoint[points.Length];
            for (int cnt = 0; cnt < points.Length; cnt++)
            {
                targetPoints[cnt] = new TargetPoint(points[cnt]);
            }
        }

        private void Centralize()
        {
            minX = targetPoints[0].TransformedPointProp.TransformedPosition.X;
            maxX = targetPoints[0].TransformedPointProp.TransformedPosition.X;

            minY = targetPoints[0].TransformedPointProp.TransformedPosition.Y;
            maxY = targetPoints[0].TransformedPointProp.TransformedPosition.Y;

            for (int cnt = 1; cnt < targetPoints.Length; cnt++)
            {
                CheckMinX(cnt);
                CheckMaxX(cnt);
                CheckMinY(cnt);
                CheckMaxY(cnt);
            }

            Vector2 centerPoint;
            centerPoint = new Vector2(minX + ((maxX - minX) / 2f), minY + ((maxY - minY) / 2f));

            for (int cnt = 0; cnt < targetPoints.Length; cnt++)
            {
                targetPoints[cnt].TransformedPointProp.TransformedPosition -= centerPoint;
            }
        }

        private void RotateGeometry()
        {
            Matrix matrix = Matrix.Identity;
            matrix.RotateZ(angle.Radians);

            for (int cnt = 0; cnt < targetPoints.Length; cnt++)
            {
                //targetPoints[cnt].TransformedPoint.TransformedPosition.TransformCoordinate(matrix);
                targetPoints[cnt].TransformedPointProp.TransformedPosition = Vector2.TransformCoordinate(targetPoints[cnt].TransformedPointProp.TransformedPosition, matrix);
            }
        }

        private void FindLeftTopGeometryPosition()
        {
            minX = targetPoints[0].TransformedPointProp.TransformedPosition.X;
            minY = targetPoints[0].TransformedPointProp.TransformedPosition.Y;
            for (int cnt = 1; cnt < targetPoints.Length; cnt++)
            {
                CheckMinX(cnt);
                CheckMinY(cnt);
            }

            leftTop = new Vector2(minX, minY);
        }

        private void ProcessRotation()
        {
            RotateGeometry();
            FindLeftTopGeometryPosition();
            
            MoveToTextureSpace();
            GenerateTextureCoordinates();
            MoveToOriginSpace();
        }

        private void GenerateOutput()
        {
            output = new Dictionary<Vertex, TargetPoint>();

            foreach (TargetPoint point in targetPoints)
            {
                output.Add(point.TransformedPointProp.OriginalPosition, point);
            }
        }

        private void GenerateTextureCoordinates()
        {
            FindGeometryWidthHeight();

            for (int cnt = 0; cnt < targetPoints.Length; cnt++)
            {
                targetPoints[cnt].TU = targetPoints[cnt].TransformedPointProp.TransformedPosition.X / geometryWidth;
                targetPoints[cnt].TV = targetPoints[cnt].TransformedPointProp.TransformedPosition.Y / geometryHeight;
            }
        }

        private void FindGeometryWidthHeight()
        {
            maxX = targetPoints[0].TransformedPointProp.TransformedPosition.X;
            maxY = targetPoints[0].TransformedPointProp.TransformedPosition.Y;
            for (int cnt = 1; cnt < targetPoints.Length; cnt++)
            {
                CheckMaxX(cnt);
                CheckMaxY(cnt);
            }

            geometryWidth = maxX;
            geometryHeight = maxY;
        }

        private void MoveToTextureSpace()
        {
            foreach (TargetPoint point in targetPoints)
            {
                point.TransformedPointProp.TransformedPosition -= leftTop;
            }
        }

        private void MoveToOriginSpace()
        {
            foreach (TargetPoint point in targetPoints)
            {
                point.TransformedPointProp.TransformedPosition += leftTop;
            }
        }

        private void CheckMinX(int cnt)
        {
            if (targetPoints[cnt].TransformedPointProp.TransformedPosition.X < minX)
            {
                minX = targetPoints[cnt].TransformedPointProp.TransformedPosition.X;
            }
        }

        private void CheckMaxX(int cnt)
        {
            if (targetPoints[cnt].TransformedPointProp.TransformedPosition.X > maxX)
            {
                maxX = targetPoints[cnt].TransformedPointProp.TransformedPosition.X;
            }
        }

        private void CheckMinY(int cnt)
        {
            if (targetPoints[cnt].TransformedPointProp.TransformedPosition.Y < minY)
            {
                minY = targetPoints[cnt].TransformedPointProp.TransformedPosition.Y;
            }
        }

        private void CheckMaxY(int cnt)
        {
            if (targetPoints[cnt].TransformedPointProp.TransformedPosition.Y > maxY)
            {
                maxY = targetPoints[cnt].TransformedPointProp.TransformedPosition.Y;
            }
        }

        #region ITextureRotatable Members

        public Angle CurrentAngle
        {
            get { return -curAngle; }
        }

        public void RotateRightBy(Angle angle)
        {
            this.angle = -angle;
            curAngle += this.angle;

            ProcessRotation();
        }

        public void RotateLeftBy(Angle angle)
        {
            this.angle = angle;
            curAngle += this.angle;

            ProcessRotation();
        }

        public void Rotate(Angle angle)
        {
            angle = -angle;
            this.angle = angle - curAngle;
            curAngle += this.angle;

            ProcessRotation();
        }

        public Vector2 GetTextureCoordinatesForPoint(Vertex point)
        {
            TargetPoint node;
            node = output[point];

            return new Vector2(node.TU, node.TV);
        }

        #endregion
    }
}
