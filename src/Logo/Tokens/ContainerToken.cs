using System.Collections.Generic;

namespace Logo.Tokens
{
    /// <summary>
    /// A type of <c>LogoToken</c> which contains zero or more other tokens, such as a list or an expression.
    /// </summary>
    public abstract class ContainerToken : Token
    {
        /// <summary>
        /// The tokens contained within this token.
        /// </summary>
        public List<Token> Contents { get; private set; }

        /// <summary>
        /// The default constructor for this class initialises the <c>InnerContents</c> property to be an empty list of tokens.
        /// </summary>
        public ContainerToken(string text) : base(text)
        {
            Contents = new List<Token>();
        }
    }
}
