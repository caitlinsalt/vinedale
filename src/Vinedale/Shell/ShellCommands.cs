using Logo.Interfaces;
using Logo.Interpretation;
using Logo.Procedures;
using Logo.Tokens;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Vinedale.Resources;

namespace Vinedale.Shell
{
    /// <summary>
    /// This class implements built-in commands for interacting with the Vinedale interface.
    /// </summary>
    public class ShellCommands : ICommandModule
    {
        private readonly IWin32Window _shellWindow;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentWindow">The parent window of this shell (used when creating and showing dialogs)</param>
        public ShellCommands(IWin32Window parentWindow)
        {
            _shellWindow = parentWindow;
        }

        /// <summary>
        /// Returns the definitions of the commands implemented by this class.
        /// </summary>
        /// <returns>A list of <c>LogoProcedure</c> objects.</returns>
        public IList<LogoProcedure> RegisterProcedures()
        {
            return new LogoProcedure[]
            {
                new LogoCommand("bye", "quit", 0, RedefinabilityType.NonRedefinable, SysExit, Strings.CommandByeHelpText, ""),
                new LogoCommand("announce", 1, RedefinabilityType.NonRedefinable, Announce, Strings.CommandAnnounceHelpText, Strings.CommandAnnountExampleText),
            };
        }

        /// <summary>
        /// Shuts down Vinedale.
        /// </summary>
        /// <remarks>This method should never exit, as it closes the application.</remarks>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Not used.</param>
        /// <returns><c>null</c></returns>
        public static Token SysExit(InterpretorContext context, params LogoValue[] input)
        {
            Environment.Exit(0);
            return null;
        }

        /// <summary>
        /// Display a message box containing a string.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain a single token consisting of the value to be displayed in the message box.</param>
        /// <returns><c>null</c>.</returns>
        public Token Announce(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            string announceText = context.CoreLanguageModule.EvaluateForPrint(context.Interpretor, input[0]);
            MessageBox.Show(_shellWindow, announceText);
            return null;
        }
    }
}
