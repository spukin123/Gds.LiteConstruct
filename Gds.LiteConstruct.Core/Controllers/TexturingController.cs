using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Environment;
using Gds.LiteConstruct.BusinessObjects;
using Gds.LiteConstruct.BusinessObjects.Sides;
using Gds.LiteConstruct.BusinessObjects.Primitives;

namespace Gds.LiteConstruct.Core.Controllers
{
    internal class TexturingController : ITexturingController
    {
        private readonly Core core;
        private readonly Workspace workspace;

        private SideBase selectedSide;
        public SideBase SelectedSide
        {
            get { return selectedSide; }
            set
            {
                if (selectedSide != null)
                {
                    selectedSide.Select(false);
                }
                selectedSide = value;
                if (selectedSide != null)
                {
                    selectedSide.Select(true);
                    core.TexturingManagerPresenter.EnableTexturing(true);
                    core.TexturingManagerPresenter.EnableRotationControl(true);
                    core.TexturingManagerPresenter.RotationAngle = selectedSide.TextureRotationAngle;
                }
                else
                {
                    core.TexturingManagerPresenter.EnableTexturing(false);
                    core.TexturingManagerPresenter.EnableRotationControl(false);
                    core.TexturingManagerPresenter.RotationAngle = Angle.A0;
                }
            }
        }

        internal TexturingController(Core core)
        {
            this.core = core;
            workspace = core.Workspace;

			PrimitiveSelection selection = core.PrimitiveManagerController.Selection;
			selection.SingleSelectionEntered += SingleSelectionEntered;
			selection.SingleSelectionLost += SingleSelectionLost;
			selection.SingleSelectionChanged += SingleSelectionChanged;
        }

		private void SingleSelectionEntered(PrimitiveBase item)
		{
			item.SelectedSideChanged += Primitive_SelectedSideChanged;
		}

		private void SingleSelectionLost(PrimitiveBase item)
		{
			item.SelectedSideChanged -= Primitive_SelectedSideChanged;
		}

		private void SingleSelectionChanged(PrimitiveBase oldItem, PrimitiveBase newItem)
		{
			oldItem.SelectedSideChanged -= Primitive_SelectedSideChanged;
			newItem.SelectedSideChanged += Primitive_SelectedSideChanged;
		}

        public void Primitive_SelectedSideChanged(SideBase newSide)
        {
            selectedSide = newSide;
        }

        #region ITexturingController Members

        public void ApplyTextureToSelectedSide(Guid textureId)
        {
            if (SelectedSide != null)
            {
                SelectedSide.SetTexture(textureId);
            }
        }

        public void CreateTexturesCategory(string name)
        {
            workspace.TexturesEnvironment.AddCategory(name);
        }

        public void RotateTextureOnSelectedSide(Angle angle)
        {
            if (SelectedSide != null)
            {
                SelectedSide.RotateTexture(angle);
            }
        }

        public void ApplyTextureToAllSides(Guid textureId)
        {
            core.PrimitiveManagerController.Selection.First.SetTexture(textureId);
        }

        #endregion
    }
}
