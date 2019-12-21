using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logo.Core.Interpretation;
using Logo.Core.Tokens;

namespace Logo.Core.Procedures
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
        public LogoList TokenisedDefinition { get; private set; }

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
        public LogoToken Execute(InterpretorContext context, LogoToken[] parameters)
        {
            context.StackFrameCreate(parameters.Select((p, i) => new LogoToken { Evaluated = true, Literal = Parameters[i], TokenValue = p.TokenValue }).ToArray());
            LogoList runList = (LogoList) TokenisedDefinition.Clone();
            InterpretationResult result = context.Interpretor.EvaluateListContents(runList, true);
            if (result != InterpretationResult.SuccessComplete || runList.InnerContents.Count == 0)
            {
                return null;
            }
            return new LogoToken { Evaluated = true, Literal = RawDefinition, TokenValue = runList.InnerContents.Last().TokenValue };
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
        public LogoDefinition(string rawCode, List<LogoToken> tokens)
        {
            Aliases = new string[0];
            RawDefinition = rawCode;
            Name = tokens[1].Literal;
            Redefinability = RedefinabilityType.Replace;
            Parameters = new List<string>();

            int paramIdx = 2;
            while (tokens[paramIdx].Literal.StartsWith(":"))
            {
                ParameterCount++;
                Parameters.Add(tokens[paramIdx].Literal.Substring(1));
                paramIdx++;
            }

            ExampleText = string.Join(" ", Parameters);
            if (tokens[paramIdx].GetType() == typeof(LogoComment))
            {
                HelpText = tokens[paramIdx].Literal.Substring(1).TrimStart();
            }

            TokenisedDefinition = new LogoList { Evaluated = false, Literal = RawDefinition };
            for (int i = paramIdx; i < tokens.Count - 1; ++i)
            {
                TokenisedDefinition.InnerContents.Add(tokens[i]);
            }
        }
    }
}
