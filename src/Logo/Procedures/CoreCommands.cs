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
                new LogoCommand(Syntax.PrintCmd, Syntax.PrCmd, 1, RedefinabilityType.DefineAlongside, Print, Strings.CommandPrintHelpText, Strings.CommandPrintExampleText),
                new LogoCommand(Syntax.NodesCmd, 0, RedefinabilityType.DefineAlongside, Nodes, Strings.CommandNodesHelpText),
                new LogoCommand(Syntax.RecycleCmd, 0, RedefinabilityType.NonRedefinable, Recycle, Strings.CommandRecycleHelpText),
                new LogoCommand(Syntax.RepeatCmd, 2, RedefinabilityType.NonRedefinable, Repeat, Strings.CommandRepeatHelpText, Strings.CommandRepeatExampleText),
                new LogoCommand(Syntax.SpaceCmd, 0, RedefinabilityType.NonRedefinable, SpaceUsed, Strings.CommandSpaceHelpText),
                new LogoCommand(Syntax.AbsCmd, 1, RedefinabilityType.NonRedefinable, MathAbs, Strings.CommandAbsHelpText, Strings.CommandAbsExampleText),
                new LogoCommand(Syntax.AndCmd, 1, RedefinabilityType.NonRedefinable, BoolAnd, Strings.CommandAndHelpText, Strings.CommandAndExampleText),
                new LogoCommand(Syntax.ArctanCmd, 1, RedefinabilityType.NonRedefinable, MathAtan, Strings.CommandArctanHelpText, Strings.CommandArctanExampleText),
                new LogoCommand(Syntax.CosCmd, 1, RedefinabilityType.NonRedefinable, MathCos, Strings.CommandCosHelpText, Strings.CommandCosExampleText),
                new LogoCommand(Syntax.SinCmd, 1, RedefinabilityType.NonRedefinable, MathSin, Strings.CommandSinHelpText, Strings.CommandSinExampleText),
                new LogoCommand(Syntax.TanCmd, 1, RedefinabilityType.NonRedefinable, MathTan, Strings.CommandTanHelpText, Strings.CommandTanExampleText),
                new LogoCommand(Syntax.MakeCmd, 2, RedefinabilityType.NonRedefinable, MakeVariable, Strings.CommandMakeHelpText, Strings.CommandMakeExampleText),
                new LogoCommand(Syntax.ClearnameCmd, 1, RedefinabilityType.NonRedefinable, ClearVariable, Strings.CommandClearnameHelpText, Strings.CommandClearnameExampleText),
                new LogoCommand(Syntax.ClearnamesCmd, 0, RedefinabilityType.NonRedefinable, ClearGlobalVariables, Strings.CommandClearnamesHelpText),
                new LogoCommand(Syntax.HelpCmd, 1, RedefinabilityType.DefineAlongside, OutputHelpText, Strings.CommandHelpHelpText, Strings.CommandHelpExampleText),
                new LogoCommand(Syntax.PiCmd, 0, RedefinabilityType.NonRedefinable, ReturnPi, Strings.CommandPiHelpText),
                new LogoCommand(Syntax.ButfirstCmd, 1, RedefinabilityType.NonRedefinable, ListButFirst, Strings.CommandButfirstHelpText, Strings.CommandButfirstExampleText),
                new LogoCommand(Syntax.ButlastCmd, 1, RedefinabilityType.NonRedefinable, ListButLast, Strings.CommandButlastHelpText, Strings.CommandButlastExampleText),
                new LogoCommand(Syntax.AsciiCmd, 1, RedefinabilityType.NonRedefinable, AsciiValue, Strings.CommandAsciiHelpText, Strings.CommandAsciiExampleText),
                new LogoCommand(Syntax.CharCmd, 1, RedefinabilityType.NonRedefinable, AsciiToChar, Strings.CommandCharHelpText, Strings.CommandCharExampleText),
                new LogoCommand(Syntax.CountCmd, 1, RedefinabilityType.NonRedefinable, Count, Strings.CommandCountHelpText, Strings.CommandCountExampleText),
                new LogoCommand(Syntax.DifferenceCmd, 2, RedefinabilityType.NonRedefinable, Difference, Strings.CommandDifferenceHelpText, Strings.CommandDifferenceExampleText),
                new LogoCommand(Syntax.FputCmd, 2, RedefinabilityType.NonRedefinable, ListPrepend, Strings.CommandFputHelpText, Strings.CommandFputExampleText),
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
            return new ValueToken(Syntax.CharCmd, new LogoValue(LogoValueType.Text, Encoding.ASCII.GetString(new[] { Convert.ToByte(input[0].Value, CultureInfo.CurrentCulture) })));
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
                return new ValueToken(Syntax.AsciiCmd, new LogoValue(LogoValueType.Number, 0));
            }
            return new ValueToken(Syntax.AsciiCmd, new LogoValue(LogoValueType.Number, Encoding.ASCII.GetBytes((string)input[0].Value)[0]));
        }

        /// <summary>
        /// Removes the first element from a list.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain a single token of list type.</param>
        /// <returns>A token containing a list.</returns>
        public static Token ListButFirst(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (!(input[0].Value is ListToken inputList))
            {
                context.Interpretor.WriteOutputLine(Strings.CommandButfirstWrongTypeError);
                return null;
            }
            ListToken outList = new ListToken(inputList.Contents.Skip(1).ToArray());
            return new ValueToken(outList.Text, new LogoValue(LogoValueType.List, outList));
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
            return new ValueToken(outList.Text, new LogoValue(LogoValueType.List, outList));
        }

        /// <summary>
        /// Prepends an element to the start of a list.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain two tokens, the second of which is a list.</param>
        /// <returns>A token that is a list whose first element is the first input token and whose remaining elements are those of the second input token.</returns>
        public static Token ListPrepend(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (!(input[1].Value is ListToken inputList))
            {
                context.Interpretor.WriteOutputLine(Strings.CommandFputWrongTypeError);
                return null;
            }
            ListToken outList = new ListToken(inputList.Contents.Prepend(new ValueToken(Syntax.FputCmd, input[0])).ToArray());
            return new ValueToken(outList.Text, new LogoValue(LogoValueType.List, outList));
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
            return new ValueToken(Syntax.CosCmd, new LogoValue(LogoValueType.Number, Convert.ToDecimal(Math.Cos(Convert.ToDouble((decimal)input[0].Value)))));
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
            return new ValueToken(Syntax.ArctanCmd, new LogoValue(LogoValueType.Number, Convert.ToDecimal(Math.Atan(Convert.ToDouble((decimal)input[0].Value)))));
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
            return new ValueToken(Syntax.SinCmd, new LogoValue(LogoValueType.Number, Convert.ToDecimal(Math.Sin(Convert.ToDouble((decimal)input[0].Value)))));
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
            return new ValueToken(Syntax.TanCmd, new LogoValue(LogoValueType.Number, Convert.ToDecimal(Math.Tan(Convert.ToDouble((decimal)input[0].Value)))));
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
            return new ValueToken(Syntax.AndCmd, new LogoValue(LogoValueType.Bool,
                (inputList.Contents.Count > 0) && inputList.Contents.All(t => t is ValueToken tl && tl.Value.Type == LogoValueType.Bool && (bool)tl.Value.Value)));
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
            return new ValueToken(Syntax.AbsCmd, new LogoValue(LogoValueType.Number, Math.Abs((decimal)input[0].Value)));
        }

        /// <summary>
        /// Returns π.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="dummy">Not used.</param>
        /// <returns>A token containing the number π.</returns>
        public static Token ReturnPi(InterpretorContext context, params LogoValue[] dummy)
        {
            return new ValueToken(Math.PI.ToString(CultureInfo.InvariantCulture), new LogoValue(LogoValueType.Number, (decimal)Math.PI));
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
            return new ValueToken(Syntax.SpaceCmd, val);
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
                    context.Interpretor.WriteOutputLine(string.Join(" ", copiedList.Contents.Select(t => (t as ValueToken).Value.Value.ToString())));
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
                return new ValueToken(Syntax.CountCmd, new LogoValue(LogoValueType.Number, (decimal)(parameters[0].Value as string).Length));
            }
            else if (parameters[0].Type == LogoValueType.List)
            {
                return new ValueToken(Syntax.CountCmd, new LogoValue(LogoValueType.Number, (decimal)(parameters[0].Value as ListToken).Contents.Count));
            }
            return new ValueToken(Syntax.CountCmd, new LogoValue(LogoValueType.Number, 1m));
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
            return new ValueToken(Syntax.DifferenceCmd, new LogoValue(LogoValueType.Number, ((decimal)parameters[0].Value) - ((decimal)parameters[1].Value)));
        }
    }
}
