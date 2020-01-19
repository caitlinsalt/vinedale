using Logo.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logo.Tests.Unit.Tokens
{
    [TestClass]
    public class TokeniserUnitTests
    {
#pragma warning disable CA1707 // Identifiers should not contain underscores

        // To test issue #57
        [TestMethod]
        public void TokeniserClass_TokeniseStringMethod_ReturnsExpectedOutput_IfTheInputIsASimpleExpression()
        {
            string testParam0 = "(2 + 2)";

            TokeniserResult testOutput = Tokeniser.TokeniseString(testParam0);

            Assert.IsNotNull(testOutput);
            Assert.AreEqual(TokeniserResultType.SuccessComplete, testOutput.ResultType);
            Assert.IsNotNull(testOutput.TokenisedData);
            Assert.AreEqual(1, testOutput.TokenisedData.Count);
            Assert.IsTrue(testOutput.TokenisedData[0] is ExpressionToken);
            ExpressionToken outputToken = testOutput.TokenisedData[0] as ExpressionToken;
            Assert.AreEqual("(2 + 2)", outputToken.Text);
            Assert.IsNotNull(outputToken.Contents);
            Assert.AreEqual(3, outputToken.Contents.Count);
            Assert.IsTrue(outputToken.Contents[0] is ValueToken);
            ValueToken outputValue = outputToken.Contents[0] as ValueToken;
            Assert.AreEqual(2m, outputValue.Value.Value);
            Assert.IsTrue(outputToken.Contents[1] is OperatorToken);
            OperatorToken outputOperator = outputToken.Contents[1] as OperatorToken;
            Assert.AreEqual(OperatorType.Add, outputOperator.Operation);
            Assert.IsTrue(outputToken.Contents[2] is ValueToken);
            outputValue = outputToken.Contents[2] as ValueToken;
            Assert.AreEqual(2m, outputValue.Value.Value);
        }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
}
