using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gds.Windows
{
    static public class MessageWindow
    {
        public static void Information(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Warning(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(string text)
        {
            Error(text, "Error");
        }

        public static void Error(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

		public static DialogResult Question(string text)
		{
			return Question(text, "Question");
		}

        public static DialogResult Question(string text, string caption)
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
