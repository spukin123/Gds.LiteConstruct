using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using Microsoft.DirectX;
using System.Drawing;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interactors;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;
using Microsoft.DirectX.Direct3D;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.GraphicManagers;
using Gds.LiteConstruct.BusinessObjects.Properties;

namespace Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.TransformationControllers
{
    public class TranslationAxisController : BufferedTransformationController, ITranslationControllerPresenter, IBillboard
    {
        protected RotationRingInteractor[] interactors = new RotationRingInteractor[2]; 
        private RotationRingInteractor activeInteractor;

        protected Ray axis;
        protected const float delta = 2.8f;
        protected const int TrianglesOffsetInIB = 2;
        protected const int SecNum = 8;
        private int iPos;

        protected const float revVecLen = 1.3f;
        protected Angle revVecAngle = Angle.A15;

        protected Matrix translationMat = Matrix.Identity;
        protected ProjectionPlane p1, p2;

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

        public float AxisLen
        {
            set 
            {
                UpdateAxisesLen(value);
            }
        }

        public TranslationAxisController(Ray axis, Color color) : base(color)
        {
            this.axis = axis;
            UpdateTranslationMatrix();

            device = DeviceObject.Device;

            CreateInteractors();
            InitBuffers();
        }

        protected void UpdateTranslationMatrix()
        {
            translationMat = Matrix.Translation(axis.Position);
        }

        protected void CreateNormals()
        {
            CustomVertex.PositionNormalColored[] vertices = (CustomVertex.PositionNormalColored[])vertexBuffer.Lock(0, 0);
            //short[] indices = (short[])indexBuffer.Lock(0, 0);

            short firstIndex, lastIndex, arrowEdgeIndex;
            firstIndex = 3;//(short)(indices[TrianglesOffsetInIB] + 2);
            lastIndex = (short)(firstIndex + SecNum - 3);
            arrowEdgeIndex = 1;//indices[TrianglesOffsetInIB];

            //indexBuffer.Unlock();

            short[] triangle1 = new short[3];
            short[] triangle2 = new short[3];
            for (short cnt1 = firstIndex; cnt1 <= lastIndex; cnt1++)
            {
                triangle1[0] = cnt1;
                triangle1[1] = arrowEdgeIndex;
                triangle1[2] = (short)(cnt1 - 1);

                triangle2[0] = cnt1;
                triangle2[1] = (short)(cnt1 + 1);
                triangle2[2] = arrowEdgeIndex;

                vertices[cnt1].Normal = CalculateNormal(vertices, triangle1, triangle2);
            }

            firstIndex--;
            lastIndex++;

            triangle1[0] = firstIndex;
            triangle1[1] = arrowEdgeIndex;
            triangle1[2] = lastIndex;

            triangle2[0] = firstIndex;
            triangle2[1] = (short)(firstIndex + 1);
            triangle2[2] = arrowEdgeIndex;

            vertices[firstIndex].Normal = CalculateNormal(vertices, triangle1, triangle2);

            triangle1[0] = lastIndex;
            triangle1[1] = arrowEdgeIndex;
            triangle1[2] = (short)(lastIndex - 1);

            triangle2[0] = lastIndex;
            triangle2[1] = firstIndex;
            triangle2[2] = arrowEdgeIndex;

            vertices[lastIndex].Normal = CalculateNormal(vertices, triangle1, triangle2);
            vertices[arrowEdgeIndex].Normal = Vector3.Normalize(axis.Direction);

            vertexBuffer.Unlock();
        }

        protected Vector3 GetNormalFromTriangle(CustomVertex.PositionNormalColored[] vertices, short[] triangle)
        {
            Plane plane;
            plane = Plane.FromPoints(vertices[triangle[0]].Position, vertices[triangle[1]].Position, vertices[triangle[2]].Position);
            return Vector3.Normalize(new Vector3(plane.A, plane.B, plane.C));
        }

        protected Vector3 CalculateNormal(CustomVertex.PositionNormalColored[] vertices, short[] triangle1, short[] triangle2)
        {
            Vector3 normal1, normal2;
            normal1 = GetNormalFromTriangle(vertices, triangle1);
            normal2 = GetNormalFromTriangle(vertices, triangle2);

            return Vector3.Normalize(normal1 + normal2);
        }

        private void tManager_RewindFinished(object sender, EventArgs e)
        {
            interactors[0].Transparent = false;
            interactors[1].Transparent = false;
            tManager.RewindFinished -= tManager_RewindFinished;
        }

        private void UpdateAxisesLen(float newLen)
        {
            axis.Direction = Vector3Utils.SetLength(axis.Direction, newLen);
            CreateVertexData(this, null);
            interactors[0].MoveTo(axis.Position + Vector3Utils.SetLength(axis.Direction, axis.Direction.Length() - delta));
            interactors[1].MoveTo(axis.Position - Vector3Utils.SetLength(axis.Direction, axis.Direction.Length() - delta));
        }

        #region Overiden Members
        
