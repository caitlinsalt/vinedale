using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logo.Core.Interpretation
{
    /// <summary>
    /// Defines the possible results of processing an input line
    /// </summary>
    public enum InterpretationResult
    {
        /// <summary>
        /// The input was parsed with no errors, and was executed immediately.
        /// </summary>
        SuccessComplete,

        /// <summary>
        /// The input was parsed with no errors, but is incomplete, and more input is required to complete execution.
        /// </summary>
        SuccessIncomplete,

        /// <summary>
        /// A parsing error occurred.
        /// </summary>
        Failure
    }
}
