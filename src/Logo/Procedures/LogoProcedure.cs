using System;
using System.Collections.Generic;
using System.Text;

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
        public string Name { get; set; }

        /// <summary>
        /// A list of alternative names that can also be used to call this procedure, such as "fd" for "forward".  At present these can only be defined for .NET-implemented commands.
        /// </summary>
        public string[] Aliases { get; set; }

        /// <summary>
        /// The number of formal parameters of this procedure.
        /// </summary>
        public int ParameterCount { get; set; }

        /// <summary>
        /// Indicates whether or not subsequently-loaded commands can replace this one.
        /// </summary>
        public RedefinabilityType Redefinability { get; set; }

        /// <summary>
        /// Text to be printed out by the <c>help</c> command to explain what this procedure does.
        /// </summary>
        public string HelpText { get; set; }

        /// <summary>
        /// Text to be printed out by the <c>help</c> command to indicate the syntax for calling this procedure.
        /// </summary>
        public string ExampleText { get; set; }
    }
}
