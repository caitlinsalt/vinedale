using Logo.Interpretation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logo.Tokens
{
    public class ProcedureToken : Token
    {
        public ProcedureToken(string text) : base(text)
        {

        }

        public override LogoValue Evaluate(InterpretorContext context)
        {
            throw new NotImplementedException();
        }
    }
}
