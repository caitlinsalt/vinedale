namespace Logo.Interpretation
{
    /// <summary>
    /// Defines the possible results of processing an input line
    /// </summary>
    public enum InterpretationResultType
    {
        /// <summary>
        /// The input was parsed with no errors, and was executed immediately.
        /// </summary>
        SuccessComplete,

        /// <summary>
        /// The input was parsed with no errors, but is incomplete, and more input is required to complete execution.
        /// </summary>
        SuccessIncomplete,

        /// <summary>
        /// A parsing error occurred.
        /// </summary>
        Failure
    }
}
