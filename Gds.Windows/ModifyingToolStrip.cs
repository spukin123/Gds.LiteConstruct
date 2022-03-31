using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gds.Windows
{
    public partial class ModifyingToolStrip : ToolStrip
    {
        [Browsable(true)]
        public event EventHandler ToolStripButtonCreate_Click
        {
            add { this.toolStripButtonCreate.Click += value; }
            remove { this.toolStripButtonCreate.Click -= value; }
        }

        [Browsable(true)]
        public event EventHandler ToolStripButtonEdit_Click
        {
            add { this.toolStripButtonEdit.Click += value; }
            remove { this.toolStripButtonEdit.Click -= value; }
        }

        [Browsable(true)]
        public event EventHandler ToolStripButtonDelete_Click
        {
            add { this.toolStripButtonDelete.Click += value; }
            remove { this.toolStripButtonDelete.Click -= value; }
        }

        public bool ButtonCreateEnabled
        {
            get { return toolStripButtonCreate.Enabled; }
            set { toolStripButtonCreate.Enabled = value; }
        }

        public bool ButtonEditEnabled
        {
            get { return toolStripButtonEdit.Enabled; }
            set { toolStripButtonEdit.Enabled = value; }
        }

        public bool ButtonDeleteEnabled
        {
            get { return toolStripButtonDelete.Enabled; }
            set { toolStripButtonDelete.Enabled = value; }
        }

        [Browsable(false)]
        public bool ModifyingButtonsEnabled
        {
            set
            {
                toolStripButtonCreate.Enabled = value;
                toolStripButtonEdit.Enabled = value;
                toolStripButtonDelete.Enabled = value;
            }
        }

        public ModifyingToolStrip()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }
    }
}
