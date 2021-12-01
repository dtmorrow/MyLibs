using MyLibs.Collections;
using MyLibs.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace MyLibs
{
    /// <summary>
    /// Methods for strings.
    /// </summary>
    public static class StringUtilities
    {
        /// <summary>
        /// "Rotate" letters by a certain amount (e.g. RotateLetters("a", 1) = "b", RotateLetters("a", 2) = "c").
        /// </summary>
        /// <param name="value">The string to rotate letters of.</param>
        /// <param name="rotate">The amount to rotate letters by.</param>
        /// <returns>A <see cref="string"/> with letters rotated.</returns>
        public static string RotateLetters(string value, int rotate)
        {
            var sb = new StringBuilder(value.Length);
            foreach (var c in value)
            {
                if (c >= 'a' && c <= 'z')
                {
                    sb.Append((char)((c - 'a' + rotate) % 26 + 'a'));
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    sb.Append((char)((c - 'A' + rotate) % 26 + 'A'));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Convert line endings to Unix format (i.e. "\r\n" -&gt; "\n").
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>A <see cref="string"/> with line endings converted to "\n"</returns>
        public static string ConvertLineEndingsToUnix(string value)
        {
            using var input = new StringReader(value);
            using var output = new StringWriter();

            FileUtilities.ConvertLineEndingsToUnixStream(input, output);
            return output.ToString();
        }

        /// <summary>
        /// Convert line endings to Windows format (i.e. "\n" -&gt; "\r\n").
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>A <see cref="string"/> with line endings converted to "\r\n"</returns>
        public static string ConvertLineEndingsToWindows(string value)
        {
            using var input = new StringReader(value);
            using var output = new StringWriter();

            FileUtilities.ConvertLineEndingsToWindowsStream(input, output);
            return output.ToString();
        }

        /// <summary>
        /// Remove all line breaks from a string.
        /// </summary>
        /// <param name="value">The string to linearize.</param>
        /// <param name="spaceBetweenNewLines">If a space should be inputted between each removed line break.</param>
        /// <returns>A <see cref="string"/> that has been linearized from the original.</returns>
        public static string LinearizeString(string value, bool spaceBetweenNewLines)
        {
            using var input = new StringReader(value);
            using var output = new StringWriter();

            FileUtilities.LinearizeStream(input, output, spaceBetweenNewLines);
            return output.ToString();
        }

        /// <summary>
        /// Remove double spaces from a string.
        /// </summary>
        /// <param name="value">The string to remove double spaces from.</param>
        /// <returns>A <see cref="string"/> that has the double spaces removed from the original.</returns>
        public static string RemoveDoubleSpaces(string value)
        {
            var sb = new StringBuilder(value.Length);
            bool space = false;
            foreach (var c in value)
            {
                if (c == ' ')
                {
                    // Only append the first space we encounter until we encounter another non-space character.
                    if (!space)
                    {
                        space = true;
                        sb.Append(c);
                    }
                }
                else
                {
                    space = false;
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Determine if a character can be interpreted as a hexadecimal character.
        /// </summary>
        /// <param name="c">The character to check.</param>
        /// <returns>true if the character can be interpreted as a hexadecimal character; otherwise false.</returns>
        public static bool IsHexChar(char c)
        {
            return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
        }

        /// <summary>
        /// Convert two hexadecimal characters to a byte.
        /// </summary>
        /// <param name="ms">The most-significant character of a byte.</param>
        /// <param name="ls">The least-significant character of a byte.</param>
        /// <returns>A <see cref="byte"/> converted from the character representation.</returns>
        public static byte HexCharsToByte(char ms, char ls)
        {
            return (byte)(HexCharToMostSignificantNibble(ms) | HexCharToLeastSignificantNibble(ls));
        }

        /// <summary>
        /// Convert a hexadecimal character representing the least-significant nibble of a byte.
        /// </summary>
        /// <param name="hex">The least-significant character of a byte.</param>
        /// <returns>A <see cref="byte"/> with the least-significant nibble converted from the hex character.</returns>
        public static byte HexCharToLeastSignificantNibble(char hex)
        {
            switch (hex)
            {
                case '0': return 0x00;
                case '1': return 0x01;
                case '2': return 0x02;
                case '3': return 0x03;
                case '4': return 0x04;
                case '5': return 0x05;
                case '6': return 0x06;
                case '7': return 0x07;
                case '8': return 0x08;
                case '9': return 0x09;
                case 'a':
                case 'A': return 0x0A;
                case 'b':
                case 'B': return 0x0B;
                case 'c':
                case 'C': return 0x0C;
                case 'd':
                case 'D': return 0x0D;
                case 'e':
                case 'E': return 0x0E;
                case 'f':
                case 'F': return 0x0F;
                default: throw new ArgumentException("Invalid Hex Char.");
            }
        }

        /// <summary>
        /// Convert a hexadecimal character representing the most-significant nibble of a byte.
        /// </summary>
        /// <param name="hex">The most-significant character of a byte.</param>
        /// <returns>A <see cref="byte"/> with the most-significant nibble converted from the hex character.</returns>
        public static byte HexCharToMostSignificantNibble(char hex)
        {
            switch (hex)
            {
                case '0': return 0x00;
                case '1': return 0x10;
                case '2': return 0x20;
                case '3': return 0x30;
                case '4': return 0x40;
                case '5': return 0x50;
                case '6': return 0x60;
                case '7': return 0x70;
                case '8': return 0x80;
                case '9': return 0x90;
                case 'a':
                case 'A': return 0xA0;
                case 'b':
                case 'B': return 0xB0;
                case 'c':
                case 'C': return 0xC0;
                case 'd':
                case 'D': return 0xD0;
                case 'e':
                case 'E': return 0xE0;
                case 'f':
                case 'F': return 0xF0;
                default: throw new ArgumentException("Invalid Hex Char.");
            }
        }

        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/#string-escape-sequences
        /// <summary>
        /// Escape special and non-ASCII characters of a string.
        /// </summary>
        /// <param name="value">The string to escape.</param>
        /// <returns>The <see cref="string"/> with all special and non-ASCII characters escaped.</returns>
        public static string EscapeString(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var sb = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                var c = value[i];

                switch (c)
                {
                    // Escapes for Printable ASCII
                    case '"': sb.Append("\\\""); break; // \u0022
                    case '\'': sb.Append("\\'"); break; // \u0027
                    case '\\': sb.Append("\\\\"); break; // \u005C

                    // Escapes for Non-Printable ASCII
                    case '\0': sb.Append("\\0"); break; // \u0000
                    case '\u0001': sb.Append("\\u0001"); break;
                    case '\u0002': sb.Append("\\u0002"); break;
                    case '\u0003': sb.Append("\\u0003"); break;
                    case '\u0004': sb.Append("\\u0004"); break;
                    case '\u0005': sb.Append("\\u0005"); break;
                    case '\u0006': sb.Append("\\u0006"); break;
                    case '\a': sb.Append("\\a"); break; // \u0007
                    case '\b': sb.Append("\\b"); break; // \u0008
                    case '\t': sb.Append("\\t"); break; // \u0009
                    case '\n': sb.Append("\\n"); break; // \u000A
                    case '\v': sb.Append("\\v"); break; // \u000B
                    case '\f': sb.Append("\\f"); break; // \u000C
                    case '\r': sb.Append("\\r"); break; // \u000D
                    case '\u000E': sb.Append("\\u000E"); break;
                    case '\u000F': sb.Append("\\u000F"); break;
                    case '\u0010': sb.Append("\\u0010"); break;
                    case '\u0011': sb.Append("\\u0011"); break;
                    case '\u0012': sb.Append("\\u0012"); break;
                    case '\u0013': sb.Append("\\u0013"); break;
                    case '\u0014': sb.Append("\\u0014"); break;
                    case '\u0015': sb.Append("\\u0015"); break;
                    case '\u0016': sb.Append("\\u0016"); break;
                    case '\u0017': sb.Append("\\u0017"); break;
                    case '\u0018': sb.Append("\\u0018"); break;
                    case '\u0019': sb.Append("\\u0019"); break;
                    case '\u001A': sb.Append("\\u001A"); break;
                    case '\u001B': sb.Append("\\u001B"); break;
                    case '\u001C': sb.Append("\\u001C"); break;
                    case '\u001D': sb.Append("\\u001D"); break;
                    case '\u001E': sb.Append("\\u001E"); break;
                    case '\u001F': sb.Append("\\u001F"); break;
                    case '\u007F': sb.Append("\\u007F"); break;
                    default:
                        // Print ASCII characters verbatim
                        if (c < 0x80)
                        {
                            sb.Append(c);
                        }
                        // If character is high surrogate of UTF-16 pair, convert to UTF-32
                        else if (char.IsHighSurrogate(c))
                        {
                            var next = i + 1;

                            // Unpaired High Surrogate Is Converted to '�'
                            if (next == value.Length)
                            {
                                sb.Append("\\uFFFD");
                                break;
                            }

                            char highSurrogate = c;
                            char lowSurrogate = value[next];

                            if (char.IsLowSurrogate(lowSurrogate))
                            {
                                var utf32 = char.ConvertToUtf32(highSurrogate, lowSurrogate);
                                sb.Append($"\\U{utf32:X8}");
                                i++; // Move to next character since we processed both high and low surrogates
                            }
                            else
                            {
                                sb.Append("\\uFFFD"); // Append '�' in place of unpaired high surrogate
                            }
                        }
                        // Unpaired Low Surrogate Is Converted to '�'
                        else if (char.IsLowSurrogate(c))
                        {
                            sb.Append("\\uFFFD");
                        }
                        // Convert all other characters to UTF-16
                        else
                        {
                            sb.Append($"\\u{((int)c):X4}");
                        }
                        break;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Unescape special and non-ASCII characters of a string.
        /// </summary>
        /// <param name="value">The string to unescape.</param>
        /// <returns>The <see cref="string"/> with all special and non-ASCII characters unescaped.</returns>
        public static string UnescapeString(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var sb = new StringBuilder();

            using (var reader = new StringReader(value))
            {
                while (true)
                {
                    if (!reader.TryReadChar(out char read))
                    {
                        break;
                    }

                    if (read == '\\')
                    {
                        sb.Append(ReadEscapedCharacter(reader));
                    }
                    else
                    {
                        sb.Append(read);
                    }
                }
            }

            return sb.ToString();
        }

        private static string ReadEscapedCharacter(TextReader reader, bool lookingForLowSurrogate = false)
        {
            if (!reader.TryReadChar(out char seq))
            {
                throw new ArgumentException("Encountered hanging '\\' at end of string.");
            }

            if (seq == 'u')
            {
                var c = ReadUtf16(reader);

                if (char.IsHighSurrogate(c))
                {
                    return ReadSurrogatePair(reader, c);
                }
                else if (char.IsLowSurrogate(c))
                {
                    if (lookingForLowSurrogate)
                    {
                        return c.ToString();
                    }
                    else
                    {
                        return "\uFFFD";
                    }
                }
                else
                {
                    return c.ToString();
                }
            }
            else if (seq == 'U')
            {
                return ReadUtf32(reader);
            }
            else if (seq == 'x')
            {
                var c = ReadHex(reader);

                if (char.IsHighSurrogate(c))
                {
                    return ReadSurrogatePair(reader, c);
                }
                else if (char.IsLowSurrogate(c))
                {
                    if (lookingForLowSurrogate)
                    {
                        return c.ToString();
                    }
                    else
                    {
                        return "\uFFFD";
                    }
                }
                else
                {
                    return c.ToString();
                }
            }
            else
            {
                return UnescapeSingleChar(seq).ToString();
            }
        }

        private static string ReadSurrogatePair(TextReader reader, char c)
        {
            if (reader.TryPeekChar(out char next))
            {
                if (next == '\\')
                {
                    reader.Read();
                    var readNext = ReadEscapedCharacter(reader, true);

                    if (readNext.Length == 1 && char.IsLowSurrogate(readNext[0]))
                    {
                        return $"{c}{readNext}";
                    }
                    else
                    {
                        return $"\uFFFD{readNext}";
                    }
                }
                else if (char.IsLowSurrogate(next))
                {
                    reader.Read();
                    return $"{c}{next}";
                }
            }

            return "\uFFFD"; // return '�'
        }

        private static char ReadUtf16(TextReader reader)
        {
            if (reader.TryReadChar(out char h1) && reader.TryReadChar(out char h2) && reader.TryReadChar(out char h3) && reader.TryReadChar(out char h4))
            {
                return UnescapeUTF16(h1, h2, h3, h4);
            }
            else
            {
                throw new ArgumentException("Not enough characters to decode UTF-16 escape sequence.");
            }
        }

        private static string ReadUtf32(TextReader reader)
        {
            if (reader.TryReadChar(out char h1) && reader.TryReadChar(out char h2) && reader.TryReadChar(out char h3) && reader.TryReadChar(out char h4) && reader.TryReadChar(out char h5) && reader.TryReadChar(out char h6) && reader.TryReadChar(out char h7) && reader.TryReadChar(out char h8))
            {
                return UnescapeUTF32(h1, h2, h3, h4, h5, h6, h7, h8);
            }
            else
            {
                throw new ArgumentException("Not enough characters to decode UTF-32 escape sequence.");
            }
        }

        private static char ReadHex(TextReader reader)
        {
            char h1 = '0';
            char h2 = '0';
            char h3 = '0';
            char h4;

            if (reader.TryReadChar(out char c1))
            {
                if (IsHexChar(c1))
                {
                    h4 = c1;
                }
                else
                {
                    throw new ArgumentException("First character of Hexadecimal escape sequence is not a hexadecimal character.");
                }
            }
            else
            {
                throw new ArgumentException("Not enough characters to decode Hexadecimal escape sequence.");
            }

            // "\x" is variable length, so we must determine how many characters are part of the escape sequence.
            if (reader.TryPeekChar(out char c2) && IsHexChar(c2))
            {
                h3 = c1;
                h4 = c2;
                reader.Read();

                if (reader.TryPeekChar(out char c3) && IsHexChar(c3))
                {
                    h2 = c1;
                    h3 = c2;
                    h4 = c3;
                    reader.Read();

                    if (reader.TryPeekChar(out char c4) && IsHexChar(c4))
                    {
                        h1 = c1;
                        h2 = c2;
                        h3 = c3;
                        h4 = c4;
                        reader.Read();
                    }
                }
            }

            return UnescapeUTF16(h1, h2, h3, h4);
        }

        private static char UnescapeSingleChar(char c)
        {
            return c switch
            {
                '\'' => '\'',
                '\"' => '\"',
                '\\' => '\\',
                '0' => '\0',
                'a' => '\a',
                'b' => '\b',
                'f' => '\f',
                'n' => '\n',
                'r' => '\r',
                't' => '\t',
                'v' => '\v',
                _ => throw new ArgumentException($"Invalid escape sequence: '\\{c}'"),
            };
        }

        private static char UnescapeUTF16(char h1, char h2, char h3, char h4)
        {
            return (char)(HexCharsToByte(h1, h2) << 8 | HexCharsToByte(h3, h4));
        }

        private static string UnescapeUTF32(char h1, char h2, char h3, char h4, char h5, char h6, char h7, char h8)
        {
            var utf32 = HexCharsToByte(h1, h2) << 24 | HexCharsToByte(h3, h4) << 16 | HexCharsToByte(h5, h6) << 8 | HexCharsToByte(h7, h8);

            // Unicode value outside of valid range
            if (utf32 < 0 || utf32 > 0x0010FFFF || (utf32 >= 0x0000D800 && utf32 <= 0x0000DFFF))
            {
                return "\uFFFD"; // '�'
            }

            return char.ConvertFromUtf32(utf32);
        }
    }
}
