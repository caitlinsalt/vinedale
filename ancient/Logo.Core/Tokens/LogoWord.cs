using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logo.Core.Tokens
{
    /// <summary>
    /// A token representing a "word".
    /// </summary>
    public class LogoWord : LogoToken
    {
        /// <summary>
        /// Create a copy of this token.
        /// </summary>
        /// <returns>A token equal to this one.</returns>
        public override LogoToken Clone()
        {
            return new LogoWord { Evaluated = Evaluated, Literal = Literal, TokenValue = TokenValue };
        }
    }
}
