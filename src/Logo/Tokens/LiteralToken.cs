namespace Logo.Tokens
{
    /// <summary>
    /// A token that contains a literal value - either a single value or a list.
    /// </summary>
    public class LiteralToken : Token
    {
        /// <summary>
        /// The value of this token.
        /// </summary>
        public LogoValue Value { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">The text for this token - this is often the name of the procedure that created it, or it may be the text that was input by the programmer.</param>
        /// <param name="value">The value of this token.</param>
        public LiteralToken(string text, LogoValue value) : base(text)
        {
            Value = value;
        }
    }
}
