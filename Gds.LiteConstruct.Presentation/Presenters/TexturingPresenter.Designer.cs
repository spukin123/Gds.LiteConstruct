namespace Gds.LiteConstruct.Presentation.Presenters
{
    partial class TexturingPresenter
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
            this.lbTextures = new System.Windows.Forms.ListBox();
            this.textureBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsTexture = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miApply = new System.Windows.Forms.ToolStripMenuItem();
            this.miApplyToAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miMakeDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.miProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxCategories = new System.Windows.Forms.ComboBox();
            this.texturesCategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddCategory = new System.Windows.Forms.ToolStripButton();
            this.tsbCategoryProperties = new System.Windows.Forms.ToolStripButton();
            this.tsbDeleteCategory = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbApplyTexture = new System.Windows.Forms.ToolStripButton();
            this.tsbApplyToAll = new System.Windows.Forms.ToolStripButton();
            this.tsbAddTexture = new System.Windows.Forms.ToolStripButton();
            this.tsbTextureProperties = new System.Windows.Forms.ToolStripButton();
            this.tsbDeleteTexture = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.tsbMarkAsDefault = new System.Windows.Forms.ToolStripButton();
            this.lblDefaultTexture = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownAngle = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.textureBindingSource)).BeginInit();
            this.cmsTexture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.texturesCategoryBindingSource)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTextures
            // 
            this.lbTextures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTextures.DataSource = this.textureBindingSource;
            this.lbTextures.DisplayMember = "Name";
            this.lbTextures.FormattingEnabled = true;
            this.lbTextures.IntegralHeight = false;
            this.lbTextures.Location = new System.Drawing.Point(6, 63);
            this.lbTextures.Name = "lbTextures";
            this.lbTextures.Size = new System.Drawing.Size(111, 81);
            this.lbTextures.Sorted = true;
            this.lbTextures.TabIndex = 1;
            this.lbTextures.DoubleClick += new System.EventHandler(this.OnLbTexturesDoubleClick);
            this.lbTextures.SelectedIndexChanged += new System.EventHandler(this.OnLbTexturesSelectedIndexChanged);
            this.lbTextures.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnLbTexturesMouseUp);
            // 
            // textureBindingSource
            // 
            this.textureBindingSource.DataSource = typeof(Gds.LiteConstruct.Environment.TextureInfo);
            // 
            // cmsTexture
            // 
            this.cmsTexture.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miApply,
            this.miApplyToAll,
            this.miMakeDefault,
            this.toolStripSeparator1,
            this.miPreview,
            this.miDelete,
            this.toolStripSeparator2,
            this.miProperties});
            this.cmsTexture.Name = "cmsTexture";
            this.cmsTexture.Size = new System.Drawing.Size(160, 148);
            // 
            // miApply
            // 
            this.miApply.Name = "miApply";
            this.miApply.Size = new System.Drawing.Size(159, 22);
            this.miApply.Text = "Apply";
            // 
            // miApplyToAll
            // 
            this.miApplyToAll.Name = "miApplyToAll";
            this.miApplyToAll.Size = new System.Drawing.Size(159, 22);
            this.miApplyToAll.Text = "Apply to all";
            // 
            // miMakeDefault
            // 
            this.miMakeDefault.Name = "miMakeDefault";
            this.miMakeDefault.Size = new System.Drawing.Size(159, 22);
            this.miMakeDefault.Text = "Mark as default";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(156, 6);
            // 
            // miPreview
            // 
            this.miPreview.Name = "miPreview";
            this.miPreview.Size = new System.Drawing.Size(159, 22);
            this.miPreview.Text = "Preview...";
            // 
            // miDelete
            // 
            this.miDelete.Name = "miDelete";
            this.miDelete.Size = new System.Drawing.Size(159, 22);
            this.miDelete.Text = "Delete";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(156, 6);
            // 
            // miProperties
            // 
            this.miProperties.Name = "miProperties";
            this.miProperties.Size = new System.Drawing.Size(159, 22);
            this.miProperties.Text = "Properties...";
            // 
            // comboBoxCategories
            // 
            this.comboBoxCategories.DataSource = this.texturesCategoryBindingSource;
            this.comboBoxCategories.DisplayMember = "Name";
            this.comboBoxCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCategories.FormattingEnabled = true;
            this.comboBoxCategories.Location = new System.Drawing.Point(6, 19);
            this.comboBoxCategories.Name = "comboBoxCategories";
            this.comboBoxCategories.Size = new System.Drawing.Size(111, 21);
            this.comboBoxCategories.TabIndex = 6;
            this.comboBoxCategories.SelectedIndexChanged += new System.EventHandler(this.OnCmbCategoriesSelectedIndexChanged);
            // 
            // texturesCategoryBindingSource
            // 
            this.texturesCategoryBindingSource.DataSource = typeof(Gds.LiteConstruct.Environment.TexturesCategory);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddCategory,
            this.tsbCategoryProperties,
            this.tsbDeleteCategory});
            this.toolStrip1.Location = new System.Drawing.Point(6, 43);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(72, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAddCategory
            // 
            this.tsbAddCategory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddCategory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddCategory.Name = "tsbAddCategory";
            this.tsbAddCategory.Size = new System.Drawing.Size(23, 22);
            this.tsbAddCategory.Text = "Add category";
            // 
            // tsbCategoryProperties
            // 
            this.tsbCategoryProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCategoryProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCategoryProperties.Name = "tsbCategoryProperties";
            this.tsbCategoryProperties.Size = new System.Drawing.Size(23, 22);
            this.tsbCategoryProperties.Text = "Category properties";
            // 
            // tsbDeleteCategory
            // 
            this.tsbDeleteCategory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDeleteCategory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteCategory.Name = "tsbDeleteCategory";
            this.tsbDeleteCategory.Size = new System.Drawing.Size(23, 22);
            this.tsbDeleteCategory.Text = "Delete category";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStrip2.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbApplyTexture,
            this.tsbApplyToAll,
            this.tsbAddTexture,
            this.tsbTextureProperties,
            this.tsbDeleteTexture});
            this.toolStrip2.Location = new System.Drawing.Point(6, 147);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(118, 25);
            this.toolStrip2.TabIndex = 11;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbApplyTexture
            // 
            this.tsbApplyTexture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbApplyTexture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbApplyTexture.Name = "tsbApplyTexture";
            this.tsbApplyTexture.Size = new System.Drawing.Size(23, 22);
            this.tsbApplyTexture.Text = "Apply";
            // 
            // tsbApplyToAll
            // 
            this.tsbApplyToAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbApplyToAll.Image = global::Gds.LiteConstruct.Presentation.Properties.Resources.Fill;
            this.tsbApplyToAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbApplyToAll.Name = "tsbApplyToAll";
            this.tsbApplyToAll.Size = new System.Drawing.Size(23, 22);
            this.tsbApplyToAll.Text = "Aplly to all";
            // 
            // tsbAddTexture
            // 
            this.tsbAddTexture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddTexture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddTexture.Name = "tsbAddTexture";
            this.tsbAddTexture.Size = new System.Drawing.Size(23, 22);
            this.tsbAddTexture.Text = "Add texture";
            // 
            // tsbTextureProperties
            // 
            this.tsbTextureProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTextureProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTextureProperties.Name = "tsbTextureProperties";
            this.tsbTextureProperties.Size = new System.Drawing.Size(23, 22);
            this.tsbTextureProperties.Text = "Texture properties";
            // 
            // tsbDeleteTexture
            // 
            this.tsbDeleteTexture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDeleteTexture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteTexture.Name = "tsbDeleteTexture";
            this.tsbDeleteTexture.Size = new System.Drawing.Size(23, 22);
            this.tsbDeleteTexture.Text = "Delete texture";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.toolStrip3);
            this.groupBox1.Controls.Add(this.lblDefaultTexture);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.toolStrip2);
            this.groupBox1.Controls.Add(this.lbTextures);
            this.groupBox1.Location = new System.Drawing.Point(1, 85);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(123, 179);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Textures";
            // 
            // toolStrip3
            // 
            this.toolStrip3.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMarkAsDefault});
            this.toolStrip3.Location = new System.Drawing.Point(90, 14);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(26, 25);
            this.toolStrip3.TabIndex = 14;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // tsbMarkAsDefault
            // 
            this.tsbMarkAsDefault.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMarkAsDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMarkAsDefault.Name = "tsbMarkAsDefault";
            this.tsbMarkAsDefault.Size = new System.Drawing.Size(23, 22);
            this.tsbMarkAsDefault.Text = "Mark as default";
            // 
            // lblDefaultTexture
            // 
            this.lblDefaultTexture.AutoSize = true;
            this.lblDefaultTexture.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDefaultTexture.Location = new System.Drawing.Point(6, 41);
            this.lblDefaultTexture.Name = "lblDefaultTexture";
            this.lblDefaultTexture.Size = new System.Drawing.Size(41, 13);
            this.lblDefaultTexture.TabIndex = 13;
            this.lblDefaultTexture.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Default texture:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxCategories);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Location = new System.Drawing.Point(1, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(123, 76);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Categories";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.numericUpDownAngle);
            this.groupBox3.Location = new System.Drawing.Point(0, 270);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(123, 49);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Texture rotation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Angle:";
            // 
            // numericUpDownAngle
            // 
            this.numericUpDownAngle.Location = new System.Drawing.Point(50, 20);
            this.numericUpDownAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDownAngle.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numericUpDownAngle.Name = "numericUpDownAngle";
            this.numericUpDownAngle.Size = new System.Drawing.Size(43, 20);
            this.numericUpDownAngle.TabIndex = 0;
            this.numericUpDownAngle.ValueChanged += new System.EventHandler(this.OnNumUpDownAngleValueChanged);
            // 
            // TexturingPresenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TexturingPresenter";
            this.Size = new System.Drawing.Size(124, 351);
            this.Load += new System.EventHandler(this.OnLoad);
            ((System.ComponentModel.ISupportInitialize)(this.textureBindingSource)).EndInit();
            this.cmsTexture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.texturesCategoryBindingSource)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbTextures;
        private System.Windows.Forms.BindingSource textureBindingSource;
        private System.Windows.Forms.ComboBox comboBoxCategories;
        private System.Windows.Forms.BindingSource texturesCategoryBindingSource;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAddCategory;
        private System.Windows.Forms.ToolStripButton tsbDeleteCategory;
        private System.Windows.Forms.ToolStripButton tsbCategoryProperties;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbAddTexture;
        private System.Windows.Forms.ToolStripButton tsbDeleteTexture;
        private System.Windows.Forms.ToolStripButton tsbTextureProperties;
		private System.Windows.Forms.ToolStripButton tsbApplyTexture;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblDefaultTexture;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownAngle;
		private System.Windows.Forms.ToolStrip toolStrip3;
		private System.Windows.Forms.ToolStripButton tsbMarkAsDefault;
		private System.Windows.Forms.ToolStripButton tsbApplyToAll;
		private System.Windows.Forms.ContextMenuStrip cmsTexture;
		private System.Windows.Forms.ToolStripMenuItem miPreview;
		private System.Windows.Forms.ToolStripMenuItem miProperties;
		private System.Windows.Forms.ToolStripMenuItem miDelete;
		private System.Windows.Forms.ToolStripMenuItem miApply;
		private System.Windows.Forms.ToolStripMenuItem miApplyToAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem miMakeDefault;
    }
}
