using Logo.Core.Procedures;
using Logo.Core.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logo.Windows.UnitTests
{
    [TestClass]
    public class SystemCommandsUnitTests
    {
        [TestMethod]
        public void RegisterProceduresReturnsIListContainingChdirCommand()
        {
            var commands = new SystemCommands();
            var procedures = commands.RegisterProcedures();

            Assert.IsNotNull(procedures.Single(p => p.Name == "chdir"));
        }


        [TestMethod]
        public void RegisterProceduresReturnsChdirCommandWith1Parameter()
        {
            var commands = new SystemCommands();
            var procedures = commands.RegisterProcedures();
            var chdirCommand = procedures.Single(p => p.Name == "chdir");

            Assert.AreEqual(1, chdirCommand.ParameterCount);
        }


        [TestMethod]
        public void RegisterProceduresReturnsChdirCommandImplementedByChangeWorkingDirectoryMethod()
        {
            var commands = new SystemCommands();
            var procedures = commands.RegisterProcedures();
            var chdirCommand = procedures.Single(p => p.Name == "chdir") as LogoCommand;

            Assert.AreEqual(commands.ChangeWorkingDirectory, chdirCommand.Implementation);
        }


        [TestMethod]
        public void ChangeWorkingDirectorySetsWorkingDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            var commands = new SystemCommands();
            commands.ChangeWorkingDirectory(null, new LogoToken { Evaluated = true, TokenValue = new LogoValue { Type = Core.Tokens.ValueType.String, Value = "C:\\" } });

            Assert.AreEqual("C:\\", Directory.GetCurrentDirectory());

            Directory.SetCurrentDirectory(currentDirectory);
        }


        [TestMethod]
        public void RegisterProceduresReturnsIListContainingCurrentDirCommand()
        {
            var commands = new SystemCommands();
            var procedures = commands.RegisterProcedures();

            Assert.IsNotNull(procedures.Single(c => c.Name == "currentdir"));
        }


        [TestMethod]
        public void RegisterProceduresReturnsCurrentDirCommandWithNoParameters()
        {
            var commands = new SystemCommands();
            var procedures = commands.RegisterProcedures();
            var currentdir = procedures.Single(c => c.Name == "currentdir");

            Assert.AreEqual(0, currentdir.ParameterCount);
        }


        [TestMethod]
        public void RegisterProceduresReturnsCurrentDirCommandImplementedByGetWorkingDirectoryMethod()
        {
            var commands = new SystemCommands();
            var procedures = commands.RegisterProcedures();
            var currentdir = procedures.Single(c => c.Name == "currentdir") as LogoCommand;

            Assert.AreEqual(commands.GetWorkingDirectory, currentdir.Implementation);
        }


        [TestMethod]
        public void GetWorkingDirectoryMethodReturnsTokenWithStringValueEqualToCurrentDirectory()
        {
            string dirName = Directory.GetCurrentDirectory();

            var commands = new SystemCommands();
            LogoToken result = commands.GetWorkingDirectory(null);

            Assert.IsTrue(result.Evaluated);
            Assert.IsNotNull(result.TokenValue);
            Assert.AreEqual(Core.Tokens.ValueType.String, result.TokenValue.Type);
            Assert.AreEqual(dirName, result.TokenValue.Value as string);
        }
    }
}
