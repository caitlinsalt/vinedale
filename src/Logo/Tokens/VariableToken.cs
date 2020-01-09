namespace Logo.Tokens
{
    /// <summary>
    /// A token representing a variable; either a variable access, or declaration as a procedure parameter.
    /// </summary>
    public class VariableToken : Token
    {
        /// <summary>
        /// The name of the variable.  Normally, this should be the same as the <see cref="Token.Text" /> property minus the initial colon, but this is 
        /// the responsibility of the caller.
        /// </summary>
        public string VariableName { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">The value to set the <see cref="Token.Text" /> property to.</param>
        /// <param name="name">The value to set the <see cref="VariableName" /> property to.
        /// Callers are expected to ensure that the <c>text</c> parameter equals this parameter prepended by a colon.</param>
        public VariableToken(string text, string name) : base(text)
        {
            VariableName = name;
        }
    }
}
