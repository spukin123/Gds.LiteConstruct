using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.Core.Presenters;
using Gds.LiteConstruct.Core.Controllers;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using Gds.Windows;
using Gds.LiteConstruct.BusinessObjects;

namespace Gds.LiteConstruct.Presentation.Presenters
{
    public partial class PrimitivePropertiesPresenter : UserControl, IPrimitivePropertiesPresenter
    {
        private PrimitivePositionControl primitivePositionControl;
        private PrimitiveRotationControl primitiveRotationControl;

        public PrimitivePropertiesPresenter()
        {
            InitializeComponent();
        }

        private void ClearPreviousPages()
        {
            primitivePositionControl = null;
            primitiveRotationControl = null;
        }

        private void UpdateTabPage(object sender, EventArgs e)
        {
            ClearPreviousPages();

            TabPage tabPage = (sender as TabControl).SelectedTab;
            if (tabPage == tabPagePosition)
            {
                primitivePositionControl = new PrimitivePositionControl(controller.SelectedPrimitive);
                SetPageControl(tabPage, primitivePositionControl);
            }
            else if (tabPage == tabPageSize)
            {
                PrimitiveSizableControlVisitor sizeVisitor = new PrimitiveSizableControlVisitor();
                controller.SelectedPrimitive.Accept(sizeVisitor);
                SetPageControl(tabPage, sizeVisitor.Result);
            }
            else if (tabPage == tabPageRotation)
            {
                primitiveRotationControl = new PrimitiveRotationControl(controller.SelectedPrimitive);
                SetPageControl(tabPage, primitiveRotationControl);
            }
            else if (tabPage == tabPageAdditional)
            {
                PrimitiveAdditionalControlVisitor additionalVisitor = new PrimitiveAdditionalControlVisitor();
                controller.SelectedPrimitive.Accept(additionalVisitor);
                if (additionalVisitor.Result != null)
                {
                    SetPageControl(tabPage, additionalVisitor.Result);
                }
                else
                {
                    ControlUtils.ClearPanel(tabPage);
                }
            }
        }

        public void SetPageControl(TabPage page, IPrimitivePropertiesControl control)
        {
            ControlUtils.ShowUC(page, control as UserControl);
        }

        #region IPrimitivePropertiesPresenter Members

        private IPrimitivePropertiesController controller;

        public IPrimitivePropertiesController PrimitivePropertiesController
        {
            get { return controller; }
            set { controller = value; }
        }

        public void ShowPrimitiveProperties(PrimitiveBase primitive)
        {
            if (primitive != null)
            {
                tabControlProperties.Enabled = true;
                UpdateTabPage(tabControlProperties, null);
            }
            else
            {
                ControlUtils.ClearPanel(tabControlProperties.SelectedTab);
                tabControlProperties.Enabled = false;
            }
        }

        public void OnPrimitivePositionChanged(float x, float y, float z)
        {
            if (primitivePositionControl != null)
            {
                primitivePositionControl.SetPosition(x, y, z);
            }
        }

        public void OnPrimitiveRotationChanged(RotationVector vector)
        {
            if (primitiveRotationControl != null)
            {
                primitiveRotationControl.SetRotation(vector);
            }
        }

        public void ShowPosition(bool state)
        {
            tabPagePosition.Enabled = state;
        }

        public void ShowSize(bool state)
        {
            tabPageSize.Enabled = state;
        }

        public void ShowRotation(bool state)
        {
            tabPageRotation.Enabled = state;
        }

        public void ShowAdditional(bool state)
        {
            tabPageAdditional.Enabled = state;
        }

        #endregion
    }
}
