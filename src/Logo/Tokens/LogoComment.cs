namespace Logo.Tokens
{
    /// <summary>
    /// A token which contains only comments.
    /// </summary>
    public class LogoComment : Word
    {
        /// <summary>
        /// Create a copy of this token.
        /// </summary>
        /// <returns>A token equal to this one.</returns>
        public override Token Clone()
        {
            return new LogoComment { Evaluated = Evaluated, Literal = Literal, TokenValue = TokenValue };
        }
    }
}
