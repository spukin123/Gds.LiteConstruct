namespace Gds.LiteConstruct.Presentation
{
    partial class ColumnAdditionalControl
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
            this.numericUpDownAnglesNumber = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAnglesNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Angles number:";
            // 
            // numericUpDownAnglesNumber
            // 
            this.numericUpDownAnglesNumber.Location = new System.Drawing.Point(98, 15);
            this.numericUpDownAnglesNumber.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownAnglesNumber.Name = "numericUpDownAnglesNumber";
            this.numericUpDownAnglesNumber.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownAnglesNumber.TabIndex = 1;
            this.numericUpDownAnglesNumber.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownAnglesNumber.ValueChanged += new System.EventHandler(this.numericUpDownAnglesNumber_ValueChanged);
            // 
            // ColumnAdditionalControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDownAnglesNumber);
            this.Controls.Add(this.label1);
            this.Name = "ColumnAdditionalControl";
            this.Size = new System.Drawing.Size(277, 51);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAnglesNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownAnglesNumber;
    }
}
