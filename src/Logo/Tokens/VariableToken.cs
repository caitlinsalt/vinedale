namespace Logo.Tokens
{
    public class VariableToken : Token
    {
        public string VariableName { get; private set; }

        public VariableToken(string text, string name) : base(text)
        {
            VariableName = name;
        }
    }
}
