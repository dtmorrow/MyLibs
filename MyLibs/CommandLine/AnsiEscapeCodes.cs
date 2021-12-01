using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLibs.CommandLine
{
    /// <summary>
    /// Methods to generate ANSI sequences. Write sequences to console to activate them. Note: compatibility is highly dependent on the console you're using.
    /// </summary>
    public static class AnsiEscapeCodes
    {
        // Source     : https://docs.microsoft.com/en-us/windows/console/console-virtual-terminal-sequences
        //            : https://en.wikipedia.org/wiki/ANSI_escape_code
        // Terminals  : I used Cmder on Windows to test most of these. Wikipedia says they're implemented in Kitty, VTE, mintty, and iTerm2. I think Windows 10 has some support?
        private const char ESC = '\u001B';

        /// <summary>
        /// Enables ANSI sequence support on some Windows systems.
        /// </summary>
        /// <returns><see langword="true"/> if ANSI support is enabled; otherwise <see langword="false"/>.</returns>
        public static bool EnableAnsi()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                IntPtr handle = Win32.GetStdHandle(Win32.STD_OUTPUT_HANDLE);
                Win32.GetConsoleMode(handle, out uint mode);
                mode |= Win32.ENABLE_VIRTUAL_TERMINAL_PROCESSING;
                return Win32.SetConsoleMode(handle, mode);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Disables ANSI sequence support on some Windows systems.
        /// </summary>
        /// <returns><see langword="true"/> if ANSI support is disabled; otherwise <see langword="false"/>.</returns>
        public static bool DisableAnsi()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                IntPtr handle = Win32.GetStdHandle(Win32.STD_OUTPUT_HANDLE);
                Win32.GetConsoleMode(handle, out uint mode);
                mode &= ~Win32.ENABLE_VIRTUAL_TERMINAL_PROCESSING;
                return Win32.SetConsoleMode(handle, mode);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Move the cursor position vertically from the current position.
        /// </summary>
        /// <param name="n">The number of lines to move the cursor. A negative number moves the cursor up, and a positive number moves the cursor down.</param>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string MoveCursorVertical(int n = 1)
        {
            if (n < 0)
            {
                return $"{ESC}[{-n}A";
            }
            else
            {
                return $"{ESC}[{n}B";
            }
        }

        /// <summary>
        /// Move the cursor position horizontally from the current position.
        /// </summary>
        /// <param name="n">The number of columns to move the cursor. A negative number moves the cursor left, and a positive number moves the cursor right.</param>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string MoveCursorHorizontal(int n = 1)
        {
            if (n < 0)
            {
                return $"{ESC}[{-n}D";
            }
            else
            {
                return $"{ESC}[{n}C";
            }
        }

        /// <summary>
        /// Move the cursor position the specified number of lines up or down. The cursor will be moved to the beginning of the line.
        /// </summary>
        /// <param name="n">The number of lines to move the cursor. A negative number moves the cursor up, and a positive number moves the cursor down.</param>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string MoveCursorLine(int n = 1)
        {
            if (n < 0)
            {
                return $"{ESC}[{-n}F";
            }
            else
            {
                return $"{ESC}[{n}E";
            }
        }

        /// <summary>
        /// Set the cursor position column.
        /// </summary>
        /// <param name="x">The one-based column to set the cursor position.</param>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string SetCursorX(int x = 1)
        {
            return $"{ESC}[{x}G";
        }

        /// <summary>
        /// Set the cursor position line.
        /// </summary>
        /// <param name="y">The one-based line to set the cursor position.</param>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string SetCursorY(int y = 1)
        {
            return $"{ESC}[{y}d";
        }

        /// <summary>
        /// Set the cursor position line.
        /// </summary>
        /// <param name="x">The one-based column to set the cursor position.</param>
        /// <param name="y">The one-based line to set the cursor position.</param>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string SetCursorPosition(int x = 1, int y = 1)
        {
            return $"{ESC}[{x};{y}H";
        }

        /// <summary>
        /// Shift the console buffer vertically.
        /// </summary>
        /// <param name="n">The number of lines to shift the buffer. A negative number moves the cursor up, and a positive number moves the cursor down.</param>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string ShiftBuffer(int n = 1)
        {
            if (n < 0)
            {
                return $"{ESC}[{-n}S";
            }
            else
            {
                return $"{ESC}[{n}T";
            }
        }

        /// <summary>
        /// Shift the characters at the current position horizontally (i.e. insert spaces/delete characters).
        /// </summary>
        /// <param name="n">The number of spaces to shift the characters. A negative shifts the characters left (deletion), and a positive number shifts the characters right (inserts spaces).</param>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string ShiftCharacters(int n = 1)
        {
            if (n < 0)
            {
                return $"{ESC}[{-n}P";
            }
            else
            {
                return $"{ESC}[{n}@";
            }
        }

        /// <summary>
        /// Shift the lines at the current position vertically  (i.e. insert/delete lines).
        /// </summary>
        /// <param name="n">The number of lines to shift the lines. A negative shifts the lines up (deletes lines), and a positive number shifts the lines down (inserts lines).</param>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string ShiftLines(int n = 1)
        {
            if (n < 0)
            {
                return $"{ESC}[{-n}M";
            }
            else
            {
                return $"{ESC}[{n}L";
            }
        }

        /// <summary>
        /// Overwrite characters to the right of the current cursor position with spaces.
        /// </summary>
        /// <param name="n">The number of characters to overwrite with spaces.</param>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string EraseCharacters(int n = 1)
        {
            return $"{ESC}[{n}X";
        }

        /// <summary>
        /// The types of display clears.
        /// </summary>
        public enum ClearDisplayType
        {
            /// <summary>
            /// Clear the display from the current cursor position to the end of the screen.
            /// </summary>
            FromCursorToEnd = 0,
            /// <summary>
            /// Clear the display from the start of the screen to the current cursor position.
            /// </summary>
            FromBeginningToCursor= 1,
            /// <summary>
            /// Clear the entire screen.
            /// </summary>
            Screen= 2,
            /// <summary>
            /// Clear the entire screen and the scroll-back buffer.
            /// </summary>
            ScreenAndScroll = 3
        }

        /// <summary>
        /// Clear the characters on the screen.
        /// </summary>
        /// <param name="type">The type of display clear.</param>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string ClearDisplay(ClearDisplayType type)
        {
            return $"{ESC}[{(int)type}J";
        }

        /// <summary>
        /// The types of line clears.
        /// </summary>
        public enum ClearLineType
        {
            /// <summary>
            /// Clear the line from the current cursor position to the end of the line.
            /// </summary>
            FromCursorToEnd = 0,
            /// <summary>
            /// Clear the line from the beginning of the line to the current cursor position.
            /// </summary>
            FromBeginningToCursor= 1,
            /// <summary>
            /// Clear the entire line.
            /// </summary>
            EntireLine = 2,
        }

        /// <summary>
        /// Clear the characters on the current line.
        /// </summary>
        /// <param name="type">The type of line clear.</param>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string ClearLine(ClearLineType type)
        {
            return $"{ESC}[{(int)type}K";
        }

        /// <summary>
        /// Save the current cursor position to memory.
        /// </summary>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string SaveCursorPosition()
        {
            return $"{ESC}7";
        }

        /// <summary>
        /// Restores the saved cursor position from memory. If no position has been saved, then moves the cursor to the top left.
        /// </summary>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string RestoreCursorPosition()
        {
            return $"{ESC}8";
        }

        // TODO: Test Tab Stops on another platform
        /// <summary>
        /// Set a tab top at the current cursor column.
        /// </summary>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string SetTabStop()
        {
            return $"{ESC}H";
        }

        /// <summary>
        /// Move the cursor to a set tab stop.
        /// </summary>
        /// <param name="n">The number of tab stops to move. A negative number moves the cursor to tab stops to the left, and a positive number moves the cursor to tab stops to the right.</param>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string MoveCursorTabStop(int n = 1)
        {
            if (n < 0)
            {
                return $"{ESC}[{-n}Z";
            }
            else
            {
                return $"{ESC}[{n}I";
            }
        }

        /// <summary>
        /// Unsets a tab stop at the current cursor column.
        /// </summary>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string ClearCurrentTabStop()
        {
            return $"{ESC}[0g";
        }

        /// <summary>
        /// Unsets all tab stops.
        /// </summary>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public static string ClearAllTabStops()
        {
            return $"{ESC}[3g";
        }
    }
}
