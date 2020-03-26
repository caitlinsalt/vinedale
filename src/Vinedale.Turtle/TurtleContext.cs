using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Vinedale.Turtle.Drawing;
using Vinedale.Turtle.Interfaces;

namespace Vinedale.Turtle
{
    /// <summary>
    /// Implementation of <c>ITurtleContext</c> which draws to a <c>TurtleWindow</c>.
    /// </summary>
    public class TurtleContext : ITurtleContext
    {
        private readonly List<Instruction> _instructionList;

        /// <summary>
        /// Event fired when the instruction list has changed.
        /// </summary>
        public event EventHandler<EventArgs> InstructionsChanged;

        /// <summary>
        /// The current active turtle.
        /// </summary>
        public ITurtle CurrentTurtle { get; }

        /// <summary>
        /// The default constructor.
        /// </summary>
        public TurtleContext()
        {
            CurrentTurtle = new SoftTurtle();
            _instructionList = new List<Instruction>();
        }

        /// <summary>
        /// Add a new drawing instruction to the instruction list and redraw the window.
        /// </summary>
        /// <param name="instruction">The <c>Instruction</c> to be drawn.</param>
        public void PendDrawingInstruction(Instruction instruction)
        {
            _instructionList.Add(instruction);
            OnInstructionsChanged();
        }

        /// <summary>
        /// Reset the drawing instruction list.
        /// </summary>
        /// <param name="resetPosition">Return the turtle to its home position if true, not if false.</param>
        public void PendCleanInstruction(bool resetPosition)
        {
            _instructionList.Clear();
            if (!resetPosition)
            {
                _instructionList.Add(new JumpToInstruction(CurrentTurtle.X, CurrentTurtle.Y, CurrentTurtle.Heading));
            }
            OnInstructionsChanged();
        }

        /// <summary>
        /// Execute the list of drawing instructions.
        /// </summary>
        /// <param name="e">The paint event args received from the system.</param>
        /// <param name="size">The size of the drawing area.</param>
        public void ExecuteDrawingInstructions(PaintEventArgs e, Rectangle size)
        {
            CurrentTurtle.Reset();
            foreach (Instruction baseInstruction in _instructionList)
            {
                if (baseInstruction is RotateInstruction rotateInstruction)
                {
                    HandleRotate(rotateInstruction);
                    continue;
                }
                if (baseInstruction is LineInstruction lineInstruction)
                {
                    HandleTranslate(lineInstruction, e, size);
                    continue;
                }
                if (baseInstruction is PenStatusInstruction penStatusInstruction)
                {
                    CurrentTurtle.PenDown = penStatusInstruction.Status;
                    continue;
                }
                if (baseInstruction is PenWidthInstruction penWidthInstruction)
                {
                    CurrentTurtle.PenSize = penWidthInstruction.Width;
                    continue;
                }
                if (baseInstruction is TurtleStatusInstruction tsInstruction)
                {
                    CurrentTurtle.TurtleShown = tsInstruction.Status;
                }
                if (baseInstruction is JumpToInstruction jumpInstruction)
                {
                    if (jumpInstruction.X.HasValue)
                    {
                        CurrentTurtle.X = jumpInstruction.X.Value;
                    }
                    if (jumpInstruction.Y.HasValue)
                    {
                        CurrentTurtle.Y = jumpInstruction.Y.Value;
                    }
                    if (jumpInstruction.Heading.HasValue)
                    {
                        CurrentTurtle.Heading = jumpInstruction.Heading.Value;
                    }
                    continue;
                }
            }
        }

        /// <summary>
        /// Emits the <see cref="InstructionsChanged" /> event.
        /// </summary>
        protected virtual void OnInstructionsChanged()
        {
            InstructionsChanged?.Invoke(this, EventArgs.Empty);
        }

        private void HandleRotate(RotateInstruction rotation)
        {
            CurrentTurtle.Heading += rotation.Angle;
        }

        private void HandleTranslate(LineInstruction instr, PaintEventArgs e, Rectangle size)
        {
            CurrentTurtle.Move(instr.Length, e, size);
        }
    }
}
