using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Rendering;
using Gds.LiteConstruct.BusinessObjects.Primitives;

namespace Gds.LiteConstruct.Core.Controllers
{
    internal class RenderModeSwitcherController : IRenderModeSwitcherController
    {
        private readonly Core core;

        public RenderModeSwitcherController(Core core)
        {
            this.core = core;
			PrimitiveSelection selection = core.PrimitiveManagerController.Selection;
			selection.SingleSelectionEntered += SingleSelectionEntered;
			selection.SingleSelectionLost += SingleSelectionLost;
        }

		private void SingleSelectionEntered(PrimitiveBase item)
		{
			core.RenderModeSwitcherPresenter.RenderModeController = core.RenderModeSwitcherController;
			core.RenderModeSwitcherPresenter.UpdateSceneRenderModeControl(RenderModeControlState.Checked);
			core.RenderModeSwitcherPresenter.UpdateTexturingRenderModeControl(RenderModeControlState.Unchecked);
			core.RenderModeSwitcherPresenter.UpdateDetailedRenderModeControl(RenderModeControlState.Unchecked);
		}

		private void SingleSelectionLost(PrimitiveBase item)
		{
			core.RenderModeSwitcherPresenter.RenderModeController = null;
			core.RenderModeSwitcherPresenter.UpdateSceneRenderModeControl(RenderModeControlState.Checked);
			core.RenderModeSwitcherPresenter.UpdateTexturingRenderModeControl(RenderModeControlState.Invisible);
			core.RenderModeSwitcherPresenter.UpdateDetailedRenderModeControl(RenderModeControlState.Invisible);
		}

        public void SetSceneRenderMode()
        {
            core.RenderModeSwitcherPresenter.UpdateSceneRenderModeControl(RenderModeControlState.Checked);
            if (core.PrimitiveManagerController.Selection.IsSingle)
            {
                core.RenderModeSwitcherPresenter.UpdateTexturingRenderModeControl(RenderModeControlState.Unchecked);
                core.RenderModeSwitcherPresenter.UpdateDetailedRenderModeControl(RenderModeControlState.Unchecked);
            }
            else
            {
                core.RenderModeSwitcherPresenter.UpdateTexturingRenderModeControl(RenderModeControlState.Invisible);
                core.RenderModeSwitcherPresenter.UpdateDetailedRenderModeControl(RenderModeControlState.Invisible);
            }

            core.MainFormPresenter.SwitchRenderMode(core.PrimitiveManagerPresenter);
            core.GraphicController.SetRenderMode(core.SceneRenderMode);
            core.CameraSwitcherPresenter.UpdateCameraMode(core.GraphicController.Camera.CanMove);

			core.TexturingController.SelectedSide = null;
            core.PrimitiveManagerController.SelectCurrentPrimitive(true);

            core.PrimitivePropertiesPresenter.ShowPosition(true);
            core.PrimitivePropertiesPresenter.ShowSize(true);
            core.PrimitivePropertiesPresenter.ShowRotation(true);
            core.PrimitivePropertiesPresenter.ShowAdditional(true);
        }

        public void SetTexturingRenderMode()
        {
            core.RenderModeSwitcherPresenter.UpdateSceneRenderModeControl(RenderModeControlState.Unchecked);
            core.RenderModeSwitcherPresenter.UpdateTexturingRenderModeControl(RenderModeControlState.Checked);
            core.RenderModeSwitcherPresenter.UpdateDetailedRenderModeControl(RenderModeControlState.Unchecked);

            core.MainFormPresenter.SwitchRenderMode(core.TexturingManagerPresenter);
            core.GraphicController.SetRenderMode(new TexturingRenderMode(core.PrimitiveManagerController.Selection.First));
            core.CameraSwitcherPresenter.UpdateCameraMode(core.GraphicController.Camera.CanMove);
            core.TexturingManagerPresenter.EnableTexturing(false);
			core.TexturingController.SelectedSide = null;
            core.PrimitiveManagerController.SelectCurrentPrimitive(false);
			
            core.PrimitivePropertiesPresenter.ShowPosition(false);
            core.PrimitivePropertiesPresenter.ShowSize(true);
            core.PrimitivePropertiesPresenter.ShowRotation(false);
            core.PrimitivePropertiesPresenter.ShowAdditional(true);
        }
    }
}
