using System;
using System.Drawing;

namespace MyLibs.CommandLine
{
    /// <summary>
    /// The color of the text.
    /// </summary>
    public class ForegroundColor
    {
        private string color = "";

        /// <summary>
        /// Convert a <see cref="ConsoleColor"/> to a <see cref="ForegroundColor"/>.
        /// </summary>
        /// <param name="color">The <see cref="ConsoleColor"/> to convert.</param>
        /// <returns>A <see cref="ForegroundColor"/> converted from the specified <see cref="ConsoleColor"/>.</returns>
        public static ForegroundColor FromConsoleColor(ConsoleColor color)
        {
            var sgr = new ForegroundColor
            {
                color = color switch
                {
                    ConsoleColor.Black => "30;",
                    ConsoleColor.DarkRed => "31;",
                    ConsoleColor.DarkGreen => "32;",
                    ConsoleColor.DarkYellow => "33;",
                    ConsoleColor.DarkBlue => "34;",
                    ConsoleColor.DarkMagenta => "35;",
                    ConsoleColor.DarkCyan => "36;",
                    ConsoleColor.Gray => "37;",

                    ConsoleColor.DarkGray => "90;",
                    ConsoleColor.Red => "91;",
                    ConsoleColor.Green => "92;",
                    ConsoleColor.Yellow => "93;",
                    ConsoleColor.Blue => "94;",
                    ConsoleColor.Magenta => "95;",
                    ConsoleColor.Cyan => "96;",
                    _ => "97;", // White
                }
            };
            return sgr;
        }

        /// <summary>
        /// Convert RGB values to a <see cref="ForegroundColor"/>.
        /// </summary>
        /// <param name="r">The red component of the color.</param>
        /// <param name="g">The green component of the color.</param>
        /// <param name="b">The blue component of the color.</param>
        /// <returns>A <see cref="ForegroundColor"/> converted from the specified RGB values.</returns>
        public static ForegroundColor FromRGB(byte r, byte g, byte b)
        {
            var sgr = new ForegroundColor
            {
                color = $"38;2;{r};{g};{b};"
            };
            return sgr;
        }

        /// <summary>
        /// Convert a <see cref="Color"/> to a <see cref="ForegroundColor"/>.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> to convert.</param>
        /// <returns>A <see cref="ForegroundColor"/> converted from the specified <see cref="Color"/>.</returns>
        public static ForegroundColor FromColor(Color color)
        {
            return FromRGB(color.R, color.G, color.B);
        }

        /// <summary>
        /// The default <see cref="ForegroundColor"/> of the console.
        /// </summary>
        /// <returns>The default <see cref="ForegroundColor"/> of the console.</returns>
        public static ForegroundColor DefaultColor()
        {
            var sgr = new ForegroundColor
            {
                color = "39;"
            };
            return sgr;
        }

        /// <summary>
        /// No change to the current color.
        /// </summary>
        /// <returns>A <see cref="ForegroundColor"/> that represents no change in the color.</returns>
        public static ForegroundColor NoChange()
        {
            var sgr = new ForegroundColor
            {
                color = ""
            };
            return sgr;
        }

        /// <summary>
        /// Returns a <see cref="string"/> to write to the console to activate the ANSI sequence.
        /// </summary>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public override string ToString() => color;
    }

    /// <summary>
    /// The color of the background.
    /// </summary>
    public class BackgroundColor
    {
        private string color = "";

        /// <summary>
        /// Convert a <see cref="ConsoleColor"/> to a <see cref="BackgroundColor"/>.
        /// </summary>
        /// <param name="color">The <see cref="ConsoleColor"/> to convert.</param>
        /// <returns>A <see cref="BackgroundColor"/> converted from the specified <see cref="ConsoleColor"/>.</returns>
        public static BackgroundColor FromConsoleColor(ConsoleColor color)
        {
            var sgr = new BackgroundColor
            {
                color = color switch
                {
                    ConsoleColor.Black => "40;",
                    ConsoleColor.DarkRed => "41;",
                    ConsoleColor.DarkGreen => "42;",
                    ConsoleColor.DarkYellow => "43;",
                    ConsoleColor.DarkBlue => "44;",
                    ConsoleColor.DarkMagenta => "45;",
                    ConsoleColor.DarkCyan => "46;",
                    ConsoleColor.Gray => "47;",

                    ConsoleColor.DarkGray => "100;",
                    ConsoleColor.Red => "101;",
                    ConsoleColor.Green => "102;",
                    ConsoleColor.Yellow => "103;",
                    ConsoleColor.Blue => "104;",
                    ConsoleColor.Magenta => "105;",
                    ConsoleColor.Cyan => "106;",
                    _ => "107;", // White
                }
            };
            return sgr;
        }

