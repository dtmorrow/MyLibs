using MyLibs.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using MyLibs;
using System.Text;
using MyLibs.IO;

namespace MyLibsConsoleTesting
{
    class Program
    {
        static void Main()
        {
            //Console.ForegroundColor = ConsoleColor.White;
            SelectTest();
        }

        static void SelectTest()
        {
            while (true)
            {
                var menu = new HashSet<MenuItem>()
                {
                    new MenuItem(1, '1', "1. Menu Test"),
                    new MenuItem(2, '2', "2. Hidden Input Test"),
                    new MenuItem(3, '3', "3. ANSI Escape Code Test"),
                    new MenuItem(int.MaxValue, 'Q', "Q. Quit"),
                };

                Console.Clear();
                var i = CommandLineUtilities.Menu(menu, "-- Select a Test --");
                Console.Clear();

                switch (i)
                {
                    case 0: MenuTest(); break;
                    case 1: HiddenTest(); break;
                    case 2: AnsiTest(); break;
                    case 3: return;
                }
            }
        }

        static void HiddenTest()
        {
            Console.Write("Hidden Test:");
            var hidden = CommandLineUtilities.ReadHiddenLine();

            Console.WriteLine("Entered:" + hidden);
            
            Console.WriteLine("\n-- Press Any Key To Exit --");
            Console.ReadKey();
        }

        static void MenuTest()
        {
            var i = CommandLineUtilities.Menu(new HashSet<MenuItem>()
            {
                new MenuItem(3, '3', "3. Third Item"),
                new MenuItem(2, '2', "2. Second Item"),
                new MenuItem(1, '1', "1. First Item"),

            }, "-- Select Number --");

            Console.WriteLine("Selected: " + (i + 1));

            i = CommandLineUtilities.Menu(new HashSet<MenuItem>()
            {
                new MenuItem(1, 'f', "1. First Item"),
                new MenuItem(2, 'e', "2. Second Item"),
                new MenuItem(3, 'i', "3. Third Item"),
            }, "-- Select Letter --");

            Console.WriteLine("Selected: " + (i + 1));

            Console.WriteLine("\n-- Press Any Key To Exit --");
            Console.ReadKey();
        }

        static void AnsiTest()
        {
            AnsiEscapeCodes.EnableAnsi();
            while (true)
            {
                var menu = new HashSet<MenuItem>()
                {
                    new MenuItem(1, '1', "1. Command Test"),
                    new MenuItem(2, '2', "2. Select Graphic Rendition Test"),
                    new MenuItem(int.MaxValue, 'Q', "Q. Quit"),
                };

                Console.Write(AnsiEscapeCodes.ClearDisplay(AnsiEscapeCodes.ClearDisplayType.Screen));
                var i = CommandLineUtilities.Menu(menu, "-- Select a Test --");
                Console.Write(AnsiEscapeCodes.ClearDisplay(AnsiEscapeCodes.ClearDisplayType.Screen));

                switch (i)
                {
                    case 0: CommandTest(); break;
                    case 1: SgrTest(); break;
                    case 2: return;
                }
            }
        }

