using Logo.Core.Procedures;
using Logo.Core.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinedale.WfTurtle.Drawing;
using Vinedale.WfTurtle.Interfaces;

namespace Vinedale.WfTurtle.UnitTests
{
    [TestClass]
    public class TurtleCommandsUnitTests
    {
        [TestMethod]
        public void RegisterProceduresReturnsListContainingForwardCommand()
        {
            var commands = new TurtleCommands(new Mock<ITurtleContext>().Object);
            var procedures = commands.RegisterProcedures();

            Assert.IsNotNull(procedures.Single(p => p.Name == "forward"));
        }


        [TestMethod]
        public void RegisterProceduresReturnsForwardCommandWithAliasFd()
        {
            var commands = new TurtleCommands(new Mock<ITurtleContext>().Object);
            var procedures = commands.RegisterProcedures();
            var forwardCommand = procedures.Single(p => p.Name == "forward");

            Assert.IsTrue(forwardCommand.Aliases.Contains("fd"));
        }


        [TestMethod]
        public void RegisterProceduresReturnsForwardCommandWith1Parameter()
        {
            var commands = new TurtleCommands(new Mock<ITurtleContext>().Object);
            var procedures = commands.RegisterProcedures();
            var forwardCommand = procedures.Single(p => p.Name == "forward");

            Assert.AreEqual(1, forwardCommand.ParameterCount);
        }


        [TestMethod]
        public void RegisterProceduresReturnsForwardCommandImplementedByForwardMethod()
        {
            var commands = new TurtleCommands(new Mock<ITurtleContext>().Object);
            var procedures = commands.RegisterProcedures();
            var forwardCommand = procedures.Single(p => p.Name == "forward") as LogoCommand;

            Assert.AreEqual(commands.Forward, forwardCommand.Implementation);
        }


        [TestMethod]
        public void ForwardMethodCallsITurtleContextPendDrawingInstruction()
        {
            var mockContext = new Mock<ITurtleContext>();
            var commands = new TurtleCommands(mockContext.Object);

            commands.Forward(null, new LogoToken { Evaluated = true, TokenValue = new LogoValue { Type = Logo.Core.Tokens.ValueType.Number, Value = 0m } });

            mockContext.Verify(c => c.PendDrawingInstruction(It.IsAny<Instruction>()));
        }


        [TestMethod]
        public void ForwardMethodCallsITurtleContextPendDrawingInstructionWithLineInstructionParameter()
        {
            var mockContext = new Mock<ITurtleContext>();
            Instruction instruction = null;
            mockContext.Setup(c => c.PendDrawingInstruction(It.IsAny<Instruction>())).Callback<Instruction>(i => { instruction = i; });
            var commands = new TurtleCommands(mockContext.Object);

            commands.Forward(null, new LogoToken { Evaluated = true, TokenValue = new LogoValue { Type = Logo.Core.Tokens.ValueType.Number, Value = 0m } });

            Assert.IsNotNull(instruction);
            Assert.IsTrue(instruction is LineInstruction);
        }


        [TestMethod]
        public void ForwardMethodPassesLineInstructionWithCorrectLengthToPendDrawingInstruction()
        {
            var random = new Random();
            decimal val = (decimal)(random.NextDouble() * 500.0);
            var mockContext = new Mock<ITurtleContext>();
            Instruction instruction = null;
            mockContext.Setup(c => c.PendDrawingInstruction(It.IsAny<Instruction>())).Callback<Instruction>(i => { instruction = i; });
            var commands = new TurtleCommands(mockContext.Object);

            commands.Forward(null, new LogoToken { Evaluated = true, TokenValue = new LogoValue { Type = Logo.Core.Tokens.ValueType.Number, Value = val } });

            LineInstruction lineInstruction = instruction as LineInstruction;
            Assert.IsNotNull(lineInstruction);
            Assert.AreEqual((double)val, lineInstruction.Length);
        }
    }
}
