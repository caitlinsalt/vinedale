using Logo.Core.Interpretation;
using Logo.Core.Procedures;
using Logo.Core.Tokens;
using System;
using System.Collections.Generic;
using Vinedale.Resources;

namespace Vinedale.Shell
{
    /// <summary>
    /// This class implements built-in commands for interacting with the Vinedale interface.
    /// </summary>
    public class ShellCommands : ICommandModule
    {
        /// <summary>
        /// Returns the definitions of the commands implemented by this class.
        /// </summary>
        /// <returns>A list of <c>LogoProcedure</c> objects.</returns>
        public IList<LogoProcedure> RegisterProcedures()
        {
            return new LogoProcedure[]
            {
                new LogoCommand
                {
                    Name = "bye",
                    Aliases = new[] { "quit" },
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 0,
                    Implementation = SysExit,
                    HelpText = Strings.CommandByeHelpText,
                    ExampleText = string.Empty,
                }
            };
        }

        /// <summary>
        /// Shuts down Vinedale.
        /// </summary>
        /// <remarks>This method should never exit, as it closes the application.</remarks>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Not used.</param>
        /// <returns><c>null</c></returns>
        public LogoToken SysExit(InterpretorContext context, params LogoToken[] input)
        {
            Environment.Exit(0);
            return null;
        }
    }
}
