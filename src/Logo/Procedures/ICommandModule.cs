using System;
using System.Collections.Generic;
using System.Text;

namespace Logo.Procedures
{
    /// <summary>
    /// The interface for classes which implement Logo commands in .NET.
    /// </summary>
    public interface ICommandModule
    {
        /// <summary>
        /// Provide definitions of the procedures implemented by this class.
        /// </summary>
        /// <returns>A list of procedure definitions.</returns>
        IList<LogoProcedure> RegisterProcedures();
    }
}
