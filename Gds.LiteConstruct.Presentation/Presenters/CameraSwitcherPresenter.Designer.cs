namespace Gds.LiteConstruct.Presentation.Presenters
{
    partial class CameraSwitcherPresenter
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
            this.whiteContainer = new Gds.Windows.WhiteContainer();
            this.radioButtonCameraModeZoom = new System.Windows.Forms.RadioButton();
            this.radioButtonCameraModeMove = new System.Windows.Forms.RadioButton();
            this.radioButtonCameraModeRotate = new System.Windows.Forms.RadioButton();
            this.whiteContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // whiteContainer
            // 
            this.whiteContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.whiteContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.whiteContainer.Controls.Add(this.radioButtonCameraModeZoom);
            this.whiteContainer.Controls.Add(this.radioButtonCameraModeMove);
            this.whiteContainer.Controls.Add(this.radioButtonCameraModeRotate);
            this.whiteContainer.Location = new System.Drawing.Point(0, 0);
            this.whiteContainer.Name = "whiteContainer";
            this.whiteContainer.Processing = false;
            this.whiteContainer.Size = new System.Drawing.Size(96, 103);
            this.whiteContainer.TabIndex = 8;
            this.whiteContainer.TitleIndent = 3;
            this.whiteContainer.TitleText = "Camera modes";
            this.whiteContainer.Header_Click += new System.EventHandler(this.whiteContainer_Header_Click);
            // 
            // radioButtonCameraModeZoom
            // 
            this.radioButtonCameraModeZoom.AutoSize = true;
            this.radioButtonCameraModeZoom.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonCameraModeZoom.Location = new System.Drawing.Point(14, 71);
            this.radioButtonCameraModeZoom.Name = "radioButtonCameraModeZoom";
            this.radioButtonCameraModeZoom.Size = new System.Drawing.Size(52, 17);
            this.radioButtonCameraModeZoom.TabIndex = 5;
            this.radioButtonCameraModeZoom.TabStop = true;
            this.radioButtonCameraModeZoom.Text = "Zoom";
            this.radioButtonCameraModeZoom.UseVisualStyleBackColor = false;
            this.radioButtonCameraModeZoom.CheckedChanged += new System.EventHandler(this.radioButtonCameraModeZoom_CheckedChanged);
            // 
            // radioButtonCameraModeMove
            // 
            this.radioButtonCameraModeMove.AutoSize = true;
            this.radioButtonCameraModeMove.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonCameraModeMove.Location = new System.Drawing.Point(14, 47);
            this.radioButtonCameraModeMove.Name = "radioButtonCameraModeMove";
            this.radioButtonCameraModeMove.Size = new System.Drawing.Size(52, 17);
            this.radioButtonCameraModeMove.TabIndex = 4;
            this.radioButtonCameraModeMove.Text = "Move";
            this.radioButtonCameraModeMove.UseVisualStyleBackColor = false;
            this.radioButtonCameraModeMove.CheckedChanged += new System.EventHandler(this.radioButtonCameraModeMove_CheckedChanged);
            // 
            // radioButtonCameraModeRotate
            // 
            this.radioButtonCameraModeRotate.AutoSize = true;
            this.radioButtonCameraModeRotate.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonCameraModeRotate.Checked = true;
            this.radioButtonCameraModeRotate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(130)))), ((int)(((byte)(30)))));
            this.radioButtonCameraModeRotate.Location = new System.Drawing.Point(14, 23);
            this.radioButtonCameraModeRotate.Name = "radioButtonCameraModeRotate";
            this.radioButtonCameraModeRotate.Size = new System.Drawing.Size(57, 17);
            this.radioButtonCameraModeRotate.TabIndex = 3;
            this.radioButtonCameraModeRotate.TabStop = true;
            this.radioButtonCameraModeRotate.Text = "Rotate";
            this.radioButtonCameraModeRotate.UseVisualStyleBackColor = false;
            this.radioButtonCameraModeRotate.CheckedChanged += new System.EventHandler(this.radioButtonCameraModeRotate_CheckedChanged);
            // 
            // CameraModeSwitcherPresenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.whiteContainer);
            this.Name = "CameraModeSwitcherPresenter";
            this.Size = new System.Drawing.Size(97, 103);
            this.whiteContainer.ResumeLayout(false);
            this.whiteContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gds.Windows.WhiteContainer whiteContainer;
        private System.Windows.Forms.RadioButton radioButtonCameraModeZoom;
        private System.Windows.Forms.RadioButton radioButtonCameraModeMove;
        private System.Windows.Forms.RadioButton radioButtonCameraModeRotate;
    }
}
