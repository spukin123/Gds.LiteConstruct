using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.BusinessObjects.SizeTypes;
using Gds.LiteConstruct.BusinessObjects.Primitives;
using System.Threading;
using Gds.LiteConstruct.BusinessObjects;

namespace Gds.LiteConstruct.Presentation
{
    public partial class StairsAdditionalControl : UserControl, IPrimitivePropertiesControl
    {
        private IStairsExtendable primitive;
        
        private decimal prevStairsNumber;
        private decimal prevHeight;
        private decimal prevLength;
        private decimal prevLeftTopAngle;
        private decimal prevRightTopAngle;

        private bool binded = true;

        public StairsAdditionalControl(IStairsExtendable primitive)
        {
            InitializeComponent();

            this.primitive = primitive;

            LoadAllData();

            if (primitive.SimpleMode)
            {
                radioButtonStairNumber.Checked = true;
            }
            else if (primitive.StairHeightMode)
            {
                radioButtonStairHeight.Checked = true;
            }
            else if (primitive.StairLengthMode)
            {
                radioButtonStairLength.Checked = true;
            }
        }

        private void Rollback()
        {
            numericUpDownStairsNum.Value = prevStairsNumber;
            numericUpDownStairHeight.Value = prevHeight;
            numericUpDownStairLength.Value = prevLength;
            numericUpDownLeftTopAngle.Value = prevLeftTopAngle;
            numericUpDownRightTopAngle.Value = prevRightTopAngle;

            primitive.ChangesApplied = false;
            primitive.StairsNumber = (int)prevStairsNumber;
            primitive.StairHeight = (float)prevHeight;
            primitive.StairLength = (float)prevLength;
            primitive.LeftTopAngle = Angle.FromDegrees((float)prevLeftTopAngle);
            primitive.RightTopAngle = Angle.FromDegrees((float)prevRightTopAngle);
            primitive.ChangesApplied = true;
        }

        private void Bind()
        {
            binded = true;
        }

        private void Unbind()
        {
            binded = false;
        }

        private void AfterSimpleModeAction()
        {
            Unbind();

            numericUpDownStairHeight.Value = (decimal)primitive.StairHeight;
            numericUpDownStairLength.Value = (decimal)primitive.StairLength;

            Bind();
        }

        private void AfterDetailedHeightModeAction()
        {
            Unbind();

            numericUpDownStairLength.Value = (decimal)primitive.StairLength;
            numericUpDownStairsNum.Value = (decimal)primitive.StairsNumber;

            Bind();
        }

        private void AfterDetailedLengthModeAction()
        {
            Unbind();

            numericUpDownStairHeight.Value = (decimal)primitive.StairHeight;
            numericUpDownStairsNum.Value = (decimal)primitive.StairsNumber;

            Bind();
        }

        private void EnableSimpleMode()
        {
            primitive.EnableSimpleMode();
            primitive.StairsNumber = (int)numericUpDownStairsNum.Value;

            numericUpDownStairsNum.Enabled = true;
            numericUpDownStairHeight.Enabled = false;
            numericUpDownStairLength.Enabled = false;

            AfterSimpleModeAction();
        }

        private void EnableDetailedHeightMode()
        {
            primitive.EnableDetailedHeightMode();
            primitive.StairHeight = (float)numericUpDownStairHeight.Value;

            numericUpDownStairsNum.Enabled = false;
            numericUpDownStairHeight.Enabled = true;
            numericUpDownStairLength.Enabled = false;

            AfterDetailedHeightModeAction();
        }

        private void EnableDetailedLenghtMode()
        {
            primitive.EnableDetailedLengthMode();
            primitive.StairLength = (float)numericUpDownStairLength.Value;

            numericUpDownStairsNum.Enabled = false;
            numericUpDownStairHeight.Enabled = false;
            numericUpDownStairLength.Enabled = true;

            AfterDetailedLengthModeAction();
        }

        private void LoadAllData()
        {
            Unbind();

            InitNumericBounds();

            numericUpDownStairsNum.Value = (decimal)primitive.StairsNumber;
            numericUpDownStairLength.Value = (decimal)primitive.StairLength;
            numericUpDownStairHeight.Value = (decimal)primitive.StairHeight;
            numericUpDownLeftTopAngle.Value = (decimal)primitive.LeftTopAngle.Degrees;
            numericUpDownRightTopAngle.Value = (decimal)primitive.RightTopAngle.Degrees;

            prevStairsNumber = numericUpDownStairsNum.Value;
            prevLength = numericUpDownStairLength.Value;
            prevHeight = numericUpDownStairHeight.Value;
            prevLeftTopAngle = numericUpDownLeftTopAngle.Value;
            prevRightTopAngle = numericUpDownRightTopAngle.Value;

            Bind();
        }

        private void InitNumericBounds()
        {
            numericUpDownStairHeight.Minimum = (decimal)primitive.MinStairHeight;
            numericUpDownStairHeight.Maximum = (decimal)primitive.MaxStairHeight;

            numericUpDownStairLength.Minimum = (decimal)primitive.MinStairLength;
            numericUpDownStairLength.Maximum = (decimal)primitive.MaxStairLength;

            numericUpDownStairsNum.Minimum = (decimal)primitive.MinStairsNumber;
            numericUpDownStairsNum.Maximum = (decimal)primitive.MaxStairsNumber;
        }

        private void radioButtonStairNumber_CheckedChanged(object sender, EventArgs e)
        {
            EnableSimpleMode();
        }

