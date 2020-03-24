namespace Vinedale.Turtle.Drawing
{
    /// <summary>
    /// An instruction representing an absolute turtle move.
    /// </summary>
    public class JumpToInstruction : Instruction
    {
        /// <summary>
        /// The absolute X-coordinate to move to, or null if the X-coordinate should not change.
        /// </summary>
        public double? X { get; }

        /// <summary>
        /// The absolute Y-coordinate to move to, or null if the Y-coordinate should not change.
        /// </summary>
        public double? Y { get; }

        /// <summary>
        /// The heading of the turtle after the instruction, or null if the heading should not change.
        /// </summary>
        public double? Heading { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">The value of the <see cref="X" /> property.</param>
        /// <param name="y">The value of the <see cref="Y" /> property.</param>
        /// <param name="heading">The value of the <see cref="Heading" /> property.</param>
        public JumpToInstruction(double? x, double? y, double? heading)
        {
            X = x;
            Y = y;
            Heading = heading;
        }
    }
}
