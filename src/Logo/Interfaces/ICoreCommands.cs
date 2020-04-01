using Logo.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logo.Interfaces
{
    /// <summary>
    /// A command module which not only contains commands, but also key shared "command functionality".
    /// </summary>
    public interface ICoreCommands : ICommandModule
    {
        /// <summary>
        /// Convert a value into a string, as used by the "pr" command.
        /// </summary>
        /// <param name="interpretor">The interpretor used to evaluate unevaluated tokens.</param>
        /// <param name="value">The value to be output.</param>
        /// <returns>A string representing the value.</returns>
        string EvaluateForPrint(IInterpretor interpretor, LogoValue value);
    }
}
