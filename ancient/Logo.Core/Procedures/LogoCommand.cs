using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logo.Core.Procedures
{
    /// <summary>
    /// A <c>LogoCommand</c> is a subclass of <c>LogoProcedure</c> which represents a procedure implemented in .NET code.
    /// </summary>
    public class LogoCommand : LogoProcedure
    {
        /// <summary>
        /// The reference to the delegate which is called to execute the command.
        /// </summary>
        public CommandImplementation Implementation { get; set; }
    }
}
