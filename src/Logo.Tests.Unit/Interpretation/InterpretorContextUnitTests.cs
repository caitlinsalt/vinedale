using Logo.Interfaces;
using Logo.Interpretation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Logo.Tests.Unit.Interpretation
{
    [TestClass]
    public class InterpretorContextUnitTests
    {

#pragma warning disable CA1707 // Identifiers should not contain underscores

        [TestMethod]
        public void IntepretorContextClass_Constructor_SetsInterpretorPropertyToParameter()
        {
            Mock<IInterpretor> testParamMock = new Mock<IInterpretor>();

            InterpretorContext testOutput = new InterpretorContext(testParamMock.Object);

            Assert.AreSame(testParamMock.Object, testOutput.Interpretor);
        }

        [TestMethod]
        public void InterpretorContextClass_Constructor_SetsRandomGeneratorPropertyToNonNullValue()
        {
            Mock<IInterpretor> testParamMock = new Mock<IInterpretor>();

            InterpretorContext testOutput = new InterpretorContext(testParamMock.Object);

            Assert.IsNotNull(testOutput.RandomGenerator);
        }

        [TestMethod]
        public void InterpretorContextClass_Constructor_SetsLoadedModulesPropertyToEmptyIList()
        {
            Mock<IInterpretor> testParamMock = new Mock<IInterpretor>();

            InterpretorContext testOutput = new InterpretorContext(testParamMock.Object);

            Assert.IsNotNull(testOutput.LoadedModules);
            Assert.AreEqual(0, testOutput.LoadedModules.Count);
        }

        [TestMethod]
        public void InterpretorContextClass_Constructor_SetsProceduresPropertyToEmptyIList()
        {
            Mock<IInterpretor> testParamMock = new Mock<IInterpretor>();

            InterpretorContext testOutput = new InterpretorContext(testParamMock.Object);

            Assert.IsNotNull(testOutput.Procedures);
            Assert.AreEqual(0, testOutput.Procedures.Count);
        }

        [TestMethod]
        public void InterpretorContextClass_Constructor_SetsProcedureNamesPropertyToEmptyIDictionary()
        {
            Mock<IInterpretor> testParamMock = new Mock<IInterpretor>();

            InterpretorContext testOutput = new InterpretorContext(testParamMock.Object);

            Assert.IsNotNull(testOutput.ProcedureNames);
            Assert.AreEqual(0, testOutput.ProcedureNames.Count);
        }

#pragma warning restore CA1707 // Identifiers should not contain underscores

    }
}
