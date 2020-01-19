using Logo.Tests.Unit.TestHelpers;
using Logo.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tests.Utility.Extensions;
using Tests.Utility.Providers;

namespace Logo.Tests.Unit.Tokens
{
    [TestClass]
    public class ValueTokenUnitTests
    {
        private static readonly Random _rnd = RandomProvider.Default;

#pragma warning disable CA1707 // Identifiers should not contain underscores

        [TestMethod]
        public void ValueTokenClass_Constructor_SetsTextPropertyToValueOfFirstParameter()
        {
            string testParam0 = _rnd.NextString(_rnd.Next(50));
            LogoValue testParam1 = _rnd.NextLogoValue();

            ValueToken testOutput = new ValueToken(testParam0, testParam1);

            Assert.AreEqual(testParam0, testOutput.Text);
        }

        [TestMethod]
        public void ValueTokenClass_Constructor_SetsValuePropertyToValueOfSecondParameter()
        {
            string testParam0 = _rnd.NextString(_rnd.Next(50));
            LogoValue testParam1 = _rnd.NextLogoValue();

            ValueToken testOutput = new ValueToken(testParam0, testParam1);

            Assert.AreEqual(testParam1, testOutput.Value);
        }
#pragma warning restore CA1707 // Identifiers should not contain underscores
    }
}
