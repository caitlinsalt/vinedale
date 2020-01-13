namespace Vinedale.Turtle.Drawing
{
    /// <summary>
    /// An instruction for the turtle to change heading.
    /// </summary>
    public class RotateInstruction : Instruction
    {
        /// <summary>
        /// The angle by which to rotate (in degrees)
        /// </summary>
        public double Angle { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="angle">The value of the <see cref="Angle" /> property.</param>
        public RotateInstruction(double angle)
        {
            Angle = angle;
        }
    }
}
