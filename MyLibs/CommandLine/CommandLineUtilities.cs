using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLibs.CommandLine
{
    /// <summary>
    /// Methods for the command line.
    /// </summary>
    public static class CommandLineUtilities
    {
        /// <summary>
        /// Read a line (terminated by the ENTER key) from standard input, hiding the characters that are entered with an asterisk. Supports backspacing.
        /// </summary>
        /// <returns>The characters entered before pressing the ENTER key.</returns>
        public static string ReadHiddenLine()
        {
            var sb = new StringBuilder();

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace && sb.Length > 0)
                {
                    sb.Length--;
                    Console.Write("\b \b");
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return sb.ToString();
                }
                else
                {
                    if (!char.IsControl(key.KeyChar))
                    {
                        sb.Append(key.KeyChar);
                        Console.Write('*');
                    }
                }
            }
        }

        /// <summary>
        /// Display a menu and get a menu selection.
        /// </summary>
        /// <param name="menuItems">The menu items to display. Hotkeys MUST be unique (enforced by HashSet) and Orderings SHOULD be unique (but is not enforced).</param>
        /// <param name="header">The header to display before the menu. If header is <see langword="null"/>, then no header is displayed. Default: <see langword="null"/></param>
        /// <param name="highlightColor">The color to output the hotkey character in for each menu item. Default: <see cref="ConsoleColor.Yellow"/></param>
        /// <returns>An <see cref="int"/> that contains the zero-based index of the menu selection.</returns>
        public static int Menu(HashSet<MenuItem> menuItems, string header = null, ConsoleColor highlightColor = ConsoleColor.Yellow)
        {
            var sorted = menuItems.OrderBy(item => item.Order).ToArray();

            if (header != null)
            {
                Console.WriteLine(header);
            }

            foreach (var item in sorted)
            {
                bool lookingForHotkey = true;

                foreach (var c in item.Text)
                {
                    if (char.ToLower(c) == item.Hotkey && lookingForHotkey)
                    {
                        var save = Console.ForegroundColor;
                        Console.ForegroundColor = highlightColor;
                        Console.Write(c);
                        Console.ForegroundColor = save;
                        lookingForHotkey = false;
                    }
                    else
                    {
                        Console.Write(c);
                    }
                }

                Console.WriteLine();
            }

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                for (int i = 0; i < sorted.Count(); i++)
                {
                    if (sorted[i].Hotkey == key.KeyChar)
                    {
                        return i;
                    }
                }
            }
        }
    }
}
