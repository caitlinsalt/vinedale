using System.Collections.Generic;

namespace Logo.Core.Tokens
{
    /// <summary>
    /// A type of <c>LogoToken</c> which contains zero or more other tokens, such as a list or an expression.
    /// </summary>
    public abstract class ContainerToken : LogoToken
    {
        /// <summary>
        /// The tokens contained within this token.
        /// </summary>
        public List<LogoToken> InnerContents { get; set; }

        /// <summary>
        /// The default constructor for this class initialises the <c>InnerContents</c> property to be an empty list of tokens.
        /// </summary>
        public ContainerToken()
        {
            InnerContents = new List<LogoToken>();
        }
    }
}
