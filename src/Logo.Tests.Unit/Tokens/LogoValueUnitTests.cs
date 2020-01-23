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
        public void LogoValueStruct_Constructor_SetsValuePropertyToSecondParameterOfConstructor_IfFirstParameterIsWordAndSecondParameterIsAProcedureToken()
        {
            ProcedureToken testParam1 = _rnd.NextProcedureToken();

            LogoValue testOutput = new LogoValue(LogoValueType.Word, testParam1);

            Assert.AreSame(testParam1, testOutput.Value);
        }

        [TestMethod]
        public void LogoValueStruct_Constructor_SetsValuePropertyToSecondValueOfConstructor_IfFirstParameterIsListAndSecondParameterIsAListToken()
        {
            ListToken testParam1 = _rnd.NextListToken();

            LogoValue testOutput = new LogoValue(LogoValueType.List, testParam1);

            Assert.AreSame(testParam1, testOutput.Value);
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

        [TestMethod]
        public void LogoValueStruct_GetHashCodeMethod_ReturnsSameValueWhenCalledTwice()
        {
            LogoValue testValue = _rnd.NextLogoValue();

            int testOutput0 = testValue.GetHashCode();
            int testOutput1 = testValue.GetHashCode();

            Assert.AreEqual(testOutput0, testOutput1);
        }

        [TestMethod]
        public void LogoValueStruct_GetHashCodeMethod_ReturnsDifferentValueWhenCalledOnDifferentValues()
        {
            LogoValue testValue0 = _rnd.NextLogoValue();
            LogoValue testValue1;
            do
            {
                testValue1 = _rnd.NextLogoValue();
            } while (testValue0 == testValue1);

            int testOutput0 = testValue0.GetHashCode();
            int testOutput1 = testValue1.GetHashCode();

            Assert.AreNotEqual(testOutput0, testOutput1);
        }

        [TestMethod]
        public void LogoValueStruct_ToStringMethod_ReturnsValue_IfTypeIsText()
        {
            LogoValue testValue = _rnd.NextLogoValue(LogoValueType.Text);

            string testOutput = testValue.ToString();

            Assert.AreEqual(testValue.Value, testOutput);
        }

        [TestMethod]
        public void LogoValueStruct_ToStringMethod_ReturnsSameValueAsToStringMethodOfValueProperty_IfTypeIsNumeric()
        {
            LogoValue testValue = _rnd.NextLogoValue(LogoValueType.Number);

            string testOutput = testValue.ToString();

            Assert.AreEqual(testValue.Value.ToString(), testOutput);
        }

        [TestMethod]
        public void LogoValueStruct_ToStringMethod_ReturnsSameValueAsToStringMethodOfValuePropertyOfValueProperty_IfTypeIsWordAndValueTypeIsValueToken()
        {
            LogoValue testValue;
            do
            {
                testValue = _rnd.NextLogoValue(LogoValueType.Word);
            } while (!(testValue.Value is ValueToken));

            string testOutput = testValue.ToString();

            ValueToken vt = testValue.Value as ValueToken;
            Assert.AreEqual(vt.Value.ToString(), testOutput);
        }

        [TestMethod]
        public void LogoValueStruct_ToStringMethod_ReturnsSameValueAsTextPropertyOfValueProperty_IfTypeIsWordAndValueTypeIsNotValueToken()
        {
            LogoValue testValue;
            do
            {
                testValue = _rnd.NextLogoValue(LogoValueType.Word);
            } while (testValue.Value is ValueToken);

            string testOutput = testValue.ToString();

            Token token = testValue.Value as Token;
            Assert.AreEqual(token.Text, testOutput);
        }

#pragma warning disable CS1718 // Comparison made to same variable
        [TestMethod]
        public void LogoValueStruct_EqualityOperator_ReturnsTrueIfBothParametersAreSame()
        {
            LogoValue testValue = _rnd.NextLogoValue();

            bool testOutput = testValue == testValue;

            Assert.IsTrue(testOutput);
        }
#pragma warning restore CS1718 // Comparison made to same variable

        [TestMethod]
        public void LogoValueStruct_EqualityOperator_ReturnsTrueIfBothParametersHaveSameProperties()
        {
            LogoValue testValue0 = _rnd.NextLogoValue();
            LogoValue testValue1 = new LogoValue(testValue0.Type, testValue0.Value);

            bool testOutput = testValue0 == testValue1;

            Assert.IsTrue(testOutput);
        }

        [TestMethod]
        public void LogoValueStruct_EqualityOperator_ReturnsFalseIfParametersAreDifferentType()
        {
            LogoValue testValue0 = _rnd.NextLogoValue();
            LogoValue testValue1;
            do
            {
                testValue1 = _rnd.NextLogoValue();
            } while (testValue0.Type == testValue1.Type);

            bool testOutput = testValue0 == testValue1;

            Assert.IsFalse(testOutput);
        }

        [TestMethod]
        public void LogoValueStruct_EqualityOperator_ReturnsFalseIfParametersAreOfSameTypeButDifferentValue()
        {
            LogoValue testValue0 = _rnd.NextLogoValue();
            LogoValue testValue1;
            do
            {
                testValue1 = _rnd.NextLogoValue(testValue0.Type);
            } while (testValue0.Value == testValue1.Value);

            bool testOutput = testValue0 == testValue1;

            Assert.IsFalse(testOutput);
        }

#pragma warning disable CS1718 // Comparison made to same variable
        [TestMethod]
        public void LogoValueStruct_InequalityOperator_ReturnsFalseIfBothParametersAreSame()
        {
            LogoValue testValue = _rnd.NextLogoValue();

            bool testOutput = testValue != testValue;

            Assert.IsFalse(testOutput);
        }
#pragma warning restore CS1718 // Comparison made to same variable

        [TestMethod]
        public void LogoValueStruct_InequalityOperator_ReturnsFalseIfBothParametersHaveSameProperties()
        {
            LogoValue testValue0 = _rnd.NextLogoValue();
            LogoValue testValue1 = new LogoValue(testValue0.Type, testValue0.Value);

            bool testOutput = testValue0 != testValue1;

            Assert.IsFalse(testOutput);
        }

        [TestMethod]
        public void LogoValueStruct_InequalityOperator_ReturnsTrueIfParametersAreDifferentType()
        {
            LogoValue testValue0 = _rnd.NextLogoValue();
            LogoValue testValue1;
            do
            {
                testValue1 = _rnd.NextLogoValue();
            } while (testValue0.Type == testValue1.Type);

            bool testOutput = testValue0 != testValue1;

            Assert.IsTrue(testOutput);
        }

        [TestMethod]
        public void LogoValueStruct_InequalityOperator_ReturnsTrueIfParametersAreOfSameTypeButDifferentValue()
        {
            LogoValue testValue0 = _rnd.NextLogoValue();
            LogoValue testValue1;
            if (testValue0.Type == LogoValueType.Bool)
            {
                testValue1 = new LogoValue(LogoValueType.Bool, !((bool)testValue0.Value));
            }
            else
            {
                do
                {
                    testValue1 = _rnd.NextLogoValue(testValue0.Type);
                } while (testValue0.Value == testValue1.Value);
            }

            bool testOutput = testValue0 != testValue1;

            Assert.IsTrue(testOutput);
        }

        [TestMethod]
        public void LogoValueStruct_GetDefaultValueMethod_ReturnsValueWithTypeBoolAndValueFalse_IfParameterIsBool()
        {
            LogoValue testOutput = LogoValue.GetDefaultValue(LogoValueType.Bool);

            Assert.AreEqual(LogoValueType.Bool, testOutput.Type);
            Assert.AreEqual(false, testOutput.Value);
        }

        [TestMethod]
        public void LogoValueStruct_GetDefaultValueMethod_ReturnsValueWithTypeListAndValueEqualToEmptyList_IfParameterIsList()
        {
            LogoValue testOutput = LogoValue.GetDefaultValue(LogoValueType.List);

            ListToken listOutput = testOutput.Value as ListToken;
            Assert.AreEqual(LogoValueType.List, testOutput.Type);
            Assert.AreEqual(0, listOutput.Contents.Count);
        }

        [TestMethod]
        public void LogoValueStruct_GetDefaultValueMethod_ReturnsValueWithTypeNumberAndValueEqualToZero_IfParameterIsNumber()
        {
            LogoValue testOutput = LogoValue.GetDefaultValue(LogoValueType.Number);

            Assert.AreEqual(LogoValueType.Number, testOutput.Type);
            Assert.AreEqual(0m, testOutput.Value);
        }

        [TestMethod]
        public void LogoValueStruct_GetDefaultValueMethod_ReturnsValueWithTypeParcelAndValueEqualToNull_IfParameterIsParcel()
        {
            LogoValue testOutput = LogoValue.GetDefaultValue(LogoValueType.Parcel);

            Assert.AreEqual(LogoValueType.Parcel, testOutput.Type);
            Assert.AreEqual(null, testOutput.Value);
        }

        [TestMethod]
        public void LogoValueStruct_GetDefaultValueMethod_ReturnsValueWithTypeTextAndValueEqualToEmptyString_IfParameterIsText()
        {
            LogoValue testOutput = LogoValue.GetDefaultValue(LogoValueType.Text);

            Assert.AreEqual(LogoValueType.Text, testOutput.Type);
            Assert.AreEqual("", testOutput.Value);
        }

        [TestMethod]
        public void LogoValueStruct_GetDefaultValueMethod_ReturnsValueWithTypeUnknownAndValueEqualToNull_IfParameterIsUnknown()
        {
            LogoValue testOutput = LogoValue.GetDefaultValue(LogoValueType.Unknown);

            Assert.AreEqual(LogoValueType.Unknown, testOutput.Type);
            Assert.AreEqual(null, testOutput.Value);
        }

        [TestMethod]
        public void LogoValueStruct_GetDefaultValueMethod_ReturnsValueWithTypeWordAndValueEqualToNull_IfParameterIsWord()
        {
            LogoValue testOutput = LogoValue.GetDefaultValue(LogoValueType.Word);

            Assert.AreEqual(LogoValueType.Word, testOutput.Type);
            Assert.AreEqual(null, testOutput.Value);
        }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
}
