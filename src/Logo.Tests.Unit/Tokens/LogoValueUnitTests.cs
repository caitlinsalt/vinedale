using Logo.Tests.Unit.TestHelpers;
using Logo.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tests.Utility.Providers;

namespace Logo.Tests.Unit.Tokens
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
    [TestClass]
    public class LogoValueUnitTests
    {
        private static readonly Random _rnd = RandomProvider.Default;

        [TestMethod]
        public void LogoValueStruct_Constructor_SetsLogoValueTypeProperty()
        {
            LogoValueType testParam0 = _rnd.NextLogoValueType();
            object testParam1 = null;

            LogoValue testOutput = new LogoValue(testParam0, testParam1);

            Assert.AreEqual(testParam0, testOutput.Type);
        }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
}
