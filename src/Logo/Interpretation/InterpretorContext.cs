using Logo.Procedures;
using Logo.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace Logo.Interpretation
{
    /// <summary>
    /// Defines the execution environment of the interpretor.
    /// </summary>
    /// <remarks>The interpretor context includes the interpretor's current stack and symbol spaces.</remarks>
    public class InterpretorContext
    {
        /// <summary>
        /// The interpretor running in this context.
        /// </summary>
        public Interpretor Interpretor { get; private set; }

        /// <summary>
        /// The collection of <c>ICommandModule</c> classes which have been loaded into the environment.
        /// </summary>
        public IList<ICommandModule> LoadedModules { get; private set; }

        /// <summary>
        /// The collection of all <c>LogoProcedure</c> definitions availanble in the environment, including both those with .NET definitions and those defined within Logo.
        /// </summary>
        public IList<LogoProcedure> Procedures { get; private set; }

        /// <summary>
        /// The <c>IDictionary</c> of all <c>LogoProcedure</c> names and aliases registered in the environment, for quick access on execution.
        /// </summary>
        public IDictionary<string, IList<LogoProcedure>> ProcedureNames { get; private set; }

        private Dictionary<string, LogoValue> Globals { get; set; }

        private Stack<Dictionary<string, LogoValue>> Locals { get; set; }

        /// <summary>
        /// <c>InterpretorContext</c> has a single constructor which requires an interpretor.
        /// </summary>
        /// <param name="interp">The interpretor which this is to be the environment for.</param>
        public InterpretorContext(Interpretor interp)
        {
            Interpretor = interp;
            LoadedModules = new List<ICommandModule>();
            Procedures = new List<LogoProcedure>();
            ProcedureNames = new Dictionary<string, IList<LogoProcedure>>();
            Globals = new Dictionary<string, LogoValue>();
            Locals = new Stack<Dictionary<string, LogoValue>>();
            Locals.Push(null);
        }

        /// <summary>
        /// Add a procedure to the environment.
        /// </summary>
        /// <param name="p">The procedure definition to be added.</param>
        public void RegisterProcedure(LogoProcedure p)
        {
            Procedures.Add(p);
            if (!ProcedureNames.ContainsKey(p.Name))
            {
                ProcedureNames.Add(p.Name, new List<LogoProcedure>());
            }
            if (ProcedureNames[p.Name].Any(x => x.Redefinability == RedefinabilityType.NonRedefinable))
            {
                return;
            }
            ProcedureNames[p.Name].Add(p);
            foreach (string alias in p.Aliases)
            {
                if (!ProcedureNames.ContainsKey(alias))
                {
                    ProcedureNames.Add(alias, new List<LogoProcedure>());
                }
                ProcedureNames[alias].Add(p);
            }
        }

        /// <summary>
        /// Gets the value of a variable from the appropriate namespace.
        /// </summary>
        /// <remarks>
        /// If the variable name exists in the current local namespace, that variable is returned.  If not, but the name exists in the global namespace, that variable is returned.
        /// If no variable called <c>varName</c> exists, a new variable with the given name and of unknown type is created in the global namespace.
        /// </remarks>
        /// <param name="varName">The variable name.</param>
        /// <returns>The <c>LogoValue</c> object of the variable.</returns>
        public LogoValue GetVariable(string varName)
        {
            if (Locals.Peek() != null && Locals.Peek().ContainsKey(varName))
            {
                return Locals.Peek()[varName];
            }
            if (!Globals.ContainsKey(varName))
            {
                Globals.Add(varName, new LogoValue(LogoValueType.Unknown, null));
            }
            return Globals[varName];
        }

        /// <summary>
        /// Sets the value of a variable.
        /// </summary>
        /// <remarks>
        /// If the variable name exists in the current local namespace, that variable has its value set.  If not, the variable is set in the global namespace.
        /// </remarks>
        /// <param name="varName">The name of the variable to set.</param>
        /// <param name="value">The value to be set.</param>
        public void SetVariable(string varName, LogoValue value)
        {
            if (Locals.Peek() != null && Locals.Peek().ContainsKey(varName))
            {
                Locals.Peek()[varName] = value;
            }
            else if (Globals.ContainsKey(varName))
            {
                Globals[varName] = value;
            }
            else
            {
                Globals.Add(varName, value);
            }
        }

        /// <summary>
        /// Removes a variable from the global namespace.
        /// </summary>
        /// <param name="varName">The variable to remove.</param>
        public void ClearVariable(string varName)
        {
            if (Globals.ContainsKey(varName))
            {
                Globals.Remove(varName);
            }
        }


        /// <summary>
        /// Clears the global variable namespace.
        /// </summary>
        public void ClearAllVariables()
        {
            Globals.Clear();
        }


        /// <summary>
        /// Create a new variable namespace on the local namespace stack, and set it as the current local namespace.
        /// </summary>
        public void StackFrameCreate()
        {
            Locals.Push(null);
        }


        /// <summary>
        /// Create a new variable namespace on the local namespace stack, set it as the current local namespace, and set the supplied list of evaluated tokens as variables in the namespace.
        /// </summary>
        /// <param name="paramList">An array of tokens to set as variables in the new local namespace.</param>
        public void StackFrameCreate(LiteralToken[] paramList)
        {
            Locals.Push(paramList.ToDictionary(p => p.Text, p => p.Value));
        }


        /// <summary>
        /// Delete the current local namespace and set the next namespace on the stack as the current one.
        /// </summary>
        public void StackFrameDestroy()
        {
            Locals.Pop();
        }
    }
}
