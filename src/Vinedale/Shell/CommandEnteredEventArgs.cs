using System;

namespace Vinedale.Shell
{
    public class CommandEnteredEventArgs : EventArgs
    {
        public string Command { get; private set; }

        public CommandEnteredEventArgs(string command)
        {
            Command = command;
        }
    }
}
