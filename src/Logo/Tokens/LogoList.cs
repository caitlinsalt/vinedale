using Logo.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logo.Tokens
{
    /// <summary>
    /// A type of token which consists of a list of other tokens.
    /// </summary>
    public class LogoList : ContainerToken
    {
        private bool _evalContents;

        /// <summary>
        /// This property is false unless all of the tokens that this token contains have also been evaluated.
        /// </summary>
        public bool EvaluatedContents
        {
            get
            {
                return _evalContents && InnerContents.All(t => t.Evaluated);
            }
            set
            {
                _evalContents = value;
            }
        }

        /// <summary>
        /// Construct a <c>LogoList</c> token from an input string.
        /// </summary>
        /// <param name="literal">The input string to be tokenised.</param>
        public LogoList(string literal)
        {
            Literal = literal;

            TokeniserResult r = TokeniseString(literal.Substring(1, literal.Length - 2));
            if (r.ResultType == TokeniserResultType.SuccessIncomplete)
            {
                throw new TokeniserException(string.Format(Strings.ListConstructorIncompleteError, literal));
            }
            InnerContents.AddRange(r.TokenisedData);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LogoList() { }

        /// <summary>
        /// Create a copy of this token.
        /// </summary>
        /// <returns>A token identical to this token.</returns>
        public override Token Clone()
        {
            LogoList newList = new LogoList() { Evaluated = Evaluated, Literal = Literal };
            newList.InnerContents.AddRange(InnerContents.Select(t => t.Clone()));
            return newList;
        }

        internal void RecreateLiteralValue()
        {
            Literal = "[" + string.Join(" ", InnerContents.Select(t => t.Literal)) + "]";
        }
    }
}
