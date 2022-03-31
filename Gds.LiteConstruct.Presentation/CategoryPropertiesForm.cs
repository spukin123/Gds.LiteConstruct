using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Gds.Windows;

namespace Gds.LiteConstruct.Presentation
{
    public partial class CategoryPropertiesForm : Form
    {
        public string CategoryName
        {
            get { return textBoxName.Text; }
        }

        public CategoryPropertiesForm()
            : this(null)
        {
        }

        public CategoryPropertiesForm(string name)
        {
            InitializeComponent();
			InitializeIcons();
			if (!string.IsNullOrEmpty(name))
			{
				this.Text = string.Format("{0} - Category properties", name);
			}
			else
			{
				this.Text = "New category";
			}
            textBoxName.Text = name;
        }

		private void InitializeIcons()
		{
			this.Icon = Icons.Folders.Properties.Icon;
		}

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Name))
            {
                MessageWindow.Warning("Name can't be empty", "");
            }
            else
            {
                this.Close();
            }
        }
    }
}