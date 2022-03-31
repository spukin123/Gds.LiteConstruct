namespace Gds.LiteConstruct.Presentation
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.labelFPS = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.miNew = new System.Windows.Forms.ToolStripMenuItem();
			this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.miSave = new System.Windows.Forms.ToolStripMenuItem();
			this.miSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.miExit = new System.Windows.Forms.ToolStripMenuItem();
			this.miTools = new System.Windows.Forms.ToolStripMenuItem();
			this.miSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.graphicWindowPresenter = new Gds.LiteConstruct.Presentation.Presenters.GraphicWindowPresenter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.primitivePropertiesPresenter = new Gds.LiteConstruct.Presentation.Presenters.PrimitivePropertiesPresenter();
			this.cameraModeSwitcherController = new Gds.LiteConstruct.Presentation.Presenters.CameraSwitcherPresenter();
			this.panel2 = new System.Windows.Forms.Panel();
			this.primitiveManagerPresenter = new Gds.LiteConstruct.Presentation.Presenters.PrimitiveManagerPresenter();
			this.renderModeSwitcherController = new Gds.LiteConstruct.Presentation.Presenters.RenderModeSwitcherController();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pictureBox1.Image = global::Gds.LiteConstruct.Presentation.Properties.Resources.DarkGrayLine;
			this.pictureBox1.Location = new System.Drawing.Point(8, 339);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(113, 2);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 18;
			this.pictureBox1.TabStop = false;
			// 
			// labelFPS
			// 
			this.labelFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelFPS.AutoSize = true;
			this.labelFPS.Location = new System.Drawing.Point(44, 344);
			this.labelFPS.Name = "labelFPS";
			this.labelFPS.Size = new System.Drawing.Size(35, 13);
			this.labelFPS.TabIndex = 17;
			this.labelFPS.Text = "label2";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(130)))), ((int)(((byte)(30)))));
			this.label1.Location = new System.Drawing.Point(8, 344);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 13);
			this.label1.TabIndex = 16;
			this.label1.Text = "FPS:";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.miTools,
            this.toolStripMenuItem2});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.menuStrip1.Size = new System.Drawing.Size(797, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNew,
            this.miOpen,
            this.toolStripSeparator1,
            this.miSave,
            this.miSaveAs,
            this.toolStripSeparator2,
            this.miExit});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(35, 20);
			this.toolStripMenuItem1.Text = "File";
			// 
			// miNew
			// 
			this.miNew.Name = "miNew";
			this.miNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.miNew.Size = new System.Drawing.Size(163, 22);
			this.miNew.Text = "New";
			this.miNew.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// miOpen
			// 
			this.miOpen.Name = "miOpen";
			this.miOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.miOpen.Size = new System.Drawing.Size(163, 22);
			this.miOpen.Text = "Open...";
			this.miOpen.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(160, 6);
			// 
			// miSave
			// 
			this.miSave.Name = "miSave";
			this.miSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.miSave.Size = new System.Drawing.Size(163, 22);
			this.miSave.Text = "Save";
			this.miSave.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// miSaveAs
			// 
			this.miSaveAs.Name = "miSaveAs";
			this.miSaveAs.Size = new System.Drawing.Size(163, 22);
			this.miSaveAs.Text = "Save As...";
			this.miSaveAs.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(160, 6);
			// 
			// miExit
			// 
			this.miExit.Name = "miExit";
			this.miExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.miExit.Size = new System.Drawing.Size(163, 22);
			this.miExit.Text = "Exit";
			this.miExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// miTools
			// 
			this.miTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSettings});
			this.miTools.Name = "miTools";
			this.miTools.Size = new System.Drawing.Size(44, 20);
			this.miTools.Text = "Tools";
			// 
			// miSettings
			// 
			this.miSettings.Name = "miSettings";
			this.miSettings.Size = new System.Drawing.Size(136, 22);
			this.miSettings.Text = "Settings...";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAbout});
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(40, 20);
			this.toolStripMenuItem2.Text = "Help";
			// 
			// miAbout
			// 
			this.miAbout.Name = "miAbout";
			this.miAbout.Size = new System.Drawing.Size(126, 22);
			this.miAbout.Text = "About Lite Construct...";
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "lcf";
			this.openFileDialog.Filter = "Lite Construct Files (*.lcf)|*.lcf";
			this.openFileDialog.Title = "Open file";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "lcf";
			this.saveFileDialog.Filter = "Lite Construct Files (*.lcf)|*.lcf";
			this.saveFileDialog.Title = "Save file";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.graphicWindowPresenter, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.renderModeSwitcherController, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(797, 475);
			this.tableLayoutPanel1.TabIndex = 19;
			// 
			// graphicWindowPresenter
			// 
			this.graphicWindowPresenter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.graphicWindowPresenter.GraphicWindowController = null;
			this.graphicWindowPresenter.Location = new System.Drawing.Point(1, 1);
			this.graphicWindowPresenter.Margin = new System.Windows.Forms.Padding(1);
			this.graphicWindowPresenter.Name = "graphicWindowPresenter";
			this.graphicWindowPresenter.Size = new System.Drawing.Size(660, 363);
			this.graphicWindowPresenter.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.primitivePropertiesPresenter);
			this.panel1.Controls.Add(this.cameraModeSwitcherController);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(3, 368);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(656, 104);
			this.panel1.TabIndex = 16;
			// 
			// primitivePropertiesPresenter
			// 
			this.primitivePropertiesPresenter.Location = new System.Drawing.Point(0, 0);
			this.primitivePropertiesPresenter.Margin = new System.Windows.Forms.Padding(0);
			this.primitivePropertiesPresenter.Name = "primitivePropertiesPresenter";
			this.primitivePropertiesPresenter.PrimitivePropertiesController = null;
			this.primitivePropertiesPresenter.Size = new System.Drawing.Size(393, 103);
			this.primitivePropertiesPresenter.TabIndex = 7;
			// 
			// cameraModeSwitcherController
			// 
			this.cameraModeSwitcherController.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cameraModeSwitcherController.CameraSwitcherController = null;
			this.cameraModeSwitcherController.Location = new System.Drawing.Point(558, 0);
			this.cameraModeSwitcherController.Name = "cameraModeSwitcherController";
			this.cameraModeSwitcherController.Size = new System.Drawing.Size(96, 108);
			this.cameraModeSwitcherController.TabIndex = 8;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.primitiveManagerPresenter);
			this.panel2.Controls.Add(this.labelFPS);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.pictureBox1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(664, 2);
			this.panel2.Margin = new System.Windows.Forms.Padding(2);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(131, 361);
			this.panel2.TabIndex = 17;
			// 
			// primitiveManagerPresenter
			// 
			this.primitiveManagerPresenter.Location = new System.Drawing.Point(3, 3);
			this.primitiveManagerPresenter.Margin = new System.Windows.Forms.Padding(0);
			this.primitiveManagerPresenter.Name = "primitiveManagerPresenter";
			this.primitiveManagerPresenter.PrimitiveManagerController = null;
			this.primitiveManagerPresenter.Size = new System.Drawing.Size(124, 333);
			this.primitiveManagerPresenter.TabIndex = 0;
			// 
			// renderModeSwitcherController
			// 
			this.renderModeSwitcherController.Location = new System.Drawing.Point(665, 368);
			this.renderModeSwitcherController.Name = "renderModeSwitcherController";
			this.renderModeSwitcherController.RenderModeController = null;
			this.renderModeSwitcherController.Size = new System.Drawing.Size(129, 103);
			this.renderModeSwitcherController.TabIndex = 15;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
			this.ClientSize = new System.Drawing.Size(797, 499);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(805, 533);
			this.Name = "MainForm";
			this.Text = "3D Object Builder";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miNew;
        private System.Windows.Forms.ToolStripMenuItem miOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miSave;
        private System.Windows.Forms.ToolStripMenuItem miSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem miAbout;
        private System.Windows.Forms.Label labelFPS;
        private System.Windows.Forms.Label label1;
        private Gds.LiteConstruct.Presentation.Presenters.PrimitivePropertiesPresenter primitivePropertiesPresenter;
        private Gds.LiteConstruct.Presentation.Presenters.RenderModeSwitcherController renderModeSwitcherController;
        private Gds.LiteConstruct.Presentation.Presenters.CameraSwitcherPresenter cameraModeSwitcherController;
        private Gds.LiteConstruct.Presentation.Presenters.GraphicWindowPresenter graphicWindowPresenter;
        private Gds.LiteConstruct.Presentation.Presenters.PrimitiveManagerPresenter primitiveManagerPresenter;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ToolStripMenuItem miTools;
		private System.Windows.Forms.ToolStripMenuItem miSettings;
    }
}

