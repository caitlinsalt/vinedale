using Logo.Interpretation;
using Logo.Resources;

namespace Logo.Tokens
{
    /// <summary>
    /// A token representating an operator within an expression.
    /// </summary>
    public class OperatorToken : Token
    {
        /// <summary>
        /// The kind of operation which this token represents.
        /// </summary>
        public OperatorType Operation { get; set; }

        /// <summary>
        /// Constructs a token from an input string.
        /// </summary>
        /// <remarks>The input to this constructor should be a single-character string consisting of an operator symbol.</remarks>
        /// <param name="sym">The symbol to build a token from.</param>
        public OperatorToken(string sym) : base(sym)
        {
            switch (sym)
            {
                case "+":
                    Operation = OperatorType.Add;
                    break;
                case "-":
                    Operation = OperatorType.Subtract;
                    break;
                case "*":
                    Operation = OperatorType.Multiply;
                    break;
                case "/":
                    Operation = OperatorType.Divide;
                    break;
                case "=":
                    Operation = OperatorType.Equals;
                    break;
            }
        }

        /// <summary>
        /// Carry out the computation represented by this operator.  The <c>TokenValue</c> property of this token is set to the result of the computation.
        /// </summary>
        /// <param name="args">The operator's arguments.</param>
        /// <returns>An <c>OperatorEvaluationResult</c> indicating success or failure and containing any error messages.</returns>
        public virtual OperatorEvaluationResult Perform(params LogoValue[] args)
        {
            switch (Operation)
            {
                case OperatorType.Add:
                    return PerformAddition(args);
                case OperatorType.Subtract:
                    return PerformSubtraction(args);
                case OperatorType.Multiply:
                    return PerformMultiplication(args);
                case OperatorType.Divide:
                    return PerformDivision(args);
                default:
                    return new OperatorEvaluationResult(InterpretationResultType.Failure, new LogoValue(), Strings.OperatorUnknownOperationError);
            }
        }

        private OperatorEvaluationResult PerformAddition(LogoValue[] args)
        {
            LogoValue tokenValue = new LogoValue();
            if (args[0].Type == args[1].Type && args[0].Type == LogoValueType.Number)
            {
                tokenValue = new LogoValue(LogoValueType.Number, (decimal)args[0].Value + (decimal)args[1].Value);
            }
            else if (args[0].Type == args[1].Type && args[0].Type == LogoValueType.Text)
            {
                tokenValue = new LogoValue(LogoValueType.Text, (string)args[0].Value + (string)args[1].Value);
            }
            else if (args[0].Type == LogoValueType.Text && args[1].Type == LogoValueType.Number)
            {
                tokenValue = new LogoValue(LogoValueType.Text, (string)args[0].Value + (decimal)args[1].Value);
            }
            else if (args[0].Type == LogoValueType.Number && args[1].Type == LogoValueType.Text)
            {
                tokenValue = new LogoValue(LogoValueType.Text, (string)args[1].Value + (decimal)args[0].Value);
            }
            else
            {
                return new OperatorEvaluationResult(InterpretationResultType.Failure, tokenValue, Strings.OperatorAdditionTypeError);
            }
            return new OperatorEvaluationResult(InterpretationResultType.SuccessComplete, tokenValue);
        }


        private OperatorEvaluationResult PerformSubtraction(LogoValue[] args)
        {
            LogoValue tokenValue = new LogoValue();
            if (args[0].Type == args[1].Type && args[0].Type == LogoValueType.Number)
            {
                tokenValue = new LogoValue(LogoValueType.Number, (decimal)args[0].Value - (decimal)args[1].Value);
            }
            else
            {
                return new OperatorEvaluationResult(InterpretationResultType.Failure, tokenValue, Strings.OperatorSubtractionTypeError);
            }
            return new OperatorEvaluationResult(InterpretationResultType.SuccessComplete, tokenValue);
        }


        private OperatorEvaluationResult PerformMultiplication(LogoValue[] args)
        {
            LogoValue tokenValue = new LogoValue();
            if (args[0].Type == args[1].Type && args[0].Type == LogoValueType.Number)
            {
                tokenValue = new LogoValue(LogoValueType.Number, (decimal)args[0].Value * (decimal)args[1].Value);
            }
            else
            {
                return new OperatorEvaluationResult(InterpretationResultType.Failure, tokenValue, Strings.OperatorMultiplicationTypeError);
            }
            return new OperatorEvaluationResult(InterpretationResultType.SuccessComplete, tokenValue);
        }


        private OperatorEvaluationResult PerformDivision(LogoValue[] args)
        {
            LogoValue tokenValue = new LogoValue();
            if (args[0].Type == args[1].Type && args[0].Type == LogoValueType.Number)
            {
                tokenValue = new LogoValue(LogoValueType.Number, (decimal)args[0].Value / (decimal)args[1].Value);
            }
            else
            {
                return new OperatorEvaluationResult(InterpretationResultType.Failure, tokenValue, Strings.OperatorDivisionTypeError);
            }
            return new OperatorEvaluationResult(InterpretationResultType.SuccessComplete, tokenValue);
        }
    }
}
