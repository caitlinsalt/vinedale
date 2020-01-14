using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vinedale.Shell
{
    /// <summary>
    /// A control for displaying a shell on-screen.
    /// </summary>
    public partial class ShellControl : UserControl, IShellControl
    {
        /// <summary>
        /// The event emitted when a user enters a line of text.
        /// </summary>
        public event Action<object, CommandEnteredEventArgs> CommandEntered;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ShellControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Whether or not this control is active.  Hides <see cref="Control.Enabled" />.
        /// </summary>
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

        /// <summary>
        /// Emit the <see cref="CommandEntered" /> event.
        /// </summary>
        /// <param name="command">The command that has been entered.</param>
        protected internal virtual void OnCommandEntered(string command)
        {
            CommandEntered?.Invoke(this, new CommandEnteredEventArgs(command));
        }

        /// <summary>
        /// The colour of shell text.
        /// </summary>
        public Color ShellTextForegroundColour
        {
            get => ShellTextBox.ForeColor;
            set => ShellTextBox.ForeColor = value;
        }

        /// <summary>
        /// The background colour of the control.
        /// </summary>
        public Color ShellTextBackgroundColour
        {
            get => ShellTextBox.BackColor;
            set => ShellTextBox.BackColor = value;
        }

        /// <summary>
        /// The font to use for writing text.
        /// </summary>
        public Font ShellTextFont
        {
            get => ShellTextBox.Font;
            set => ShellTextBox.Font = value;
        }

        /// <summary>
        /// Clear the content of the control.
        /// </summary>
        public void Clear()
        {
            ShellTextBox.Clear();
        }

        /// <summary>
        /// Write text to the control.
        /// </summary>
        /// <param name="text">The text to be written.</param>
        public void WriteText(string text)
        {
            ShellTextBox.WriteText(text);
        }

        /// <summary>
        /// The shell prompt text.
        /// </summary>
        public string Prompt
        {
            get => ShellTextBox.Prompt;
            set => ShellTextBox.Prompt = value;
        }
    }
}
