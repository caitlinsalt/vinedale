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
    /// A delegate type to represent the .NET implementation of a Logo procedure.
    /// </summary>
    /// <param name="interpretationContext">The interpretor context which represents the environment the procedure is being executed in, so that it can access variables and other 
    /// environment features.</param>
    /// <param name="parameters">The parameters to the procedure.  The interpretor guarantees the number of elements in the array will match the specified number of parameters in the 
    /// procedure definition.</param>
    /// <returns>A <c>LogoToken</c> if the command returns a value, or <c>null</c> otherwise.</returns>
    public delegate LogoToken CommandImplementation(InterpretorContext interpretationContext, params LogoToken[] parameters);
}
