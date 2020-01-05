using System;
using System.Drawing;
using System.Windows.Forms;
using Vinedale.Turtle.Drawing;
using Vinedale.Turtle.Interfaces;

namespace Vinedale.Turtle
{
    /// <summary>
    /// The class which represents an on-screen turtle.
    /// </summary>
    public class SoftTurtle : ITurtle
    {
        private const float TopPointRadius = 20;
        private const float BottomPointRadius = 12;
        private const double BottomLeftPointAngle = 225 * Math.PI / 180;
        private const double BottomRightPointAngle = 135 * Math.PI / 180;

        /// <summary>
        /// The direction in which the turtle is facing, expressed in degrees.
        /// </summary>
        public double Heading { get; set; }

        /// <summary>
        /// The turtle's X location.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The turtle's Y location.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// The current status of the pen.
        /// </summary>
        public PenStatus PenDown { get; set; }

        public TurtleStatus TurtleShown { get; set; }

        /// <summary>
        /// The direction in which the turtle is facing, expressed in radians.
        /// </summary>
        public double HeadingRadians
        {
            get
            {
                return Heading * Math.PI / 180;
            }
            set
            {
                Heading = value / (Math.PI / 180);
            }
        }

        /// <summary>
        /// Draw the turtle to a graphics context.
        /// </summary>
        /// <param name="e">The event encapsulating the graphics context.</param>
        /// <param name="windowSize">A <c>Rectangle</c> representing the size of the drawing window.</param>
        public void DrawTurtle(PaintEventArgs e, Rectangle windowSize)
        {
            if (TurtleShown == TurtleStatus.Hidden)
            {
                return;
            }

            double topPointXOffset = TopPointRadius * Math.Sin(HeadingRadians);
            double topPointYOffset = TopPointRadius * Math.Cos(HeadingRadians);
            double blPointXOffset = BottomPointRadius * Math.Sin(HeadingRadians + BottomLeftPointAngle);
            double blPointYOffset = BottomPointRadius * Math.Cos(HeadingRadians + BottomLeftPointAngle);
            double brPointXOffset = BottomPointRadius * Math.Sin(HeadingRadians + BottomRightPointAngle);
            double brPointYOffset = BottomPointRadius * Math.Cos(HeadingRadians + BottomRightPointAngle);

            PointF centrePoint = new PointF(windowSize.Width / 2f, windowSize.Height / 2f);
            PointF locPoint = new PointF(centrePoint.X + (float)X, centrePoint.Y - (float)Y);
            PointF blPoint = new PointF(locPoint.X + (float)blPointXOffset, locPoint.Y - (float)blPointYOffset);
            PointF brPoint = new PointF(locPoint.X + (float)brPointXOffset, locPoint.Y - (float)brPointYOffset);
            PointF topPoint = new PointF(locPoint.X + (float)topPointXOffset, locPoint.Y - (float)topPointYOffset);
            using (Pen pen = new Pen(Color.White))
            {
                e.Graphics.DrawLine(pen, locPoint, blPoint);
                e.Graphics.DrawLine(pen, locPoint, brPoint);
                e.Graphics.DrawLine(pen, topPoint, blPoint);
                e.Graphics.DrawLine(pen, topPoint, brPoint);
            }
        }

        /// <summary>
        /// Move the turtle a given distance, drawing a line behind it.
        /// </summary>
        /// <param name="distance">The distance to move.</param>
        /// <param name="e">Paint event args</param>
        /// <param name="windowSize">Size of the drawing area.</param>
        public void Move(double distance, PaintEventArgs e, Rectangle windowSize)
        {
            PointF centrePoint = new PointF(windowSize.Width / 2f, windowSize.Height / 2f);

            double xMove = distance * Math.Sin(HeadingRadians);
            double yMove = distance * Math.Cos(HeadingRadians);

            if (PenDown == PenStatus.Down)
            {
                using (Pen pen = new Pen(Color.White))
                {
                    e.Graphics.DrawLine(pen, new PointF(centrePoint.X + (float)X, centrePoint.Y - (float)Y), new PointF(centrePoint.X + (float)(X + xMove), centrePoint.Y - (float)(Y + yMove)));
                }
            }

            X += xMove;
            Y += yMove;
        }

        /// <summary>
        /// Reset the turtle's location and heading.
        /// </summary>
        public void Reset()
        {
            TurtleShown = TurtleStatus.Shown;
            PenDown = PenStatus.Down;
            Heading = 0;
            X = 0;
            Y = 0;
        }
    }
}
