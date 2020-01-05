using System;

namespace Logo.Tokens
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
