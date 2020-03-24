using Logo.Interpretation;
using Logo.Procedures;
using Logo.Tokens;
using System;
using System.Collections.Generic;
using Vinedale.Turtle.Drawing;
using Vinedale.Turtle.Interfaces;
using Vinedale.Turtle.Resources;

namespace Vinedale.Turtle
{
    /// <summary>
    /// The class that defines turtle drawing commands.
    /// </summary>
    public class TurtleCommands : ICommandModule
    {
        private readonly ITurtleContext _parentContext;

        /// <summary>
        /// The sole constructor for this class.
        /// </summary>
        /// <param name="context">The drawing context for the commands.</param>
        public TurtleCommands(ITurtleContext context)
        {
            _parentContext = context;
        }

        /// <summary>
        /// Returns the commands implemented by this class.
        /// </summary>
        /// <returns>A list of procedure definitions.</returns>
        public IList<LogoProcedure> RegisterProcedures()
        {
            return new List<LogoProcedure>
            {
                new LogoCommand("forward", "fd", 1, RedefinabilityType.NonRedefinable, Forward, Strings.CommandForwardHelpText, Strings.CommandForwardExampleText),
                new LogoCommand("back", new [] { "bk", "backward", "backwards" }, 1, RedefinabilityType.NonRedefinable, Backwards, Strings.CommandBackHelpText, Strings.CommandBackExampleText),
                new LogoCommand("right", "rt", 1, RedefinabilityType.NonRedefinable, Right, Strings.CommandRightHelpText, Strings.CommandRightExampleText),
                new LogoCommand("left", "lt", 1, RedefinabilityType.NonRedefinable, Left, Strings.CommandLeftHelpText, Strings.CommandLeftExampleText),
                new LogoCommand("penup", "pu", 0, RedefinabilityType.NonRedefinable, PenUp, Strings.CommandPenUpHelpText, ""),
                new LogoCommand("pendown", "pd", 0, RedefinabilityType.NonRedefinable, PenDown, Strings.CommandPenDownHelpText, ""),
                new LogoCommand("cleargraphics", new [] { "cg", "cleangraphics" }, 0, RedefinabilityType.NonRedefinable, ClearGraphics, Strings.CommandClearGraphicsHelpText, ""),
                new LogoCommand("clean", 0, RedefinabilityType.NonRedefinable, Clean, Strings.CommandCleanHelpText, ""),
                new LogoCommand("showturtle", "st", 0, RedefinabilityType.NonRedefinable, ShowTurtle, Strings.CommandShowTurtleHelpText, ""),
                new LogoCommand("home", 0, RedefinabilityType.NonRedefinable, Home, Strings.CommandHomeHelpText, ""),
                new LogoCommand("setx", 1, RedefinabilityType.NonRedefinable, SetX, Strings.CommandSetXHelpText, Strings.CommandSetXExampleText),
                new LogoCommand("sety", 1, RedefinabilityType.NonRedefinable, SetY, Strings.CommandSetYHelpText, Strings.CommandSetYExampleText),
            };
        }

        /// <summary>
        /// Move the turtle forwards.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token containing the distance to move forwards.</param>
        /// <returns><c>null</c>.</returns>
        public Token Forward(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (input[0].Type != LogoValueType.Number)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandForwardWrongTypeError);
                return null;
            }

