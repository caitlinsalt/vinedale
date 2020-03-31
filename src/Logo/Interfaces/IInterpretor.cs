using Logo.Interpretation;
using Logo.Tokens;
using System.IO;

namespace Logo.Interfaces
{
    /// <summary>
    /// A Logo interpretation engine.
    /// </summary>
    public interface IInterpretor
    {
        /// <summary>
        /// Writer for receiving debug output.
        /// </summary>
        TextWriter DebugOutputWriter { get; set; }

        /// <summary>
        /// Evaluate the content of a list in place.
        /// </summary>
        /// <param name="list">The list of tokens to be evaluated.</param>
        /// <param name="literalEvaluationOfUndefinedWords">If true, undefined words should be evaluated as string literals.  If false, they will output an error.</param>
        /// <returns>An <see cref="InterpretationResultType" /> value indicating if evaluation was completed successfully, errored, or requires more input.</returns>
        InterpretationResultType EvaluateListContents(ListToken list, bool literalEvaluationOfUndefinedWords);

        /// <summary>
        /// Write output to the output writer.
        /// </summary>
        /// <param name="output">The text to be output.</param>
        void WriteOutput(string output);

        /// <summary>
        /// Write output to the output writer, followed by a new line.
        /// </summary>
        /// <param name="output">The text to be output.</param>
        void WriteOutputLine(string output);
    }
}
