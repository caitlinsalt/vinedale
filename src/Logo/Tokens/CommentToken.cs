using Logo.Interpretation;

namespace Logo.Tokens
{
    /// <summary>
    /// A token which contains only comments.
    /// </summary>
    public class CommentToken : Token
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">The original text of this token.</param>
        public CommentToken(string text) : base(text)
        {
        }
    }
}
