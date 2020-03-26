using System;
using System.Collections.Generic;
using System.Text;

namespace Vinedale.Turtle.Drawing
{
    /// <summary>
    /// An instruction to change the pen width.
    /// </summary>
    public class PenWidthInstruction : Instruction
    {
        /// <summary>
        /// The width to set the pen to.
        /// </summary>
        public double Width { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="width">The new pen width.</param>
        public PenWidthInstruction(double width)
        {
            Width = width;
        }
    }
}
