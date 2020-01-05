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
        public PenStatus Status { get; set; }
    }
}
