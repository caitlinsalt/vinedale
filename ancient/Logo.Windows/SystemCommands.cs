using Logo.Core.Interpretation;
using Logo.Core.Procedures;
using Logo.Core.Tokens;
using Logo.Windows.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;

namespace Logo.Windows
{
    /// <summary>
    /// This class defines built-in commands which interact with the operating system, such as "chdir".
    /// </summary>
    public class SystemCommands : ICommandModule
    {
        /// <summary>
        /// Gives the definitions of commands implemented in this class.
        /// </summary>
        /// <returns>A list of <c>LogoProcedure</c> definitions.</returns>
        public IList<LogoProcedure> RegisterProcedures()
        {
            return new LogoProcedure[]
            {
                new LogoCommand
                {
                    Name = "chdir",
                    Aliases = new string[0],
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = ChangeWorkingDirectory,
                    HelpText = Strings.CommandChdirHelpText,
                    ExampleText = Strings.CommandChdirExampleText,
                },
                new LogoCommand
                {
                    Name = "currentdir",
                    Aliases = new string[0],
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 0,
                    Implementation = GetWorkingDirectory,
                    HelpText = Strings.CommandCurrentdirHelpText,
                    ExampleText = string.Empty,
                },
                new LogoCommand
                {
                    Name = "directories",
                    Aliases = new string[0],
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 0,
                    Implementation = GetSubdirectories,
                    HelpText = Strings.CommandDirectoriesHelpText,
                    ExampleText = string.Empty,
                },
                new LogoCommand
                {
                    Name = "files",
                    Aliases = new string[0],
                    Redefinability = RedefinabilityType.NonRedefinable,
                    ParameterCount = 1,
                    Implementation = GetFiles,
                    HelpText = Strings.CommandFilesHelpText,
                    ExampleText = Strings.CommandFilesExampleText,
                }
            };
        }

        /// <summary>
        /// Get the CWD.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Not used.</param>
        /// <returns>A token whose value is the current working directory.</returns>
        public LogoToken GetWorkingDirectory(InterpretorContext context, params LogoToken [] input)
        {
            return new LogoToken
            {
                Evaluated = true,
                Literal = "currentdir",
                TokenValue = new LogoValue { Type = Core.Tokens.ValueType.String, Value = Directory.GetCurrentDirectory() }
            };
        }


        /// <summary>
        /// Gets subdirectories of the CWD.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Not used.</param>
        /// <returns>A token whose value is a list of strings which are the names of subdirectories of the current working directory.</returns>
        public LogoToken GetSubdirectories(InterpretorContext context, params LogoToken[] input)
        {
            LogoList list = new LogoList
            {
                Evaluated = true,
                Literal = "directories",
                InnerContents = Directory.GetDirectories(Directory.GetCurrentDirectory())
                    .Select(d => new LogoToken { Evaluated = true, Literal = d, TokenValue = new LogoValue { Type = Core.Tokens.ValueType.String, Value = d } })
                    .ToList()
            };
            list.TokenValue = new LogoValue { Type = Core.Tokens.ValueType.List, Value = list };
            return list;
        }


        /// <summary>
        /// Gets files contained in the CWD with names matching an extension.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">The first element of the array should be a string whose name is a file-matching pattern or an extension.</param>
        /// <returns>A token whose value is a list of strings which are the names of files in the current working directory.</returns>
        public LogoToken GetFiles(InterpretorContext context, params LogoToken[] input)
        {
            if (input[0].TokenValue.Type != Core.Tokens.ValueType.String)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandFilesWrongTypeError);
                return null;
            }

            string pattern = (string)input[0].TokenValue.Value;
            if (!pattern.Contains("."))
            {
                pattern = "*." + pattern;
            }

            LogoList list = new LogoList
            {
                Evaluated = true,
                Literal = "files",
                InnerContents = Directory.GetFiles(Directory.GetCurrentDirectory(), pattern)
                    .Select(f => new LogoToken { Evaluated = true, Literal = Path.GetFileName(f), TokenValue = new LogoValue { Type = Core.Tokens.ValueType.String, Value = Path.GetFileName(f) } })
                    .ToList(),
            };
            list.TokenValue = new LogoValue { Type = Core.Tokens.ValueType.List, Value = list };
            return list;
        }


        /// <summary>
        /// Change the CWD.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token, which is the path to the new CWD in OS format.</param>
        /// <returns><c>null</c></returns>
        public LogoToken ChangeWorkingDirectory(InterpretorContext context, params LogoToken[] input)
        {
            if (input[0].TokenValue.Type != Core.Tokens.ValueType.String)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandChdirWrongTypeError);
                return null;
            }

            try
            {
                Directory.SetCurrentDirectory((string)input[0].TokenValue.Value);
            }
            catch (FileNotFoundException)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandChdirFileNotFoundError);
            }
            catch (DirectoryNotFoundException)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandChdirDirectoryNotFoundError);
            }
            catch (PathTooLongException)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandChdirPathTooLongError);
            }
            catch (IOException)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandChdirGeneralIOError);
            }
            catch (ArgumentNullException)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandChdirArgumentNullError);
            }
            catch (ArgumentException)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandChdirGeneralArgumentError);
            }
            catch (SecurityException)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandChdirSecurityError);
            }

            return null;
        }
    }
}
