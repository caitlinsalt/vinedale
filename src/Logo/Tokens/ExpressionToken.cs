using Logo.Interpretation;
using Logo.Resources;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Logo.Tokens
{
    /// <summary>
    /// A token which represents an expression.
    /// </summary>
    public class ExpressionToken : ContainerToken
    {
        /// <summary>
        /// The standard constructor for this class builds a list of tokens from an input string.
        /// </summary>
        /// <param name="literal">The input to be tokenised.</param>
        public ExpressionToken(string literal) : base(literal)
        {
            if (literal == null)
            {
                literal = "";
            }

            TokeniserResult r = Tokeniser.TokeniseString(literal.Substring(1, literal.Length - 2));
            if (r.ResultType == TokeniserResultType.SuccessIncomplete)
            {
                throw new TokeniserException(string.Format(CultureInfo.CurrentCulture, Strings.ExpressionConstructorIncompleteError, literal));
            }
            Contents.AddRange(r.TokenisedData);
        }

        public ExpressionToken(IList<Token> contents) : base(CreateText(contents))
        {
            Contents.AddRange(contents);
        }

        public override LogoValue Evaluate(InterpretorContext context)
        {
            throw new System.NotImplementedException();
        }

        private static string CreateText(IList<Token> fromTokens)
        {
            return "(" + string.Join(" ", fromTokens.Select(t => t is LiteralToken literal ? literal.Value.ToString() : t.Text)) + ")";
        }
    }
}
