using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MyLibs.IO
{
    /// <summary>
    /// Methods for files and streams.
    /// </summary>
    public static class FileUtilities
    {
        private static readonly char[] invalidChars = Path.GetInvalidFileNameChars();

        /// <summary>
        /// Convert invalid characters in a filename to underscores.
        /// </summary>
        /// <param name="filename">Original filename.</param>
        /// <returns>Filename with all invalid characters converted to underscores.</returns>
        /// 
        public static string SanitizeFileName(string filename)
        {
            var replacementRules = Enumerable.Empty<KeyValuePair<string, string>>();

            return SanitizeFileName(filename, replacementRules);
        }

        /// <summary>
        /// Convert invalid characters in a filename to underscores. Convert double quotes to single quotes, colons to dashes surrounded by spaces, forward slashes and backward slashes to dashes, and question marks to periods.
        /// </summary>
        /// <param name="filename">Original filename.</param>
        /// <returns>Filename with all invalid characters converted to underscores and replacements.</returns>
        public static string SanitizeFileNameCustom(string filename)
        {
            var replacementRules = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>( "\"", "'"),  // Replace double quotes with single quotes
                new KeyValuePair<string, string>( ":", " - "), // Replace colon with spaces and dash
                new KeyValuePair<string, string>( "/", "-"),   // Replace forward slash with dash
                new KeyValuePair<string, string>( "\\", "-"),  // Replace backward slash with dash
                new KeyValuePair<string, string>( "?", ".")    // Replace question mark with period
            };

            return SanitizeFileName(filename, replacementRules);
        }

        /// <summary>
        /// Convert invalid characters in a filename to underscores and replace specified strings using custom replacement rules.
        /// </summary>
        /// <param name="filename">Original filename.</param>
        /// <param name="replacementRules">Collection of <see cref="KeyValuePair{string, string}"/> where Key is the string to be replaced and Value is string to replace with.</param>
        /// <returns>Filename with all invalid characters converted to underscores and custom replacements.</returns>
        public static string SanitizeFileName(string filename, IEnumerable<KeyValuePair<string, string>> replacementRules)
        {
            var sb = new StringBuilder(filename);

            // Replace using replacement rules
            foreach (var rule in replacementRules)
            {
                sb.Replace(rule.Key, rule.Value);
            }

            // Replace invalid characters with underscores
            foreach (var invalidChar in invalidChars)
            {
                sb.Replace(invalidChar, '_');
            }

            // Trim leading and trailing space, and trim any trailing periods
            sb.Trim().TrimEnd('.');

            return sb.ToString();
        }

        /// <summary>
        /// Create a soft-link to a file or directory. Windows Only.
        /// </summary>
        /// <param name="targetPath">Original file to link to.</param>
        /// <param name="linkPath">New soft-link that links to original file.</param>
        /// <returns><see langword="true"/> if soft-link creation was successful; otherwise <see langword="false"/>.</returns>
        public static bool CreateSoftLink(string targetPath, string linkPath)
        {
            if (Directory.Exists(targetPath))
            {
                return Win32.CreateSymbolicLink(linkPath, targetPath, Win32.SYMBOLIC_LINK_FLAG.Directory);
            }
            else if (File.Exists(targetPath))
            {
                return Win32.CreateSymbolicLink(linkPath, targetPath, Win32.SYMBOLIC_LINK_FLAG.File);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Create a hard-link to a file. Windows Only.
        /// </summary>
        /// <param name="targetPath">Original file to link to.</param>
        /// <param name="linkPath">New hard-link that links to original file.</param>
        /// <returns><see langword="true"/> if hard-link creation was successful; otherwise <see langword="false"/>.</returns>
        public static bool CreateHardLink(string targetPath, string linkPath)
        {
            if (Directory.Exists(targetPath))
            {
                return false; // Can't Hard Link directories. Create a Directory Junction instead?
            }
            else if (File.Exists(targetPath))
            {
                return Win32.CreateHardLink(linkPath, targetPath, IntPtr.Zero);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get directory of the currently running executable.
        /// </summary>
        /// <returns><see cref="string"/> containing the directory of the currently running executable.</returns>
        public static string GetExecutableDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// Open a file with the file type's default associated application.
        /// </summary>
        /// <param name="filepath">The path to the file.</param>
        /// <param name="args">Any arguments to supply with the open operation.</param>
        /// <param name="workingDirectory">The working directory for the opened application. If <see langword="null"/>, then the working directory is the current working directory.</param>
        /// <returns><see langword="true"/> if the file is opened successfully; otherwise <see langword="false"/>.</returns>
        public static bool OpenFile(string filepath, string args = "", string workingDirectory = null)
        {
            if (workingDirectory == null)
            {
                workingDirectory = Directory.GetCurrentDirectory();
            }

            var info = new ProcessStartInfo(filepath)
            {
                UseShellExecute = true,
                Verb = "open",
                Arguments = args,
                WorkingDirectory = workingDirectory
            };

            using var process = new Process
            {
                StartInfo = info
            };

            return process.Start();
        }

        /// <summary>
        /// Write all text from an input <see cref="TextReader"/> into an output <see cref="TextWriter"/>, removing line breaks.
        /// </summary>
        /// <param name="input">The <see cref="TextReader"/> to read all text from.</param>
        /// <param name="output">The <see cref="TextWriter"/> to write all text to, without line breaks.</param>
        /// <param name="spaceBetweenNewLines">If a space should be inputted between each removed line break. Default: <see langword="false"/></param>
        public static void LinearizeStream(TextReader input, TextWriter output, bool spaceBetweenNewLines = false)
        {
            bool newLine = true; // Marks when we're on a new line and no non-whitespace characters have been encountered.
            var whitespace = new StringBuilder(); // Tracks whitespace in-between normal characters (i.e. non-leading and non-trailing whitespace).

            while (input.TryReadChar(out char c))
            {
                if (c == '\n')
                {
                    if (spaceBetweenNewLines && !newLine)
                    {
                        output.Write(' ');
                    }

                    newLine = true;
                    whitespace.Clear();
                }
                else if (char.IsWhiteSpace(c))
                {
                    if (!newLine)
                    {
                        whitespace.Append(c);
                    }
                }
                else
                {
                    output.Write(whitespace);
                    output.Write(c);

                    newLine = false;
                    whitespace.Clear();
                }
            }
        }

        /// <summary>
        /// Write all text from an input <see cref="TextReader"/> into an output <see cref="TextWriter"/>, converting line endings to Unix format (i.e. "\r\n" -&gt; "\n").
        /// </summary>
        /// <param name="input">The <see cref="TextReader"/> to read all text from.</param>
        /// <param name="output">The <see cref="TextWriter"/> to write all text to, converting line endings to Unix format.</param>
        public static void ConvertLineEndingsToUnixStream(TextReader input, TextWriter output)
        {
            while (input.TryReadChar(out char c))
            {
                if (c != '\r' || !input.TryPeekChar(out char next) || next != '\n')
                {
                    output.Write(c);
                }
            }
        }

        /// <summary>
        /// Write all text from an input <see cref="TextReader"/> into an output <see cref="TextWriter"/>, converting line endings to Windows format (i.e. "\n" -&gt; "\r\n").
        /// </summary>
        /// <param name="input">The <see cref="TextReader"/> to read all text from.</param>
        /// <param name="output">The <see cref="TextWriter"/> to write all text to, converting line endings to Windows format.</param>
        public static void ConvertLineEndingsToWindowsStream(TextReader input, TextWriter output)
        {
            while (input.TryReadChar(out char c))
            {
                if (c == '\r')
                {
                    output.Write(c);
                    
                    if (input.TryPeekChar(out char next) && next == '\n')
                    {
                        input.Read();
                        output.Write('\n');
                    }
                }
                else if (c == '\n')
                {
                    output.Write('\r');
                    output.Write('\n');
                }
                else
                {
                    output.Write(c);
                }
            }
        }
    }
}
