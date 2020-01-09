using Logo.Interpretation;
using Logo.Resources;
using Logo.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Logo.Procedures
{
    /// <summary>
    /// This class contains the implementations of core language features.
    /// </summary>
    public class CoreCommands : ICommandModule
    {
        /// <summary>
        /// Provides the definitions of procedures implemented in this class.
        /// </summary>
        /// <returns>The list of procedures defined in this class.</returns>
        public IList<LogoProcedure> RegisterProcedures()
        {
            return new LogoProcedure[]
            {
                new LogoCommand
                {
                    Name = Syntax.PrintCmd,
                    Aliases = new[] { Syntax.PrCmd },
                    Redefinability = RedefinabilityType.DefineAlongside,
                    ParameterCount = 1,
                    Implementation = Print,
                    HelpText = Strings.CommandPrintHelpText,
                    ExampleText = Strings.CommandPrintExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.NodesCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.DefineAlongside,
                    ParameterCount = 0,
                    Implementation = Nodes,
                    HelpText = Strings.CommandNodesHelpText,
                    ExampleText = string.Empty,
                },
                new LogoCommand
                {
                    Name = Syntax.RecycleCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 0,
                    Implementation = Recycle,
                    HelpText = Strings.CommandRecycleHelpText,
                    ExampleText = string.Empty,
                },
                new LogoCommand
                {
                    Name = Syntax.RepeatCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 2,
                    Implementation = Repeat,
                    HelpText = Strings.CommandRepeatHelpText,
                    ExampleText = Strings.CommandRepeatExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.SpaceCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 0,
                    Implementation = SpaceUsed,
                    HelpText = Strings.CommandSpaceHelpText,
                    ExampleText = string.Empty,
                },
                new LogoCommand
                {
                    Name = Syntax.AbsCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = MathAbs,
                    HelpText = Strings.CommandAbsHelpText,
                    ExampleText = Strings.CommandAbsExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.AndCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = BoolAnd,
                    HelpText = Strings.CommandAndHelpText,
                    ExampleText = Strings.CommandAndExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.ArctanCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = MathAtan,
                    HelpText = Strings.CommandArctanHelpText,
                    ExampleText = Strings.CommandArctanExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.CosCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = MathCos,
                    HelpText = Strings.CommandCosHelpText,
                    ExampleText = Strings.CommandCosExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.SinCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = MathSin,
                    HelpText = Strings.CommandSinHelpText,
                    ExampleText = Strings.CommandSinExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.TanCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = MathTan,
                    HelpText = Strings.CommandTanHelpText,
                    ExampleText = Strings.CommandTanExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.MakeCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 2,
                    Implementation = MakeVariable,
                    HelpText = Strings.CommandMakeHelpText,
                    ExampleText = Strings.CommandMakeExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.ClearnameCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = ClearVariable,
                    HelpText = Strings.CommandClearnameHelpText,
                    ExampleText = Strings.CommandClearnameExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.ClearnamesCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 0,
                    Implementation = ClearGlobalVariables,
                    HelpText = Strings.CommandClearnamesHelpText,
                    ExampleText = string.Empty,
                },
                new LogoCommand
                {
                    Name = Syntax.HelpCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.DefineAlongside,
                    ParameterCount = 1,
                    Implementation = OutputHelpText,
                    HelpText = Strings.CommandHelpHelpText,
                    ExampleText = Strings.CommandHelpExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.PiCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 0,
                    Implementation = ReturnPi,
                    HelpText = Strings.CommandPiHelpText,
                    ExampleText = string.Empty,
                },
                new LogoCommand
                {
                    Name = Syntax.ButfirstCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = ListButFirst,
                    HelpText = Strings.CommandButfirstHelpText,
                    ExampleText = Strings.CommandButfirstExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.ButlastCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = ListButLast,
                    HelpText = Strings.CommandButlastHelpText,
                    ExampleText = Strings.CommandButlastExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.AsciiCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = AsciiValue,
                    HelpText = Strings.CommandAsciiHelpText,
                    ExampleText = Strings.CommandAsciiExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.CharCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = AsciiToChar,
                    HelpText = Strings.CommandCharHelpText,
                    ExampleText = Strings.CommandCharExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.CountCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = Count,
                    HelpText = Strings.CommandCountHelpText,
                    ExampleText = Strings.CommandCountExampleText,
                },
                new LogoCommand
                {
                    Name = Syntax.DifferenceCmd,
                    Aliases = Array.Empty<string>(),
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 2,
                    Implementation = Difference,
                    HelpText = Strings.CommandDifferenceHelpText,
                    ExampleText = Strings.CommandDifferenceExampleText,
                },
            };
        }

        /// <summary>
        /// Converts a number to a character.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one parameter representing a number.</param>
        /// <returns>A token containing a string.</returns>
        public static Token AsciiToChar(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (input[0].Type != LogoValueType.Number)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandCharWrongTypeError);
                return null;
            }
            return new LiteralToken(Syntax.CharCmd, new LogoValue(LogoValueType.Text, Encoding.ASCII.GetString(new[] { Convert.ToByte(input[0].Value, CultureInfo.CurrentCulture) })));
        }

        /// <summary>
        /// Converts a character to its ASCII number.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one parameter representing a string.</param>
        /// <returns>A token containing a number.</returns>
        public static Token AsciiValue(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (input[0].Type != LogoValueType.Text)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandAsciiWrongTypeError);
                return null;
            }
            if (string.IsNullOrEmpty((string)input[0].Value))
            {
                return new LiteralToken(Syntax.AsciiCmd, new LogoValue(LogoValueType.Number, 0));
            }
            return new LiteralToken(Syntax.AsciiCmd, new LogoValue(LogoValueType.Number, Encoding.ASCII.GetBytes((string)input[0].Value)[0]));
        }

        /// <summary>
        /// Removes the first element from a list.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain a single token of list type.</param>
        /// <returns>A token containing a list.</returns>
        public static Token ListButFirst(InterpretorContext context, params LogoValue[] input)
        {
            ListToken outList = new ListToken((input[0].Value as ListToken).Contents.Skip(1).ToArray());
            return new LiteralToken(outList.Text, new LogoValue(LogoValueType.List, outList));
        }

        /// <summary>
        /// Removes the last element from a list.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain a single token of list type.</param>
        /// <returns>A token containing a list.</returns>
        public static Token ListButLast(InterpretorContext context, params LogoValue[] input)
        {
            List<Token> inContents = (input[0].Value as ListToken).Contents;
            ListToken outList = new ListToken(inContents.Take(inContents.Count - 1).ToArray());
            return new LiteralToken(outList.Text, new LogoValue(LogoValueType.List, outList));
        }

        /// <summary>
        /// Defines and/or sets the value of a variable.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain two tokens, the first being the variable name and the second the variable value.</param>
        /// <returns><c>null</c></returns>
        public static Token MakeVariable(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Interpretor.DebugOutputWriter.WriteLine(Strings.CommandMakeStartDebugMessage);
            context.SetVariable((string)input[0].Value, input[1]);
            return null;
        }

        /// <summary>
        /// Removes a variable from the global namespace.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token containing the variable name to remove.</param>
        /// <returns><c>null</c></returns>
        public static Token ClearVariable(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.ClearVariable((string)input[0].Value);
            return null;
        }

        /// <summary>
        /// Clears the global variable namespace.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Not used.</param>
        /// <returns><c>null</c></returns>
        public static Token ClearGlobalVariables(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.ClearAllVariables();
            return null;
        }

        /// <summary>
        /// Implements the mathematical cosine function.
        /// </summary>
        /// <remarks>All trigonometrical functions assume the input is in radians.</remarks>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token containing a number.</param>
        /// <returns>A token containing the cosine of the input.</returns>
        public static Token MathCos(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (input[0].Type != LogoValueType.Number)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandCosWrongTypeError);
                return null;
            }
            return new LiteralToken(Syntax.CosCmd, new LogoValue(LogoValueType.Number, Convert.ToDecimal(Math.Cos(Convert.ToDouble((decimal)input[0].Value)))));
        }

        /// <summary>
        /// Implements the mathematical arctangent function.
        /// </summary>
        /// <remarks>All trigonometrical functions assume the input is in radians.</remarks>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token containing a number.</param>
        /// <returns>A token containing the arctangent of the input.</returns>
        public static Token MathAtan(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (input[0].Type != LogoValueType.Number)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandArctanWrongTypeError);
                return null;
            }
            return new LiteralToken(Syntax.ArctanCmd, new LogoValue(LogoValueType.Number, Convert.ToDecimal(Math.Atan(Convert.ToDouble((decimal)input[0].Value)))));
        }

        /// <summary>
        /// Implements the sine function.
        /// </summary>
        /// <remarks>All trigonometrical functions assume the input is in radians.</remarks>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token containing a number.</param>
        /// <returns>A token containing the sine of the input.</returns>
        public static Token MathSin(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (input[0].Type != LogoValueType.Number)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandSinWrongTypeError);
                return null;
            }
            return new LiteralToken(Syntax.SinCmd, new LogoValue(LogoValueType.Number, Convert.ToDecimal(Math.Sin(Convert.ToDouble((decimal)input[0].Value)))));
        }

        /// <summary>
        /// Implements the tangent function
        /// </summary>
        /// <remarks>All trigonometrical functions assume the input is in radians.</remarks>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token containing a number.</param>
        /// <returns>A token containing the tangent of the input.</returns>
        public static Token MathTan(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (input[0].Type != LogoValueType.Number)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandTanWrongTypeError);
                return null;
            }
            return new LiteralToken(Syntax.TanCmd, new LogoValue(LogoValueType.Number, Convert.ToDecimal(Math.Tan(Convert.ToDouble((decimal)input[0].Value)))));
        }

        /// <summary>
        /// Performs a boolean AND operation on the elements of a list.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token containing a list.</param>
        /// <returns><c>true</c> if all elements in the input list evaluate to <c>true</c>, <c>false</c> otherwise.</returns>
        public static Token BoolAnd(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (input[0].Type != LogoValueType.List)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandAndWrongTypeError);
                return null;
            }

            ListToken inputList = new ListToken(((ListToken)input[0].Value).Contents);
            context.Interpretor.EvaluateListContents(inputList, false);
            return new LiteralToken(Syntax.AndCmd, new LogoValue(LogoValueType.Bool,
                (inputList.Contents.Count > 0) && inputList.Contents.All(t => t is LiteralToken tl && tl.Value.Type == LogoValueType.Bool && (bool)tl.Value.Value)));
        }

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token containing a number.</param>
        /// <returns>The absolute value of the input.</returns>
        public static Token MathAbs(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (input[0].Type != LogoValueType.Number)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandAbsWrongTypeError);
                return null;
            }
            return new LiteralToken(Syntax.AbsCmd, new LogoValue(LogoValueType.Number, Math.Abs((decimal)input[0].Value)));
        }

        /// <summary>
        /// Returns π.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="dummy">Not used.</param>
        /// <returns>A token containing the number π.</returns>
        public static Token ReturnPi(InterpretorContext context, params LogoValue[] dummy)
        {
            return new LiteralToken(Math.PI.ToString(CultureInfo.InvariantCulture), new LogoValue(LogoValueType.Number, (decimal)Math.PI));
        }

        /// <summary>
        /// Calls the runtime garbage collector.  Largely implemented for nostalgia purposes.
        /// </summary>
        /// <param name="contaxt">Not used.</param>
        /// <param name="input">Not used.</param>
        /// <returns><c>null</c></returns>
        public static Token Recycle(InterpretorContext contaxt, params LogoValue[] input)
        {
            GC.Collect();
            return null;
        }

        /// <summary>
        /// Returns the total amount of system memory used by the running process.  Largely implemented for nostalgia purposes.
        /// </summary>
        /// <param name="context">Not used.</param>
        /// <param name="input">Not used.</param>
        /// <returns>A token containing the bytes used by the running process.</returns>
        public static Token SpaceUsed(InterpretorContext context, params LogoValue[] input)
        {
            LogoValue val = new LogoValue(LogoValueType.Number, (decimal)GC.GetTotalMemory(false));
            return new LiteralToken(Syntax.SpaceCmd, val);
        }

        /// <summary>
        /// Outputs the first parameter token to the context's output writer, followed by a new line.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="output">Should contain one token whose value is to be printed.</param>
        /// <returns><c>null</c></returns>
        public static Token Print(InterpretorContext context, params LogoValue[] output)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output[0].Type == LogoValueType.List)
            {
                ListToken copiedList = new ListToken(((ListToken)output[0].Value).Contents);
                if (context.Interpretor.EvaluateListContents(copiedList, true) == InterpretationResultType.SuccessComplete)
                {
                    context.Interpretor.WriteOutputLine(string.Join(" ", copiedList.Contents.Select(t => (t as LiteralToken).Value.Value.ToString())));
                }
            }
            else
            {
                context.Interpretor.WriteOutputLine(output[0].Value.ToString());
            }
            return null;
        }

        /// <summary>
        /// Prints the total number of defined procedures and the number of distinct procedure names and aliases in the interpretor context, to the context's output writer.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="dummy">Not used.</param>
        /// <returns><c>null</c></returns>
        public static Token Nodes(InterpretorContext context, params LogoValue[] dummy)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Interpretor.WriteOutputLine(string.Format(CultureInfo.CurrentCulture, Strings.CommandNodesOutput, context.Procedures.Count, context.ProcedureNames.Count));
            return null;
        }

        /// <summary>
        /// Prints the example text and help text for the given procedure or alias, to the context's output writer.
        /// </summary>
        /// <remarks>If the user has asked for help about the help command, by entering <c>help "help</c>, also prints a list of all procedures.</remarks>
        /// <param name="context">The interpretor context.</param>
        /// <param name="cmd">Should contain one token containing a string.</param>
        /// <returns><c>null</c></returns>
        public static Token OutputHelpText(InterpretorContext context, params LogoValue[] cmd)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.ProcedureNames.ContainsKey(cmd[0].Value.ToString()))
            {
                context.Interpretor.WriteOutputLine(string.Format(CultureInfo.CurrentCulture, Strings.CommandHelpUnknownProcedureError, cmd[0].Value.ToString()));
                return null;
            }

            IList<LogoProcedure> procList = context.ProcedureNames[cmd[0].Value.ToString()];
            if (procList.Count > 1)
            {
                context.Interpretor.WriteOutputLine(string.Format(CultureInfo.CurrentCulture, Strings.CommandHelpActionCountOutput, cmd[0].Value.ToString(), procList.Count));
            }
            foreach (LogoProcedure proc in procList)
            {
                context.Interpretor.WriteOutputLine(cmd[0].Value.ToString() + " " + proc.ExampleText);
                context.Interpretor.WriteOutputLine(proc.HelpText + "\n");
            }
            if (Syntax.HelpCmd == (string)cmd[0].Value)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandHelpHeadingOutput);
                foreach (string procName in context.ProcedureNames.Keys.OrderBy(s => s))
                {
                    context.Interpretor.WriteOutputLine("  " + procName);
                }
            }

            return null;
        }

        /// <summary>
        /// Executes a list of instructions a defined number of times.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="parameters">Should contain two tokens: the first should be the number of times to repeat the instruction list, and the second should be that list.</param>
        /// <returns><c>null</c></returns>
        public static Token Repeat(InterpretorContext context, params LogoValue[] parameters)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (parameters[0].Type != LogoValueType.Number)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandRepeatWrongRepeatTypeError);
                return null;
            }

            if (parameters[1].Type != LogoValueType.List)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandRepeatWrongListTypeError);
                return null;
            }

            for (int i = 0; i < Convert.ToInt32(parameters[0].Value, CultureInfo.CurrentCulture); ++i)
            {
                ListToken exec = new ListToken((parameters[1].Value as ListToken).Contents);
                context.Interpretor.EvaluateListContents(exec, false);
            }

            return null;
        }

        /// <summary>
        /// Gives the length of a list or string.
        /// </summary>
        /// <param name="context">The intepretor context.</param>
        /// <param name="parameters">Should contain a single token, either a list or a string.</param>
        /// <returns>A token containing the number of elements (characters or list items) in the input token.</returns>
        public static Token Count(InterpretorContext context, params LogoValue[] parameters)
        {
            if (parameters[0].Type == LogoValueType.Text)
            {
                return new LiteralToken(Syntax.CountCmd, new LogoValue(LogoValueType.Number, (decimal)(parameters[0].Value as string).Length));
            }
            else if (parameters[0].Type == LogoValueType.List)
            {
                return new LiteralToken(Syntax.CountCmd, new LogoValue(LogoValueType.Number, (decimal)(parameters[0].Value as ListToken).Contents.Count));
            }
            return new LiteralToken(Syntax.CountCmd, new LogoValue(LogoValueType.Number, 1m));
        }

        /// <summary>
        /// Subtracts one number from another.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="parameters">Should contain two tokens, both numbers.</param>
        /// <returns>A token containing the difference between the two numbers.</returns>
        public static Token Difference(InterpretorContext context, params LogoValue[] parameters)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (parameters[0].Type != LogoValueType.Number || parameters[1].Type != LogoValueType.Number)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandDifferenceTypeError);
                return null;
            }
            return new LiteralToken(Syntax.DifferenceCmd, new LogoValue(LogoValueType.Number, ((decimal)parameters[0].Value) - ((decimal)parameters[1].Value)));
        }
    }
}
