using Logo.Tests.Unit.TestHelpers;
using Logo.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Tests.Utility.Extensions;
using Tests.Utility.Providers;

namespace Logo.Tests.Unit.Tokens
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
    [TestClass]
    public class LogoValueUnitTests
    {
        private static readonly Random _rnd = RandomProvider.Default;

        [TestMethod]
        public void LogoValueStruct_Constructor_SetsTypePropertyToFirstParameterOfConstructor()
        {
            LogoValueType testParam0 = _rnd.NextLogoValueType();
            object testParam1 = null;

            LogoValue testOutput = new LogoValue(testParam0, testParam1);

            Assert.AreEqual(testParam0, testOutput.Type);
        }

        [TestMethod]
        public void LogoValueStruct_Constructor_SetsValuePropertyToSecondParameterOfConstructor_IfFirstParameterIsUnknownAndSecondParameterIsNull()
        {
            LogoValue testOutput = new LogoValue(LogoValueType.Unknown, null);

            Assert.IsNull(testOutput.Value);
        }

        [TestMethod]
        public void LogoValueStruct_Constructor_SetsValuePropertyToSecondParameterOfConstructor_IfFirstParameterIsBoolAndSecondParameterIsFalse()
        {
            LogoValue testOutput = new LogoValue(LogoValueType.Bool, false);

            Assert.AreEqual(false, testOutput.Value);
        }

        [TestMethod]
        public void LogoValueStruct_Constructor_SetsValuePropertyToSecondParameterOfConstructor_IfFirstParameterIsBoolAndSecondParameterIsTrue()
        {
            LogoValue testOutput = new LogoValue(LogoValueType.Bool, true);

            Assert.AreEqual(true, testOutput.Value);
        }

        [TestMethod]
        public void LogoValueStruct_Constructor_SetsValuePropertyToSecondParameterOfConstructor_IfFirstParameterIsTextAndSecondParameterIsAString()
        {
            string testParam1 = _rnd.NextString(_rnd.Next(512));

            LogoValue testOutput = new LogoValue(LogoValueType.Text, testParam1);

            Assert.AreEqual(testParam1, testOutput.Value);
        }

        [TestMethod]
        public void LogoValueStruct_Constructor_SetsValuePropertyToSecondParameterOfConstructor_IfFirstParameterIsNumberAndSecondParameterIsAnInt()
        {
            int testParam1 = _rnd.Next();

            LogoValue testOutput = new LogoValue(LogoValueType.Number, testParam1);

            Assert.AreEqual(testParam1, testOutput.Value);
        }

        [TestMethod]
        public void LogoValueStruct_Constructor_SetsValuePropertyToSecondParameterOfConstructor_IfFirstParameterIsNumberAndSecondParameterIsADouble()
        {
            double testParam1 = _rnd.NextDouble();

            LogoValue testOutput = new LogoValue(LogoValueType.Number, testParam1);

            Assert.AreEqual(testParam1, testOutput.Value);
        }

        [TestMethod]
        public void LogoValueStruct_Constructor_SetsValuePropertyToSecondParameterOfConstructor_IfFirstParameterIsNumberAndSecondParameterIsADecimal()
        {
            decimal testParam1 = (decimal)_rnd.NextDouble();

            LogoValue testOutput = new LogoValue(LogoValueType.Number, testParam1);

            Assert.AreEqual(testParam1, testOutput.Value);
        }

        [TestMethod]
        public void LogoValueStruct_EqualsMethodWithObjectParameter_ReturnsTrueIfParameterHasSameProperties()
        {
            LogoValue testValue = _rnd.NextLogoValue();
            object testParam = new LogoValue(testValue.Type, testValue.Value);

            bool testOutput = testValue.Equals(testParam);

            Assert.IsTrue(testOutput);
        }

        [TestMethod]
        public void LogoValueStruct_EqualsMethodWithObjectParameter_ReturnsFalseIfParameterHasDifferentProperties()
        {
            LogoValue testValue = _rnd.NextLogoValue();
            object testParam;
            LogoValue tp;
            do
            {
                tp = _rnd.NextLogoValue();
            } while (tp.Type == testValue.Type && tp.Value == testValue.Value);
            testParam = tp;

            bool testOutput = testValue.Equals(testParam);

            Assert.IsFalse(testOutput);
        }

        [TestMethod]
        public void LogoValueStruct_EqualsMethodWithObjectParameter_ReturnsFalseIfParameterIsOfDifferentType()
        {
            LogoValue testValue = _rnd.NextLogoValue();
            object testParam = new List<int>();

            bool testOutput = testValue.Equals(testParam);

            Assert.IsFalse(testOutput);
        }

        [TestMethod]
        public void LogoValueStruct_EqualsMethodWithObjectParameter_ReturnsFalseIfParameterIsNull()
        {
            LogoValue testValue = _rnd.NextLogoValue();
            object testParam = null;

            bool testOutput = testValue.Equals(testParam);

            Assert.IsFalse(testOutput);
        }

        [TestMethod]
        public void LogoValueStruct_EqualsMethodWithLogoValueParameter_ReturnsTrueIfParameterHasSameProperties()
        {
            LogoValue testValue = _rnd.NextLogoValue();
            LogoValue testParam = new LogoValue(testValue.Type, testValue.Value);

            bool testOutput = testValue.Equals(testParam);

            Assert.IsTrue(testOutput);
        }

        [TestMethod]
        public void LogoValueStruct_EqualsMethodWithLogoValueParameter_ReturnsFalseIfParameterHasDifferentProperties()
        {
            LogoValue testValue = _rnd.NextLogoValue();
            LogoValue testParam;
            do
            {
                testParam = _rnd.NextLogoValue();
            } while (testParam.Type == testValue.Type && testParam.Value == testValue.Value);

            bool testOutput = testValue.Equals(testParam);

            Assert.IsFalse(testOutput);
        }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
}
