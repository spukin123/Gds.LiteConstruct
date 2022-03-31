namespace Gds.LiteConstruct.Presentation.Presenters
{
    partial class PrimitivePropertiesPresenter
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
            this.tabControlProperties = new System.Windows.Forms.TabControl();
            this.tabPagePosition = new System.Windows.Forms.TabPage();
            this.tabPageSize = new System.Windows.Forms.TabPage();
            this.tabPageRotation = new System.Windows.Forms.TabPage();
            this.tabPageAdditional = new System.Windows.Forms.TabPage();
            this.tabControlProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Controls.Add(this.tabPagePosition);
            this.tabControlProperties.Controls.Add(this.tabPageSize);
            this.tabControlProperties.Controls.Add(this.tabPageRotation);
            this.tabControlProperties.Controls.Add(this.tabPageAdditional);
            this.tabControlProperties.Location = new System.Drawing.Point(0, 0);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(388, 105);
            this.tabControlProperties.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlProperties.TabIndex = 6;
            this.tabControlProperties.SelectedIndexChanged += new System.EventHandler(this.UpdateTabPage);
            // 
            // tabPagePosition
            // 
            this.tabPagePosition.Location = new System.Drawing.Point(4, 22);
            this.tabPagePosition.Name = "tabPagePosition";
            this.tabPagePosition.Size = new System.Drawing.Size(380, 79);
            this.tabPagePosition.TabIndex = 0;
            this.tabPagePosition.Text = "Position";
            this.tabPagePosition.UseVisualStyleBackColor = true;
            //this.tabPagePosition.Layout += new System.Windows.Forms.LayoutEventHandler(this.UpdateTabPage);
            // 
            // tabPageSize
            // 
            this.tabPageSize.Location = new System.Drawing.Point(4, 22);
            this.tabPageSize.Name = "tabPageSize";
            this.tabPageSize.Size = new System.Drawing.Size(380, 79);
            this.tabPageSize.TabIndex = 0;
            this.tabPageSize.Text = "Size";
            this.tabPageSize.UseVisualStyleBackColor = true;
            //this.tabPageSize.Layout += new System.Windows.Forms.LayoutEventHandler(this.UpdateTabPage);
            // 
            // tabPageRotation
            // 
            this.tabPageRotation.Location = new System.Drawing.Point(4, 22);
            this.tabPageRotation.Name = "tabPageRotation";
            this.tabPageRotation.Size = new System.Drawing.Size(380, 79);
            this.tabPageRotation.TabIndex = 1;
            this.tabPageRotation.Text = "Rotation";
            this.tabPageRotation.UseVisualStyleBackColor = true;
            //this.tabPageRotation.Layout += new System.Windows.Forms.LayoutEventHandler(this.UpdateTabPage);
            // 
            // tabPageAdditional
            // 
            this.tabPageAdditional.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdditional.Name = "tabPageAdditional";
            this.tabPageAdditional.Size = new System.Drawing.Size(380, 79);
            this.tabPageAdditional.TabIndex = 2;
            this.tabPageAdditional.Text = "Additional";
            this.tabPageAdditional.UseVisualStyleBackColor = true;
            //this.tabPageAdditional.Layout += new System.Windows.Forms.LayoutEventHandler(this.UpdateTabPage);
            // 
            // PrimitivePropertiesPresenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlProperties);
            this.Name = "PrimitivePropertiesPresenter";
            this.Size = new System.Drawing.Size(393, 111);
            this.tabControlProperties.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.TabPage tabPagePosition;
        private System.Windows.Forms.TabPage tabPageSize;
        private System.Windows.Forms.TabPage tabPageRotation;
        private System.Windows.Forms.TabPage tabPageAdditional;
    }
}
