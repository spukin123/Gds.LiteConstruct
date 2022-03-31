using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Primitives;

namespace Gds.LiteConstruct.Core.Controllers
{
    internal class PrimitivePropertiesController : IPrimitivePropertiesController
    {
        private Core core;
		private PrimitiveManagerController primitiveController;

        public PrimitivePropertiesController(Core core)
        {
            this.core = core;
			primitiveController = core.PrimitiveManagerController;
			primitiveController.Selection.SingleSelectionEntered += PrimitiveSelection_SingleSelectionEntered;
			primitiveController.Selection.SingleSelectionLost += PrimitiveSelection_SingleSelectionLost;
			primitiveController.Selection.SingleSelectionChanged += PrimitiveSelection_SingleSelectionChanged;
        }

		private void PrimitiveSelection_SingleSelectionEntered(PrimitiveBase item)
		{
			core.PrimitivePropertiesPresenter.PrimitivePropertiesController = this;
			core.PrimitivePropertiesPresenter.ShowPrimitiveProperties(item);

			BindPrimitiveEvents(item);
		}

		private void PrimitiveSelection_SingleSelectionLost(PrimitiveBase item)
		{
			core.PrimitivePropertiesPresenter.PrimitivePropertiesController = null;
			core.PrimitivePropertiesPresenter.ShowPrimitiveProperties(null);

			UnbindPrimitiveEvents(item);
		}

		private void PrimitiveSelection_SingleSelectionChanged(PrimitiveBase oldItem, PrimitiveBase newItem)
		{
			UnbindPrimitiveEvents(oldItem);
			core.PrimitivePropertiesPresenter.ShowPrimitiveProperties(newItem);
			BindPrimitiveEvents(newItem);
		}

		private void BindPrimitiveEvents(PrimitiveBase primitive)
		{
			primitive.PositionChanged += core.PrimitivePropertiesPresenter.OnPrimitivePositionChanged;
			primitive.RotationChanged += core.PrimitivePropertiesPresenter.OnPrimitiveRotationChanged;
		}

		private void UnbindPrimitiveEvents(PrimitiveBase primitive)
		{
			primitive.PositionChanged -= core.PrimitivePropertiesPresenter.OnPrimitivePositionChanged;
			primitive.RotationChanged -= core.PrimitivePropertiesPresenter.OnPrimitiveRotationChanged;
		}

        #region IPrimitivePropertiesController Members

#warning Remove this fuck`n property
        public PrimitiveBase SelectedPrimitive
        {
            get { return core.PrimitiveManagerController.Selection.First; }
        }

        #endregion
    }
}
