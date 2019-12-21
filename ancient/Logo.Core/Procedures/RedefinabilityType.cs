using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logo.Core.Procedures
{
    /// <summary>
    /// Indicates whether or not a procedure can have its definition altered or replaced.
    /// </summary>
    public enum RedefinabilityType
    {
        /// <summary>
        /// The procedure cannot be replaced once defined.
        /// </summary>
        NonRedefinable,

        /// <summary>
        /// Enable other modules to provide a parallel implementation of this procedure.
        /// </summary>
        DefineAlongside,

        /// <summary>
        /// Enable other modules or user-defined code to replace this procedure entirely with a new definition.
        /// </summary>
        Replace
    }
}
