using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tests.Utility.Providers;
using Vinedale.Turtle.Drawing;

namespace Vinedale.Turtle.Tests.Unit.Drawing
{
    [TestClass]
    public class RotateInstructionUnitTests
    {
        private static readonly Random _rnd = RandomProvider.Default;

#pragma warning disable CA1707 // Identifiers should not contain underscores
        [TestMethod]
        public void RotateInstruction_Constructor_SetsAnglePropertyToValueOfParameter()
        {
            double testParam0 = _rnd.NextDouble() * 1000;

            RotateInstruction testOutput = new RotateInstruction(testParam0);

            Assert.AreEqual(testParam0, testOutput.Angle);
        }
#pragma warning restore CA1707 // Identifiers should not contain underscores
    }
}