        /// <summary>
        /// Convert RGB values to a <see cref="BackgroundColor"/>.
        /// </summary>
        /// <param name="r">The red component of the color.</param>
        /// <param name="g">The green component of the color.</param>
        /// <param name="b">The blue component of the color.</param>
        /// <returns>A <see cref="BackgroundColor"/> converted from the specified RGB values.</returns>
        public static BackgroundColor FromRGB(byte r, byte g, byte b)
        {
            var sgr = new BackgroundColor
            {
                color = $"48;2;{r};{g};{b};"
            };
            return sgr;
        }

        /// <summary>
        /// Convert a <see cref="Color"/> to a <see cref="BackgroundColor"/>.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> to convert.</param>
        /// <returns>A <see cref="BackgroundColor"/> converted from the specified <see cref="Color"/>.</returns>
        public static BackgroundColor FromColor(Color color)
        {
            return FromRGB(color.R, color.G, color.B);
        }

        /// <summary>
        /// The default <see cref="BackgroundColor"/> of the console.
        /// </summary>
        /// <returns>The default <see cref="BackgroundColor"/> of the console.</returns>
        public static BackgroundColor DefaultColor()
        {
            var sgr = new BackgroundColor
            {
                color = "49;"
            };
            return sgr;
        }

        /// <summary>
        /// No change to the current color.
        /// </summary>
        /// <returns>A <see cref="BackgroundColor"/> that represents no change in the color.</returns>
        public static BackgroundColor NoChange()
        {
            var sgr = new BackgroundColor
            {
                color = ""
            };
            return sgr;
        }

        /// <summary>
        /// Returns a <see cref="string"/> to write to the console to activate the ANSI sequence.
        /// </summary>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public override string ToString() => color;
    }

    /// <summary>
    /// The color of the underline.
    /// </summary>
    public class UnderLineColor
    {
        private string color = "";

        /// <summary>
        /// Convert RGB values to a <see cref="UnderLineColor"/>.
        /// </summary>
        /// <param name="r">The red component of the color.</param>
        /// <param name="g">The green component of the color.</param>
        /// <param name="b">The blue component of the color.</param>
        /// <returns>A <see cref="UnderLineColor"/> converted from the specified RGB values.</returns>
        public static UnderLineColor FromRGB(byte r, byte g, byte b)
        {
            var sgr = new UnderLineColor
            {
                color = $"58;2;{r};{g};{b};"
            };
            return sgr;
        }

        /// <summary>
        /// Convert a <see cref="Color"/> to a <see cref="UnderLineColor"/>.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> to convert.</param>
        /// <returns>A <see cref="UnderLineColor"/> converted from the specified <see cref="Color"/>.</returns>
        public static UnderLineColor FromColor(Color color)
        {
            return FromRGB(color.R, color.G, color.B);
        }

        /// <summary>
        /// The default <see cref="UnderLineColor"/> of the console.
        /// </summary>
        /// <returns>The default <see cref="UnderLineColor"/> of the console.</returns>
        public static UnderLineColor DefaultColor()
        {
            var sgr = new UnderLineColor
            {
                color = "59;"
            };
            return sgr;
        }

        /// <summary>
        /// No change to the current color.
        /// </summary>
        /// <returns>A <see cref="UnderLineColor"/> that represents no change in the color.</returns>
        public static UnderLineColor NoChange()
        {
            var sgr = new UnderLineColor
            {
                color = ""
            };
            return sgr;
        }

        /// <summary>
        /// Returns a <see cref="string"/> to write to the console to activate the ANSI sequence.
        /// </summary>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public override string ToString() => color;
    }
}
