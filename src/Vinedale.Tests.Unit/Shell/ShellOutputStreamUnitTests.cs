using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Vinedale.Shell;

namespace Vinedale.Tests.Unit.Shell
{
    [TestClass]
    public class ShellOutputStreamUnitTests
    {

#pragma warning disable CA1707 // Identifiers should not contain underscores

        [TestMethod]
        public void ShellOutputStreamClass_FlushMethod_CallsRefreshMethodOfConstructorParameter()
        {
            Mock<IShellControl> testParamMock = new Mock<IShellControl>();
            using ShellOutputStream testObject = new ShellOutputStream(testParamMock.Object);

            testObject.Flush();

            testParamMock.Verify(m => m.Refresh(), Times.Once());

        }

#pragma warning restore CA1707 // Identifiers should not contain underscores

    }
}
