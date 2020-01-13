namespace Vinedale.Turtle.Drawing
{
    /// <summary>
    /// An instruction to (potentially) change the turtle status.
    /// </summary>
    public class TurtleStatusInstruction : Instruction
    {
        /// <summary>
        /// The status to leave the turtle in after this instruction has been executed.
        /// </summary>
        public TurtleStatus Status { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="status">Value of the <see cref="Status" /> property.</param>
        public TurtleStatusInstruction(TurtleStatus status)
        {
            Status = status;
        }
    }
}
