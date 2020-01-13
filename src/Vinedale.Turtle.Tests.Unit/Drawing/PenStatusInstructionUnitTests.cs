using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tests.Utility.Providers;
using Vinedale.Turtle.Drawing;
using Vinedale.Turtle.Tests.Unit.TestHelpers;

namespace Vinedale.Turtle.Tests.Unit.Drawing
{
    [TestClass]
    public class PenStatusInstructionUnitTests
    {
        private static readonly Random _rnd = RandomProvider.Default;

#pragma warning disable CA1707 // Identifiers should not contain underscores
        [TestMethod]
        public void PenStatusClass_Constructor_SetsStatusPropertyToValueOfParameter()
        {
            PenStatus testParam0 = _rnd.NextPenStatus();

            PenStatusInstruction testOutput = new PenStatusInstruction(testParam0);

            Assert.AreEqual(testParam0, testOutput.Status);
        }
#pragma warning restore CA1707 // Identifiers should not contain underscores
    }
}
