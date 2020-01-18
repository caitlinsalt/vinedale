using System;
using System.Collections.Generic;
using System.Linq;

namespace Logo.Procedures
{
    /// <summary>
    /// The <c>LogoProcedure</c> class provides the definition of a generic Logo procedure, either implemented in .NET or implemented in Logo (whether built-in or user-defined).
    /// </summary>
    public class LogoProcedure
    {
        /// <summary>
        /// The primary name of the procedure.
        /// </summary>
        public string Name { get; private set; }

        private readonly string[] _aliases;

        /// <summary>
        /// A list of alternative names that can also be used to call this procedure, such as "fd" for "forward".  At present these can only be defined for .NET-implemented commands.
        /// </summary>
        public IList<string> Aliases => _aliases.ToArray(); 

        /// <summary>
        /// The number of formal parameters of this procedure.
        /// </summary>
        public int ParameterCount { get; private protected set; }

        /// <summary>
        /// Indicates whether or not subsequently-loaded commands can replace this one.
        /// </summary>
        public RedefinabilityType Redefinability { get; private set; }

        /// <summary>
        /// Text to be printed out by the <c>help</c> command to explain what this procedure does.
        /// </summary>
        public string HelpText { get; private protected set; }

        /// <summary>
        /// Text to be printed out by the <c>help</c> command to indicate the syntax for calling this procedure.
        /// </summary>
        public string ExampleText { get; private protected set; }

        /// <summary>
        /// Constructor for a <see cref="LogoProcedure" /> with multiple aliases.
        /// </summary>
        /// <param name="name">The primary name of the procedure.</param>
        /// <param name="aliases">Alias names for the procedure (if any).</param>
        /// <param name="paramCount">The number of parameters used by the procedure.</param>
        /// <param name="redefinability">Whether the procedure should be redefinable.</param>
        /// <param name="helpText">The procedure help text.</param>
        /// <param name="exampleText">The procedure example text.</param>
        public LogoProcedure(string name, IEnumerable<string> aliases, int paramCount, RedefinabilityType redefinability, string helpText, string exampleText)
        {
            Name = name;
            _aliases = aliases?.ToArray() ?? Array.Empty<string>();
            ParameterCount = paramCount;
            Redefinability = redefinability;
            HelpText = helpText;
            ExampleText = exampleText;
        }

        /// <summary>
        /// Constructor for a <see cref="LogoProcedure" /> with one alias.
        /// </summary>
        /// <param name="name">The primary name of the procedure.</param>
        /// <param name="alias">Alias name for the procedure.</param>
        /// <param name="paramCount">The number of parameters used by the procedure.</param>
        /// <param name="redefinability">Whether the procedure should be redefinable.</param>
        /// <param name="helpText">The procedure help text.</param>
        /// <param name="exampleText">The procedure example text.</param>
        public LogoProcedure(string name, string alias, int paramCount, RedefinabilityType redefinability, string helpText, string exampleText)
        {
            Name = name;
            ParameterCount = paramCount;
            Redefinability = redefinability;
            HelpText = helpText;
            ExampleText = exampleText;
            if (string.IsNullOrWhiteSpace(alias))
            {
                _aliases = Array.Empty<string>();
            }
            else
            {
                _aliases = new[] { alias };
            }
        }

        /// <summary>
        /// Constructor for a <see cref="LogoProcedure" /> with no aliases.
        /// </summary>
        /// <param name="name">The primary name of the procedure.</param>
        /// <param name="paramCount">The number of parameters used by the procedure.</param>
        /// <param name="redefinability">Whether the procedure should be redefinable.</param>
        /// <param name="helpText">The procedure help text.</param>
        /// <param name="exampleText">The procedure example text.</param>
        public LogoProcedure(string name, int paramCount, RedefinabilityType redefinability, string helpText, string exampleText)
        {
            Name = name;
            _aliases = Array.Empty<string>();
            ParameterCount = paramCount;
            Redefinability = redefinability;
            HelpText = helpText;
            ExampleText = exampleText;
        }

        /// <summary>
        /// Protected constructor, called by the <see cref="LogoDefinition" /> constructor.
        /// </summary>
        /// <param name="name">Procedure name.</param>
        /// <param name="redefinability">Procedure redefinability type.</param>
        private protected LogoProcedure(string name, RedefinabilityType redefinability)
        {
            Name = name;
            _aliases = Array.Empty<string>();
            Redefinability = redefinability;
        }
    }
}
