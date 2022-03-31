using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects;
using Gds.LiteConstruct.BusinessObjects.Sides;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.TransformationControllers;
using System.Drawing;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Gds.LiteConstruct.BusinessObjects.Axises;
using System.Runtime.Serialization;

namespace Gds.LiteConstruct.BusinessObjects.Primitives
{
    [Serializable]
    public abstract class PrimitiveBase : IRotatable, IMovable, IRenderable, IDisposable, IMouseTransformationManagement
    {
        private Guid id;
        protected Guid[] idsForAxises;

        public Guid Id
        {
            get { return id; }
        }

        [NonSerialized]
        protected bool selected = false;
        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                Highlight(value);
            }
        }

        private bool transparent = false;
        public bool Transparent
        {
            get { return transparent; }
            set 
            { 
                transparent = value;
                MakeTransparent(value);
            }
        }

        [NonSerialized]
        private bool transformationHidden = false;

        [NonSerialized]
        protected MouseTransformation transformation;
        public MouseTransformation Transformation
        {
            get { return transformation; }
        }

        public Guid[] TextureIDs
        {
            get
            {
                Guid[] ids = new Guid[allSides.Length];
                for (int cnt = 0; cnt < allSides.Length; cnt++)
                {
                    ids[cnt] = allSides[cnt].TextureId;
                }

                return ids;
            }
        }

        protected Vector3 position;
        protected RotationVector rotation = new RotationVector();
        [NonSerialized]
        protected Vertex[] vertices;

        [NonSerialized]
        protected SideDimension[] simpleSideDimensions;
        protected SimpleSide[] simpleSides;
        [NonSerialized]
        protected SideDimension[][] masterSideDimensions;
        protected MasterSide[] masterSides;

        [NonSerialized]
        private MasterSide[] previousMasterSides;
        [NonSerialized]
        private SimpleSide[] previousSimpleSides;

        private SideBase[] allSides;

        [NonSerialized]
        protected ITextureProvider textureProvider;

		public event PrimitivePositionChangedHandler PositionChanged;
        public event PrimitiveRotationChangedHandler RotationChanged;
        public event EventHandler SizeChanged;
        public event PrimitiveSelectedSideChanged SelectedSideChanged;

        protected PrimitiveBase()
        {
            CreateIds();
            SetDefaultSize();
            PreInit();
            SetVertices();
            SetSideDimensions();
            CreateSimpleSides();
            CreateMasterSides();
            ObtainAllSides();
        }

        private void CreateIds()
        {
            id = Guid.NewGuid();

            idsForAxises = new Guid[AxesNumber];
            for (int cnt = 0; cnt < AxesNumber; cnt++)
            {
                idsForAxises[cnt] = Guid.NewGuid();
            }
        }

        public virtual void Initialize(ITextureProvider textureProvider)
        {
            this.textureProvider = textureProvider;
            
            PreInit();
            SetVertices();
            SetSideDimensions();
            SetSides();
            ObtainAllSides();
            InitializeSidesTextures();
        }

        public void SetTextureProvider(ITextureProvider textureProvider)
        {
            this.textureProvider = textureProvider;
            foreach (SideBase side in allSides)
            {
                side.TextureProvider = textureProvider;
                //side.SetDefaultTexture();
            }
        }

        public void ClearEvents()
        {
            //PositionChanged.
            PositionChanged = null;
            RotationChanged = null;
            SizeChanged = null;
            SelectedSideChanged = null;
        }

        public void SetTexture(Guid id)
        {
            foreach (SideBase side in allSides)
            {
                side.SetTexture(id);
            }
        }

        private void SetTextureIDsForSides(Guid[] ids)
        {
            for (int cnt = 0; cnt < allSides.Length; cnt++)
            {
                allSides[cnt].SetTexture(ids[cnt]);
            }
        }

        public PrimitiveBase Clone()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                using (MemoryStream streamCopy = new MemoryStream(stream.ToArray()))
                {
                    PrimitiveBase clone = (PrimitiveBase)formatter.Deserialize(streamCopy);

                    clone.Initialize(textureProvider);
                    return clone;
                }
            }
        }

        protected abstract int AxesNumber { get; }
        protected virtual void PreInit() { }
        protected abstract void SetDefaultSize();
        protected abstract void SetVertices();
        protected abstract void SetSideDimensions();

        public abstract void Accept(ISizableVisitor visitor);
        public abstract void Accept(IAdditionalVisitor visitor);

        #region Sides Methods

        protected virtual void CreateSimpleSides()
        {
            if (simpleSideDimensions != null && simpleSideDimensions.Length > 0)
            {
                simpleSides = new SimpleSide[simpleSideDimensions.Length];
                for (int cnt = 0; cnt < simpleSideDimensions.Length; cnt++)
                {
                    simpleSides[cnt] = simpleSideDimensions[cnt].CreateSide();
                }
            }
        }

        protected virtual void CreateMasterSides()
        {
        }

        protected void SetSides()
        {
            if (simpleSideDimensions != null)
            {
                for (int cnt = 0; cnt < simpleSideDimensions.Length; cnt++)
                {
                    simpleSides[cnt].Initialize(simpleSideDimensions[cnt]);
                }
            }
            if (masterSideDimensions != null)
            {
                for (int cnt = 0; cnt < masterSideDimensions.Length; cnt++)
                {
                    masterSides[cnt].Initialize(masterSideDimensions[cnt]);
                }
            }
        }

        protected void ObtainAllSides()
        {
            List<SideBase> sidesList = new List<SideBase>();

            if (simpleSides != null)
                sidesList.AddRange(simpleSides);
            if (masterSides != null)
                sidesList.AddRange(masterSides);

            allSides = sidesList.ToArray();
        }

        protected void InitializeSidesTextures()
        {
            //No texture provider!
            foreach (SideBase side in allSides)
            {
                side.TextureProvider = textureProvider;
                side.InitializeTexture();
            }
        }

        protected void UpdateSidesPositions()
        {
            foreach (SideBase side in allSides)
            {
                side.UpdatePosition();
            }
        }

        protected void SavePreviousSides()
        {
            if (simpleSides != null)
            {
                previousSimpleSides = simpleSides;
            }

            if (masterSides != null)
            {
                previousMasterSides = masterSides;
            }
        }

        protected void ApplyPreviousSidesState()
        {
            if (masterSides != null)
            {
                for (int cnt = 0; cnt < previousMasterSides.Length; cnt++)
                {
                    masterSides[cnt].TextureProvider = textureProvider;
                    masterSides[cnt].Select(previousMasterSides[cnt].Selected);
                    masterSides[cnt].RotateTexture(previousMasterSides[cnt].TextureRotationAngle);
                    masterSides[cnt].SetTexture(previousMasterSides[cnt].TextureId);
                }
            }
            
            if (simpleSides != null)
            {
                for (int cnt = 0; cnt < simpleSides.Length; cnt++)
                {
                    simpleSides[cnt].TextureProvider = textureProvider;
                    simpleSides[cnt].Select(previousSimpleSides[cnt].Selected);
                    simpleSides[cnt].RotateTexture(previousSimpleSides[cnt].TextureRotationAngle);
                    simpleSides[cnt].SetTexture(previousSimpleSides[cnt].TextureId);
                }
            }
        }

        protected void ClearPreviousSides()
        {
            if (previousMasterSides != null)
            {
                foreach (MasterSide masterSide in previousMasterSides)
                {
                    masterSide.Dispose();
                }
                previousMasterSides = null;
            }

            if (previousSimpleSides != null)
            {
                foreach (SimpleSide simpleSide in previousSimpleSides)
                {
                    simpleSide.Dispose();
                }
                previousSimpleSides = null;
            }
        }

        protected void UpdateTextureCoordinates()
        {
            foreach (SideBase side in allSides)
            {
                side.UpdateTextureCoordinates();
            }
        }

        #endregion

        #region Rendering Methods

        protected Matrix GetTransformationMatrix()
        {
            Matrix posMat = Matrix.Identity;
            posMat.Translate(position);
            return rotation.GetRotationMatrix() * posMat;
        }

        public void Render()
        {
            DeviceObject.Device.Transform.World = GetTransformationMatrix();

            foreach (SideBase side in allSides)
            {
                side.Render();
            }

            if (MouseTransformationOn && !transformationHidden)
            {
                transformation.Render();
            }
        }

        public void RenderInLCS()
        {
            Matrix matrix = Matrix.Identity;
            DeviceObject.Device.Transform.World = matrix;

            foreach (SideBase side in allSides)
            {
                side.Render();
            }
        }

        #endregion

        #region Intersections Methods

        public Vertex GetIntersectionPoint(Ray ray)
        {
            Matrix transform = GetTransformationMatrix();
            Ray transformRay = new Ray();
            transform.Invert();
            transformRay.Direction = Vector3.TransformCoordinate(ray.Position + ray.Direction, transform);
            transformRay.Position = Vector3.TransformCoordinate(ray.Position, transform);
            transformRay.Direction -= transformRay.Position;

            Vertex result = GetIntersectionPointInLCS(transformRay);
            if (result != null)
                return new Vertex(Vector3.TransformCoordinate(result.Vector, Matrix.Invert(transform)));
            else
                return null;
        }

        public Vertex GetIntersectionPointInLCS(Ray ray)
        {
            Intersection intersection = GetIntersectionInLCS(ray);
            return (intersection != null) ? intersection.Point : null;
        }

        public SideBase GetIntersectionSideInLCS(Ray ray)
        {
            Intersection intersection = GetIntersectionInLCS(ray);
            return (intersection != null) ? intersection.Side : null;
        }

        private Intersection GetIntersectionInLCS(Ray ray)
        {
            Vertex point;
            Intersection nearestIntersection = null;
            float nearestDist = 0;
            foreach (SideBase side in allSides)
            {
                point = side.GetIntersectionPoint(ray);
                if (point != null)
                {
                    if (nearestIntersection == null || 
                        Vector3Utils.DistanceBetweenPoints(ray.Position, point.Vector) < nearestDist)
                    {
                        nearestIntersection = new Intersection(side, point);
                        nearestDist = Vector3Utils.DistanceBetweenPoints(ray.Position, point.Vector);
                    }
                }
            }
            return nearestIntersection;
        }

        #endregion

        #region Axis bindings Methods

        public virtual FreeBindingAxis[] GetAxes()
        {
            return new FreeBindingAxis[1] { FreeBindingAxis.Empty };
        }

        public FreeBindingAxis GetAxisById(Guid id)
        {
            FreeBindingAxis[] axes;
            axes = GetAxes();
            if (axes == null)
            {
                throw new Exception("Primitive has no axes.");
            }

            foreach (FreeBindingAxis axis in axes)
            {
                if (axis.Id == id)
                {
                    return axis;
                }
            }

            return null;
        }

        #endregion

        private void Highlight(bool doHighlight)
        {
            foreach (SideBase side in allSides)
            {
                side.Select(doHighlight);
            }
        }

        private void MakeTransparent(bool doMakeTransparent)
        {
            foreach (SideBase side in allSides)
            {
                side.MakeTransparent(doMakeTransparent);
            }
        }

        public float FarestPointDistance
        {
            get 
            {
                float maxDist = vertices[0].Vector.Length();
                foreach (Vertex vertex in vertices)
                {
                    if (vertex.Vector.Length() > maxDist)
                    {
                        maxDist = vertex.Vector.Length();
                    }
                }

                return maxDist;
            }
        }
        
        private void RaisePositionChangedEvent()
        {
            if (PositionChanged != null)
            {
                PositionChanged(position.X, position.Y, position.Z);
            }
        }
        
        protected void RaiseSizeChangedEvent()
        {
            if (SizeChanged != null)
            {
                SizeChanged(this, null);
            }
        }

        private void RaiseRotationChangedEvent()
        {
            if (RotationChanged != null)
            {
                RotationChanged(rotation);
            }
        }

        protected void RaiseSelectedSideChangedEvent()
        {
            bool isOneSideSelected = false;
            SideBase selectedSide = null;
            foreach (SideBase side in allSides)
	        {
                if (side.Selected)
                {
                    if (!isOneSideSelected)
                    {
                        isOneSideSelected = true;
                        selectedSide = side;
                    }
                    else
                    {
                        isOneSideSelected = false;
                        break;
                    }
                }
	        }

            if (SelectedSideChanged != null && isOneSideSelected)
            {
                SelectedSideChanged(selectedSide);
            }
        }

        protected virtual void DoRotateX(Angle angle)
        {
            rotation.X = angle;
            RaiseRotationChangedEvent();
        }

        protected virtual void DoRotateY(Angle angle)
        {
            rotation.Y = angle;
            RaiseRotationChangedEvent();
        }

        protected virtual void DoRotateZ(Angle angle)
        {
            rotation.Z = angle;
            RaiseRotationChangedEvent();
        }

        protected virtual void DoRotateXYZ(RotationVector vector)
        {
            rotation = vector;
            RaiseRotationChangedEvent();
        }

        #region IRotatable Members

        public abstract bool CanRotateX { get; }
        public abstract bool CanRotateY { get; }
        public abstract bool CanRotateZ { get; }

        public void RotateX(Angle angle)
        {
            DoRotateX(angle); 
        }

        public void RotateY(Angle angle)
        {
            DoRotateY(angle);
        }

        public void RotateZ(Angle angle)
        {
            DoRotateZ(angle);
        }

        public RotationVector Rotation
        {
            get { return rotation; }
            set 
            { 
                DoRotateXYZ(value);
            }
        }

        #endregion

        #region IMovable Members

        public void MoveTo(Vector3 pos)
        {
            this.position = pos;
            RaisePositionChangedEvent();
        }

        public void MoveBy(Vector3 mpos)
        {
            this.position += mpos;
            RaisePositionChangedEvent();
        }

        public void MoveByX(float mx)
        {
            this.position.X += mx;
            RaisePositionChangedEvent();
        }

        public void MoveByY(float my)
        {
            this.position.Y += my;
            RaisePositionChangedEvent();
        }

        public void MoveByZ(float mz)
        {
            this.position.Z += mz;
            RaisePositionChangedEvent();
        }

        public void MoveX(float x)
        {
            this.position.X = x;
            RaisePositionChangedEvent();
        }

        public void MoveY(float y)
        {
            this.position.Y = y;
            RaisePositionChangedEvent();
        }

        public void MoveZ(float z)
        {
            this.position.Z = z;
            RaisePositionChangedEvent();
        }

        public Vector3 Position
        {
            get { return position; }
            set 
            { 
                position = value;
                RaisePositionChangedEvent();
            }
        }

        #endregion

		[NonSerialized]
		private PrimitivePositionChangedHandler tmpPositionChanged;
		[NonSerialized]
		private PrimitiveRotationChangedHandler tmpRotationChanged;
		[NonSerialized]
		private EventHandler tmpSizeChanged;
		[NonSerialized]
		private PrimitiveSelectedSideChanged tmpSelectedSideChanged;

		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			tmpPositionChanged = PositionChanged;
			tmpRotationChanged = RotationChanged;
			tmpSizeChanged = SizeChanged;
			tmpSelectedSideChanged = SelectedSideChanged;

			PositionChanged = null;
			RotationChanged = null;
			SizeChanged = null;
			SelectedSideChanged = null;
		}

		[OnSerialized]
		private void OnSerialized(StreamingContext context)
		{
			PositionChanged = tmpPositionChanged;
			RotationChanged = tmpRotationChanged;
			SizeChanged = tmpSizeChanged;
			SelectedSideChanged = tmpSelectedSideChanged;
		}


        private class Intersection
        {
            private SideBase side;

            public SideBase Side
            {
                get { return side; }
                set { side = value; }
            }

            private Vertex point;

            public Vertex Point
            {
                get { return point; }
                set { point = value; }
            }

            public Intersection(SideBase side, Vertex point)
            {
                this.side = side;
                this.point = point;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            foreach (SideBase side in allSides)
            {
                side.Dispose();
            }
            vertices = null;
            simpleSides = null;
        }

        #endregion

        #region MouseTransformationManagement Members

        [NonSerialized]
        private bool rotationActive = false;
        [NonSerialized]
        private bool translationActive = false;

        public bool MouseTransformationOn
        {
            get { return transformation != null; }
        }

        public void SetMouseTranslationByPlaneMode()
        {
            transformation = new MouseTranslationByPlane(this);
        }

        public void SetMouseTranslatonByAxisMode(Vector3 look)
        {
            transformation = new MouseTranslationByAxis(this, look);
        }

        public void SetMouseRotationMode(Vector3 look)
        {
            if (MouseTransformationOn)
            {
                MouseTransformation copy = transformation;
                transformation = null;
                copy.Dispose();
            }
            transformation = new MouseRotation(this, look);

            translationActive = false;
            rotationActive = true;
        }

        public void SetMouseTranslationMode(Vector3 look)
        {
            if (MouseTransformationOn)
            {
                MouseTransformation copy = transformation;
                transformation = null;
                copy.Dispose();
            }
            transformation = new MouseTranslation(this, look);

            translationActive = true;
            rotationActive = false;
        }

        public void TurnOffMouseTransformation()
        {
            if (MouseTransformationOn)
            {
                transformation.Dispose();
            }
            
            transformation = null;
            transformationHidden = false;

            rotationActive = false;
            translationActive = false;
        }

        public void HideMouseTransformation()
        {
            transformationHidden = true;
        }

        public void ShowMouseTransformation()
        {
            transformationHidden = false;
        }

        public bool MouseTranslationIsOn
        {
            get { return translationActive; }
        }

        public bool MouseRotationIsOn
        {
            get { return rotationActive; }
        }

        #endregion
    }
}
