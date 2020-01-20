using System.Collections.Generic;
using System.Linq;

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
        public TokeniserResultType ResultType { get; private set; }

        /// <summary>
        /// The list of tokens produced by tokenisation.
        /// </summary>
        public IList<Token> TokenisedData { get; private set; }

        /// <summary>
        /// Any remaining input which was not able to be processed by tokenisation.  If the <c>ResultType</c> is <c>TokeniserResultType.SuccessComplete</c>, this should be an empty string.
        /// </summary>
        public string NonConsumedInput { get; private set; }

        /// <summary>
        /// Any error message produced by tokenisation.
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="resultType">The type of result returned by the tokeniser</param>
        /// <param name="tokenisedData">The result of successful tokenisation</param>
        /// <param name="nonConsumedInput">Any input which could not be consumed by the tokeniser.  Defaults to null.</param>
        /// <param name="errorMessage">Any error message resulting from tokenisation.  Defaults to null.</param>
        public TokeniserResult(TokeniserResultType resultType, IEnumerable<Token> tokenisedData, string nonConsumedInput = null, string errorMessage = null)
        {
            TokenisedData = tokenisedData?.ToArray();
            ResultType = resultType;
            NonConsumedInput = nonConsumedInput ?? "";
            ErrorMessage = errorMessage;
        }
    }
}
