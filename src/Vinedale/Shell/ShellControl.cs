using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vinedale.Shell
{
    public partial class ShellControl : UserControl, IShellControl
    {
        public event Action<object, CommandEnteredEventArgs> CommandEntered;

        public ShellControl()
        {
            InitializeComponent();
        }

        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                ShellTextBox.Enabled = value;
                base.Enabled = value;
            }
        }

        protected internal virtual void OnCommandEntered(string command)
        {
            CommandEntered?.Invoke(this, new CommandEnteredEventArgs(command));
        }

        public Color ShellTextForegroundColour
        {
            get => ShellTextBox.ForeColor;
            set => ShellTextBox.ForeColor = value;
        }

        public Color ShellTextBackgroundColour
        {
            get => ShellTextBox.BackColor;
            set => ShellTextBox.BackColor = value;
        }

        public Font ShellTextFont
        {
            get => ShellTextBox.Font;
            set => ShellTextBox.Font = value;
        }

        public void Clear()
        {
            ShellTextBox.Clear();
        }

        public void WriteText(string text)
        {
            ShellTextBox.WriteText(text);
        }

        public string Prompt
        {
            get => ShellTextBox.Prompt;
            set => ShellTextBox.Prompt = value;
        }
    }
}
