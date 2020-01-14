using System;

namespace Vinedale.Shell
{
    /// <summary>
    /// The event arguments class for a command being entered in the shell.
    /// </summary>
    public class CommandEnteredEventArgs : EventArgs
    {
        /// <summary>
        /// The text that the user entered.
        /// </summary>
        public string Command { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="command">Value of the <see cref="Command" /> property.</param>
        public CommandEnteredEventArgs(string command)
        {
            Command = command;
        }
    }
}
