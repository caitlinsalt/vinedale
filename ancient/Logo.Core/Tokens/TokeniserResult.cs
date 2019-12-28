using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logo.Core.Tokens
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
        public IList<LogoToken> TokenisedData { get; set; }

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
