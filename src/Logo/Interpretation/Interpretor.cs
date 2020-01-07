using Logo.Procedures;
using Logo.Resources;
using Logo.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Logo.Interpretation
{
    /// <summary>
    /// The core of the language interpretation engine.
    /// </summary>
    public class Interpretor
    {
        /// <summary>
        /// The system context for this interpretor, including the symbol table and object stack.
        /// </summary>
        public InterpretorContext Context { get; private set; }

        /// <summary>
        /// Output writer for debugging information.
        /// </summary>
        public TextWriter DebugOutputWriter { get; set; }

        /// <summary>
        /// Output writer for normal output.
        /// </summary>
        public TextWriter StandardOutputWriter { get; set; }

        /// <summary>
        /// Level of information to output to the <c>DebugOutputWriter</c> writer.
        /// </summary>
        public DebugMessageLevel DebugVerbosity { get; set; }

        private List<Token> _tokBuffer;
        private string _inputBuffer;
        private string _definitionBuffer;
        private List<Token> _defTokBuffer;

        private bool _insideDefinition;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>The <c>DebugVerbosity</c> property is set to <c>DebugMessageLevel.Terse</c> by default.</remarks>
        public Interpretor()
        {
            _tokBuffer = new List<Token>();
            _defTokBuffer = new List<Token>();
            _inputBuffer = String.Empty;
            Context = new InterpretorContext(this);
            DebugVerbosity = DebugMessageLevel.Terse;
        }

        /// <summary>
        /// Constructor which sets initial values of public properties.
        /// </summary>
        /// <param name="standardOutputDestination">Initial value of the <c>StandardOutputWriter</c> property.</param>
        /// <param name="debugMessageDestination">Initial value of the <c>DebugOutputWriter</c> property.</param>
        /// <param name="debugVerbosity">Initial value of the <c>DebugVerbosity</c> property.</param>
        public Interpretor(TextWriter standardOutputDestination, TextWriter debugMessageDestination, DebugMessageLevel debugVerbosity) : this()
        {
            StandardOutputWriter = standardOutputDestination;
            DebugOutputWriter = debugMessageDestination;
            DebugVerbosity = debugVerbosity;
            LoadModule(new CoreCommands());
        }

        /// <summary>
        /// This method is called to initialise an interactive instance of the class, as distinct to other instances.
        /// </summary>
        /// <remarks>At present the primary purpose of this method is to print out a welcome message, as there is no other difference between interactive and other instances.
        /// In future however it may be used for interactive interpretor specific initialisation.</remarks>
        public void StartInteractiveInterpretor()
        {
            if (DebugVerbosity >= DebugMessageLevel.Terse)
            {
                StandardOutputWriter.Write(Strings.InterpretorWelcomeMessage);
            }
        }

        /// <summary>
        /// Imports the commands defined in an <c>ICommandModule</c> into the interpretor context's symbol table.
        /// </summary>
        /// <param name="module"></param>
        public void LoadModule(ICommandModule module)
        {
            if (DebugVerbosity >= DebugMessageLevel.Logorrheic)
            {
                DebugOutputWriter.WriteLine(string.Format(Strings.InterpretorModuleLoadingDebugMessage, module.GetType(), module.GetType().Assembly.GetName().Name));
            }

            foreach (LogoProcedure p in module.RegisterProcedures())
            {
                Context.RegisterProcedure(p);
                if (DebugVerbosity >= DebugMessageLevel.Logorrheic)
                {
                    DebugOutputWriter.WriteLine(string.Format(Strings.InterpretorRegisteredProcedureDebugMessage, p.Name));
                }
            }

            Context.LoadedModules.Add(module);

            if (DebugVerbosity >= DebugMessageLevel.Verbose)
            {
                StandardOutputWriter.WriteLine(string.Format(Strings.InterpretorLoadedModuleDebugMessage, module.GetType().Name));
            }
        }

        /// <summary>
        /// Interprets a line of input.
        /// </summary>
        /// <param name="input">The line of code to be interpreted.</param>
        /// <returns>A value indicating whether the line was interpreted fully and successfully, was interpreted successfullly but requires more input, or was not interpreted.</returns>
        public InterpretationResult Interpret(string input)
        {
            DateTime watermark = DateTime.Now;
            TokeniserResult tokeniserResult = Tokeniser.TokeniseString(_inputBuffer + input);

            if (DebugVerbosity >= DebugMessageLevel.Verbose)
            {
                DebugOutputWriter.WriteLine(string.Format(Strings.InterpretorInterpretTokeniserResultDebugMessage, tokeniserResult.ResultType));
                DebugOutputWriter.WriteLine(string.Format(Strings.InterpretorInterpretTokenCountDebugMessage, tokeniserResult.TokenisedData.Count));
            }

            if (DebugVerbosity >= DebugMessageLevel.Logorrheic)
            {
                foreach (Token tok in tokeniserResult.TokenisedData)
                {
                    DumpToken(string.Empty, DebugOutputWriter, tok);
                }
            }

            _tokBuffer.AddRange(tokeniserResult.TokenisedData);

            if (_tokBuffer.Any() && _tokBuffer[0].Text == "to")
            {
                _insideDefinition = true;
            }

            _inputBuffer = tokeniserResult.NonConsumedInput;
            if (tokeniserResult.ResultType == TokeniserResultType.SuccessIncomplete)
            {
                return InterpretationResult.SuccessIncomplete;
            }
            if (tokeniserResult.ResultType == TokeniserResultType.Failure)
            {
                StandardOutputWriter.WriteLine(tokeniserResult.ErrorMessage);
                return InterpretationResult.Failure;
            }

            if (_insideDefinition)
            {
                _definitionBuffer += _inputBuffer + input;
                _definitionBuffer = _definitionBuffer.Substring(0, _definitionBuffer.Length - tokeniserResult.NonConsumedInput.Length);
                while (_tokBuffer.Any())
                {
                    _defTokBuffer.Add(_tokBuffer[0]);
                    if (_tokBuffer[0].Text == "end")
                    {
                        _insideDefinition = false;
                        if (_defTokBuffer[1].Text == "to")
                        {
                            WriteOutputLine(Strings.InterpretorInterpretProcedureIsNonRedefinableError);
                        }
                        else if (_defTokBuffer.Count > 2)
                        {
                            DefineProcedure();
                        }
                        _defTokBuffer.Clear();
                        _definitionBuffer = string.Empty;
                        _tokBuffer.RemoveAt(0);
                        break;
                    }
                    _tokBuffer.RemoveAt(0);
                }
            }

            if (_insideDefinition)
            {
                return InterpretationResult.SuccessIncomplete;
            }

            InterpretationResult executionResult = ExecuteTokenBuffer();
            if (executionResult != InterpretationResult.SuccessIncomplete)
            {
                _tokBuffer.Clear();
                if (DebugVerbosity >= DebugMessageLevel.Chatty)
                {
                    TimeSpan processingTime = DateTime.Now - watermark;
                    WriteDebugOutputLine(processingTime.TotalSeconds.ToString());
                }
            }

            return executionResult;
        }

        private void DefineProcedure()
        {
            string procName = _defTokBuffer[1].Text;
            if (Context.ProcedureNames.ContainsKey(procName) && Context.ProcedureNames[procName].Any(p => p.Redefinability == RedefinabilityType.NonRedefinable))
            {
                WriteOutputLine(string.Format(Strings.InterpretorDefineProcedureProcedureIsNonRedefinableError, procName));
                return;
            }
            Context.RegisterProcedure(new LogoDefinition(_definitionBuffer, _defTokBuffer));
        }

        private InterpretationResult ExecuteTokenBuffer() 
        { 
            return ExecuteTokens(_tokBuffer, false);
        }

        private InterpretationResult ExecuteTokens(List<Token> tokens, bool literalEvaluateUndefinedWords)
        {
            if (!tokens.Any(t => !(t is LiteralToken)))
            {
                return InterpretationResult.SuccessComplete;
            }

            int firstTokenIdx = tokens.FindIndex(t => !(t is LiteralToken));

            if (tokens[firstTokenIdx] is CommentToken)
            {
                tokens.RemoveAt(firstTokenIdx);
                return ExecuteTokenBuffer();
            }

            if (tokens[firstTokenIdx] is ListToken)
            {
                StandardOutputWriter.WriteLine(string.Format(Strings.InterpretorExecuteTokenBufferCannotExecuteBareListError, tokens[firstTokenIdx].Text));
                return InterpretationResult.Failure;
            }

            if (tokens[firstTokenIdx] is ExpressionToken)
            {
                StandardOutputWriter.WriteLine(string.Format(Strings.InterpretorExecuteTokenBufferCannotExecuteBareExpressionError, tokens[firstTokenIdx].Text));
                return InterpretationResult.Failure;
            }

            InterpretationResult execResult = EvaluateWord(tokens, firstTokenIdx, literalEvaluateUndefinedWords);
            return execResult == InterpretationResult.SuccessComplete ? ExecuteTokens(tokens, literalEvaluateUndefinedWords) : execResult;
        }

        /// <summary>
        /// Evaluates the indicated token in a list of input tokens.
        /// </summary>
        /// <param name="tokens">The list of input tokens.  This will be modified in-place.</param>
        /// <param name="index">The index of the token to be evaluated</param>
        /// <param name="literalEvaluateUndefinedWords">If <c>true</c>, words which are currently undefined will be processed as if they were string literals.</param>
        /// <returns>A value indicating success, failure, or that more input is required.</returns>
        public InterpretationResult EvaluateToken(IList<Token> tokens, int index, bool literalEvaluateUndefinedWords)
        {
            if (!tokens.Any())
            {
                return InterpretationResult.SuccessComplete;
            }

            if (tokens[index] is ProcedureToken)
            {
                return EvaluateWord(tokens, index, literalEvaluateUndefinedWords);
            }
            if (tokens[index] is ListToken)
            {
                tokens[index] = new LiteralToken(tokens[index].Text, new LogoValue(LogoValueType.List, tokens[index]));
                return InterpretationResult.SuccessComplete;
            }
            if (tokens[index] is OperatorToken)
            {
                return InterpretationResult.SuccessComplete;
            }
            if (tokens[index] is ExpressionToken)
            {
                return EvaluateExpression((ExpressionToken)tokens[index], literalEvaluateUndefinedWords);
            }
            if (tokens[index] is VariableToken vt)
            {
                tokens[index] = new LiteralToken(vt.Text, Context.GetVariable(vt.VariableName));
                return InterpretationResult.SuccessComplete;
            }

            return InterpretationResult.Failure;
        }

        /// <summary>
        /// Evaluates the indicated word in a list of input tokens.
        /// </summary>
        /// <param name="tokens">The list of input tokens.  This will be modified in-place.</param>
        /// <param name="index">The index of the token to be evaluated</param>
        /// <param name="literalEvaluateUndefinedWords">If <c>true</c>, words which are currently undefined will be processed as if they were string literals.</param>
        /// <returns>A value indicating success, failure, or that more input is required.</returns>
        public InterpretationResult EvaluateWord(IList<Token> tokens, int index, bool literalEvaluateUndefinedWords)
        {
            if (tokens[index].Text == "true")
            {
                tokens[index] = new LiteralToken("true", new LogoValue(LogoValueType.Bool, true));
                return InterpretationResult.SuccessComplete;
            }
            if (tokens[index].Text == "false")
            {
                tokens[index] = new LiteralToken("false", new LogoValue(LogoValueType.Bool, false));
                return InterpretationResult.SuccessComplete;
            }

            if (literalEvaluateUndefinedWords && !Context.ProcedureNames.ContainsKey(tokens[index].Text))
            {
                tokens[index] = new LiteralToken(tokens[index].Text, new LogoValue(LogoValueType.Text, tokens[index].Text));
                return InterpretationResult.SuccessComplete;
            }

            if (!Context.ProcedureNames.ContainsKey(tokens[index].Text))
            {
                StandardOutputWriter.WriteLine(string.Format(Strings.InterpretorExecuteWordUndefinedProcedureError, tokens[index].Text));
                return InterpretationResult.Failure;
            }

            int parmCount = Context.ProcedureNames[tokens[index].Text].First().ParameterCount;

            for (int i = 0; i < parmCount; ++i)
            {
                if (tokens.Count <= index + i + 1)
                {
                    return InterpretationResult.SuccessIncomplete;
                }
                do
                {
                    InterpretationResult result = EvaluateToken(tokens, index + i + 1, true);
                    if (result == InterpretationResult.SuccessIncomplete)
                    {
                        return InterpretationResult.SuccessIncomplete;
                    }
                    if (tokens.Count <= index + i + 1)
                    {
                        return InterpretationResult.SuccessIncomplete;
                    }
                } while (!(tokens[index + i + 1] is LiteralToken));
            }

            LogoValue[] parmBuffer = new LogoValue[parmCount];
            for (int i = 0; i < parmCount; ++i)
            {
                parmBuffer[i] = (tokens[index + 1] as LiteralToken).Value;
                tokens.RemoveAt(index + 1);
            }

            List<Token> returnTokens = new List<Token>();
            foreach (LogoProcedure cmdToExec in Context.ProcedureNames[tokens[index].Text])
            {
                if (cmdToExec.GetType() == typeof(LogoCommand))
                {
                    LogoCommand cmd = (LogoCommand)cmdToExec;
                    Context.StackFrameCreate();
                    returnTokens.Add(cmd.Implementation(Context, parmBuffer));
                }
                else
                {
                    LogoDefinition def = (LogoDefinition)cmdToExec;
                    returnTokens.Add(def.Execute(Context, parmBuffer));
                }
                Context.StackFrameDestroy();
            }
            if (returnTokens.All(t => t == null))
            {
                tokens.RemoveAt(index);
            }
            else
            {
                Token returnToken = returnTokens.Last(t => t != null);
                tokens[index] = returnToken;
            }

            return InterpretationResult.SuccessComplete;
        }

        /// <summary>
        /// Evaluates an entire list.
        /// </summary>
        /// <param name="list">The list to be evaluated.</param>
        /// <param name="literalEvaluateUndefinedWords">If <c>true</c>, words which are currently undefined will be processed as if they were string literals.</param>
        /// <returns>A value indicating success or failure.</returns>
        public InterpretationResult EvaluateListContents(ListToken list, bool literalEvaluateUndefinedWords)
        {
            return ExecuteTokens(list.Contents, literalEvaluateUndefinedWords);
        }

        /// <summary>
        /// Evaluates an expression.
        /// </summary>
        /// <param name="expr">The expression to be evaluated.</param>
        /// <param name="literalEvaluateUndefinedWords">If <c>true</c>, words which are currently undefined will be processed as if they were string literals.</param>
        /// <returns>A value indicating success or failure.</returns>
        public InterpretationResult EvaluateExpression(ExpressionToken expr, bool literalEvaluateUndefinedWords)
        {
            throw new NotImplementedException();
            //DebugOutputWriter.WriteLine(string.Format(Strings.InterpretorEvaluateExpressionDebugMessage, expr.Text));
            //InterpretationResult result = EvaluateExpressionContents(expr, literalEvaluateUndefinedWords);
            //if (result != InterpretationResult.SuccessComplete)
            //{
            //    return result;
            //}

            //foreach (OperatorType type in Enum.GetValues(typeof(OperatorType)))
            //{
            //    while (expr.Contents.Select(t => t as OperatorToken).Any(t => t != null && t.Operation == type))
            //    {
            //        result = EvaluateOperator(expr, expr.Contents.FindIndex(t => t is OperatorToken && ((OperatorToken)t).Operation == type));
            //        if (result != InterpretationResult.SuccessComplete)
            //        {
            //            return result;
            //        }
            //    }
            //}

            //if (expr.Contents.Count != 1)
            //{
            //    StandardOutputWriter.WriteLine(string.Format(Strings.InterpretorEvaluateExpressionGeneralError, expr.Text));
            //    return InterpretationResult.Failure;
            //}

            //expr.TokenValue = expr.Contents[0].TokenValue;
            //expr.Evaluated = true;
            //return InterpretationResult.SuccessComplete;
        }

        private InterpretationResult EvaluateOperator(ExpressionToken expr, int idx)
        {
            throw new NotImplementedException();
            //if (idx == 0)
            //{
            //    StandardOutputWriter.WriteLine(string.Format(Strings.InterpretorEvaluateExpressionStartsWithOperatorError, expr.Contents[idx].Text, expr.Text));
            //    return InterpretationResult.Failure;
            //}

            //if (idx == expr.Contents.Count - 1)
            //{
            //    StandardOutputWriter.WriteLine(string.Format(Strings.InterpretorEvaluateExpressionEndsWithOperatorError, expr.Contents[idx].Text, expr.Text));
            //    return InterpretationResult.Failure;
            //}

            //OperatorToken op = (OperatorToken)expr.Contents[idx];
            //Token firstArg = expr.Contents[idx - 1];
            //Token secondArg = expr.Contents[idx + 1];
            //if (firstArg.TokenValue.Type == LogoValueType.Unknown)
            //{
            //    firstArg.TokenValue = LogoValue.GetDefaultValue(secondArg.TokenValue.Type);
            //}
            //if (secondArg.TokenValue.Type == LogoValueType.Unknown)
            //{
            //    secondArg.TokenValue = LogoValue.GetDefaultValue(firstArg.TokenValue.Type);
            //}

            //OperatorEvaluationResult opResult = op.Perform(firstArg, secondArg);
            //if (opResult.Result == InterpretationResult.Failure)
            //{
            //    StandardOutputWriter.Write(opResult.Message);
            //    return InterpretationResult.Failure;
            //}

            //op.Evaluated = true;
            //expr.Contents.RemoveAt(idx + 1);
            //expr.Contents.RemoveAt(idx - 1);
            //return InterpretationResult.SuccessComplete;
        }

        private InterpretationResult EvaluateExpressionContents(ExpressionToken expr, bool literalEvaluateUndefinedWords)
        {
            throw new NotImplementedException();
            //if (expr.Contents.Count == 0 || expr.Contents.Where(t => !(t is OperatorToken)).All(t => t.Evaluated))
            //{
            //    return InterpretationResult.SuccessComplete;
            //}

            //int firstNonEvaldWord = expr.Contents.FindIndex(t => !(t is OperatorToken) && !t.Evaluated);
            //InterpretationResult result = EvaluateToken(expr.Contents, firstNonEvaldWord, literalEvaluateUndefinedWords);

            //if (result == InterpretationResult.SuccessIncomplete)
            //{
            //    StandardOutputWriter.WriteLine(string.Format(Strings.InterpretorEvaluateExpressionContentsIncompleteContentsError, expr.Text));
            //    return InterpretationResult.Failure;
            //}
            //if (result == InterpretationResult.Failure)
            //{
            //    return InterpretationResult.Failure;
            //}

            //return EvaluateExpressionContents(expr, literalEvaluateUndefinedWords);
        }

        /// <summary>
        /// Write a string to the normal output writer.
        /// </summary>
        /// <param name="output">A string to be written to the output.</param>
        public void WriteOutput(string output)
        {
            StandardOutputWriter.Write(output);
        }

        /// <summary>
        /// Write a string to the normal output writer, terminated with a new line.
        /// </summary>
        /// <param name="output">A string to be written to the output.</param>
        public void WriteOutputLine(string output)
        {
            StandardOutputWriter.WriteLine(output);
        }

        /// <summary>
        /// Write a string to the debug output writer, followed by a new line.
        /// </summary>
        /// <param name="output">A string to be written to the output.</param>
        public void WriteDebugOutputLine(string output)
        {
            DebugOutputWriter.WriteLine(output);
        }

        private void DumpToken(string prefix, TextWriter writer, Token tok)
        {
            writer.WriteLine(string.Format(Strings.InterpretorDumpTokenOutput, prefix, tok.GetType(), tok.Text));
            if (tok.GetType().IsSubclassOf(typeof(ContainerToken)))
            {
                foreach (Token innerTok in ((ContainerToken)tok).Contents)
                {
                    DumpToken(prefix + " ", writer, innerTok);
                }
            }
        }
    }
}