        private void radioButtonStairHeight_CheckedChanged(object sender, EventArgs e)
        {
            EnableDetailedHeightMode();
        }

        private void radioButtonStairLength_CheckedChanged(object sender, EventArgs e)
        {
            EnableDetailedLenghtMode();
        }

        private void numericUpDownStairsNum_ValueChanged(object sender, EventArgs e)
        {
            if (binded)
            {
                primitive.ChangesApplied = false;
                primitive.StairsNumber = (int)numericUpDownStairsNum.Value;
                primitive.ChangesApplied = true;

                Unbind();

                bool flag = true;
                if ((decimal)primitive.StairHeight < numericUpDownStairHeight.Minimum || (decimal)primitive.StairHeight > numericUpDownStairHeight.Maximum)
                {
                    Rollback();
                    flag = false;
                }

                if ((decimal)primitive.StairLength < numericUpDownStairLength.Minimum || (decimal)primitive.StairLength > numericUpDownStairLength.Maximum)
                {
                    Rollback();
                    flag = false;
                }

                if (flag)
                {
                    primitive.StairsNumber = (int)numericUpDownStairsNum.Value;

                    AfterSimpleModeAction();

                    prevStairsNumber = (decimal)primitive.StairsNumber;
                    prevHeight = (decimal)primitive.StairHeight;
                    prevLength = (decimal)primitive.StairLength;
                }

                Bind();
            }
        }

        private void numericUpDownStairHeight_ValueChanged(object sender, EventArgs e)
        {
            if (binded)
            {
                primitive.ChangesApplied = false;
                primitive.StairHeight = (float)numericUpDownStairHeight.Value;
                primitive.ChangesApplied = true;

                Unbind();

                bool flag = true;
                if ((decimal)primitive.StairLength < numericUpDownStairLength.Minimum || (decimal)primitive.StairLength > numericUpDownStairLength.Maximum)
                {
                    Rollback();
                    flag = false;
                }

                if ((decimal)primitive.StairsNumber < numericUpDownStairsNum.Minimum || (decimal)primitive.StairsNumber > numericUpDownStairsNum.Maximum)
                {
                    Rollback();
                    flag = false; 
                }

                if (flag)
                {
                    primitive.StairHeight = (float)numericUpDownStairHeight.Value;

                    AfterDetailedHeightModeAction();

                    prevStairsNumber = (decimal)primitive.StairsNumber;
                    prevHeight = (decimal)primitive.StairHeight;
                    prevLength = (decimal)primitive.StairLength;
                }

                Bind();
            }
        }

        private void numericUpDownStairLength_ValueChanged(object sender, EventArgs e)
        {
            if (binded)
            {
                primitive.ChangesApplied = false;
                primitive.StairLength = (float)numericUpDownStairLength.Value;
                primitive.ChangesApplied = true;

                Unbind();

                bool flag = true;
                if ((decimal)primitive.StairHeight < numericUpDownStairHeight.Minimum || (decimal)primitive.StairHeight > numericUpDownStairHeight.Maximum)
                {
                    Rollback();
                    flag = false;
                }

                if ((decimal)primitive.StairsNumber < numericUpDownStairsNum.Minimum || (decimal)primitive.StairsNumber > numericUpDownStairsNum.Maximum)
                {
                    Rollback();
                    flag = false;
                }

                if (flag)
                {
                    primitive.StairLength = (float)numericUpDownStairLength.Value;

                    AfterDetailedLengthModeAction();

                    prevStairsNumber = (decimal)primitive.StairsNumber;
                    prevHeight = (decimal)primitive.StairHeight;
                    prevLength = (decimal)primitive.StairLength;
                }

                Bind();
            }
        }

        private void numericUpDownLeftTopAngle_ValueChanged(object sender, EventArgs e)
        {
            if (binded)
            {
                primitive.ChangesApplied = false;
                primitive.LeftTopAngle = Angle.FromDegrees((float)numericUpDownLeftTopAngle.Value);
                primitive.ChangesApplied = true;

                Unbind();

                bool noRollback = true;
                if (primitive.BottomBorderLength < primitive.MinStairsX)
                {
                    Rollback();
                    noRollback = false;
                }

                if (noRollback)
                {
                    primitive.LeftTopAngle = Angle.FromDegrees((float)numericUpDownLeftTopAngle.Value);

                    prevLeftTopAngle = (decimal)primitive.LeftTopAngle.Degrees;
                }

                Bind();
            }
        }

        private void numericUpDownRightTopAngle_ValueChanged(object sender, EventArgs e)
        {
            if (binded)
            {
                primitive.ChangesApplied = false;
                primitive.RightTopAngle = Angle.FromDegrees((float)numericUpDownRightTopAngle.Value);
                primitive.ChangesApplied = true;

                Unbind();

                bool noRollback = true;
                if (primitive.BottomBorderLength < primitive.MinStairsX)
                {
                    Rollback();
                    noRollback = false;
                }

                if (noRollback)
                {
                    primitive.RightTopAngle = Angle.FromDegrees((float)numericUpDownRightTopAngle.Value);

                    prevRightTopAngle = (decimal)primitive.RightTopAngle.Degrees;
                }

                Bind();
            }
        }

        #region IPrimitivePropertiesControl Members

        public void SetPrimitive(object primitive)
        {
            this.primitive = primitive as IStairsExtendable;
        }

        #endregion
    }
}
