using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Gds.LiteConstruct.Environment;
using Microsoft.DirectX;
using Gds.LiteConstruct.BusinessObjects;
using System.Drawing;

namespace Gds.LiteConstruct.Core.Controllers
{
    public class PrimitiveManagerController : IPrimitiveManagerController
    {
        private readonly Core core;
        private readonly Workspace workspace;

		private PrimitiveSelection primitiveSelection = new PrimitiveSelection();

		public PrimitiveSelection Selection
		{
			get { return primitiveSelection; }
		}

		private PrimitiveBase addingPrimitive = null;
        public PrimitiveBase AddingPrimitive
        {
            get { return addingPrimitive; }
            set { addingPrimitive = value; }
        }

        internal PrimitiveManagerController(Core core)
        {
            this.core = core;
            workspace = core.Workspace;
			primitiveSelection.ItemsAdded += primitiveSelection_ItemsAdded;
			primitiveSelection.ItemsRemoved += primitiveSelection_ItemsRemoved;
			primitiveSelection.NoneSelectionEntered += primitiveSelection_NoneSelectionEntered;
			primitiveSelection.NoneSelectionLost += primitiveSelection_NoneSelectionLost;
		}

        private void ProcessBinding()
        {
            if (primitiveSelection.Count == 2)
            {
                core.PrimitiveEditModeSwitcherPresenter.EnableBinding(true);
            }
            else
            {
                core.PrimitiveEditModeSwitcherPresenter.EnableBinding(false);
                if (core.PrimitiveEditModeSwitcherController.BindingManager != null)
                {
                    core.PrimitiveEditModeSwitcherController.FinishBinding();
                }
            }
        }

		private void primitiveSelection_ItemsAdded(PrimitiveBase[] items)
		{
			foreach (PrimitiveBase primitive in items)
			{
				primitive.Selected = true;
			}

            ProcessBinding();
		}

		private void primitiveSelection_ItemsRemoved(PrimitiveBase[] items)
		{
			foreach (PrimitiveBase primitive in items)
			{
				primitive.Selected = false;
			}

            ProcessBinding();
		}

		private void primitiveSelection_NoneSelectionEntered()
		{
			core.PrimitiveManagerPresenter.DeletePrimitiveEnabled = false;
            ProcessBinding();
		}

		private void primitiveSelection_NoneSelectionLost()
		{
			core.PrimitiveManagerPresenter.DeletePrimitiveEnabled = true;
		}

        public void UnbindFromSelectedPrimitive(object sender, EventArgs e)
        {
            if (primitiveSelection.Type == SelectionType.Single)
            {
				primitiveSelection.First.PositionChanged -= core.PrimitivePropertiesPresenter.OnPrimitivePositionChanged;
				primitiveSelection.First.RotationChanged -= core.PrimitivePropertiesPresenter.OnPrimitiveRotationChanged;
				primitiveSelection.First.SelectedSideChanged -= core.TexturingController.Primitive_SelectedSideChanged;
            }
			if (primitiveSelection.Type != SelectionType.None)
			{
				if (primitiveSelection.Last.MouseTransformationOn)
				{
					primitiveSelection.Last.Transformation.UnbindFromPrimitive();
				}
			}
        }

        public void BindToSelectedPrimitive(object sender, EventArgs e)
        {
			if (primitiveSelection.Type == SelectionType.Single)
            {
				primitiveSelection.First.PositionChanged += core.PrimitivePropertiesPresenter.OnPrimitivePositionChanged;
				primitiveSelection.First.RotationChanged += core.PrimitivePropertiesPresenter.OnPrimitiveRotationChanged;
				primitiveSelection.First.SelectedSideChanged += core.TexturingController.Primitive_SelectedSideChanged;
            }
			if (primitiveSelection.Type != SelectionType.None)
			{
				if (primitiveSelection.Last.MouseTransformationOn)
				{
					primitiveSelection.Last.Transformation.BindToPrimitive();
				}
			}
        }

        public void SelectCurrentPrimitive(bool select)
        {
            if (primitiveSelection.Type == SelectionType.Single)
            {
				primitiveSelection.First.Selected = select;
            }
        }

        public void DoAddPrimitive(int cursorX, int cursorY)
        {
            addingPrimitive.Transparent = false;
            primitiveSelection.Set(addingPrimitive);
            addingPrimitive = null;

            core.PrimitiveManagerPresenter.PrimitiveAdded();
        }

        public void CancelPrimitiveAdding()
        {
            core.Workspace.Model.RemovePrimitive(addingPrimitive);
            addingPrimitive = null;

            core.PrimitiveManagerPresenter.PrimitiveAddingCancelled();
        }

        public void AddPrimitive(PrimitiveBase primitive)
        {
			Point mousePosition = core.GraphicWindowController.LastMousePosition;
            core.SceneRenderMode.AddPrimitiveByScreenPosition(primitive, mousePosition.X, mousePosition.Y);
            primitiveSelection.Set(primitive);
        }

        public void PreAddPrimitive(PrimitiveBase primitive)
        {
			primitiveSelection.Clear();
            addingPrimitive = primitive;
            workspace.Model.AddPrimitive(primitive);
            addingPrimitive.MoveTo(new Vector3(5000f, 0f, 0f));
            //addingPrimitive.Transparent = true;

            core.GraphicWindowController.ProjectionPlane = new ProjectionPlane(Vector3Utils.AlignedZVector, Vector3Utils.ZeroVector);
        }

        public void DeleteSelection()
        {
            PrimitiveBase[] primitives = primitiveSelection.Items;
            primitiveSelection.Clear();

            foreach (PrimitiveBase primitive in primitives)
            {
                workspace.Model.RemovePrimitive(primitive);
            }
        }

        public void CloneSelectedPrimitive()
        {
            UnbindFromSelectedPrimitive(this, null);

            addingPrimitive = primitiveSelection.First.Clone();
            core.GraphicWindowController.ProjectionPlane = new ProjectionPlane(Vector3Utils.AlignedZVector,
					new Vector3(0f, 0f, addingPrimitive.Position.Z));
            core.Workspace.Model.AddClonedPrimitive(addingPrimitive);

			BindToSelectedPrimitive(this, null);
        }
    }
}
