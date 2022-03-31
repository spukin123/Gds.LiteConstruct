using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using Microsoft.DirectX;
using System.Drawing;
using Microsoft.DirectX.Direct3D;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interactors
{
    public class ArrowMouseInteractor : CylindricalInteractor, IScalable, IColorable, ITranspareable
    {
        private float arrowTipLength;
        private Angle arrowTipAngle;

        private GeometricalFigure cone;
        private GeometricalFigure arrow;

        private Ray TubePart
        {
            get { return new Ray(axis.Position, Vector3Utils.SetLength(axis.Direction, TubePartLen)); }
        }

        private float TubePartLen
        {
            get { return axis.Direction.Length() - arrowTipLength; }
        }

        private int ArrowVerticesBeginAt
        {
            get { return cylinder.PointsCount; }
        }

        private int ArrowIndicesBeginAt
        {
            get { return cylinder.IndicesCount; }
        }

        private Vector3 ArrowBaseVector
        {
            get { return Vector3Utils.AlignedZVector * (axis.Direction.Length() / 2f - arrowTipLength); }
        }

        private Vector3 ArrowTipVector
        {
            get { return ArrowBaseVector + Vector3Utils.AlignedZVector * arrowTipLength; }
        }

        public ArrowMouseInteractor(Ray axis, int sidesNumber, float radius, float arrowTipLength, Angle arrowTipAngle, Color color)
            : base(axis, sidesNumber, radius, color)
        {
            this.arrowTipLength = arrowTipLength;
            this.arrowTipAngle = arrowTipAngle;

            cylinder = new Cylinder(TubePart, sidesNumber, radius);
            cone = new Cone(arrowTipLength, GetArrowRadius(), sidesNumber);
            cylinder.TranslateBy(-Vector3Utils.AlignedZVector * arrowTipLength * 0.5f);
            cone.TranslateBy(ArrowBaseVector + Vector3Utils.AlignedZVector * arrowTipLength * 0.5f);
            arrow = GeometricalFigure.Merge(cylinder, cone);

            InitStartRotation();
            InitBuffers();
            InitStruct();

            Origin = axis.Position + HalfDirection;
        }

        private float GetArrowRadius()
        {
            RightTriangle triangle;
            triangle = RightTriangle.FromAdjacentAndAlpha(arrowTipLength, new Angle(arrowTipAngle.Radians / 2f));

            return triangle.Opposite;
        }

        private void CreateTubePartVertices()
        {
            CustomVertex.PositionColored[] vertices = LockVB();

            for (int cnt = 0; cnt < ArrowVerticesBeginAt; cnt++)
            {
                vertices[cnt].Position = cylinder.Points[cnt] - Vector3Utils.AlignedZVector * arrowTipLength * 0.5f;
                vertices[cnt].Color = defaultColor.ToArgb();
            }

            UnlockVB();
        }

        private void CreateArrowPartVertices()
        {
            Vector3[] basePoints = new Vector3[sidesNumber];
            Vector3 arrowTipPoint = Vector3Utils.ZeroVector;
            Vector3 startVector = new Vector3(0f, -1f, 0f);
            Matrix rotation = Matrix.RotationZ(2f * Angle.Pi / sidesNumber);

            float arrowRadius;
            arrowRadius = GetArrowRadius();

            for (int cnt = 0; cnt < sidesNumber; cnt++)
            {
                basePoints[cnt] = startVector * arrowRadius;
                startVector = Vector3.TransformCoordinate(startVector, rotation);
            }
            int vertexCnt = ArrowVerticesBeginAt;

            CustomVertex.PositionColored[] vertices = LockVB();

            vertices[vertexCnt].Position = arrowTipPoint + ArrowTipVector;
            vertices[vertexCnt].Color = defaultColor.ToArgb();
            vertexCnt++;

            for (int cnt = 0; cnt < sidesNumber; cnt++)
            {
                vertices[vertexCnt + cnt].Position = basePoints[cnt] + ArrowBaseVector;
                vertices[vertexCnt + cnt].Color = defaultColor.ToArgb();
            }

            UnlockVB();
        }

        private void CreateTubePartIndices()
        {
            short[] indices = LockIB();

            for (int cnt = 0; cnt < ArrowIndicesBeginAt; cnt++)
            {
                indices[cnt] = cylinder.Indices[cnt];
            }

            UnlockIB();
        }

        private void CreateArrowPartIndices()
        {
            short[] indices = LockIB();

            //int indexCnt = ArrowIndicesBeginAt;
            //for (int cnt = 0; cnt < sidesNumber - 1; cnt++)
            //{
            //    indices[indexCnt] = (short)(ArrowVerticesBeginAt + 0);
            //    indexCnt++;
            //    indices[indexCnt] = (short)(ArrowVerticesBeginAt + cnt + 1);
            //    indexCnt++;
            //    indices[indexCnt] = (short)(ArrowVerticesBeginAt + cnt + 2);
            //    indexCnt++;
            //}

            //indices[indexCnt] = (short)(ArrowVerticesBeginAt + 0);
            //indexCnt++;
            //indices[indexCnt] = (short)(ArrowVerticesBeginAt + sidesNumber);
            //indexCnt++;
            //indices[indexCnt] = (short)(ArrowVerticesBeginAt + 1);

            UnlockIB();
        }

        #region Overriden Members

        protected override int TrianglesNumber
        {
            get { return arrow.IndicesCount / 3; }
        }

        protected override int VerticesCount
        {
            get { return arrow.PointsCount; }
        }

        protected override int IndicesCount
        {
            get { return arrow.IndicesCount; }
        }

        protected override void CreateVertexData(object sender, EventArgs e)
        {
            CustomVertex.PositionColored[] vertices = LockVB();

            for (int cnt = 0; cnt < arrow.PointsCount; cnt++)
            {
                vertices[cnt].Position = arrow.Points[cnt];
                vertices[cnt].Color = defaultColor.ToArgb();
            }

            UnlockVB();

            //CreateTubePartVertices();
            //CreateArrowPartVertices();
        }

        protected override void CreateIndexData(object sender, EventArgs e)
        {
            short[] indices = LockIB();

            for (int cnt = 0; cnt < arrow.IndicesCount; cnt++)
            {
                indices[cnt] = arrow.Indices[cnt];
            }

            UnlockIB();

            
            //CreateTubePartIndices();
            //CreateArrowPartIndices();
        }

        #endregion

        #region IScalable Members

        public void SetScale(float scaleFactor)
        {
            scaleMatrix = Matrix.Scaling(scaleFactor, scaleFactor, scaleFactor);
            ApplyChangesToStruct();
        }

        #endregion

        #region IColorable Members

        public void SetColor(Color color)
        {
            defaultColor = color;
            ApplyColorToVertices();
        }

        #endregion

        #region ITranspareable Members

        public void SetTransparency(int transparency)
        {
            defaultColor = Color.FromArgb(transparency, defaultColor.R, defaultColor.G, defaultColor.B);
            ApplyColorToVertices();
        }

        #endregion
    }
}