        static void CommandTest()
        {
            const string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            Console.WriteLine("'Q' to quit. Arrow Keys to move cursor. '+'/'-' to move cursor line. 'Enter' to set cursor center.");
            Console.WriteLine("'Page Up/Down' to move buffer. 'Home'/'End' to insert/delete characters. 'Ins'/'Del' to insert/delete lines.");
            Console.WriteLine("'[' to erase characters left of cursor. ']' to erase characters right of cursor. '|' to erase line.");
            Console.WriteLine("',' to save cursor position. '.' to restore cursor position.");
            Console.WriteLine("'`' to set tab stop. Tab/Shift+Tab to move tab stops.");
            Console.WriteLine("----------");
            Console.WriteLine(text);
            Console.WriteLine(text);
            Console.WriteLine(text);
            Console.WriteLine(text);
            Console.WriteLine(text);

            while (true)
            {
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Q: return;
                    case ConsoleKey.UpArrow: Console.Write(AnsiEscapeCodes.MoveCursorVertical(-1)); break;
                    case ConsoleKey.DownArrow: Console.Write(AnsiEscapeCodes.MoveCursorVertical(1)); break;
                    case ConsoleKey.LeftArrow: Console.Write(AnsiEscapeCodes.MoveCursorHorizontal(-1)); break;
                    case ConsoleKey.RightArrow: Console.Write(AnsiEscapeCodes.MoveCursorHorizontal(1)); break;
                    case ConsoleKey.OemMinus: Console.Write(AnsiEscapeCodes.MoveCursorLine(-1)); break;
                    case ConsoleKey.OemPlus: Console.Write(AnsiEscapeCodes.MoveCursorLine(1)); break;
                    case ConsoleKey.Enter: Console.Write(AnsiEscapeCodes.SetCursorX((26 * 2 + 10) / 2) + AnsiEscapeCodes.SetCursorY(8)); break;
                    case ConsoleKey.PageUp: Console.Write(AnsiEscapeCodes.ShiftBuffer(-1)); break;
                    case ConsoleKey.PageDown: Console.Write(AnsiEscapeCodes.ShiftBuffer(1)); break;
                    case ConsoleKey.Home: Console.Write(AnsiEscapeCodes.ShiftCharacters(-1)); break;
                    case ConsoleKey.End: Console.Write(AnsiEscapeCodes.ShiftCharacters(1)); break;
                    case ConsoleKey.Delete: Console.Write(AnsiEscapeCodes.ShiftLines(-1)); break;
                    case ConsoleKey.Insert: Console.Write(AnsiEscapeCodes.ShiftLines(1)); break;
                    case ConsoleKey.Backspace: Console.Write(AnsiEscapeCodes.EraseCharacters(1)); break;
                    case ConsoleKey.Oem4: Console.Write(AnsiEscapeCodes.ClearLine(AnsiEscapeCodes.ClearLineType.FromBeginningToCursor)); break;
                    case ConsoleKey.Oem6: Console.Write(AnsiEscapeCodes.ClearLine(AnsiEscapeCodes.ClearLineType.FromCursorToEnd)); break;
                    case ConsoleKey.Oem5: Console.Write(AnsiEscapeCodes.ClearLine(AnsiEscapeCodes.ClearLineType.EntireLine)); break;
                    case ConsoleKey.OemComma: Console.Write(AnsiEscapeCodes.SaveCursorPosition()); break;
                    case ConsoleKey.OemPeriod: Console.Write(AnsiEscapeCodes.RestoreCursorPosition()); break;
                    case ConsoleKey.Oem3: Console.Write(AnsiEscapeCodes.SetTabStop()); break;
                    case ConsoleKey.Tab:
                        if (key.Modifiers.HasFlag(ConsoleModifiers.Shift))
                        {
                            Console.Write(AnsiEscapeCodes.MoveCursorTabStop(-1));
                        }
                        else
                        {
                            Console.Write(AnsiEscapeCodes.MoveCursorTabStop(1));
                        }
                        break;
                }
            }
        }

