namespace Gds.LiteConstruct.Presentation.Settings
{
	partial class SceneSettingsControl
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
			this.components = new System.ComponentModel.Container();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.pbGridColor = new System.Windows.Forms.PictureBox();
			this.btnChooseGridColor = new System.Windows.Forms.Button();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btChooseBackColor = new System.Windows.Forms.Button();
			this.pbBackColor = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbGridColor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbBackColor)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource, "CellLength", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.numericUpDown1.DecimalPlaces = 1;
			this.numericUpDown1.Location = new System.Drawing.Point(113, 24);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(55, 20);
			this.numericUpDown1.TabIndex = 0;
			this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			// 
			// bindingSource
			// 
			this.bindingSource.DataSource = typeof(Gds.LiteConstruct.Rendering.SceneSettings);
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource, "CellCount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.numericUpDown2.Location = new System.Drawing.Point(113, 61);
			this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(55, 20);
			this.numericUpDown2.TabIndex = 1;
			this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// pbGridColor
			// 
			this.pbGridColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbGridColor.Location = new System.Drawing.Point(113, 96);
			this.pbGridColor.Name = "pbGridColor";
			this.pbGridColor.Size = new System.Drawing.Size(22, 22);
			this.pbGridColor.TabIndex = 2;
			this.pbGridColor.TabStop = false;
			// 
			// btnChooseGridColor
			// 
			this.btnChooseGridColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnChooseGridColor.Location = new System.Drawing.Point(143, 96);
			this.btnChooseGridColor.Name = "btnChooseGridColor";
			this.btnChooseGridColor.Size = new System.Drawing.Size(25, 23);
			this.btnChooseGridColor.TabIndex = 3;
			this.btnChooseGridColor.Text = "...";
			this.btnChooseGridColor.UseVisualStyleBackColor = true;
			this.btnChooseGridColor.Click += new System.EventHandler(this.btnChooseGridColor_Click);
			// 
			// colorDialog
			// 
			this.colorDialog.AnyColor = true;
			this.colorDialog.FullOpen = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Cell width:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Cells count:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 101);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(55, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Grid color:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(25, 165);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(94, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Background color:";
			// 
			// btChooseBackColor
			// 
			this.btChooseBackColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btChooseBackColor.Location = new System.Drawing.Point(157, 160);
			this.btChooseBackColor.Name = "btChooseBackColor";
			this.btChooseBackColor.Size = new System.Drawing.Size(25, 23);
			this.btChooseBackColor.TabIndex = 8;
			this.btChooseBackColor.Text = "...";
			this.btChooseBackColor.UseVisualStyleBackColor = true;
			this.btChooseBackColor.Click += new System.EventHandler(this.btChooseBackColor_Click);
			// 
			// pbBackColor
			// 
			this.pbBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbBackColor.Location = new System.Drawing.Point(127, 160);
			this.pbBackColor.Name = "pbBackColor";
			this.pbBackColor.Size = new System.Drawing.Size(22, 22);
			this.pbBackColor.TabIndex = 7;
			this.pbBackColor.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.numericUpDown2);
			this.groupBox1.Controls.Add(this.pbGridColor);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.numericUpDown1);
			this.groupBox1.Controls.Add(this.btnChooseGridColor);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(14, 14);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(188, 132);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Coordinate system";
			// 
			// SceneSettingsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btChooseBackColor);
			this.Controls.Add(this.pbBackColor);
			this.Name = "SceneSettingsControl";
			this.Size = new System.Drawing.Size(255, 207);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbGridColor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbBackColor)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.PictureBox pbGridColor;
		private System.Windows.Forms.Button btnChooseGridColor;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.BindingSource bindingSource;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btChooseBackColor;
		private System.Windows.Forms.PictureBox pbBackColor;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}
