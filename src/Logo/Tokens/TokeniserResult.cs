using System;
using System.Collections.Generic;
using System.Text;

namespace Logo.Tokens
{
    /// <summary>
    /// A class that encapsulates the results and output of tokenisation.
    /// </summary>
    public class TokeniserResult
    {
        /// <summary>
        /// The overall result of tokenisation, in terms of success, failure; completeness and incompleteness.
        /// </summary>
        public TokeniserResultType ResultType { get; set; }

        /// <summary>
        /// The list of tokens produced by tokenisation.
        /// </summary>
        public IList<Token> TokenisedData { get; set; }

        /// <summary>
        /// Any remaining input which was not able to be processed by tokenisation.  If the <c>ResultType</c> is <c>TokeniserResultType.SuccessComplete</c>, this should be an empty string.
        /// </summary>
        public string NonConsumedInput { get; set; }

        /// <summary>
        /// Any error message produced by tokenisation.
        /// </summary>
        public string ErrorMessage;
    }
}
