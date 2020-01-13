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
        public double X { get; }

        /// <summary>
        /// The absolute Y-coordinate to move to.
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">The value of the <see cref="X" /> property.</param>
        /// <param name="y">The value of the <see cref="Y" /> property.</param>
        public JumpToInstruction(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
