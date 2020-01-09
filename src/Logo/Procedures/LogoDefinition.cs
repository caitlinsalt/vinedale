using Logo.Interpretation;
using Logo.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logo.Procedures
{
    /// <summary>
    /// A subclass of <c>LogoProcedure</c> which represents a procedure defined within Logo itself (using the <c>to PROCEDURE</c> command).
    /// </summary>
    public class LogoDefinition : LogoProcedure
    {
        /// <summary>
        /// The list of parameter names of this procedure.
        /// </summary>
        public List<string> Parameters { get; private set; }

        /// <summary>
        /// The procedure definition, pre-interpreted into a list of tokens to be executed at runtime.
        /// </summary>
        public ListToken TokenisedDefinition { get; private set; }

        /// <summary>
        /// The raw text of the procedure definition, so that it can be edited and reloaded later if required.
        /// </summary>
        public string RawDefinition { get; private set; }

        /// <summary>
        /// The method which executes this procedure's code.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="parameters">The parameters to the procedure.</param>
        /// <returns>A token containing the procedure output, or <c>null</c>.</returns>
        public Token Execute(InterpretorContext context, LogoValue[] parameters)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            context.StackFrameCreate(parameters.Select((p, i) => new LiteralToken(Parameters[i], p)).ToArray());
            ListToken runList = new ListToken(TokenisedDefinition.Contents);
            InterpretationResultType result = context.Interpretor.EvaluateListContents(runList, true);
            if (result != InterpretationResultType.SuccessComplete || runList.Contents.Count == 0)
            {
                return null;
            }
            return runList.Contents.Last();
        }

        /// <summary>
        /// The constructor for the <c>LogoDefinition</c> class builds a procedure definition from a list of tokens.
        /// </summary>
        /// <remarks>
        /// If the definition starts with one or more variables, these are used as the formal parameters of the procedure.  If the next token is a comment, that comment is used as the
        /// interactive help text for the procedure.  The remaining tokens are used as the executable code of the procedure.
        /// </remarks>
        /// <param name="rawCode">The original input text, saved so that it can be edited later.</param>
        /// <param name="tokens">The tokenised code of the procedure.</param>
        public LogoDefinition(string rawCode, List<Token> tokens)
        {
            if (tokens is null)
            {
                tokens = new List<Token>();
            }

            Aliases = Array.Empty<string>();
            RawDefinition = rawCode;
            Name = tokens[1].Text;
            Redefinability = RedefinabilityType.Replace;
            Parameters = new List<string>();

            int paramIdx = 2;
            while (tokens[paramIdx] is VariableToken vt)
            {
                ParameterCount++;
                Parameters.Add(vt.VariableName);
                paramIdx++;
            }

            ExampleText = string.Join(" ", Parameters);
            if (tokens[paramIdx] is CommentToken ct)
            {
                HelpText = ct.Text.TrimStart();
            }

            TokenisedDefinition = new ListToken(tokens.Skip(paramIdx).ToArray());
        }
    }
}
