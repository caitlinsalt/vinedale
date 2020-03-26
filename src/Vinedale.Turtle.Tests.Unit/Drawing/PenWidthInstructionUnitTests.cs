using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tests.Utility.Providers;
using Vinedale.Turtle.Drawing;

namespace Vinedale.Turtle.Tests.Unit.Drawing
{
    [TestClass]
    public class PenWidthInstructionUnitTests
    {
        private static readonly Random _rnd = RandomProvider.Default;

#pragma warning disable CA1707 // Identifiers should not contain underscores

        [TestMethod]
        public void PenWidthInstructionClass_Constructor_SetsWidthProperty()
        {
            double testParam0 = _rnd.NextDouble() * 10;

            PenWidthInstruction testOutput = new PenWidthInstruction(testParam0);

            Assert.AreEqual(testParam0, testOutput.Width);
        }

#pragma warning restore CA1707 // Identifiers should not contain underscores

    }
}
