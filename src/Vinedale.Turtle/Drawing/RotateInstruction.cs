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
        public double Angle { get; set; }
    }
}
