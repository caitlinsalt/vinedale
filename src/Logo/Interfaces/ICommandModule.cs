using Logo.Procedures;
using System.Collections.Generic;

namespace Logo.Interfaces
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
