using System;
using System.Collections.Generic;
using System.Text;

namespace Logo.Interpretation
{
    /// <summary>
    /// Defines the degree of debug information to output.
    /// </summary>
    public enum DebugMessageLevel
    {
        /// <summary>
        /// No output.
        /// </summary>
        Silent,

        /// <summary>
        /// Brief output.
        /// </summary>
        Terse,

        /// <summary>
        /// Medium amount of output.
        /// </summary>
        Chatty,

        /// <summary>
        /// Large amout of output.
        /// </summary>
        Verbose,

        /// <summary>
        /// Maxiumum output.
        /// </summary>
        Logorrheic
    }
}
