namespace Logo.Tokens
{
    /// <summary>
    /// A token representing a "word".
    /// </summary>
    public class Word : Token
    {
        /// <summary>
        /// Create a copy of this token.
        /// </summary>
        /// <returns>A token equal to this one.</returns>
        public override Token Clone()
        {
            return new Word { Evaluated = Evaluated, Literal = Literal, TokenValue = TokenValue };
        }
    }
}
