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
    }
}
