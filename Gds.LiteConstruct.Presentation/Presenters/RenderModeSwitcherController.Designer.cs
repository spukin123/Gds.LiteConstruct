namespace Gds.LiteConstruct.Presentation.Presenters
{
    partial class RenderModeSwitcherController
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
			this.btnSceneMode = new System.Windows.Forms.Button();
			this.btnTexturizeMode = new System.Windows.Forms.Button();
			this.whiteContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// whiteContainer
			// 
			this.whiteContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.whiteContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.whiteContainer.Controls.Add(this.btnSceneMode);
			this.whiteContainer.Controls.Add(this.btnTexturizeMode);
			this.whiteContainer.Location = new System.Drawing.Point(0, 0);
			this.whiteContainer.Name = "whiteContainer";
			this.whiteContainer.Processing = false;
			this.whiteContainer.Size = new System.Drawing.Size(127, 103);
			this.whiteContainer.TabIndex = 16;
			this.whiteContainer.TitleIndent = 18;
			this.whiteContainer.TitleText = "Render modes";
			// 
			// btnSceneMode
			// 
			this.btnSceneMode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnSceneMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnSceneMode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSceneMode.Location = new System.Drawing.Point(10, 22);
			this.btnSceneMode.Name = "btnSceneMode";
			this.btnSceneMode.Size = new System.Drawing.Size(106, 38);
			this.btnSceneMode.TabIndex = 10;
			this.btnSceneMode.Text = "    Scene";
			this.btnSceneMode.UseVisualStyleBackColor = true;
			this.btnSceneMode.Click += new System.EventHandler(this.btnSceneMode_Click);
			// 
			// btnTexturizeMode
			// 
			this.btnTexturizeMode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnTexturizeMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnTexturizeMode.Image = global::Gds.LiteConstruct.Presentation.Properties.Resources.Fill;
			this.btnTexturizeMode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnTexturizeMode.Location = new System.Drawing.Point(10, 65);
			this.btnTexturizeMode.Name = "btnTexturizeMode";
			this.btnTexturizeMode.Size = new System.Drawing.Size(106, 30);
			this.btnTexturizeMode.TabIndex = 11;
			this.btnTexturizeMode.Text = "          Texturing";
			this.btnTexturizeMode.UseVisualStyleBackColor = true;
			this.btnTexturizeMode.Click += new System.EventHandler(this.btnTexturingMode_Click);
			// 
			// RenderModeSwitcherController
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.whiteContainer);
			this.Name = "RenderModeSwitcherController";
			this.Size = new System.Drawing.Size(127, 103);
			this.whiteContainer.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSceneMode;
		private System.Windows.Forms.Button btnTexturizeMode;
		private Gds.Windows.WhiteContainer whiteContainer;
    }
}
