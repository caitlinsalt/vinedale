using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vinedale.Shell
{
    /// <summary>
    /// The component used to display interactive interpretors.
    /// </summary>
    internal partial class ShellTextBox : TextBox
    {
        /// <summary>
        /// The current user prompt.
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// The <c>Enabled</c> property shadows the base <c>Enabled</c> and also controls whether or not to display the <c>Prompt</c>.
        /// </summary>
        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                if (!base.Enabled && value)
                {
                    printPrompt();
                }
                base.Enabled = value;
            }
        }
        

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ShellTextBox()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0302: // WM_PASTE
                case 0x0300: // WM_CUT
                case 0x000C: // WM_SETTEXT
                    if (!IsCaretAtWritablePosition())
                    {
                        MoveCaretToEndOfText();
                    }
                    break;
                case 0x0303: // WM_CLEAR
                    return;
            }
            base.WndProc(ref m);
        }

        private void printPrompt()
        {
            string currentText = Text;
            if (currentText.Length != 0 && currentText[currentText.Length - 1] != '\n')
            {
                printLine();
            }
            AddText(Prompt);
        }

        private void printLine()
        {
            AddText(Environment.NewLine);
        }

        private void shellTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 8 && IsCaretJustBeforePrompt())
            {
                e.Handled = true;
                return;
            }

            if (IsTerminatorKey(e.KeyChar))
            {
                e.Handled = true;
                string currentCommand = GetTextAtPrompt() + Environment.NewLine;
                printLine();
                ((ShellControl) Parent).FireCommandEntered(currentCommand);
                printPrompt();
            }
        }

        private void shellTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsCaretAtWritablePosition() && !(e.Control || IsTerminatorKey(e.KeyCode)))
            {
                MoveCaretToEndOfText();
            }

            if (e.KeyCode == Keys.Left && IsCaretJustBeforePrompt())
            {
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                e.Handled = true;
            }
        }

        private string GetCurrentLine()
        {
            if (Lines.Length > 0)
            {
                return (string)Lines.GetValue(Lines.GetLength(0) - 1);
            }
            return string.Empty;
        }

        private string GetTextAtPrompt()
        {
            return GetCurrentLine().Substring(Prompt.Length);
        }

        private void ReplaceTextAtPrompt(string text)
        {
            string currentLine = GetCurrentLine();
            int charsAfterPrompt = currentLine.Length - Prompt.Length;

            if (charsAfterPrompt == 0)
            {
                AddText(text);
            }
            else
            {
                Select(TextLength - charsAfterPrompt, charsAfterPrompt);
                SelectedText = text;
            }
        }

        private bool IsCaretAtCurrentLine()
        {
            return TextLength - SelectionStart <= GetCurrentLine().Length;
        }

        private void MoveCaretToEndOfText()
        {
            SelectionStart = TextLength;
            ScrollToCaret();
        }

        private bool IsCaretJustBeforePrompt()
        {
            return IsCaretAtCurrentLine() && GetCurrentCaretColumnPosition() == Prompt.Length;
        }

        private int GetCurrentCaretColumnPosition()
        {
            return SelectionStart - TextLength + GetCurrentLine().Length;
        }

        private bool IsCaretAtWritablePosition()
        {
            return IsCaretAtCurrentLine() && GetCurrentCaretColumnPosition() >= Prompt.Length;
        }

        public void WriteText(string text)
        {
            AddText(text);
        }

        private bool IsTerminatorKey(Keys key)
        {
            return key == Keys.Enter;
        }

        private bool IsTerminatorKey(char keyChar)
        {
            return keyChar == 13;
        }

        private void AddText(string text)
        {
            Text += text;
            MoveCaretToEndOfText();
        }
    }
}
