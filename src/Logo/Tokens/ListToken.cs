using Logo.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Logo.Tokens
{
    /// <summary>
    /// A type of token which consists of a list of other tokens.
    /// </summary>
    public class ListToken : ContainerToken
    {
        /// <summary>
        /// Construct a <see cref="ListToken" /> from an input string.
        /// </summary>
        /// <param name="text">The input string to be tokenised.</param>
        /// <param name="parse">Whether or not the input string should be parsed and used to set the contents.</param>
        public ListToken(string text, bool parse = true) : base(text)
        {
            if (parse)
            {
                if (text is null)
                {
                    throw new ArgumentNullException(nameof(text));
                }
                TokeniserResult r = Tokeniser.TokeniseString(text.Substring(1, text.Length - 2));
                if (r.ResultType == TokeniserResultType.SuccessIncomplete)
                {
                    throw new TokeniserException(string.Format(CultureInfo.CurrentCulture, Strings.ListConstructorIncompleteError, text));
                }
                Contents.AddRange(r.TokenisedData);
            }
        }

        /// <summary>
        /// Construct a <see cref="ListToken" /> from a list of existing tokens.
        /// </summary>
        /// <param name="contents">The tokens that will become the contents of this list token.</param>
        public ListToken(IList<Token> contents) : base(CreateText(contents))
        {
            Contents.AddRange(contents);
        }

        private static string CreateText(IList<Token> fromTokens)
        {
            return "[" + string.Join(" ", fromTokens.Select(t => t is LiteralToken literal ? literal.Value.ToString() : t.Text)) + "]";
        }
    }
}
