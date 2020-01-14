using System.Drawing;
using System.Windows.Forms;
using Vinedale.Turtle.Drawing;

namespace Vinedale.Turtle.Interfaces
{
    /// <summary>
    /// The interface representing an on-screen turtle.
    /// </summary>
    public interface ITurtle
    {
        /// <summary>
        /// The direction in which the turtle is facing.
        /// </summary>
        double Heading { get; set; }

        /// <summary>
        /// The current absolute X-coordinate of the turtle.
        /// </summary>
        double X { get; set; }

        /// <summary>
        /// The current absolute Y-coordinate of the turtle.
        /// </summary>
        double Y { get; set; }

        /// <summary>
        /// The current status of the pen.
        /// </summary>
        PenStatus PenDown { get; set; }

        /// <summary>
        /// The current status of the turtle (shown or hidden).
        /// </summary>
        TurtleStatus TurtleShown { get; set; }

        /// <summary>
        /// Move the turtle a set distance.
        /// </summary>
        /// <param name="distance">The distance to move (positive is forward, negative is backward)</param>
        /// <param name="e"></param>
        /// <param name="windowSize"></param>
        void Move(double distance, PaintEventArgs e, Rectangle windowSize);

        /// <summary>
        /// Reset the turtle's location and heading.
        /// </summary>
        void Reset();

        /// <summary>
        /// Draw the turtle to a Graphics context.
        /// </summary>
        /// <param name="e">The event arguments including the graphics context.</param>
        /// <param name="windowSize">A <c>Rectangle</c> representing the size of the drawing window.</param>
        void DrawTurtle(PaintEventArgs e, Rectangle windowSize);
    }
}
