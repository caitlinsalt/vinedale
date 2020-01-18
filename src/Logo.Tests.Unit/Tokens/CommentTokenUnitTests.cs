using Logo.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tests.Utility.Extensions;
using Tests.Utility.Providers;

namespace Logo.Tests.Unit.Tokens
{
    [TestClass]
    public class CommentTokenUnitTests
    {
        private static readonly Random _rnd = RandomProvider.Default;

#pragma warning disable CA1707 // Identifiers should not contain underscores
        [TestMethod]
        public void CommentTokenClass_Constructor_SetsTextPropertyToParameter()
        {
            string testParam0 = _rnd.NextString(_rnd.Next(1, 50));

            CommentToken testOutput = new CommentToken(testParam0);

            Assert.AreEqual(testParam0, testOutput.Text);
        }
#pragma warning restore CA1707 // Identifiers should not contain underscores
    }
}
