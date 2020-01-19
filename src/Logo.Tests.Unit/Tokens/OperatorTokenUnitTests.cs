using Logo.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tests.Utility.Providers;

namespace Logo.Tests.Unit.Tokens
{
    [TestClass]
    public class OperatorTokenUnitTests
    {
        private static readonly Random _rnd = RandomProvider.Default;

        private string GetSymbol()
        {
            const string symbols = "+-*/";
            int idx = _rnd.Next(symbols.Length);
            return symbols.Substring(idx, 1);
        }

#pragma warning disable CA1707 // Identifiers should not contain underscores

        [TestMethod]
        public void OperatorTokenClass_Constructor_SetsTextPropertyToValueOfParameter_IfParameterIsValidOperatorSymbol()
        {
            string testParam0 = GetSymbol();

            OperatorToken testOutput = new OperatorToken(testParam0);

            Assert.AreEqual(testParam0, testOutput.Text);
        }

#pragma warning restore CA1707 // Identifiers should not contain underscores
    }
}
