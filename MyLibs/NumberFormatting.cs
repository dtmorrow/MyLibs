using MyLibs.Collections;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MyLibs
{
    /// <summary>
    /// Methods to format integers using Scientific Notation and Simplified Notation.
    /// </summary>
    public static class NumberFormatting
    {
        private static CircularArray<char> ToCharArray(ulong value, int length, out int startIndex, out int digitCount)
        {
            // Initialize a circular array of all '0'
            var digits = new CircularArray<char>(length);
            for (int i = 0; i < length; i++)
            {
                digits[i] = '0';
            }

            // Keep track of how many digits have been processed and where the first digit of the number is (digits will be added from the end)
            digitCount = 0;
            startIndex = length - 1;

            // Set the current digit, and "shift" the value right by dividing by 10. When value == 0, all digits have been shifted.
            while (value != 0)
            {
                digits[startIndex--] = (char)(value % 10 + '0');
                digitCount++;
                value /= 10;
            }
            startIndex++;

            return digits;
        }

        // Works exactly the same as ulong version
        private static CircularArray<char> ToCharArray(BigInteger value, int length, out int startIndex, out int digitCount)
        {
            var digits = new CircularArray<char>(length);
            for (int i = 0; i < length; i++)
            {
                digits[i] = '0';
            }

            digitCount = 0;
            startIndex = length - 1;

            while (value != 0)
            {
                digits[startIndex--] = (char)(value % 10 + '0');
                digitCount++;
                value /= 10;
            }
            startIndex++;

            return digits;
        }

        #region Simplified Notation
        /// <summary>
        /// Get the simplified power for a provided power. A simplified power is a power of 3 formatted as letters. For example, 10^3 = 'a', 10^6 = 'b', ... 10^(3 * 26) = 'z', 10^(3 * 27) == "aa", 10^(3 * 28) = "ab", etc.
        /// </summary>
        /// <param name="power">The power to convert.</param>
        /// <returns>A <see cref="string"/> containing the simplified power.</returns>
        public static string GetSimplifiedPower(int power)
        {
            int magnitude = power / 3;
            var sb = new StringBuilder();
            while (magnitude != 0)
            {
                char c = (char)('a' + ((magnitude - 1) % 26));
                sb.Insert(0, c);
                magnitude = (magnitude - 1) / 26;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Formats an unsigned integer using simplified notation. See <see cref="GetSimplifiedPower(int)"/> for description of simplified notation.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <param name="decimalPlaces">The number of decimal places to include in the format. Default: 2</param>
        /// <returns>A <see cref="string"/> containing the value formatted using simplified notation.</returns>
        public static string ToSimplifiedNotation(long value, int decimalPlaces = 2)
        {
            bool negative = value < 0;
            if (negative)
            {
                value = -value;
            }
            return ToSimplifiedNotation(negative, (ulong)value, decimalPlaces);
        }

        /// <summary>
        /// Formats a signed integer using simplified notation. See <see cref="GetSimplifiedPower(int)"/> for description of simplified notation.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <param name="decimalPlaces">The number of decimal places to include in the format. Default: 2</param>
        /// <returns>A <see cref="string"/> containing the value formatted using simplified notation.</returns>
        public static string ToSimplifiedNotation(ulong value, int decimalPlaces = 2)
        {
            return ToSimplifiedNotation(false, value, decimalPlaces);
        }

        private static string ToSimplifiedNotation(bool negative, ulong value, int decimalPlaces)
        {
            if (value < 1000)
            {
                return $"{(negative ? "-" : "")}{value}";
            }

            StringBuilder sb = new StringBuilder(8);

            if (negative)
            {
                sb.Append('-');
            }

            var digits = ToCharArray(value, decimalPlaces + 3, out int startIndex, out int digitCount);

            int beforeDecimal = digitCount % 3;
            if (beforeDecimal == 0) beforeDecimal = 3;

            for (int i = 0; i < beforeDecimal; i++)
            {
                sb.Append(digits[startIndex++]);
            }

            if (decimalPlaces > 0)
            {
                sb.Append('.');

                for (int i = 0; i < decimalPlaces; i++)
                {
                    sb.Append(digits[i + startIndex]);
                }
            }

            sb.Append(GetSimplifiedPower(digitCount - 1));

            return sb.ToString();
        }

        /// <summary>
        /// Formats a <see cref="BigInteger"/> using simplified notation. See <see cref="GetSimplifiedPower(int)"/> for description of simplified notation.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <param name="decimalPlaces">The number of decimal places to include in the format. Default: 2</param>
        /// <returns>A <see cref="string"/> containing the value formatted using simplified notation.</returns>
        public static string ToSimplifiedNotation(BigInteger value, int decimalPlaces = 2)
        {
            bool negative = value < 0;
            if (negative)
            {
                value = -value;
            }

            if (value < 1000)
            {
                return $"{(negative ? "-" : "")}{value}";
            }

            StringBuilder sb = new StringBuilder(8);

            if (negative)
            {
                sb.Append('-');
            }

            var digits = ToCharArray(value, decimalPlaces + 3, out int startIndex, out int digitCount);

            int beforeDecimal = digitCount % 3;
            if (beforeDecimal == 0) beforeDecimal = 3;

            for (int i = 0; i < beforeDecimal; i++)
            {
                sb.Append(digits[startIndex++]);
            }

            if (decimalPlaces > 0)
            {
                sb.Append('.');

                for (int i = 0; i < decimalPlaces; i++)
                {
                    sb.Append(digits[i + startIndex]);
                }
            }

            sb.Append(GetSimplifiedPower(digitCount - 1));

            return sb.ToString();
        }
        #endregion

        #region Scientific Notation
        /// <summary>
        /// Types of scientific notation formatting.
        /// </summary>
        public enum ScientificNotationFormat
        {
            /// <summary>
            /// 1,000 -&gt; 1e+3
            /// </summary>
            LowercaseE,
            /// <summary>
            /// 1,000 -&gt; 1E+3
            /// </summary>
            UppercaseE,
            /// <summary>
            /// 1,000 -&gt; 1x10^3
            /// </summary>
            Multiplication
        }

        /// <summary>
        /// Format an unsigned integer using scientific notation.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <param name="decimalPlaces">The number of decimal places to include in the format. Default: 2</param>
        /// <param name="format">The type of scientific notation format to use. Default: LowercaseE</param>
        /// <returns>A <see cref="string"/> containing the value formatted using scientific notation.</returns>
        public static string ToScientificNotation(ulong value, int decimalPlaces = 2, ScientificNotationFormat format = ScientificNotationFormat.LowercaseE)
        {
            return ToScientificNotation(false, value, decimalPlaces, format);
        }

        /// <summary>
        /// Format a signed integer using scientific notation.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <param name="decimalPlaces">The number of decimal places to include in the format. Default: 2</param>
        /// <param name="format">The type of scientific notation format to use. Default: LowercaseE</param>
        /// <returns>A <see cref="string"/> containing the value formatted using scientific notation.</returns>
        public static string ToScientificNotation(long value, int decimalPlaces = 2, ScientificNotationFormat format = ScientificNotationFormat.LowercaseE)
        {
            bool negative = value < 0;
            if (negative)
            {
                value = -value;
            }
            return ToScientificNotation(negative, (ulong)value, decimalPlaces, format);
        }


        private static string ToScientificNotation(bool negative, ulong value, int decimalPlaces, ScientificNotationFormat format = ScientificNotationFormat.LowercaseE)
        {
            StringBuilder sb = new StringBuilder(decimalPlaces + 8);

            if (negative)
            {
                sb.Append('-');
            }

            var digits = ToCharArray(value, decimalPlaces + 1, out int startIndex, out int digitCount);

            if (digitCount < decimalPlaces + 1)
            {
                for (int i = 0; i < digitCount; i++)
                {
                    sb.Append(digits[i + startIndex]);
                }
                return sb.ToString();
            }

            sb.Append(digits[startIndex++]);

            if (decimalPlaces > 0)
            {
                sb.Append('.');

                for (int i = 0; i < decimalPlaces; i++)
                {
                    sb.Append(digits[i + startIndex]);
                }
            }

            switch (format)
            {
                case ScientificNotationFormat.Multiplication: sb.Append("x10^"); break;
                case ScientificNotationFormat.UppercaseE: sb.Append("E+"); break;
                case ScientificNotationFormat.LowercaseE:
                default: sb.Append("e+"); break;
            }

            sb.Append(digitCount - 1);

            return sb.ToString();
        }

        /// <summary>
        /// Format a <see cref="BigInteger"/> using scientific notation.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <param name="decimalPlaces">The number of decimal places to include in the format. Default: 2</param>
        /// <param name="format">The type of scientific notation format to use. Default: LowercaseE</param>
        /// <returns>A <see cref="string"/> containing the value formatted using scientific notation.</returns>
        public static string ToScientificNotation(BigInteger value, int decimalPlaces = 2, ScientificNotationFormat format = ScientificNotationFormat.LowercaseE)
        {
            StringBuilder sb = new StringBuilder(decimalPlaces + 8);

            if (value < 0)
            {
                value = -value;
                sb.Append('-');
            }

            var digits = ToCharArray(value, decimalPlaces + 1, out int startIndex, out int digitCount);

            if (digitCount < decimalPlaces + 1)
            {
                for (int i = 0; i < digitCount; i++)
                {
                    sb.Append(digits[i + startIndex]);
                }
                return sb.ToString();
            }

            sb.Append(digits[startIndex++]);

            if (decimalPlaces > 0)
            {
                sb.Append('.');

                for (int i = 0; i < decimalPlaces; i++)
                {
                    sb.Append(digits[i + startIndex]);
                }
            }

            switch (format)
            {
                case ScientificNotationFormat.Multiplication: sb.Append("x10^"); break;
                case ScientificNotationFormat.UppercaseE: sb.Append("E+"); break;
                case ScientificNotationFormat.LowercaseE:
                default: sb.Append("e+"); break;
            }

            sb.Append(digitCount - 1);

            return sb.ToString();
        }
        #endregion
    }
}
