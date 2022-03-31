using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects;
using System.Drawing;
using Gds.LiteConstruct.BusinessObjects.MouseRotationTranslation.Interfaces;
using Gds.LiteConstruct.Rendering;
using System.Windows.Forms;

namespace Gds.LiteConstruct.Core.Controllers
{
    internal class GraphicWindowController : IGraphicWindowController
    {
        private readonly Core core;
		private readonly PrimitiveManagerController primitiveController;
		private readonly PrimitiveSelection primitiveSelection;

        private Point lastMousePosition;
        public Point LastMousePosition
        {
            get { return lastMousePosition; }
            set { lastMousePosition = value; }
        }

        public GraphicWindowController(Core core)
        {
            this.core = core;
			primitiveController = core.PrimitiveManagerController;
			primitiveSelection = core.PrimitiveManagerController.Selection;
        }

        private ProjectionPlane projectionPlane;
        public ProjectionPlane ProjectionPlane
        {
            get { return projectionPlane; }
            set { projectionPlane = value; }
        }

        private void RaiseFreeMouseMove(int x, int y)
        {
            if (FreeMouseMoveAppeared != null)
            {
                FreeMouseMoveAppeared(x, y);
            }
        }

        private void RaiseClampedMouseMove(int x, int y)
        {
            if (ClampedMouseMoveAppeared != null)
            {
                ClampedMouseMoveAppeared(x, y);
            }
        }

        private void RaiseMousePrimaryClick(int x, int y)
        {
            if (MousePrimaryClickAppeared != null)
            {
                MousePrimaryClickAppeared(x, y);
            }
        }

        private void RaiseMouseSecondaryClick(int x, int y)
        {
            if (MouseSecondaryClickAppeared != null)
            {
                MouseSecondaryClickAppeared(x, y);
            }
        }

        public void MouseDown()
        {
            if (core.MouseActionMode == MouseActionMode.PrimitiveEditMode)
            {
                if (primitiveSelection.Type != SelectionType.None)
                {
					if (primitiveSelection.Last.MouseTransformationOn)
                    {
                        primitiveSelection.Last.Transformation.ProcessWithMouseDown();
                    }
                }
            }
        }

        public void MouseUp()
        {
            if (core.MouseActionMode == MouseActionMode.PrimitiveEditMode)
            {
				if (primitiveSelection.Type != SelectionType.None)
                {
					if (primitiveSelection.Last.MouseTransformationOn)
                    {
						primitiveSelection.Last.Transformation.ProcessWithMouseUp();
                    }
                }
            }
        }

        public void FreeMouseMove(int x, int y)
        {
			if (primitiveController.AddingPrimitive != null)
            {
                Vector3 position;
                position = ProjectionPlane.MakeProjection(Ray.GetRayFromScreenCoordinates(x, y));
				primitiveController.AddingPrimitive.MoveTo(position);
            }

            if (core.MouseActionMode == MouseActionMode.PrimitiveEditMode)
            {
				if (primitiveSelection.Type != SelectionType.None)
                {
					if (primitiveSelection.Last.MouseTransformationOn && core.GraphicController.IsPaused == false)
                    {
						primitiveSelection.Last.Transformation.ProcessWithMousePosition(new Point(x, y));
                    }
                }
            }

            RaiseFreeMouseMove(x, y);
        }

        public void ClampedMouseMove(int x, int y)
        {
            if (core.MouseActionMode == MouseActionMode.PrimitiveEditMode)
            {
				if (primitiveSelection.Type != SelectionType.None)
                {
					if (primitiveSelection.Last.MouseTransformationOn)
                    {
						primitiveSelection.Last.Transformation.ProcessWithMouseClamped(new Point(x, y));
                    }
                }
            }

            RaiseClampedMouseMove(x, y);
        }

        public void DeltaClampedMouseMove(int mx, int my)
        {
            if (core.MouseActionMode == MouseActionMode.CameraMode)
            {
                core.CameraSwitcherController.CameraMode.Execute(core.GraphicController.Camera, mx, my);
				if (primitiveSelection.Type != SelectionType.None)
                {
					if (primitiveSelection.Last.MouseTransformationOn)
                    {
						if (primitiveSelection.Last.Transformation.IsBillboard)
                        {
							IBillboard transformation = (IBillboard)primitiveSelection.Last.Transformation;
                            transformation.Look = core.GraphicController.Camera.Look;
                        }
                    }
                }
            }
        }

        public void SelectEntity(int x, int y)
        {
            if (core.GraphicController.CurentRenderMode == core.SceneRenderMode)
            {
                PrimitiveBase primitive = core.SceneRenderMode.GetPrimitiveByScreenPosition(x, y);
				if (primitive != null)
				{
					//if (AppContext.Get<IKeyboardHandler>().IsAnyKeyPressed(Key.LeftControl, Key.RightControl))
					if (Control.ModifierKeys == Keys.Control)
					{
						if (primitiveSelection.Contains(primitive))
							primitiveSelection.Remove(primitive);
						else
							primitiveSelection.Add(primitive);
					}
					else
					{
						primitiveSelection.Set(primitive);
					}
				}
				else
				{
					primitiveSelection.Clear();
				}
            }
            else if (core.GraphicController.CurentRenderMode is TexturingRenderMode)
            {
                core.TexturingController.SelectedSide = (core.GraphicController.CurentRenderMode as TexturingRenderMode).GetSideByScreenPosition(x, y);
            }
        }

        public void MousePrimaryClick(int x, int y)
        {
			if (primitiveController.AddingPrimitive != null)
            {
				primitiveController.DoAddPrimitive(x, y);
            }

            RaiseMousePrimaryClick(x, y);
        }

        public void MouseSecondaryClick(int x, int y)
        {
			if (primitiveController.AddingPrimitive != null)
            {
				primitiveController.CancelPrimitiveAdding();
                return;
            }

            if (core.GraphicController.CurentRenderMode == core.SceneRenderMode)
            {
                lastMousePosition = new Point(x, y);
                PrimitiveBase primitive;
                primitive = core.SceneRenderMode.GetPrimitiveByScreenPosition(lastMousePosition.X, lastMousePosition.Y);
                if (primitive != null)
                {
					primitiveSelection.Set(primitive);
                    core.GraphicWindowPresenter.ShowPrimitiveContextMenu();
                }
                else
                {
                    core.GraphicWindowPresenter.ShowGroundContextMenu();
                }
            }

            RaiseMouseSecondaryClick(x, y);
        }

        public void SetNextCameraMode()
        {
            if (core.MouseActionMode == MouseActionMode.CameraMode)
            {
                core.CameraSwitcherController.SetNextMode();
                core.CameraSwitcherPresenter.SelectNextCameraMode();
            }
        }

        public void SetPrevCameraMode()
        {
            if (core.MouseActionMode == MouseActionMode.CameraMode)
            {
                core.CameraSwitcherController.SetPrevMode();
                core.CameraSwitcherPresenter.SelectPrevCameraMode();
            }
        }

        public void SetTexturizeRenderMode()
        {
            core.RenderModeSwitcherController.SetTexturingRenderMode();
        }

        public void SetDetailedRenderMode()
        {
        }

        public event MousePositionHandler FreeMouseMoveAppeared;
        public event MousePositionHandler ClampedMouseMoveAppeared;
        public event MousePositionHandler MousePrimaryClickAppeared;
        public event MousePositionHandler MouseSecondaryClickAppeared;
    }
}
