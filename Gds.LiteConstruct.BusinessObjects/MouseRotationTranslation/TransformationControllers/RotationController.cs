using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using Microsoft.DirectX;
using System.Drawing;
using Microsoft.DirectX.Direct3D;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interactors;
using System.Windows.Forms;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.GraphicManagers;
using Gds.LiteConstruct.BusinessObjects.Properties;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.TransformationControllers
{
    public class RotationController : BufferedTransformationController, IMovable
    {
        protected RotationRingInteractor[] interactors = new RotationRingInteractor[2];
        private RotationRingInteractor activeInteractor;
        private Vector3[] defaultInteractorsPositions = new Vector3[2];

        protected const int SecNumb = 50;
        protected const float SecH = 0.5f;
        protected ProjectionPlane projPlane;
        protected float radius;
        private float radiusPrev;
        protected int iPos;

        protected Vector3 normalVec;
        public Vector3 NormalVec
        {
            get { return normalVec; }
            set 
            { 
                normalVec = value;
                InitProjectionPlane();
                CreateInteractors();
                CreateVertexData(this, null);
                CreateIndexData(this, null);
                //CreateRing();
            }
        }

        protected Matrix translationMat = Matrix.Identity;
        protected Vector3 position;

        private Vector3 look;
        public Vector3 Look
        {
            set
            {
                look = value;
                interactors[0].Look = value;
                interactors[1].Look = value;
            }
        }

        public float Radius
        {
            get { return radius; }
            set 
            {
                radiusPrev = radius;
                radius = value;
                CreateVertexData(this, null);
                UpdateInteractorsPos();
            }
        }

        //protected Vector3[] ringPoints;
        //protected Vector2[] ringPoints;

        public RotationController(Vector3 normalVec, Color color, float radius, Vector3 position) : base(color)
        {
            device = DeviceObject.Device;
            
            //ringPoints = new Vector3[SecNumb];
            //ringPoints = new Vector2[SecNumb];

            this.normalVec = normalVec;
            this.color = color;
            this.radius = radius;
            radiusPrev = radius;
            this.position = position;

            InitProjectionPlane();
            //UpdateAfterTranslation();
            UpdateTranslationMatrix();
            CreateInteractors();
            //CreateRing();
            InitBuffers();
        }

        protected void UpdateTranslationMatrix()
        {
            translationMat.Translate(position);
        }

        //protected void UpdateAfterTranslation()
        //{
        //    UpdateTranslationMatrix();
        //    projPlane.MoveTo(position);
        //}

        protected void InitProjectionPlane()
        {
            projPlane = new ProjectionPlane(normalVec, position);
        }

        protected void ApplyTransparency(int transparency)
        {
            color = Color.FromArgb(transparency, color.R, color.G, color.B);
        }

        //public RotationVector GetRotation(Point point, Point vector)
        //{
        //    Vector3 prevPoint, curPoint;
        //    prevPoint = projPlane.MakeProjection(Ray.GetRayFromScreenCoordinates(point.X, point.Y));
        //    curPoint = projPlane.MakeProjection(Ray.GetRayFromScreenCoordinates(point.X + vector.X, point.Y + vector.Y));

        //    Vector3 prevVec, curVec;
        //    prevVec = prevPoint - position;
        //    curVec = curPoint - position;

        //    return Vector3Utils.TransitionRotationByAxises(prevVec, curVec);
        //}

        public void RestoreInteractors()
        {
            interactors[0].MoveTo(defaultInteractorsPositions[0]);
            interactors[1].MoveTo(defaultInteractorsPositions[1]);
        }

        public AxisAngle GetRotation(Point point, Point vector)
        {
            Vector3 prevPoint, curPoint;
            prevPoint = projPlane.MakeProjection(Ray.GetRayFromScreenCoordinates(point.X, point.Y));
            curPoint = projPlane.MakeProjection(Ray.GetRayFromScreenCoordinates(point.X + vector.X, point.Y + vector.Y));

            Vector3 prevVec, curVec;
            prevVec = prevPoint - position;
            curVec = curPoint - position;

            BindInteractorsToMouse(curVec);

            //--Versioned
            //if (Vector3Utils.VectorsLieOnSameLine(prevVec, curVec, 0.01f) || Vector3Utils.IsZeroVector(prevVec, 0.00001f) || Vector3Utils.IsZeroVector(curVec, 0.00001f))
            //{
            //    return new RotationVector(Angle.A0, Angle.A0, Angle.A0);
            //}
            //else
            //{
            //    return Vector3Utils.TransitionRotationByAxis(prevVec, curVec).ToRotationVector();
            //}
            
            if (Vector3Utils.VectorsLieOnSameLine(prevVec, curVec, 0.01f) || Vector3Utils.IsZeroVector(prevVec, 0.00001f) || Vector3Utils.IsZeroVector(curVec, 0.00001f))
            {
                return new AxisAngle(Quaternion.Identity);
            }
            else
            {
                return Vector3Utils.TransitionRotationByAxis(prevVec, curVec);
            }
        }

        private void BindInteractorsToMouse(Vector3 radiusVec)
        {
            foreach (RotationRingInteractor interactor in interactors)
            {
                if (interactor == activeInteractor)
                {
                    interactor.MoveTo(position + Vector3Utils.SetLength(radiusVec, radius));
                }
                else
                {
                    interactor.MoveTo(position + -Vector3Utils.SetLength(radiusVec, radius));
                }
            }
        }

        private void UpdateInteractorsPos()
        {
            float scaleK;
            scaleK = radius / radiusPrev;

            UpdateConcreteInteractorPos(interactors[0], scaleK);
            UpdateConcreteInteractorPos(interactors[1], scaleK);
        }

        private void UpdateConcreteInteractorPos(IMovable interactor, float scaleK)
        {
            Vector3 newIVec;
            newIVec = interactor.Position - position;
            newIVec *= scaleK;
            interactor.MoveTo(position + newIVec);
        }

        //protected void CreateRing()
        //{
        //    Vector3 startVec, curVec;
        //    Matrix mat = Matrix.Identity;

        //    startVec = Vector3Utils.GetPerpendicularTo(normalVec);
        //    startVec *= radius;

        //    Angle deltaAngle = new Angle(2f * Angle.Pi / SecNumb);
        //    Angle curAngle = Angle.A0;

        //    int ARGBColor = color.ToArgb();
        //    for (int cnt1 = 0; cnt1 < SecNumb; cnt1++)
        //    {
        //        mat.RotateAxis(normalVec, curAngle.Radians);
        //        curVec = Vector3.TransformCoordinate(startVec, mat);

        //        //Vector3 proj = Vector3.TransformCoordinate(curVec, device.Transform.Projection * device.Transform.View);//One more matrix
        //        //ringPoints[cnt1] = new Vector2(proj.X, proj.Y);
        //        ringPoints[cnt1] = curVec;

        //        curAngle += deltaAngle;
        //    }
        //}

        #region Overriden Members

        protected override void CreateInteractors()
        {
            Vector3 startVec = Vector3Utils.GetNormalizedPerpendicularTo(normalVec) * radius;

            Matrix mat = Matrix.Identity;
            Vector3 interactorPosition;

            mat.RotateAxis(normalVec, Angle.A45.Radians);
            interactorPosition = position + Vector3.TransformCoordinate(startVec, mat);
            defaultInteractorsPositions[0] = interactorPosition;
            interactors[0] = new RotationRingInteractor(interactorPosition, new Size2(0.7f, 0.7f), Resources.Interactor, color);

            mat.RotateAxis(normalVec, Angle.A45.Radians + Angle.A180.Radians);
            interactorPosition = position + Vector3.TransformCoordinate(startVec, mat);
            defaultInteractorsPositions[1] = interactorPosition;
            interactors[1] = new RotationRingInteractor(interactorPosition, new Size2(0.7f, 0.7f), Resources.Interactor, color);
        }

        protected override void CreateIndexData(object sender, EventArgs e)
        {
            #region Lines
            short[] indices = (short[])indexBuffer.Lock(0, 0);
            int cnt2 = 0, cnt1 = 0;
            for (cnt1 = 0; cnt1 < SecNumb - 1; cnt1++)
            {
                indices[cnt2] = (short)cnt1;
                cnt2++;
                indices[cnt2] = (short)(cnt1 + 1);
                cnt2++;
            }
            indices[cnt2] = (short)cnt1;
            cnt2++;
            indices[cnt2] = 0;

            indexBuffer.Unlock();
            #endregion

            #region Triangles
            //short[] indices = (short[])indexBuffer.Lock(0, 0);
            //short lastIndex = 0;
            //int cnt2 = 0;
            //for (int cnt1 = 1; cnt1 <= SecNumb - 1; cnt1++)
            //{
            //    indices[cnt2] = lastIndex;
            //    cnt2++;
            //    indices[cnt2] = (short)(lastIndex + 2);
            //    cnt2++;
            //    indices[cnt2] = (short)(lastIndex + 1);
            //    cnt2++;

            //    lastIndex += 2;

            //    indices[cnt2] = lastIndex;
            //    cnt2++;
            //    indices[cnt2] = (short)(lastIndex + 1);
            //    cnt2++;
            //    indices[cnt2] = (short)(lastIndex - 1);
            //    cnt2++;
            //}

            //indices[cnt2] = lastIndex;
            //cnt2++;
            //indices[cnt2] = (short)0;
            //cnt2++;
            //indices[cnt2] = (short)(lastIndex + 1);
            //cnt2++;

            //indices[cnt2] = (short)0;
            //cnt2++;
            //indices[cnt2] = (short)1;
            //cnt2++;
            //indices[cnt2] = (short)(lastIndex + 1);

            //indexBuffer.Unlock();
            #endregion
        }

        protected override void CreateVertexData(object sender, EventArgs e)
        {
            #region Lines
            Vector3 startVec, curVec;
            Matrix mat = Matrix.Identity;

            startVec = Vector3Utils.GetNormalizedPerpendicularTo(normalVec);
            startVec *= radius;

            Angle deltaAngle = new Angle(2f * Angle.Pi / SecNumb);
            Angle curAngle = Angle.A0;

            CustomVertex.PositionColored[] vertices = (CustomVertex.PositionColored[])vertexBuffer.Lock(0, 0);
            int ARGBColor = color.ToArgb();
            for (int cnt1 = 0; cnt1 < SecNumb; cnt1++)
            {
                mat.RotateAxis(normalVec, curAngle.Radians);
                curVec = Vector3.TransformCoordinate(startVec, mat);

                vertices[cnt1].Position = curVec;
                vertices[cnt1].Color = ARGBColor;

                curAngle += deltaAngle;
            }

            vertexBuffer.Unlock();
            #endregion

            #region Triangles
            //Vector3 startVec, curVec;
            //Matrix mat = Matrix.Identity;

            //startVec = Vector3Utils.GetPerpendicularTo(normalVec);
            //startVec *= radius;

            //Angle deltaAngle = new Angle(2f * Angle.Pi / SecNumb);
            //Angle curAngle = Angle.A0;

            //CustomVertex.PositionColored[] vertices = (CustomVertex.PositionColored[])vertexBuffer.Lock(0, 0);
            //int ARGBColor = color.ToArgb();
            //int cnt2 = 0;
            //for (int cnt1 = 1; cnt1 <= SecNumb; cnt1++)
            //{
            //    mat.RotateAxis(normalVec, curAngle.Radians);
            //    curVec = Vector3.TransformCoordinate(startVec, mat);

            //    vertices[cnt2].Position = curVec;
            //    vertices[cnt2].Color = ARGBColor;
            //    cnt2++;
            //    vertices[cnt2].Position = Vector3.Normalize(curVec) * (radius - SecH);
            //    vertices[cnt2].Color = ARGBColor;
            //    cnt2++;

            //    curAngle += deltaAngle;
            //}

            //vertexBuffer.Unlock();
            #endregion
        }

        protected override int VerticesCount
        {
            get { return SecNumb; }
            //get { return 2 * SecNumb; } Triangles
        }

        protected override int IndicesCount
        {
            get { return 2 * SecNumb; }
            //get { return 6 * SecNumb; } Triangles
        }

        protected override void ApplyColorToVertices(Color color)
        {
            CustomVertex.PositionColored[] vertices = (CustomVertex.PositionColored[])vertexBuffer.Lock(0, 0);

            int colorARGB = color.ToArgb();
            for (int cnt1 = 0; cnt1 < VerticesCount; cnt1++)
            {
                vertices[cnt1].Color = colorARGB;
            }

            vertexBuffer.Unlock();
        }

        protected override void ApplyTransparencyToVertices(int transparency)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IControllerPresenter Members

        public override void MakePrepared()
        {
            if (bManager != null)
            {
                bManager.StopAction();
            }
            if (sManager != null)
            {
                sManager.StopAction();
            }
            
            bManager = new BrightnessManager(color, 100f, 9.5f, 10);
            bManager.ConnectTo(new IColorable[2] { this, activeInteractor });
            bManager.StartAction();

            sManager = new ScaleManager(1f, 0.5f, 0.07f, 10);
            sManager.ConnectTo(new IScalable[1] { activeInteractor });
            sManager.StartAction();
        }

        public override void MakeUnprepared()
        {
            bManager.StopAction();
            sManager.StopAction();

            bManager.Rewind();
            sManager.Rewind();
        }

        public override void MakeActive()
        {
            //tManager.Rewind();
        }

        public override void MakeUnactive()
        {
            //tManager = new TransparencyManager(255, -100, 0.7f, 10);
            //tManager.ConnectTo(new ITranspareable[1] { this });
            //tManager.StartAction();
        }

        public override bool InteractedWithMouse(Point mousePos)
        {
            for (int cnt1 = 0; cnt1 < interactors.Length; cnt1++)
            {
                if (interactors[cnt1].Interacted(mousePos))
                {
                    iPos = cnt1;
                    activeInteractor = interactors[iPos];

                    return true;
                }
            }

            iPos = -1;
            return false;
        }

        public override Vector3 InteractionPoint
        {
            get { return interactors[iPos].InteractionPoint; }
        }

        public override void Render()
        {
            #region Render with lines 3D
            //using (Line line = new Line(device))
            //{
            //    line.Width = 3f;

            //    Vector3[] linePoints = new Vector3[2];
            //    int cnt1;
            //    for (cnt1 = 0; cnt1 < SecNumb - 1; cnt1++)
            //    {
            //        linePoints[0] = ringPoints[cnt1];
            //        linePoints[1] = ringPoints[cnt1 + 1];

            //        line.Begin();
            //        line.DrawTransform(linePoints, translationMat, color.ToArgb());
            //        line.End();
            //    }

            //    linePoints[0] = ringPoints[cnt1];
            //    linePoints[1] = ringPoints[0];

            //    line.Begin();
            //    line.DrawTransform(linePoints, translationMat, color.ToArgb());
            //    line.End();
            //}

            #endregion

            #region Render with lines 2D
            //using (Line line = new Line(device))
            //{
            //    line.Width = 3f;

            //    Vector2[] linePoints = new Vector2[2];
            //    int cnt1;
            //    for (cnt1 = 0; cnt1 < SecNumb - 1; cnt1++)
            //    {
            //        linePoints[0] = ringPoints[cnt1];
            //        linePoints[1] = ringPoints[cnt1 + 1];

            //        line.Begin();
            //        line.Draw(linePoints, color.ToArgb());
            //        line.End();
            //    }

            //    linePoints[0] = ringPoints[cnt1];
            //    linePoints[1] = ringPoints[0];

            //    line.Begin();
            //    line.Draw(linePoints, color.ToArgb());
            //    line.End();
            //}
            #endregion

            device.Transform.World = translationMat;

            device.VertexFormat = CustomVertex.PositionColored.Format;
            device.SetStreamSource(0, vertexBuffer, 0);
            device.Indices = indexBuffer;

            device.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, SecNumb, 0, SecNumb);

            foreach (RotationRingInteractor interactor in interactors)
            {
                interactor.Render();
            }
        }

        public override void Accept(IResizableVisitorPresenter visitor)
        {
            visitor.Visit(this);
        }

        public override void Dispose()
        {
            base.Dispose();

            foreach (MouseInteractor3D interactor in interactors)
            {
                interactor.Dispose();
            }
        }

        #endregion

        #region IColorable Members

        public override void SetColor(Color color)
        {
            ApplyColorToVertices(color);
        }

        #endregion

        #region ITranspareable Members

        public override void SetTransparency(int transparency)
        {
            ApplyTransparency(transparency);

            foreach (RotationRingInteractor interactor in interactors)
            {
                interactor.SetTransparency(transparency);
            }
        }

        #endregion

        #region IMovable Members

        public void MoveTo(Vector3 pos)
        {
            position = pos;
            UpdateTranslationMatrix();
            projPlane.MoveTo(pos);
            interactors[0].MoveTo(pos);
            interactors[1].MoveTo(pos);
        }

        public void MoveBy(Vector3 mpos)
        {
            position += mpos;
            UpdateTranslationMatrix();
            projPlane.MoveBy(mpos);
            interactors[0].MoveBy(mpos);
            interactors[1].MoveBy(mpos);
        }

        public void MoveByX(float mx)
        {
            position += new Vector3(mx, 0f, 0f);
            UpdateTranslationMatrix();
            projPlane.MoveByX(mx);
            interactors[0].MoveByX(mx);
            interactors[1].MoveByX(mx);
        }

        public void MoveByY(float my)
        {
            position += new Vector3(0f, my, 0f);
            UpdateTranslationMatrix();
            projPlane.MoveByY(my);
            interactors[0].MoveByY(my);
            interactors[1].MoveByY(my);
        }

        public void MoveByZ(float mz)
        {
            position += new Vector3(0f, 0f, mz);
            UpdateTranslationMatrix();
            projPlane.MoveByZ(mz);
            interactors[0].MoveByZ(mz);
            interactors[1].MoveByZ(mz);
        }

        public void MoveX(float x)
        {
            position = new Vector3(x, 0f, 0f);
            UpdateTranslationMatrix();
            projPlane.MoveX(x);
            interactors[0].MoveX(x);
            interactors[1].MoveX(x);
        }

        public void MoveY(float y)
        {
            position = new Vector3(0f, y, 0f);
            UpdateTranslationMatrix();
            projPlane.MoveY(y);
            interactors[0].MoveY(y);
            interactors[1].MoveY(y);
        }

        public void MoveZ(float z)
        {
            position = new Vector3(0f, 0f, z);
            UpdateTranslationMatrix();
            projPlane.MoveZ(z);
            interactors[0].MoveZ(z);
            interactors[1].MoveZ(z);
        }

        public Vector3 Position
        {
            get { return position; }
            set { MoveTo(value); }
        }

        #endregion
    }
}
