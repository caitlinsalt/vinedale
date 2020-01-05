namespace Vinedale.Turtle.Drawing
{
    /// <summary>
    /// An instruction representing an absolute turtle move.
    /// </summary>
    public class JumpToInstruction : Instruction
    {
        /// <summary>
        /// The absolute X-coordinate to move to.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The absolute Y-coordinate to move to.
        /// </summary>
        public double Y { get; set; }
    }
}
