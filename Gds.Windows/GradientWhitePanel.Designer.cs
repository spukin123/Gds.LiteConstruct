using System.Windows.Forms;
namespace Gds.Windows
{
    partial class GradientWhitePanel
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
            this.Image = Properties.Resources.GradientWhitePanel;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            labelTitle = new Label();
            labelTitle.Name = "labelTitle";
            labelTitle.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            labelTitle.ForeColor = System.Drawing.Color.FromArgb(20, 20, 20);
            labelTitle.BackColor = System.Drawing.Color.Transparent;
            labelTitle.Location = new System.Drawing.Point(10, 0);
            labelTitle.Size = new System.Drawing.Size(500, 20);
            this.Controls.Add(labelTitle);
        }

        #endregion

        private Label labelTitle;
    }
}
