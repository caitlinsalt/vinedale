namespace Vinedale.Turtle.Drawing
{
    /// <summary>
    /// An instruction to (potentially) change the pen status.
    /// </summary>
    public class PenStatusInstruction : Instruction
    {
        /// <summary>
        /// The new pen status.
        /// </summary>
        public PenStatus Status { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="status">The value of the <see cref="Status" /> property.</param>
        public PenStatusInstruction(PenStatus status)
        {
            Status = status;
        }
    }
}
