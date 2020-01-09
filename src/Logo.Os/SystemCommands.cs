using Logo.Interpretation;
using Logo.Os.Resources;
using Logo.Procedures;
using Logo.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;

namespace Logo.Os
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
        public Token GetWorkingDirectory(InterpretorContext context, params LogoValue[] input)
        {
            return new LiteralToken("currentdir", new LogoValue(LogoValueType.Text, Directory.GetCurrentDirectory()));
        }


        /// <summary>
        /// Gets subdirectories of the CWD.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Not used.</param>
        /// <returns>A token whose value is a list of strings which are the names of subdirectories of the current working directory.</returns>
        public Token GetSubdirectories(InterpretorContext context, params LogoValue[] input)
        {
            List<Token> tokenList = new List<Token>();
            tokenList.AddRange(Directory.GetDirectories(Directory.GetCurrentDirectory()).Select(d => new LiteralToken(d, new LogoValue(LogoValueType.Text, d))));
            return new ListToken(tokenList);
        }


        /// <summary>
        /// Gets files contained in the CWD with names matching an extension.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">The first element of the array should be a string whose name is a file-matching pattern or an extension.</param>
        /// <returns>A token whose value is a list of strings which are the names of files in the current working directory.</returns>
        public Token GetFiles(InterpretorContext context, params LogoValue[] input)
        {
            if (input[0].Type != LogoValueType.Text)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandFilesWrongTypeError);
                return null;
            }

            string pattern = (string)input[0].Value;
            if (!pattern.Contains("."))
            {
                pattern = "*." + pattern;
            }

            ListToken list = new ListToken(Directory.GetFiles(Directory.GetCurrentDirectory(), pattern)
                .Select(f => new LiteralToken(Path.GetFileName(f), new LogoValue(LogoValueType.Text, Path.GetFileName(f)))).ToArray());
            return list;
        }


        /// <summary>
        /// Change the CWD.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token, which is the path to the new CWD in OS format.</param>
        /// <returns><c>null</c></returns>
        public Token ChangeWorkingDirectory(InterpretorContext context, params LogoValue[] input)
        {
            if (input[0].Type != LogoValueType.Text)
            {
                context.Interpretor.WriteOutputLine(Strings.CommandChdirWrongTypeError);
                return null;
            }

            try
            {
                Directory.SetCurrentDirectory((string)input[0].Value);
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
