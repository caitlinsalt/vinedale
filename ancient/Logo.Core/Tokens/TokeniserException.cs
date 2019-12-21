using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logo.Core.Tokens
{
    /// <summary>
    /// An <c>Exception</c> subclass representing exceptions occurring during tokenisation.
    /// </summary>
    public class TokeniserException : Exception
    {
        /// <summary>
        /// The sole constructor for this class.
        /// </summary>
        /// <param name="message">The error message to be used to populate the <c>Message</c> property.</param>
        public TokeniserException(string message) : base(message)
        {
        }
    }
}
