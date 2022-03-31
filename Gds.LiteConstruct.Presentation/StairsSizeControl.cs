using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;

namespace Gds.LiteConstruct.Presentation
{
    public partial class StairsSizeControl : UserControl, IPrimitivePropertiesControl
    {
        private IStairsSizable primitive;

        private bool binded;
        private decimal prevX, prevY, prevZ;

        public StairsSizeControl(IStairsSizable primitive)
        {
            InitializeComponent();

            this.primitive = primitive;

            LoadAllData();
        }

        private void Bind()
        {
            binded = true;
        }

        private void Unbind()
        {
            binded = false;
        }

        private void LoadAllData()
        {
            Unbind();

            numericUpDownX.Value = (decimal)primitive.X;
            numericUpDownY.Value = (decimal)primitive.Y;
            numericUpDownZ.Value = (decimal)primitive.Z;

            prevX = (decimal)primitive.X;
            prevY = (decimal)primitive.Y;
            prevZ = (decimal)primitive.Z;

            Bind();
        }

        private void Rollback()
        {
            Unbind();

            primitive.ChangesApplied = false;
            primitive.X = (float)prevX;
            primitive.Y = (float)prevY;
            primitive.Z = (float)prevZ;
            primitive.ChangesApplied = true;

            numericUpDownX.Value = prevX;
            numericUpDownY.Value = prevY;
            numericUpDownZ.Value = prevZ;

            Bind();
        }

        private bool CheckForRollback()
        {
            IStairsExtendable extendedPrimitive = primitive as IStairsExtendable;
            
            if (extendedPrimitive.StairHeight < primitive.MinStairHeight || extendedPrimitive.StairHeight > primitive.MaxStairHeight)
            {
                Rollback();
                return true;
            }

            if (extendedPrimitive.StairLength < primitive.MinStairLength || extendedPrimitive.StairLength > primitive.MaxStairLength)
            {
                Rollback();
                return true;
            }

            if (extendedPrimitive.StairsNumber < primitive.MinStairsNumber || extendedPrimitive.StairsNumber > primitive.MaxStairsNumber)
            {
                Rollback();
                return true;
            }

            if (primitive.X < extendedPrimitive.MinStairsX || extendedPrimitive.BottomBorderLength < extendedPrimitive.MinStairsX)
            {
                Rollback();
                return true;
            }

            return false;
        }

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            if (binded)
            {
                primitive.ChangesApplied = false;
                primitive.X = (float)numericUpDownX.Value;
                primitive.ChangesApplied = true;

                if (!CheckForRollback())
                {
                    primitive.X = (float)numericUpDownX.Value;
                    prevX = numericUpDownX.Value;
                }
            }
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            if (binded)
            {
                primitive.ChangesApplied = false;
                primitive.Y = (float)numericUpDownY.Value;
                primitive.ChangesApplied = true;

                if (!CheckForRollback())
                {
                    primitive.Y = (float)numericUpDownY.Value;
                    prevY = numericUpDownY.Value;
                }
            }
        }

        private void numericUpDownZ_ValueChanged(object sender, EventArgs e)
        {
            if (binded)
            {
                primitive.ChangesApplied = false;
                primitive.Z = (float)numericUpDownZ.Value;
                primitive.ChangesApplied = true;

                if (!CheckForRollback())
                {
                    primitive.Z = (float)numericUpDownZ.Value;
                    prevZ = numericUpDownZ.Value;
                }
            }
        }

        private void ScaleControl_Apply(float scaleFactor)
        {
            primitive.ChangesApplied = false;
            primitive.Scale(scaleFactor);
            primitive.ChangesApplied = true;

            if (!CheckForRollback())
            {
                Unbind();

                primitive.X = primitive.X;
                primitive.Y = primitive.Y;
                primitive.Z = primitive.Z;

                numericUpDownX.Value = (decimal)primitive.X;
                numericUpDownY.Value = (decimal)primitive.Y;
                numericUpDownZ.Value = (decimal)primitive.Z;

                prevX = (decimal)primitive.X;
                prevY = (decimal)primitive.Y;
                prevZ = (decimal)primitive.Z;

                Bind();
            }
        }

        #region IPrimitivePropertiesControl Members

        public void SetPrimitive(object primitive)
        {
            this.primitive = primitive as IStairsSizable;
        }

        #endregion
    }
}
