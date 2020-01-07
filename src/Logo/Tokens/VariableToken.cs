using Logo.Interpretation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logo.Tokens
{
    public class VariableToken : Token
    {
        public string VariableName { get; private set; }

        public VariableToken(string text, string name) : base(text)
        {
            VariableName = name;
        }

        public override LogoValue Evaluate(InterpretorContext context)
        {
            throw new NotImplementedException();
        }
    }
}
