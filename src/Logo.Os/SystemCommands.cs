using Logo.Interpretation;
using Logo.Os.Resources;
using Logo.Procedures;
using Logo.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;

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
                new LogoCommand("chdir", 1, RedefinabilityType.NonRedefinable, ChangeWorkingDirectory, Strings.CommandChdirHelpText, Strings.CommandChdirExampleText),
                new LogoCommand("currentdir", 0, RedefinabilityType.NonRedefinable, GetWorkingDirectory, Strings.CommandCurrentdirHelpText),
                new LogoCommand("directories", 0, RedefinabilityType.NonRedefinable, GetSubdirectories, Strings.CommandDirectoriesHelpText),
                new LogoCommand("files", 1, RedefinabilityType.NonRedefinable, GetFiles, Strings.CommandFilesHelpText, Strings.CommandFilesExampleText),
            };
        }

        /// <summary>
        /// Get the CWD.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Not used.</param>
        /// <returns>A token whose value is the current working directory.</returns>
        public static Token GetWorkingDirectory(InterpretorContext context, params LogoValue[] input)
        {
            return new ValueToken(Syntax.CurrentDir, new LogoValue(LogoValueType.Text, Directory.GetCurrentDirectory()));
        }


        /// <summary>
        /// Gets subdirectories of the CWD.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Not used.</param>
        /// <returns>A token whose value is a list of strings which are the names of subdirectories of the current working directory.</returns>
        public static Token GetSubdirectories(InterpretorContext context, params LogoValue[] input)
        {
            List<Token> tokenList = new List<Token>();
            tokenList.AddRange(Directory.GetDirectories(Directory.GetCurrentDirectory()).Select(d => new ValueToken(d, new LogoValue(LogoValueType.Text, d))));
            return new ListToken(tokenList);
        }


        /// <summary>
        /// Gets files contained in the CWD with names matching an extension.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">The first element of the array should be a string whose name is a file-matching pattern or an extension.</param>
        /// <returns>A token whose value is a list of strings which are the names of files in the current working directory.</returns>
        public static Token GetFiles(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

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
                .Select(f => new ValueToken(Path.GetFileName(f), new LogoValue(LogoValueType.Text, Path.GetFileName(f)))).ToArray());
            return list;
        }


        /// <summary>
        /// Change the CWD.
        /// </summary>
        /// <param name="context">The interpretor context.</param>
        /// <param name="input">Should contain one token, which is the path to the new CWD in OS format.</param>
        /// <returns><c>null</c></returns>
        public static Token ChangeWorkingDirectory(InterpretorContext context, params LogoValue[] input)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

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
