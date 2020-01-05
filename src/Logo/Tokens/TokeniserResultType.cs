using System;
using System.Collections.Generic;
using System.Text;

namespace Logo.Tokens
{
    /// <summary>
    /// The possible result states of tokenisation.
    /// </summary>
    public enum TokeniserResultType
    {
        /// <summary>
        /// All input was successfully tokenised and produced a complete token list.
        /// </summary>
        SuccessComplete,

        /// <summary>
        /// Input was successfully tokenised, but the token list is incomplete and cannot be executed.
        /// </summary>
        SuccessIncomplete,

        /// <summary>
        /// Input was not successfully tokenised.
        /// </summary>
        Failure
    }
}
