using Logo.Interpretation;
using Logo.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Logo.Tokens
{
    /// <summary>
    /// A type of token which consists of a list of other tokens.
    /// </summary>
    public class ListToken : ContainerToken
    {
        /// <summary>
        /// Construct a <c>LogoList</c> token from an input string.
        /// </summary>
        /// <param name="text">The input string to be tokenised.</param>
        public ListToken(string text, bool parse = true) : base(text)
        {
            if (parse)
            {
                TokeniserResult r = Tokeniser.TokeniseString(text.Substring(1, text.Length - 2));
                if (r.ResultType == TokeniserResultType.SuccessIncomplete)
                {
                    throw new TokeniserException(string.Format(CultureInfo.CurrentCulture, Strings.ListConstructorIncompleteError, text));
                }
                Contents.AddRange(r.TokenisedData);
            }
        }

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
