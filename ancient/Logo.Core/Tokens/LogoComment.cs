using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logo.Core.Tokens
{
    /// <summary>
    /// A token which contains only comments.
    /// </summary>
    public class LogoComment : LogoWord
    {
        /// <summary>
        /// Create a copy of this token.
        /// </summary>
        /// <returns>A token equal to this one.</returns>
        public override LogoToken Clone()
        {
            return new LogoComment { Evaluated = Evaluated, Literal = Literal, TokenValue = TokenValue };
        }
    }
}
