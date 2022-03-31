namespace Gds.LiteConstruct.Presentation
{
    partial class ColumnSizeControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownZ = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRadius = new System.Windows.Forms.NumericUpDown();
            this.primitiveScaleControl1 = new Gds.LiteConstruct.Presentation.PrimitiveScaleControl();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Z:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Radius:";
            // 
            // numericUpDownZ
            // 
            this.numericUpDownZ.DecimalPlaces = 3;
            this.numericUpDownZ.Location = new System.Drawing.Point(28, 15);
            this.numericUpDownZ.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownZ.Name = "numericUpDownZ";
            this.numericUpDownZ.Size = new System.Drawing.Size(56, 20);
            this.numericUpDownZ.TabIndex = 2;
            this.numericUpDownZ.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownZ.ValueChanged += new System.EventHandler(this.numericUpDownZ_ValueChanged);
            // 
            // numericUpDownRadius
            // 
            this.numericUpDownRadius.DecimalPlaces = 3;
            this.numericUpDownRadius.Location = new System.Drawing.Point(141, 15);
            this.numericUpDownRadius.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRadius.Name = "numericUpDownRadius";
            this.numericUpDownRadius.Size = new System.Drawing.Size(56, 20);
            this.numericUpDownRadius.TabIndex = 3;
            this.numericUpDownRadius.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRadius.ValueChanged += new System.EventHandler(this.numericUpDownRadius_ValueChanged);
            // 
            // primitiveScaleControl1
            // 
            this.primitiveScaleControl1.Location = new System.Drawing.Point(8, 41);
            this.primitiveScaleControl1.Name = "primitiveScaleControl1";
            this.primitiveScaleControl1.Size = new System.Drawing.Size(197, 32);
            this.primitiveScaleControl1.TabIndex = 4;
            this.primitiveScaleControl1.ButtonApplyClick += new Gds.LiteConstruct.Presentation.ScaleEventHandler(this.ScaleControl_Apply);
            // 
            // ColumnSizeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.primitiveScaleControl1);
            this.Controls.Add(this.numericUpDownRadius);
            this.Controls.Add(this.numericUpDownZ);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ColumnSizeControl";
            this.Size = new System.Drawing.Size(300, 78);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownZ;
        private System.Windows.Forms.NumericUpDown numericUpDownRadius;
        private PrimitiveScaleControl primitiveScaleControl1;
    }
}
