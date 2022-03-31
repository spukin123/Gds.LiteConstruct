using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Gds.Windows
{
    public partial class ErrorProviderEx : ErrorProvider
    {
        private int errorsCount = 0;

        public int ErrorsCount
        {
            get { return errorsCount; }
        }

        public bool IsUsing
        {
            get { return (errorsCount > 0); }
        }


        public ErrorProviderEx()
            : base()
        {
            InitializeComponent();
        }

        public ErrorProviderEx(IContainer container)
            : base(container)
        {
            //container.Add(this);

            InitializeComponent();
        }

        public new void SetError(Control control, string value)
        {
            base.SetError(control, value);
            errorsCount++;
        }

        public new void Clear()
        {
            base.Clear();
            errorsCount = 0;
        }
    }
}