        static void SgrTest()
        {
            var sgr = new SgrBuilder();

            while (true)
            {
                Console.WriteLine("----------");
                Console.WriteLine($"Current SGR:{sgr.ToString().Replace("\u001B", "[ESC]")}");
                Console.WriteLine("----------");
                Console.WriteLine(" Q. Quit");
                Console.WriteLine(" 0. Clear All Formatting");
                Console.WriteLine(" 1. Font Weight: Bold");
                Console.WriteLine(" 2. Font Weight: Faint");
                Console.WriteLine(" 3. Italic: On");
                Console.WriteLine(" 4. Underline: Single");
                Console.WriteLine(" 5. Blink Speed: Slow");
                Console.WriteLine(" 6. Blink Speed: Fast");
                Console.WriteLine(" 7. Invert: On");
                Console.WriteLine(" 8. Conceal: On");
                Console.WriteLine(" 9. Strikeout: On");
                Console.WriteLine("10-19. Alternate Fonts");
                Console.WriteLine("20. Fraktur");
                Console.WriteLine("21. Underline: Double");
                Console.WriteLine("22. Font Weight: Reset");
                Console.WriteLine("23. Italic: Off");
                Console.WriteLine("24. Underline: Off");
                Console.WriteLine("25. Blink Speed: Off");
                Console.WriteLine("26. Proportional Spacing: On");
                Console.WriteLine("27. Invert: Off");
                Console.WriteLine("28. Conceal: Off");
                Console.WriteLine("29. Strikeout: On");
                Console.WriteLine("50. Proportional Spacing: Off");
                Console.WriteLine("51. Framing: Framed");
                Console.WriteLine("52. Framing: Encircled");
                Console.WriteLine("53. Overline: On");
                Console.WriteLine("54. Framing: None");
                Console.WriteLine("55. Overline: Off");

                Console.WriteLine("\nFG:[index|r;g;b]");
                Console.WriteLine("BG:[index|r;g;b]");
                Console.WriteLine("UL:r;g;b");
                Console.WriteLine("CUSTOM:[PARAMS]");

                var read = Console.ReadLine();

                if (read == "q" || read == "Q")
                {
                    sgr.ClearAll();
                    Console.WriteLine(sgr.ToString());
                    return;
                }
                else if (read.StartsWith("fg:", StringComparison.OrdinalIgnoreCase) || read.StartsWith("bg:", StringComparison.OrdinalIgnoreCase))
                {
                    bool fg = read.StartsWith("fg:", StringComparison.OrdinalIgnoreCase);

                    var color = read[(read.IndexOf(':') + 1)..];
                    var split = color.Split(';');
                    
                    if (split.Length == 1 && int.TryParse(split[0], out int index))
                    {
                        sgr.ClearAll();

                        if (fg)
                        {
                            sgr.ForegroundColor = ForegroundColor.FromConsoleColor((ConsoleColor)index);
                        }
                        else
                        {
                            sgr.BackgroundColor = BackgroundColor.FromConsoleColor((ConsoleColor)index);
                        }

                        Console.Write(sgr.ToString());
                    }
                    else if (split.Length == 3 && byte.TryParse(split[0], out byte r) && byte.TryParse(split[1], out byte g) && byte.TryParse(split[2], out byte b))
                    {
                        sgr.ClearAll();

                        if (fg)
                        {
                            sgr.ForegroundColor = ForegroundColor.FromRGB(r, g, b);
                        }
                        else
                        {
                            sgr.BackgroundColor = BackgroundColor.FromRGB(r, g, b);
                        }

                        Console.Write(sgr.ToString());
                    }
                }
                else if (read.StartsWith("ul:", StringComparison.OrdinalIgnoreCase))
                {
                    var color = read[(read.IndexOf(':') + 1)..];
                    var split = color.Split(';');
                    
                    if (split.Length == 3 && byte.TryParse(split[0], out byte r) && byte.TryParse(split[1], out byte g) && byte.TryParse(split[2], out byte b))
                    {
                        sgr.ClearAll();

                        sgr.UnderLineColor = UnderLineColor.FromRGB(r, g, b);

                        Console.Write(sgr.ToString());
                    }

                }
                else if (read.StartsWith("custom:", StringComparison.OrdinalIgnoreCase))
                {
                    var custom = read[(read.IndexOf(':') + 1)..];
                    sgr.ClearAll();
                    sgr.AddCustom(custom);
                    Console.Write(sgr.ToString());
                }
                else if (int.TryParse(read, out int result))
                {
                    sgr.ClearAll();
                    sgr.AddCustom(result.ToString());

                    Console.Write(sgr.ToString());
                }
            }
        }
    }
}
