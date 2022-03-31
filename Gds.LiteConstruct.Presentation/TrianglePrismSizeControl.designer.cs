namespace Gds.LiteConstruct.Presentation
{
    partial class TrianglePrismSizeControl
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownA = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownC = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownB = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownZ = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.bEquilateral = new System.Windows.Forms.Button();
            this.scaleControl = new Gds.LiteConstruct.Presentation.PrimitiveScaleControl();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(172, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "C:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "B:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "A:";
            // 
            // numericUpDownA
            // 
            this.numericUpDownA.DecimalPlaces = 3;
            this.numericUpDownA.Location = new System.Drawing.Point(29, 15);
            this.numericUpDownA.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownA.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownA.Name = "numericUpDownA";
            this.numericUpDownA.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownA.TabIndex = 10;
            this.numericUpDownA.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownA.ValueChanged += new System.EventHandler(this.numericUpDownA_ValueChanged);
            // 
            // numericUpDownC
            // 
            this.numericUpDownC.DecimalPlaces = 3;
            this.numericUpDownC.Location = new System.Drawing.Point(195, 15);
            this.numericUpDownC.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownC.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownC.Name = "numericUpDownC";
            this.numericUpDownC.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownC.TabIndex = 12;
            this.numericUpDownC.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownC.ValueChanged += new System.EventHandler(this.numericUpDownC_ValueChanged);
            // 
            // numericUpDownB
            // 
            this.numericUpDownB.DecimalPlaces = 3;
            this.numericUpDownB.Location = new System.Drawing.Point(112, 15);
            this.numericUpDownB.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownB.Name = "numericUpDownB";
            this.numericUpDownB.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownB.TabIndex = 11;
            this.numericUpDownB.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownB.ValueChanged += new System.EventHandler(this.numericUpDownB_ValueChanged);
            // 
            // numericUpDownZ
            // 
            this.numericUpDownZ.DecimalPlaces = 3;
            this.numericUpDownZ.Location = new System.Drawing.Point(278, 15);
            this.numericUpDownZ.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownZ.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownZ.Name = "numericUpDownZ";
            this.numericUpDownZ.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownZ.TabIndex = 14;
            this.numericUpDownZ.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownZ.ValueChanged += new System.EventHandler(this.numericUpDownZ_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Z:";
            // 
            // bEquilateral
            // 
            this.bEquilateral.Location = new System.Drawing.Point(19, 46);
            this.bEquilateral.Name = "bEquilateral";
            this.bEquilateral.Size = new System.Drawing.Size(75, 22);
            this.bEquilateral.TabIndex = 15;
            this.bEquilateral.Text = "Equilateral";
            this.bEquilateral.UseVisualStyleBackColor = true;
            this.bEquilateral.Click += new System.EventHandler(this.buttonEquilateral_Click);
            // 
            // scaleControl
            // 
            this.scaleControl.Location = new System.Drawing.Point(119, 41);
            this.scaleControl.Name = "scaleControl";
            this.scaleControl.Size = new System.Drawing.Size(203, 32);
            this.scaleControl.TabIndex = 16;
            this.scaleControl.ButtonApplyClick += new Gds.LiteConstruct.Presentation.ScaleEventHandler(this.scaleControl_ButtonApplyClick);
            // 
            // TrianglePrismSizeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scaleControl);
            this.Controls.Add(this.bEquilateral);
            this.Controls.Add(this.numericUpDownZ);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownC);
            this.Controls.Add(this.numericUpDownB);
            this.Controls.Add(this.numericUpDownA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "TrianglePrismSizeControl";
            this.Size = new System.Drawing.Size(363, 77);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownA;
        private System.Windows.Forms.NumericUpDown numericUpDownC;
        private System.Windows.Forms.NumericUpDown numericUpDownB;
        private System.Windows.Forms.NumericUpDown numericUpDownZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bEquilateral;
        private PrimitiveScaleControl scaleControl;
    }
}
