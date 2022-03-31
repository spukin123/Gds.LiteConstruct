namespace Gds.LiteConstruct.Presentation.Presenters
{
    partial class GraphicWindowPresenter
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
			this.contextMenuOnEmpty = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miAddPrimitive = new System.Windows.Forms.ToolStripMenuItem();
			this.miWallRectangle = new System.Windows.Forms.ToolStripMenuItem();
			this.miWallTriangle = new System.Windows.Forms.ToolStripMenuItem();
			this.miPlaneRectangle = new System.Windows.Forms.ToolStripMenuItem();
			this.miPlaneTriangle = new System.Windows.Forms.ToolStripMenuItem();
			this.miStairs = new System.Windows.Forms.ToolStripMenuItem();
			this.miCameraMode = new System.Windows.Forms.ToolStripMenuItem();
			this.miRotateCamera = new System.Windows.Forms.ToolStripMenuItem();
			this.miMoveCamera = new System.Windows.Forms.ToolStripMenuItem();
			this.miZoomCamera = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuOnPrimitive = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miToTexturingMode = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.miMove = new System.Windows.Forms.ToolStripMenuItem();
			this.miRotate = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.miClone = new System.Windows.Forms.ToolStripMenuItem();
			this.miCloneAndMove = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.miUnbind = new System.Windows.Forms.ToolStripMenuItem();
			this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.graphicWindow = new System.Windows.Forms.PictureBox();
			this.contextMenuOnEmpty.SuspendLayout();
			this.contextMenuOnPrimitive.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.graphicWindow)).BeginInit();
			this.SuspendLayout();
			// 
			// contextMenuOnEmpty
			// 
			this.contextMenuOnEmpty.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAddPrimitive,
            this.miCameraMode});
			this.contextMenuOnEmpty.Name = "contextMenu1";
			this.contextMenuOnEmpty.Size = new System.Drawing.Size(152, 48);
			// 
			// miAddPrimitive
			// 
			this.miAddPrimitive.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miWallRectangle,
            this.miWallTriangle,
            this.miPlaneRectangle,
            this.miPlaneTriangle,
            this.miStairs});
			this.miAddPrimitive.Name = "miAddPrimitive";
			this.miAddPrimitive.Size = new System.Drawing.Size(151, 22);
			this.miAddPrimitive.Text = "Add Primitive";
			// 
			// miWallRectangle
			// 
			this.miWallRectangle.Name = "miWallRectangle";
			this.miWallRectangle.Size = new System.Drawing.Size(167, 22);
			this.miWallRectangle.Text = "Wall (rectangle)";
			this.miWallRectangle.Click += new System.EventHandler(this.miWallRectangle_Click);
			// 
			// miWallTriangle
			// 
			this.miWallTriangle.Name = "miWallTriangle";
			this.miWallTriangle.Size = new System.Drawing.Size(167, 22);
			this.miWallTriangle.Text = "Wall (triangle)";
			this.miWallTriangle.Click += new System.EventHandler(this.miWallTriangle_Click);
			// 
			// miPlaneRectangle
			// 
			this.miPlaneRectangle.Name = "miPlaneRectangle";
			this.miPlaneRectangle.Size = new System.Drawing.Size(167, 22);
			this.miPlaneRectangle.Text = "Plane (rectangle)";
			this.miPlaneRectangle.Click += new System.EventHandler(this.miPlaneRectangle_Click);
			// 
			// miPlaneTriangle
			// 
			this.miPlaneTriangle.Name = "miPlaneTriangle";
			this.miPlaneTriangle.Size = new System.Drawing.Size(167, 22);
			this.miPlaneTriangle.Text = "Plane (triangle)";
			this.miPlaneTriangle.Click += new System.EventHandler(this.miPlaneTriangle_Click);
			// 
			// miStairs
			// 
			this.miStairs.Name = "miStairs";
			this.miStairs.Size = new System.Drawing.Size(167, 22);
			this.miStairs.Text = "Stairs";
			this.miStairs.Click += new System.EventHandler(this.miStairs_Click);
			// 
			// miCameraMode
			// 
			this.miCameraMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRotateCamera,
            this.miMoveCamera,
            this.miZoomCamera});
			this.miCameraMode.Name = "miCameraMode";
			this.miCameraMode.Size = new System.Drawing.Size(151, 22);
			this.miCameraMode.Text = "Camera Mode";
			this.miCameraMode.Visible = false;
			// 
			// miRotateCamera
			// 
			this.miRotateCamera.Name = "miRotateCamera";
			this.miRotateCamera.Size = new System.Drawing.Size(152, 22);
			this.miRotateCamera.Text = "Rotate";
			this.miRotateCamera.Click += new System.EventHandler(this.miRotateCamera_Click);
			// 
			// miMoveCamera
			// 
			this.miMoveCamera.Name = "miMoveCamera";
			this.miMoveCamera.Size = new System.Drawing.Size(152, 22);
			this.miMoveCamera.Text = "Move";
			this.miMoveCamera.Click += new System.EventHandler(this.miMoveCamera_Click);
			// 
			// miZoomCamera
			// 
			this.miZoomCamera.Name = "miZoomCamera";
			this.miZoomCamera.Size = new System.Drawing.Size(152, 22);
			this.miZoomCamera.Text = "Zoom";
			this.miZoomCamera.Click += new System.EventHandler(this.miZoomCamera_Click);
			// 
			// contextMenuOnPrimitive
			// 
			this.contextMenuOnPrimitive.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miToTexturingMode,
            this.toolStripSeparator1,
            this.miMove,
            this.miRotate,
            this.toolStripSeparator2,
            this.miClone,
            this.miCloneAndMove,
            this.toolStripSeparator3,
            this.miUnbind,
            this.miDelete});
			this.contextMenuOnPrimitive.Name = "contextMenu2";
			this.contextMenuOnPrimitive.Size = new System.Drawing.Size(176, 198);
			// 
			// miToTexturingMode
			// 
			this.miToTexturingMode.Name = "miToTexturingMode";
			this.miToTexturingMode.Size = new System.Drawing.Size(175, 22);
			this.miToTexturingMode.Text = "To Texturing Mode";
			this.miToTexturingMode.Click += new System.EventHandler(this.texturingModeToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(172, 6);
			// 
			// miMove
			// 
			this.miMove.Name = "miMove";
			this.miMove.Size = new System.Drawing.Size(175, 22);
			this.miMove.Text = "Move";
			this.miMove.Click += new System.EventHandler(this.miMove_Click);
			// 
			// miRotate
			// 
			this.miRotate.Name = "miRotate";
			this.miRotate.Size = new System.Drawing.Size(175, 22);
			this.miRotate.Text = "Rotate";
			this.miRotate.Click += new System.EventHandler(this.miRotate_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(172, 6);
			// 
			// miClone
			// 
			this.miClone.Name = "miClone";
			this.miClone.Size = new System.Drawing.Size(175, 22);
			this.miClone.Text = "Clone";
			this.miClone.Click += new System.EventHandler(this.cloneToolStripMenuItem_Click);
			// 
			// miCloneAndMove
			// 
			this.miCloneAndMove.Name = "miCloneAndMove";
			this.miCloneAndMove.Size = new System.Drawing.Size(175, 22);
			this.miCloneAndMove.Text = "Clone And Move";
			this.miCloneAndMove.Visible = false;
			this.miCloneAndMove.Click += new System.EventHandler(this.miCloneAndMove_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(172, 6);
			// 
			// miUnbind
			// 
			this.miUnbind.Enabled = false;
			this.miUnbind.Name = "miUnbind";
			this.miUnbind.Size = new System.Drawing.Size(175, 22);
			this.miUnbind.Text = "Unbind";
			this.miUnbind.Visible = false;
			// 
			// miDelete
			// 
			this.miDelete.Name = "miDelete";
			this.miDelete.Size = new System.Drawing.Size(175, 22);
			this.miDelete.Text = "Delete";
			this.miDelete.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// graphicWindow
			// 
			this.graphicWindow.BackColor = System.Drawing.SystemColors.Control;
			this.graphicWindow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.graphicWindow.Cursor = System.Windows.Forms.Cursors.Default;
			this.graphicWindow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.graphicWindow.Location = new System.Drawing.Point(0, 0);
			this.graphicWindow.Name = "graphicWindow";
			this.graphicWindow.Size = new System.Drawing.Size(337, 295);
			this.graphicWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.graphicWindow.TabIndex = 1;
			this.graphicWindow.TabStop = false;
			this.graphicWindow.DoubleClick += new System.EventHandler(this.pictureBoxScene_DoubleClick);
			this.graphicWindow.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBoxScene_MouseWheel);
			this.graphicWindow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxScene_MouseDown);
			this.graphicWindow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxScene_MouseMove);
			this.graphicWindow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxScene_Click);
			this.graphicWindow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxScene_MouseUp);
			// 
			// GraphicWindowPresenter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.graphicWindow);
			this.Name = "GraphicWindowPresenter";
			this.Size = new System.Drawing.Size(337, 295);
			this.contextMenuOnEmpty.ResumeLayout(false);
			this.contextMenuOnPrimitive.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.graphicWindow)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox graphicWindow;
        private System.Windows.Forms.ContextMenuStrip contextMenuOnEmpty;
        private System.Windows.Forms.ContextMenuStrip contextMenuOnPrimitive;
        private System.Windows.Forms.ToolStripMenuItem miToTexturingMode;
        private System.Windows.Forms.ToolStripMenuItem miAddPrimitive;
        private System.Windows.Forms.ToolStripMenuItem miWallRectangle;
        private System.Windows.Forms.ToolStripMenuItem miWallTriangle;
        private System.Windows.Forms.ToolStripMenuItem miPlaneRectangle;
		private System.Windows.Forms.ToolStripMenuItem miPlaneTriangle;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem miStairs;
        private System.Windows.Forms.ToolStripMenuItem miClone;
		private System.Windows.Forms.ToolStripMenuItem miMove;
		private System.Windows.Forms.ToolStripMenuItem miRotate;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem miUnbind;
		private System.Windows.Forms.ToolStripMenuItem miCloneAndMove;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem miCameraMode;
		private System.Windows.Forms.ToolStripMenuItem miRotateCamera;
		private System.Windows.Forms.ToolStripMenuItem miMoveCamera;
		private System.Windows.Forms.ToolStripMenuItem miZoomCamera;
    }
}
