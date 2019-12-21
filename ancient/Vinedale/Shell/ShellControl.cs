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
    public partial class ShellControl : UserControl
    {
        public event EventCommandEntered CommandEntered;

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
                shellTextBox.Enabled = value;
                base.Enabled = value;
            }
        }

        internal void FireCommandEntered(string command)
        {
            OnCommandEntered(command);
        }

        protected virtual void OnCommandEntered(string command)
        {
            if (CommandEntered != null)
            {
                CommandEntered(command, new CommandEnteredEventArgs(command));
            }
        }

        public Color ShellTextForeColour
        {
            get
            {
                return shellTextBox != null ? shellTextBox.ForeColor : Color.Green;
            }
            set
            {
                if (shellTextBox != null)
                {
                    shellTextBox.ForeColor = value;
                }
            }
        }

        public Color ShellTextBackColour
        {
            get
            {
                return shellTextBox != null ? shellTextBox.BackColor : Color.Black;
            }
            set
            {
                if (shellTextBox != null)
                {
                    shellTextBox.BackColor = value;
                }
            }
        }

        public Font ShellTextFont
        {
            get
            {
                return shellTextBox != null ? shellTextBox.Font : new Font("Tahoma", 8);
            }
            set
            {
                if (shellTextBox != null)
                {
                    shellTextBox.Font = value;
                }
            }
        }

        public void Clear()
        {
            shellTextBox.Clear();
        }

        public void WriteText(string text)
        {
            shellTextBox.WriteText(text);
        }

        public string Prompt
        {
            get
            {
                return shellTextBox != null ? shellTextBox.Prompt : string.Empty;
            }
            set
            {
                if (shellTextBox != null)
                {
                    shellTextBox.Prompt = value;
                }
            }
        }

        public class CommandEnteredEventArgs : EventArgs
        {
            public string Command { get; private set; }
            
            public CommandEnteredEventArgs(string command)
            {
                Command = command;
            }
        }

        public delegate void EventCommandEntered(object sender, CommandEnteredEventArgs e);
    }
}
