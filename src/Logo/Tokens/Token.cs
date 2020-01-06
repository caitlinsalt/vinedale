namespace Logo.Tokens
{
    /// <summary>
    /// The parent class of all Logo input tokens.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// A textual representation of this token, such as the text originally entered by the user.
        /// </summary>
        public string Literal { get; set; }

        /// <summary>
        /// Whether this token has already had its value computed.
        /// </summary>
        public virtual bool Evaluated { get; set; }

        /// <summary>
        /// The value of this token.
        /// </summary>
        public LogoValue TokenValue { get; set; }

        /// <summary>
        /// Produces a copy of this token.
        /// </summary>
        /// <returns>A token identical to this one.</returns>
        public virtual Token Clone()
        {
            return new Token { Evaluated = Evaluated, Literal = Literal, TokenValue = TokenValue };
        }
    }
}
