using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Gds.LiteConstruct.PrimitivesManagement;
using Gds.LiteConstruct.PrimitivesManagement.AxisBindings;

namespace Gds.LiteConstruct.Core.Controllers
{
    internal class PrimitiveEditModeSwitcherController : IPrimitiveEditModeSwitcherController
    {
        private readonly Core core;
		private readonly PrimitiveSelection primitiveSelection;
        private BindingsManager bindingManager;

        public BindingsManager BindingManager
        {
            get { return bindingManager; }
        }

        public PrimitiveEditModeSwitcherController(Core core)
        {
            this.core = core;
			primitiveSelection = core.PrimitiveManagerController.Selection;
			primitiveSelection.SingleSelectionEntered += SingleSelectionEntered;
			primitiveSelection.SingleSelectionLost += SingleSelectionLost;
			primitiveSelection.SingleSelectionChanged += SingleSelectionChanged;
        }

		private void SingleSelectionEntered(PrimitiveBase item)
		{
			core.PrimitiveEditModeSwitcherPresenter.Show(true);
		}

		private void SingleSelectionLost(PrimitiveBase item)
		{
			core.PrimitiveEditModeSwitcherPresenter.Show(false);
			if (item.MouseTransformationOn)
			{
				item.TurnOffMouseTransformation();
			}
		}

		private void SingleSelectionChanged(PrimitiveBase oldItem, PrimitiveBase newItem)
		{
			if (newItem != oldItem)
			{
				if (oldItem.MouseTransformationOn)
				{
					oldItem.TurnOffMouseTransformation();
				}
				core.PrimitiveEditModeSwitcherPresenter.Show(true);
			}
		}

        public void FinishBinding()
        {
            bindingManager.StopBinding();
            BindingManager_BindingFinished();
        }

        private void BindingManager_BindingFinished()
        {
            core.SceneRenderMode.Render -= bindingManager.Render;
            core.GraphicWindowController.FreeMouseMoveAppeared -= bindingManager.FreeMouseMove;
            core.GraphicWindowController.MousePrimaryClickAppeared -= bindingManager.PrimaryMouseClick;
            bindingManager = null;
        }

        public void BeginBinding()
        {
            PrimitiveSelection selection;
            selection = core.PrimitiveManagerController.Selection;

            bindingManager = new AxisBindingManager(selection[0], selection[1], core.Workspace.Model);
            core.SceneRenderMode.Render += bindingManager.Render;
            core.GraphicWindowController.FreeMouseMoveAppeared += bindingManager.FreeMouseMove;
            core.GraphicWindowController.MousePrimaryClickAppeared += bindingManager.PrimaryMouseClick;
            bindingManager.BindingFinished += BindingManager_BindingFinished;

            bindingManager.StartBinding();
        }

        public void BeginTranslation()
        {
			if (primitiveSelection.Type != SelectionType.None)
            {
                core.MouseActionMode = MouseActionMode.PrimitiveEditMode;
                core.CameraSwitcherPresenter.Show(false);

				if (!primitiveSelection.Last.MouseTranslationIsOn)
                {
					primitiveSelection.Last.SetMouseTranslationMode(core.GraphicController.Camera.Look);
                }

				primitiveSelection.Last.ShowMouseTransformation();
            }
        }

        public void BeginRotation()
        {
			if (primitiveSelection.Type == SelectionType.Single)
            {
                core.MouseActionMode = MouseActionMode.PrimitiveEditMode;
                core.CameraSwitcherPresenter.Show(false);

				if (!primitiveSelection.First.MouseRotationIsOn)
                {
					primitiveSelection.First.SetMouseRotationMode(core.GraphicController.Camera.Look);
                }

				primitiveSelection.First.ShowMouseTransformation();
            }
        }
    }
}
