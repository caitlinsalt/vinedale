using System;

namespace Logo.Tokens
{
    /// <summary>
    /// An <see cref="Exception" /> subclass representing exceptions occurring during tokenisation.
    /// </summary>
    public class TokeniserException : Exception
    {
        /// <summary>
        /// Constructor which sets the <see cref="Exception.Message" /> and <see cref="Exception.InnerException" /> properties.
        /// </summary>
        /// <param name="message">The error message to be used to populate the <see cref="Exception.Message" /> property.</param>
        /// <param name="innerException">The <see cref="Exception" /> to be used to populate the <see cref="Exception.InnerException" /> property.</param>
        public TokeniserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor which sets the <see cref="Exception.Message" /> property.
        /// </summary>
        /// <param name="message">The error message to be used to populate the <see cref="Exception.Message" /> property.</param>
        public TokeniserException(string message) : base(message)
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TokeniserException() : base()
        {
        }
    }
}
