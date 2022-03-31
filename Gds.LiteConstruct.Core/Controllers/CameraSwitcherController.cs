using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Core.CameraModes;
using Gds.LiteConstruct.BusinessObjects.Primitives;

namespace Gds.LiteConstruct.Core.Controllers
{
	internal class CameraSwitcherController : ICameraSwitcherController
	{
		private readonly Core core;

		private CameraMode cameraMode = CameraModeManager.Rotatable;
		public CameraMode CameraMode
		{
			get { return cameraMode; }
		}

		public CameraSwitcherController(Core core)
		{
			this.core = core;
			PrimitiveSelection selection = core.PrimitiveManagerController.Selection;
			selection.SingleSelectionLost += SingleSelectionLost;
		}

		private void SingleSelectionLost(PrimitiveBase item)
		{
			core.CameraSwitcherPresenter.Show(true);
			core.MouseActionMode = MouseActionMode.CameraMode;
		}

		public void Activate()
		{
			if (core.MouseActionMode != MouseActionMode.CameraMode)
			{
				core.MouseActionMode = MouseActionMode.CameraMode;

                core.CameraSwitcherPresenter.Show(true);
                core.PrimitiveEditModeSwitcherPresenter.Show(true);

                if (core.PrimitiveManagerController.Selection.Type != SelectionType.None)
				{
					if (core.PrimitiveManagerController.Selection.Last.MouseTransformationOn)
					{
                        core.PrimitiveManagerController.Selection.Last.HideMouseTransformation();
					}
				}
			}
		}

		public void SetRotateMode()
		{
			cameraMode = CameraModeManager.Rotatable;
		}

		public void SetMoveMode()
		{
			cameraMode = CameraModeManager.Movable;
		}

		public void SetZoomMode()
		{
			cameraMode = CameraModeManager.Zoomable;
		}

		public void SetNextMode()
		{
			cameraMode = CameraModeManager.GetNextMode(cameraMode);
		}

		public void SetPrevMode()
		{
			cameraMode = CameraModeManager.GetPrevMode(cameraMode);
		}
	}
}
