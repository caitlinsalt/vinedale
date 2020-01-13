namespace Vinedale.Turtle.Drawing
{
    /// <summary>
    /// An instruction for the turtle to draw a line.
    /// </summary>
    public class LineInstruction : Instruction
    {
        /// <summary>
        /// Length of the line to draw.
        /// </summary>
        public double Length { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="length">Value of the <see cref="Length" /> property.</param>
        public LineInstruction(double length)
        {
            Length = length;
        }
    }
}
