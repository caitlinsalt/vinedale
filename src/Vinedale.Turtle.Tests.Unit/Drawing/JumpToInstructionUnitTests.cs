using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tests.Utility.Providers;
using Vinedale.Turtle.Drawing;

namespace Vinedale.Turtle.Tests.Unit.Drawing
{
    [TestClass]
    public class JumpToInstructionUnitTests
    {
        private static readonly Random _rnd = RandomProvider.Default;

#pragma warning disable CA1707 // Identifiers should not contain underscores
        [TestMethod]
        public void JumpToInstructionClass_Constructor_SetsXPropertyToValueOfFirstParameter()
        {
            double testParam0 = _rnd.NextDouble() * 1000;
            double testParam1 = _rnd.NextDouble() * 1000;

            JumpToInstruction testOutput = new JumpToInstruction(testParam0, testParam1);

            Assert.AreEqual(testParam0, testOutput.X);
        }

        [TestMethod]
        public void JumpToInstructionClass_Constructor_SetsYPropertyToValueOfSecondParameter()
        {
            double testParam0 = _rnd.NextDouble() * 1000;
            double testParam1 = _rnd.NextDouble() * 1000;

            JumpToInstruction testOutput = new JumpToInstruction(testParam0, testParam1);

            Assert.AreEqual(testParam1, testOutput.Y);
        }
#pragma warning restore CA1707 // Identifiers should not contain underscores
    }
}
