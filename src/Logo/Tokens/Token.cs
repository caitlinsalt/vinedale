using Logo.Interpretation;

namespace Logo.Tokens
{
    /// <summary>
    /// The parent class of all Logo input tokens.
    /// </summary>
    public abstract class Token
    {
        /// <summary>
        /// A textual representation of this token, such as the text originally entered by the user.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Whether this token has already had its value computed.
        /// </summary>
        //public virtual bool Evaluated { get; set; }

        /// <summary>
        /// The value of this token.
        /// </summary>
        //public LogoValue TokenValue { get; set; }

        public abstract LogoValue Evaluate(InterpretorContext context);

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">The value to assign to the <see cref="Text" /> property.</param>
        public Token(string text)
        {
            Text = text;
        }
    }
}
