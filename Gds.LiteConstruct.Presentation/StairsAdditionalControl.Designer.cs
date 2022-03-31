namespace Gds.LiteConstruct.Presentation
{
    partial class StairsAdditionalControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioButtonStairNumber = new System.Windows.Forms.RadioButton();
            this.radioButtonStairHeight = new System.Windows.Forms.RadioButton();
            this.numericUpDownStairsNum = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownStairHeight = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownStairLength = new System.Windows.Forms.NumericUpDown();
            this.radioButtonStairLength = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownLeftTopAngle = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRightTopAngle = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStairsNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStairHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStairLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeftTopAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRightTopAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButtonStairNumber
            // 
            this.radioButtonStairNumber.AutoSize = true;
            this.radioButtonStairNumber.Location = new System.Drawing.Point(7, 9);
            this.radioButtonStairNumber.Name = "radioButtonStairNumber";
            this.radioButtonStairNumber.Size = new System.Drawing.Size(92, 17);
            this.radioButtonStairNumber.TabIndex = 0;
            this.radioButtonStairNumber.TabStop = true;
            this.radioButtonStairNumber.Text = "Stairs number:";
            this.radioButtonStairNumber.UseVisualStyleBackColor = true;
            this.radioButtonStairNumber.CheckedChanged += new System.EventHandler(this.radioButtonStairNumber_CheckedChanged);
            // 
            // radioButtonStairHeight
            // 
            this.radioButtonStairHeight.AutoSize = true;
            this.radioButtonStairHeight.Location = new System.Drawing.Point(7, 30);
            this.radioButtonStairHeight.Name = "radioButtonStairHeight";
            this.radioButtonStairHeight.Size = new System.Drawing.Size(81, 17);
            this.radioButtonStairHeight.TabIndex = 1;
            this.radioButtonStairHeight.TabStop = true;
            this.radioButtonStairHeight.Text = "Stair height:";
            this.radioButtonStairHeight.UseVisualStyleBackColor = true;
            this.radioButtonStairHeight.CheckedChanged += new System.EventHandler(this.radioButtonStairHeight_CheckedChanged);
            // 
            // numericUpDownStairsNum
            // 
            this.numericUpDownStairsNum.Location = new System.Drawing.Point(107, 7);
            this.numericUpDownStairsNum.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownStairsNum.Name = "numericUpDownStairsNum";
            this.numericUpDownStairsNum.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownStairsNum.TabIndex = 2;
            this.numericUpDownStairsNum.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownStairsNum.ValueChanged += new System.EventHandler(this.numericUpDownStairsNum_ValueChanged);
            // 
            // numericUpDownStairHeight
            // 
            this.numericUpDownStairHeight.DecimalPlaces = 2;
            this.numericUpDownStairHeight.Location = new System.Drawing.Point(107, 29);
            this.numericUpDownStairHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStairHeight.Name = "numericUpDownStairHeight";
            this.numericUpDownStairHeight.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownStairHeight.TabIndex = 5;
            this.numericUpDownStairHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStairHeight.ValueChanged += new System.EventHandler(this.numericUpDownStairHeight_ValueChanged);
            // 
            // numericUpDownStairLength
            // 
            this.numericUpDownStairLength.DecimalPlaces = 2;
            this.numericUpDownStairLength.Location = new System.Drawing.Point(107, 51);
            this.numericUpDownStairLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStairLength.Name = "numericUpDownStairLength";
            this.numericUpDownStairLength.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownStairLength.TabIndex = 7;
            this.numericUpDownStairLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStairLength.ValueChanged += new System.EventHandler(this.numericUpDownStairLength_ValueChanged);
            // 
            // radioButtonStairLength
            // 
            this.radioButtonStairLength.AutoSize = true;
            this.radioButtonStairLength.Location = new System.Drawing.Point(7, 52);
            this.radioButtonStairLength.Name = "radioButtonStairLength";
            this.radioButtonStairLength.Size = new System.Drawing.Size(81, 17);
            this.radioButtonStairLength.TabIndex = 9;
            this.radioButtonStairLength.TabStop = true;
            this.radioButtonStairLength.Text = "Stair length:";
            this.radioButtonStairLength.UseVisualStyleBackColor = true;
            this.radioButtonStairLength.CheckedChanged += new System.EventHandler(this.radioButtonStairLength_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Left top angle:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Right top angle:";
            // 
            // numericUpDownLeftTopAngle
            // 
            this.numericUpDownLeftTopAngle.DecimalPlaces = 2;
            this.numericUpDownLeftTopAngle.Location = new System.Drawing.Point(269, 7);
            this.numericUpDownLeftTopAngle.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDownLeftTopAngle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLeftTopAngle.Name = "numericUpDownLeftTopAngle";
            this.numericUpDownLeftTopAngle.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownLeftTopAngle.TabIndex = 12;
            this.numericUpDownLeftTopAngle.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLeftTopAngle.ValueChanged += new System.EventHandler(this.numericUpDownLeftTopAngle_ValueChanged);
            // 
            // numericUpDownRightTopAngle
            // 
            this.numericUpDownRightTopAngle.DecimalPlaces = 2;
            this.numericUpDownRightTopAngle.Location = new System.Drawing.Point(269, 29);
            this.numericUpDownRightTopAngle.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDownRightTopAngle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRightTopAngle.Name = "numericUpDownRightTopAngle";
            this.numericUpDownRightTopAngle.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownRightTopAngle.TabIndex = 13;
            this.numericUpDownRightTopAngle.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRightTopAngle.ValueChanged += new System.EventHandler(this.numericUpDownRightTopAngle_ValueChanged);
            // 
            // StairsAdditionalControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDownRightTopAngle);
            this.Controls.Add(this.numericUpDownLeftTopAngle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButtonStairLength);
            this.Controls.Add(this.radioButtonStairNumber);
            this.Controls.Add(this.radioButtonStairHeight);
            this.Controls.Add(this.numericUpDownStairLength);
            this.Controls.Add(this.numericUpDownStairHeight);
            this.Controls.Add(this.numericUpDownStairsNum);
            this.Name = "StairsAdditionalControl";
            this.Size = new System.Drawing.Size(374, 74);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStairsNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStairHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStairLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeftTopAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRightTopAngle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonStairNumber;
        private System.Windows.Forms.RadioButton radioButtonStairHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownStairsNum;
        private System.Windows.Forms.NumericUpDown numericUpDownStairHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownStairLength;
        private System.Windows.Forms.RadioButton radioButtonStairLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownLeftTopAngle;
        private System.Windows.Forms.NumericUpDown numericUpDownRightTopAngle;
    }
}
