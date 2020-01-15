using Logo.Tests.Unit.TestHelpers;
using Logo.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.Utility.Extensions;
using Tests.Utility.Providers;

namespace Logo.Tests.Unit.Tokens
{
    [TestClass]
    public class TokeniserResultUnitTests
    {
        private static readonly Random _rnd = RandomProvider.Default;

        private IEnumerable<Token> GetTokens()
        {
            int count = _rnd.Next(10);
            for (int i = 0; i < count; ++i)
            {
                yield return _rnd.NextToken();
            }
        }

#pragma warning disable CA1707 // Identifiers should not contain underscores
        [TestMethod]
        public void TokeniserResultClass_Constructor_SetsResultTypePropertyToValueOfFirstParameter()
        {
            TokeniserResultType testParam0 = _rnd.NextTokeniserResultType();
            IEnumerable<Token> testParam1 = GetTokens();
            string testParam2 = _rnd.NextString(_rnd.Next(50));
            string testParam3 = _rnd.NextString(_rnd.Next(50));

            TokeniserResult testOutput = new TokeniserResult(testParam0, testParam1, testParam2, testParam3);

            Assert.AreEqual(testParam0, testOutput.ResultType);
        }

        [TestMethod]
        public void TokeniserResultClass_Constructor_SetsTokenisedDataPropertyToValueOfSecondParameter_IfSecondParameterIsNull()
        {
            TokeniserResultType testParam0 = _rnd.NextTokeniserResultType();
            IEnumerable<Token> testParam1 = null;
            string testParam2 = _rnd.NextString(_rnd.Next(50));
            string testParam3 = _rnd.NextString(_rnd.Next(50));

            TokeniserResult testOutput = new TokeniserResult(testParam0, testParam1, testParam2, testParam3);

            Assert.IsNull(testOutput.TokenisedData);
        }

        [TestMethod]
        public void TokeniserResultClass_Constructor_SetsTokenisedDataPropertyToObjectWithSameLengthAsSecondParameter_IfSecondParameterIsNotNull()
        {
            TokeniserResultType testParam0 = _rnd.NextTokeniserResultType();
            IList<Token> testParam1 = GetTokens().ToArray();
            string testParam2 = _rnd.NextString(_rnd.Next(50));
            string testParam3 = _rnd.NextString(_rnd.Next(50));

            TokeniserResult testOutput = new TokeniserResult(testParam0, testParam1, testParam2, testParam3);

            Assert.AreEqual(testParam1.Count, testOutput.TokenisedData.Count);
        }

        [TestMethod]
        public void TokeniserResultClass_Constructor_SetsTokenisedDataPropertyToObjectWithSameContentsAsSecondParameter_IfSecondParameterIsNotNull()
        {
            TokeniserResultType testParam0 = _rnd.NextTokeniserResultType();
            IList<Token> testParam1 = GetTokens().ToArray();
            string testParam2 = _rnd.NextString(_rnd.Next(50));
            string testParam3 = _rnd.NextString(_rnd.Next(50));

            TokeniserResult testOutput = new TokeniserResult(testParam0, testParam1, testParam2, testParam3);

            for (int i = 0; i < testParam1.Count; ++i)
            {
                Assert.AreSame(testParam1[i], testOutput.TokenisedData[i]);
            }
        }

        [TestMethod]
        public void TokeniserResultClass_Constructor_SetsNonConsumedInputPropertyToNull_IfThirdParameterIsNotSupplied()
        {
            TokeniserResultType testParam0 = _rnd.NextTokeniserResultType();
            IList<Token> testParam1 = GetTokens().ToArray();

            TokeniserResult testOutput = new TokeniserResult(testParam0, testParam1);

            Assert.IsNull(testOutput.NonConsumedInput);
        }

        [TestMethod]
        public void TokeniserResultClass_Constructor_SetsNonConsumedInputPropertyToNull_IfThirdParameterIsNull()
        {
            TokeniserResultType testParam0 = _rnd.NextTokeniserResultType();
            IList<Token> testParam1 = GetTokens().ToArray();
            string testParam2 = null;
            string testParam3 = _rnd.NextString(_rnd.Next(50));

            TokeniserResult testOutput = new TokeniserResult(testParam0, testParam1, testParam2, testParam3);

            Assert.IsNull(testOutput.NonConsumedInput);
        }

        [TestMethod]
        public void TokeniserResultClass_Constructor_SetsNonConsumedInputPropertyToValueOfThirdParameter_IfThirdParameterIsNotNull()
        {
            TokeniserResultType testParam0 = _rnd.NextTokeniserResultType();
            IList<Token> testParam1 = GetTokens().ToArray();
            string testParam2 = _rnd.NextString(_rnd.Next(50));
            string testParam3 = _rnd.NextString(_rnd.Next(50));

            TokeniserResult testOutput = new TokeniserResult(testParam0, testParam1, testParam2, testParam3);

            Assert.AreEqual(testParam2, testOutput.NonConsumedInput);
        }

        [TestMethod]
        public void TokeniserResultClass_Constructor_SetsErrorMessagePropertyToNull_IfFourthParameterIsNotSupplied()
        {
            TokeniserResultType testParam0 = _rnd.NextTokeniserResultType();
            IList<Token> testParam1 = GetTokens().ToArray();
            string testParam2 = _rnd.NextString(_rnd.Next(50));

            TokeniserResult testOutput = new TokeniserResult(testParam0, testParam1, testParam2);

            Assert.IsNull(testOutput.ErrorMessage);
        }

        [TestMethod]
        public void TokeniserResultClass_Constructor_SetsErrorMessagePropertyToNull_IfFourthParameterIsNull()
        {
            TokeniserResultType testParam0 = _rnd.NextTokeniserResultType();
            IList<Token> testParam1 = GetTokens().ToArray();
            string testParam2 = _rnd.NextString(_rnd.Next(50));
            string testParam3 = null;

            TokeniserResult testOutput = new TokeniserResult(testParam0, testParam1, testParam2, testParam3);

            Assert.IsNull(testOutput.ErrorMessage);
        }

        [TestMethod]
        public void TokeniserResultClass_Constructor_SetsErrorMessagePropertyToValueOfThirdParameter_IfFourthParameterIsNotNull()
        {
            TokeniserResultType testParam0 = _rnd.NextTokeniserResultType();
            IList<Token> testParam1 = GetTokens().ToArray();
            string testParam2 = _rnd.NextString(_rnd.Next(50)); ;
            string testParam3 = _rnd.NextString(_rnd.Next(50));

            TokeniserResult testOutput = new TokeniserResult(testParam0, testParam1, testParam2, testParam3);

            Assert.AreEqual(testParam3, testOutput.ErrorMessage);
        }
#pragma warning restore CA1707 // Identifiers should not contain underscores
    } 
}