            PendTranslateInstruction(GetDouble(input[0]));
            return null;
        }

        /// <summary>
        /// Move the turtle backwards.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token containing the distance to move backwards.</param>
        /// <returns><c>null</c>.</returns>
        public Token Backwards(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (input[0].Type != LogoValueType.Number)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandBackWrongTypeError);
                return null;
            }

            PendTranslateInstruction(-GetDouble(input[0]));
            return null;
        }

        /// <summary>
        /// Rotate the turtle to the right.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token containing the angle to rotate to the right (in degrees).</param>
        /// <returns><c>null</c></returns>
        public Token Right(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (input[0].Type != LogoValueType.Number)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandRightWrongTypeError);
                return null;
            }

            PendRotateInstruction(GetDouble(input[0]));
            return null;
        }

        /// <summary>
        /// Rotate the turtle to the left.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token containing the angle to rotate to the left (in degrees).</param>
        /// <returns><c>null</c></returns>
        public Token Left(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (input[0].Type != LogoValueType.Number)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandLeftWrongTypeError);
                return null;
            }

            PendRotateInstruction(-GetDouble(input[0]));
            return null;
        }

        /// <summary>
        /// Lift the turtle's pen.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Ignored.</param>
        /// <returns><c>null</c>.</returns>
        public Token PenUp(InterpretorContext context, params LogoValue[] input)
        {
            _parentContext.PendDrawingInstruction(new PenStatusInstruction(PenStatus.Up));
            return null;
        }

        /// <summary>
        /// Lower the turtle's pen.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Ignored.</param>
        /// <returns><c>null</c>.</returns>
        public Token PenDown(InterpretorContext context, params LogoValue[] input)
        {
            _parentContext.PendDrawingInstruction(new PenStatusInstruction(PenStatus.Down));
            return null;
        }

        /// <summary>
        /// Make the turtle visible.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Ignored.</param>
        /// <returns><c>null</c>.</returns>
        public Token ShowTurtle(InterpretorContext context, params LogoValue[] input)
        {
            _parentContext.PendDrawingInstruction(new TurtleStatusInstruction(TurtleStatus.Shown));
            return null;
        }

        /// <summary>
        /// Clean the screen without moving the turtle.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Ignored.</param>
        /// <returns><c>null</c>.</returns>
        public Token Clean(InterpretorContext context, params LogoValue[] input)
        {
            _parentContext.PendCleanInstruction(false);
            return null;
        }

        /// <summary>
        /// Move the turtle back to its home position.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Ignored.</param>
        /// <returns></returns>
        public Token Home(InterpretorContext context, params LogoValue[] input)
        {
            _parentContext.PendDrawingInstruction(new JumpToInstruction(0, 0, 0));
            return null;
        }

        /// <summary>
        /// Move the turtle by setting its absolute X coordinate.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token consisting of the new X coordinate value.</param>
        /// <returns></returns>
        public Token SetX(InterpretorContext context, params LogoValue[] input)
        {
            SetXY(context, input, Strings.CommandSetXWrongTypeError, BuildSetXInstruction);
            return null;
        }

        /// <summary>
        /// Move the turtle by setting its absolute Y coordinate.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token consisting of the new Y coordinate value.</param>
        /// <returns></returns>
        public Token SetY(InterpretorContext context, params LogoValue[] input)
        {
            SetXY(context, input, Strings.CommandSetYWrongTypeError, BuildSetYInstruction);
            return null;
        }

        private void SetXY(InterpretorContext context, LogoValue[] input, string wrongTypeError, Func<LogoValue[], JumpToInstruction> instructionBuilder)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (input[0].Type != LogoValueType.Number)
            {
                context.Interpretor.WriteOutputLine(wrongTypeError);
                return;
            }
            _parentContext.PendDrawingInstruction(instructionBuilder(input));
        }

        private JumpToInstruction BuildSetXInstruction(LogoValue[] t) => new JumpToInstruction(GetDouble(t[0]), null, null);

        private JumpToInstruction BuildSetYInstruction(LogoValue[] t) => new JumpToInstruction(null, GetDouble(t[0]), null);

        /// <summary>
        /// Clean the screen and return the turtle to its starting position.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Ignored.</param>
        /// <returns><c>null</c>.</returns>
        public Token ClearGraphics(InterpretorContext context, params LogoValue[] input)
        {
            _parentContext.PendCleanInstruction(true);
            return null;
        }

        private void PendTranslateInstruction(double length)
        {
            _parentContext.PendDrawingInstruction(new LineInstruction(length));
        }

        private void PendRotateInstruction(double angle)
        {
            _parentContext.PendDrawingInstruction(new RotateInstruction(angle));
        }

        private static double GetDouble(LogoValue token)
        {
            return Convert.ToDouble((decimal)token.Value);
        }
    }
}
