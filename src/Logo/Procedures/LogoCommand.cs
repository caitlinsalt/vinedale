using System.Collections.Generic;

namespace Logo.Procedures
{
    /// <summary>
    /// A <c>LogoCommand</c> is a subclass of <c>LogoProcedure</c> which represents a procedure implemented in .NET code.
    /// </summary>
    public class LogoCommand : LogoProcedure
    {
        /// <summary>
        /// The reference to the delegate which is called to execute the command.
        /// </summary>
        public CommandImplementation Implementation { get; private set; }

        /// <summary>
        /// Constructor for a <see cref="LogoCommand" /> with multiple aliases.
        /// </summary>
        /// <param name="name">Primary name of the command.</param>
        /// <param name="aliases">Alias names of the command.</param>
        /// <param name="paramCount">Number of parameters.</param>
        /// <param name="redefinability">Redefinability of this command.</param>
        /// <param name="implementation">Command implementation routine.</param>
        /// <param name="helpText">Command help text.</param>
        /// <param name="exampleText">Command example text.</param>
        public LogoCommand(string name, IEnumerable<string> aliases, int paramCount, RedefinabilityType redefinability, CommandImplementation implementation, string helpText, string exampleText = "")
            : base(name, aliases, paramCount, redefinability, helpText, exampleText)
        {
            Implementation = implementation;
        }

        /// <summary>
        /// Constructor for a <see cref="LogoCommand" /> with one alias.
        /// </summary>
        /// <param name="name">Primary name of the command.</param>
        /// <param name="alias">Alias name of the command.</param>
        /// <param name="paramCount">Number of parameters.</param>
        /// <param name="redefinability">Redefinability of this command.</param>
        /// <param name="implementation">Command implementation routine.</param>
        /// <param name="helpText">Command help text.</param>
        /// <param name="exampleText">Command example text.</param>
        public LogoCommand(string name, string alias, int paramCount, RedefinabilityType redefinability, CommandImplementation implementation, string helpText, string exampleText = "")
            : base(name, alias, paramCount, redefinability, helpText, exampleText)
        {
            Implementation = implementation;
        }

        /// <summary>
        /// Constructor for a <see cref="LogoCommand" /> with no aliases.
        /// </summary>
        /// <param name="name">Primary name of the command.</param>
        /// <param name="paramCount">Number of parameters.</param>
        /// <param name="redefinability">Redefinability of this command.</param>
        /// <param name="implementation">Command implementation routine.</param>
        /// <param name="helpText">Command help text.</param>
        /// <param name="exampleText">Command example text.</param>
        public LogoCommand(string name, int paramCount, RedefinabilityType redefinability, CommandImplementation implementation, string helpText, string exampleText = "")
            : base(name, paramCount, redefinability, helpText, exampleText)
        {
            Implementation = implementation;
        }
    }
}
