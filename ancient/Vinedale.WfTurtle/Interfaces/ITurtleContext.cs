using System;
using System.Drawing;
using System.Windows.Forms;
using Vinedale.WfTurtle.Drawing;

namespace Vinedale.WfTurtle.Interfaces
{
    /// <summary>
    /// The turtle's equivalent of <c>InterpretorContext</c>.
    /// </summary>
    public interface ITurtleContext
    {
        /// <summary>
        /// Submit a drawing instruction to the list of instructions needed to compose the current screenview.
        /// </summary>
        /// <param name="instruction">The instruction to be added to the draw list.</param>
        void PendDrawingInstruction(Instruction instruction);

        /// <summary>
        /// Modify the instruction list to carry out the "clean" instruction behaviour.
        /// </summary>
        void PendCleanInstruction(bool resetPosition);

        /// <summary>
        /// Execute the list of drawing instructions that compose the current screenview.
        /// </summary>
        /// <param name="e">The paint event arguments.</param>
        /// <param name="size">The size of the screenview; needed to compute wrapping etc.</param>
        void ExecuteDrawingInstructions(PaintEventArgs e, Rectangle size);

        /// <summary>
        /// Event fired when the list of drawing instructions has changed, so that anything using this context can request a repaint.
        /// </summary>
        event EventHandler<EventArgs> InstructionsChanged;

        /// <summary>
        /// The active turtle.
        /// </summary>
        ITurtle CurrentTurtle { get; }
    }
}