        protected override void CreateInteractors()
        {
            interactors[0] = new RotationRingInteractor(axis.Position + Vector3Utils.SetLength(axis.Direction, axis.Direction.Length() - delta), new Size2(0.7f, 0.7f), Resources.Interactor, color);
            interactors[1] = new RotationRingInteractor(axis.Position - Vector3Utils.SetLength(axis.Direction, axis.Direction.Length() - delta), new Size2(0.7f, 0.7f), Resources.Interactor, color);
        }
        
        protected override void InitBuffers()
        {
            vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionNormalColored), VerticesCount, device, Usage.Dynamic, CustomVertex.PositionNormalColored.Format, Pool.Default);
            vertexBuffer.Created += new EventHandler(CreateVertexData);
            indexBuffer = new IndexBuffer(typeof(short), IndicesCount, device, Usage.WriteOnly, Pool.Default);
            indexBuffer.Created += new EventHandler(CreateIndexData);

            CreateVertexData(vertexBuffer, new EventArgs());
            CreateIndexData(indexBuffer, new EventArgs());
            CreateNormals();
        }

        protected override void CreateVertexData(object sender, EventArgs e)
        {
            CustomVertex.PositionNormalColored[] vertices = (CustomVertex.PositionNormalColored[])vertexBuffer.Lock(0, 0);
            
            int colorARGB = color.ToArgb();
            vertices[0].Position = -axis.Direction;
            vertices[0].Color = colorARGB;
            vertices[1].Position = axis.Direction;
            vertices[1].Color = colorARGB;

            Vector3 revVec;
            revVec = -axis.Direction;
            revVec = Vector3Utils.SetLength(revVec, revVecLen);

            Vector3 perp, startVec;
            perp = Vector3Utils.GetNormalizedPerpendicularTo(axis.Direction);
            startVec = Vector3.TransformCoordinate(revVec, Matrix.RotationAxis(perp, revVecAngle.Radians));

            Angle deltaAngle = new Angle(2f * Angle.Pi / SecNum);
            Angle curAngle = Angle.A0;
            Matrix rotMat = Matrix.Identity;
            int cnt1;
            for (cnt1 = 2; cnt1 < SecNum + 2; cnt1++)
            {
                rotMat.RotateAxis(axis.Direction, curAngle.Radians);
                vertices[cnt1].Position = axis.Direction + Vector3.TransformCoordinate(startVec, rotMat);
                vertices[cnt1].Color = colorARGB;
                vertices[cnt1].Normal = new Vector3(-0.02f, 0.6f, 0.3f);
                
                curAngle += deltaAngle;
            }

            vertexBuffer.Unlock();
        }

        protected override void CreateIndexData(object sender, EventArgs e)
        {
            short[] indices = (short[])indexBuffer.Lock(0, 0);

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 1;

            int cnt2 = TrianglesOffsetInIB;
            int cnt1;
            for (cnt1 = 1; cnt1 < SecNum; cnt1++)
            {
                indices[cnt2] = indices[TrianglesOffsetInIB];
                cnt2++;
                indices[cnt2] = (short)(cnt1 + indices[TrianglesOffsetInIB]);
                cnt2++;
                indices[cnt2] = (short)(cnt1 + indices[TrianglesOffsetInIB] + 1);
                cnt2++;
            }
            indices[cnt2] = indices[TrianglesOffsetInIB];
            cnt2++;
            indices[cnt2] = (short)(cnt1 + indices[TrianglesOffsetInIB]);
            cnt2++;
            indices[cnt2] = (short)(indices[TrianglesOffsetInIB] + 1);

            indexBuffer.Unlock();
        }

        protected override int VerticesCount
        {
            get { return SecNum + 2; }
        }

        protected override int IndicesCount
        {
            get { return SecNum * 3 + 2; }
        }

        protected override void ApplyColorToVertices(Color color)
        {
            CustomVertex.PositionNormalColored[] vertices = (CustomVertex.PositionNormalColored[])vertexBuffer.Lock(0, 0);

            int colorARGB = color.ToArgb();
            for (int cnt1 = 0; cnt1 < VerticesCount; cnt1++)
            {
                vertices[cnt1].Color = colorARGB;
            }

            vertexBuffer.Unlock();
        }

        protected override void ApplyTransparencyToVertices(int transparency)
        {
            ApplyColorToVertices(Color.FromArgb(transparency, color));
        }

        #endregion

        #region ITranslationControllerMembers
        
        public Vector3 GetTranslationVector(Point point, Point vector)
        {
            //- Transform to screen space
            Point[] axisPoints;
            axisPoints = axis.ProjectToScreenSpace();

            Vector2 axisStartPoint, axisDirection;
            axisStartPoint = PointConverter.PointToVector2(axisPoints[0]);
            axisDirection = PointConverter.PointToVector2(axisPoints[1]) - PointConverter.PointToVector2(axisPoints[0]);

            //- Find projection on projected axis vector
            Vector2 projPoint1, projPoint2;
            projPoint1 = Vector2Utils.ProjectPointOnLine(PointConverter.PointToVector2(point), axisStartPoint, axisDirection);
            projPoint2 = Vector2Utils.ProjectPointOnLine(PointConverter.PointToVector2(point) + PointConverter.PointToVector2(vector), axisStartPoint, axisDirection);

            //- Go back to world by this two rays
            Ray ray1, ray2;
            ray1 = Ray.GetRayFromScreenCoordinates(projPoint1.X, projPoint1.Y);
            ray2 = Ray.GetRayFromScreenCoordinates(projPoint2.X, projPoint2.Y);

            //- Find cross with real axis in world
            Vector3 worldPoint1, worldPoint2;
            worldPoint1 = ray1.Position + (Vector3Utils.SetLength(ray1.Direction, ray1.CrossWith(axis)));
            worldPoint2 = ray2.Position + (Vector3Utils.SetLength(ray2.Direction, ray2.CrossWith(axis)));

            //- Find move vector
            Vector3 moveVec = worldPoint2 - worldPoint1;
            if (Vector3Utils.VectorsHaveSameDirection(moveVec, axis.Direction))
            {
                return Vector3Utils.SetLength(axis.Direction, moveVec.Length());
            }
            else
            {
                return -Vector3Utils.SetLength(axis.Direction, moveVec.Length());
            }
        }

        public bool IsBillboard
        {
            get { return true; }
        }

        public void MoveTo(Vector3 pos)
        {
            axis.Position = pos;
            UpdateTranslationMatrix();
            interactors[0].MoveTo(pos);
            interactors[1].MoveTo(pos);
        }

        public void MoveBy(Vector3 mpos)
        {
            axis.Position += mpos;
            UpdateTranslationMatrix();
            interactors[0].MoveBy(mpos);
            interactors[1].MoveBy(mpos);
        }

        public void MoveByX(float mx)
        {
            axis.Position += new Vector3(mx, 0f, 0f);
            UpdateTranslationMatrix();
            interactors[0].MoveByX(mx);
            interactors[1].MoveByX(mx);
        }

        public void MoveByY(float my)
        {
            axis.Position += new Vector3(0f, my, 0f);
            UpdateTranslationMatrix();
            interactors[0].MoveByY(my);
            interactors[1].MoveByY(my);
        }

        public void MoveByZ(float mz)
        {
            axis.Position += new Vector3(0f, 0f, mz);
            UpdateTranslationMatrix();
            interactors[0].MoveByZ(mz);
            interactors[1].MoveByZ(mz);
        }

        public void MoveX(float x)
        {
            axis.Position = new Vector3(x, 0f, 0f);
            UpdateTranslationMatrix();
            interactors[0].MoveX(x);
            interactors[1].MoveX(x);
        }

        public void MoveY(float y)
        {
            axis.Position = new Vector3(0f, y, 0f);
            UpdateTranslationMatrix();
            interactors[0].MoveY(y);
            interactors[1].MoveY(y);
        }

        public void MoveZ(float z)
        {
            axis.Position = new Vector3(0f, 0f, z);
            UpdateTranslationMatrix();
            interactors[0].MoveZ(z);
            interactors[1].MoveZ(z);
        }

        public Vector3 Position
        {
            get { return axis.Position; }
            set { MoveTo(value); }
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
            //tManager.StopAction();
            //tManager.RewindFinished += new EventHandler(tManager_RewindFinished);
            //tManager.Rewind();
        }

        public override void MakeUnactive()
        {
            //tManager = new TransparencyManager(255, -100, 5.7f, 10);
            //tManager.ConnectTo(new ITranspareable[1] { this });
            //tManager.StartAction();

            //interactor1.Transparent = true;
            //interactor2.Transparent = true;
        }

        public override bool InteractedWithMouse(Point mousePos)
        {
            //if (interactor1.Interacted(mousePos))
            //{
            //    activeInteractor = interactor1;
            //    return true;
            //}
            //else if (interactor2.Interacted(mousePos))
            //{
            //    activeInteractor = interactor2;
            //    return true;
            //}
            //else
            //{
            //    activeInteractor = null;
            //}

            //return false;
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
            get { return activeInteractor.InteractionPoint; }
        }

        public override void Render()
        {
            device.Transform.World = translationMat;
            device.RenderState.CullMode = Cull.None;

            device.Lights[0].Type = LightType.Directional;
            device.Lights[0].Direction = new Vector3(-0.2f, 0f, 0f);
            device.Lights[0].Diffuse = Color.White;
            device.Lights[0].Enabled = true;

            device.VertexFormat = CustomVertex.PositionNormalColored.Format;
            device.SetStreamSource(0, vertexBuffer, 0);
            device.Indices = indexBuffer;

            device.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, VerticesCount, 0, 1);
            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, VerticesCount, 2, SecNum);

            device.Lights[0].Enabled = false;

            foreach (MouseInteractor3D interactor in interactors)
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
            ApplyTransparencyToVertices(transparency);

            foreach (RotationRingInteractor interactor in interactors)
            {
                interactor.SetTransparency(transparency);
            }
        }

        #endregion
    }
}
