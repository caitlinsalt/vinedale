namespace Logo.Tokens
{
    /// <summary>
    /// A token that represents a procedure (potentially)
    /// </summary>
    public class ProcedureToken : Token
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">The value to assign to the <see cref="Token.Text" /> property.</param>
        public ProcedureToken(string text) : base(text)
        {
        }
    }
}
