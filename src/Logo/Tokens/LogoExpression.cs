using Logo.Resources;
using System.Linq;

namespace Logo.Tokens
{
    /// <summary>
    /// A token which represents an expression.
    /// </summary>
    public class LogoExpression : ContainerToken
    {
        /// <summary>
        /// The standard constructor for this class builds a list of tokens from an input string.
        /// </summary>
        /// <param name="literal">The input to be tokenised.</param>
        public LogoExpression(string literal)
        {
            Literal = literal;

            TokeniserResult r = TokeniseString(literal.Substring(1, literal.Length - 2));
            if (r.ResultType == TokeniserResultType.SuccessIncomplete)
            {
                throw new TokeniserException(string.Format(Strings.ExpressionConstructorIncompleteError, literal));
            }
            InnerContents.AddRange(r.TokenisedData);
        }

        private LogoExpression() { }

        /// <summary>
        /// Produce a copy of this token.
        /// </summary>
        /// <returns>A token which is identical to this one.</returns>
        public override Token Clone()
        {
            LogoExpression newExpr = new LogoExpression() { Evaluated = Evaluated, Literal = Literal };
            newExpr.InnerContents.AddRange(InnerContents.Select(t => t.Clone()));
            return newExpr;
        }
    }
}
