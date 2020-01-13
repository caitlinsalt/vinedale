using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tests.Utility.Providers;
using Vinedale.Turtle.Drawing;
using Vinedale.Turtle.Tests.Unit.TestHelpers;

namespace Vinedale.Turtle.Tests.Unit.Drawing
{
    [TestClass]
    public class TurtleStatusInstructionUnitTests
    {
        private static readonly Random _rnd = RandomProvider.Default;

#pragma warning disable CA1707 // Identifiers should not contain underscores
        [TestMethod]
        public void TurtleStatusInstructionClass_Constructor_SetsStatusPropertyToValueOfParameter()
        {
            TurtleStatus testParam0 = _rnd.NextTurtleStatus();

            TurtleStatusInstruction testOutput = new TurtleStatusInstruction(testParam0);

            Assert.AreEqual(testParam0, testOutput.Status);
        }
#pragma warning restore CA1707 // Identifiers should not contain underscores
    }
}
