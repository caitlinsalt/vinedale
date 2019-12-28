using System;
using System.Windows.Forms;

namespace Vinedale.Shell
{
    public partial class ShellTextBox : TextBox
    {
        public string Prompt { get; set; }

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
                    PrintPrompt();
                }
                base.Enabled = value;
            }
        }

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

        private void PrintPrompt()
        {
            string currentText = Text;
            if (currentText.Length != 0 && currentText[^1] != '\n')
            {
                PrintNewLine();
            }
            AddText(Prompt);
        }

        private void PrintNewLine()
        {
            AddText(Environment.NewLine);
        }

        private void ShellTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8 && IsCaretJustBeforePrompt())
            {
                e.Handled = true;
                return;
            }

            if (IsTerminatorKey(e.KeyChar))
            {
                e.Handled = true;
                string currentCommand = GetTextAtPrompt() + Environment.NewLine;
                PrintNewLine();
                (Parent as ShellControl)?.OnCommandEntered(currentCommand);
                PrintPrompt();
            }
        }

        private void ShellTextBox_KeyDown(object sender, KeyEventArgs e)
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
