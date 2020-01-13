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
                new LogoCommand
                {
                    Name = "forward",
                    Aliases = new [] { "fd" },
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = Forward,
                    HelpText = Strings.CommandForwardHelpText,
                    ExampleText = Strings.CommandForwardExampleText,
                },
                new LogoCommand
                {
                    Name = "back",
                    Aliases = new [] { "bk", "backward", "backwards" },
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = Backwards,
                    HelpText = Strings.CommandBackHelpText,
                    ExampleText = Strings.CommandBackExampleText,
                },
                new LogoCommand
                {
                    Name = "right",
                    Aliases = new [] { "rt" },
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = Right,
                    HelpText = Strings.CommandRightHelpText,
                    ExampleText = Strings.CommandRightExampleText,
                },
                new LogoCommand
                {
                    Name = "left",
                    Aliases = new [] { "lt" },
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = Left,
                    HelpText = Strings.CommandLeftHelpText,
                    ExampleText = Strings.CommandLeftExampleText,
                },
                new LogoCommand
                {
                    Name = "penup",
                    Aliases = new [] { "pu" },
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 0,
                    Implementation = PenUp,
                    HelpText = Strings.CommandPenUpHelpText,
                    ExampleText = string.Empty,
                },
                new LogoCommand
                {
                    Name = "pendown",
                    Aliases = new [] { "pd" },
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 0,
                    Implementation = PenDown,
                    HelpText = Strings.CommandPenDownHelpText,
                    ExampleText = string.Empty,
                },
                new LogoCommand
                {
                    Name = "cleargraphics",
                    Aliases = new [] { "cg", "cleangraphics" },
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 0,
                    Implementation = ClearGraphics,
                    HelpText = Strings.CommandClearGraphicsHelpText,
                    ExampleText = string.Empty,
                },
                new LogoCommand
                {
                    Name = "clean",
                    Aliases = new string[0],
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 0,
                    Implementation = Clean,
                    HelpText = Strings.CommandCleanHelpText,
                    ExampleText = string.Empty,
                },
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

        private double GetDouble(LogoValue token)
        {
            return Convert.ToDouble((decimal)token.Value);
        }
    }
}
