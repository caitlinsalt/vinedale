using Logo.Core.Interpretation;
using Logo.Core.Procedures;
using Logo.Core.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;

namespace Logo.Core.UnitTests.Procedures
{
    [TestClass]
    public class CoreCommandsUnitTests
    {
        [TestMethod]
        public void PrintCommandForUndefinedVariableDoesNotCrash()
        {
            Mock<TextWriter> mockStandardOutput = new Mock<TextWriter>();
            Mock<TextWriter> mockDebugOutput = new Mock<TextWriter>();
            Interpretor interpretor = new Interpretor(mockStandardOutput.Object, mockDebugOutput.Object, DebugMessageLevel.Logorrheic);
            InterpretorContext context = new InterpretorContext(interpretor);
            CoreCommands commands = new CoreCommands();

            LogoToken token = new LogoWord { Evaluated = true, Literal = ":nonexistant", TokenValue = context.GetVariable("nonexistant") };
            commands.Print(context, token);
        }
    }
}
