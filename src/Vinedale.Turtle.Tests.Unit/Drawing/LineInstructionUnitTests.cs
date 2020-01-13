using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tests.Utility.Providers;
using Vinedale.Turtle.Drawing;

namespace Vinedale.Turtle.Tests.Unit.Drawing
{
    [TestClass]
    public class LineInstructionUnitTests
    {
        private static readonly Random _rnd = RandomProvider.Default;

#pragma warning disable CA1707 // Identifiers should not contain underscores
        [TestMethod]
        public void LineInstructionClass_Constructor_SetsLengthPropertyToValueOfParameter()
        {
            double testParam0 = _rnd.NextDouble() * 1000;

            LineInstruction testOutput = new LineInstruction(testParam0);

            Assert.AreEqual(testParam0, testOutput.Length);
        }
#pragma warning restore CA1707 // Identifiers should not contain underscores
    }
}
