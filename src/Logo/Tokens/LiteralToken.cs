using Logo.Interpretation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logo.Tokens
{
    public class LiteralToken : Token
    {
        public LogoValue Value { get; private set; }

        public LiteralToken(string text, LogoValue value) : base(text)
        {
            Value = value;
        }

        /// <summary>
        /// Evaluate this token.  Returns the <see cref="Value" /> property.
        /// </summary>
        /// <returns>The value of this token.</returns>
        public override LogoValue Evaluate(InterpretorContext context)
        {
            return Value;
        }
    }
}
