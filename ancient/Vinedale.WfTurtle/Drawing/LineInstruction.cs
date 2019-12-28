namespace Vinedale.WfTurtle.Drawing
{
    /// <summary>
    /// An instruction for the turtle to draw a line.
    /// </summary>
    public class LineInstruction : Instruction
    {
        /// <summary>
        /// Length of the line to draw.
        /// </summary>
        public double Length { get; set; }
    }
}
