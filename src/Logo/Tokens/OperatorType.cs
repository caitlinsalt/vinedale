namespace Logo.Tokens
{
    /// <summary>
    /// The operator types supported in the language.
    /// </summary>
    /// <remarks>The integer values of the enum define the operator precedence order, with the lowest enum value being the highest-precedence operator.</remarks>
    public enum OperatorType
    {
        /// <summary>
        /// Multiplication.
        /// </summary>
        Multiply,

        /// <summary>
        /// Division.
        /// </summary>
        Divide,

        /// <summary>
        /// Addition.
        /// </summary>
        Add,

        /// <summary>
        /// Subtraction.
        /// </summary>
        Subtract,

        /// <summary>
        /// Equality testing.
        /// </summary>
        Equals
    }
}
