namespace Gds.Windows
{
    partial class WhiteContainer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WhiteContainer));
            this.gradientWhitePanel = new Gds.Windows.GradientWhitePanel();
            ((System.ComponentModel.ISupportInitialize)(this.gradientWhitePanel)).BeginInit();
            this.SuspendLayout();
            // 
            // gradientWhitePanel
            // 
            this.gradientWhitePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gradientWhitePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientWhitePanel.Image = Properties.Resources.GradientWhitePanel;
            this.gradientWhitePanel.Location = new System.Drawing.Point(0, 0);
            this.gradientWhitePanel.Name = "gradientWhitePanel";
            this.gradientWhitePanel.Size = new System.Drawing.Size(150, 15);
            this.gradientWhitePanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.gradientWhitePanel.TabIndex = 0;
            this.gradientWhitePanel.TabStop = false;
            this.gradientWhitePanel.TitleText = "Camera modes";
            // 
            // WhiteContainer
            // 
            this.BackColor = System.Drawing.Color.FromArgb(showColorValue, showColorValue, showColorValue);
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.gradientWhitePanel);
            this.Name = "WhiteContainer";
            this.Size = new System.Drawing.Size(150, 115);
            ((System.ComponentModel.ISupportInitialize)(this.gradientWhitePanel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GradientWhitePanel gradientWhitePanel;
    }
}