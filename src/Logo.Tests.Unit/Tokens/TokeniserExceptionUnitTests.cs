using Logo.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tests.Utility.Extensions;
using Tests.Utility.Providers;

namespace Logo.Tests.Unit.Tokens
{
    [TestClass]
    public class TokeniserExceptionUnitTests
    {
        private static readonly Random _rnd = RandomProvider.Default;

#pragma warning disable CA1707 // Identifiers should not contain underscores

        [TestMethod]
        public void TokeniserExceptionClass_ConstructorWithNoParameters_SetsMessagePropertyToDefaultText()
        {
            TokeniserException testOutput = new TokeniserException();

            Assert.AreEqual("Exception of type 'Logo.Tokens.TokeniserException' was thrown.", testOutput.Message);
        }

        [TestMethod]
        public void TokeniserExceptionClass_ConstructorWithNoParameters_SetsInnerExceptionPropertyToNull()
        {
            TokeniserException testOutput = new TokeniserException();

            Assert.IsNull(testOutput.InnerException);
        }

        [TestMethod]
        public void TokeniserExceptionClass_ConstructorWithStringParameter_SetsMessagePropertyToParameter()
        {
            string testParam = _rnd.NextString(_rnd.Next(512));

            TokeniserException testOutput = new TokeniserException(testParam);

            Assert.AreEqual(testParam, testOutput.Message);
        }

        [TestMethod]
        public void TokeniserExceptionClass_ConstructorWithStringParameter_SetsInnerExceptionPropertyToNull()
        {
            string testParam = _rnd.NextString(_rnd.Next(512));

            TokeniserException testOutput = new TokeniserException(testParam);

            Assert.IsNull(testOutput.InnerException);
        }

        [TestMethod]
        public void TokeniserExceptionClass_ConstructorWithStringAndExceptionParameters_SetsMessagePropertyToFirstParameter()
        {
            string testParam0 = _rnd.NextString(_rnd.Next(512));
            Exception testParam1 = new Exception();

            TokeniserException testOutput = new TokeniserException(testParam0, testParam1);

            Assert.AreEqual(testParam0, testOutput.Message);
        }

        [TestMethod]
        public void TokeniserExceptionClass_ConstructorWithStringAndExceptionParameters_SetsInnerExceptionPropertyToSecondParameter()
        {
            string testParam0 = _rnd.NextString(_rnd.Next(512));
            Exception testParam1 = new Exception();

            TokeniserException testOutput = new TokeniserException(testParam0, testParam1);

            Assert.AreSame(testParam1, testOutput.InnerException);
        }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
}
