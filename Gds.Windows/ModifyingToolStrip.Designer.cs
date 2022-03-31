namespace Gds.Windows
{
    partial class ModifyingToolStrip
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
            components = new System.ComponentModel.Container();
            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModifyingToolStrip));
            System.Resources.ResourceManager resources = Properties.Resources.ResourceManager;
            this.toolStripButtonCreate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            // 
            // this
            // 
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonCreate,
            this.toolStripButtonEdit,
            this.toolStripButtonDelete});
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "toolStrip1";
            this.Size = new System.Drawing.Size(600, 25);
            this.TabIndex = 1;
            this.Text = "toolStrip1";
            this.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            // 
            // toolStripButton1
            // 
            this.toolStripButtonCreate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            this.toolStripButtonCreate.Image = ((System.Drawing.Image)(Properties.Resources.ToolStripButtonNew));
            this.toolStripButtonCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreate.Name = "toolStripButtonCreate";
            this.toolStripButtonCreate.Size = new System.Drawing.Size(44, 22);
            this.toolStripButtonCreate.Text = "Create";
            // 
            // toolStripButton2
            // 
            this.toolStripButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            this.toolStripButtonEdit.Image = ((System.Drawing.Image)(Properties.Resources.ToolStripButtonEdit));
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Size = new System.Drawing.Size(29, 22);
            this.toolStripButtonEdit.Text = "Edit";
            // 
            // toolStripButton3
            // 
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            this.toolStripButtonDelete.Image = ((System.Drawing.Image)(Properties.Resources.ToolStripButtonDelete));
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(42, 22);
            this.toolStripButtonDelete.Text = "Delete";
        }

        #endregion

        private System.Windows.Forms.ToolStripButton toolStripButtonCreate;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
    }
}
