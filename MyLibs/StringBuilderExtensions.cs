using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLibs
{
    /// <summary>
    /// Extension to <see cref="StringBuilder"/>.
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Removes all leading and trailing white-space characters from the <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to trim.</param>
        /// <returns>A reference to this instance after the trim operation has completed.</returns>
        public static StringBuilder Trim(this StringBuilder sb)
        {
            return sb.TrimStart().TrimEnd();
        }

        /// <summary>
        /// Removes all the leading white-space characters from the <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to trim.</param>
        /// <returns>A reference to this instance after the trim operation has completed.</returns>
        public static StringBuilder TrimStart(this StringBuilder sb)
        {
            if (sb == null)
            {
                throw new NullReferenceException();
            }

            if (sb.Length == 0)
            {
                return sb;
            }

            int remove = 0;
            for (int i = 0; i < sb.Length; i++)
            {
                if (!char.IsWhiteSpace(sb[i]))
                {
                    break;
                }
                remove++;
            }
            sb.Remove(0, remove);

            return sb;
        }

        /// <summary>
        /// Removes all the trailing white-space characters from the <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to trim.</param>
        /// <returns>A reference to this instance after the trim operation has completed.</returns>
        public static StringBuilder TrimEnd(this StringBuilder sb)
        {
            if (sb == null)
            {
                throw new NullReferenceException();
            }

            if (sb.Length == 0)
            {
                return sb;
            }

            int remove = 0;
            for (int i = sb.Length - 1; i >= 0; i--)
            {
                if (!char.IsWhiteSpace(sb[i]))
                {
                    break;
                }
                remove++;
            }
            sb.Length -= remove;

            return sb;
        }

        /// <summary>
        /// Removes all leading and trailing occurrences of a set of characters specified in an array from the <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to trim.</param>
        /// <param name="trimChars">An array of Unicode characters to remove, or null.</param>
        /// <returns>A reference to this instance after the trim operation has completed. If trimChars is null or an empty array, white-space characters are removed instead.</returns>
        public static StringBuilder Trim(this StringBuilder sb, params char[] trimChars)
        {
            return sb.TrimStart(trimChars).TrimEnd(trimChars);
        }

        /// <summary>
        /// Removes all leading occurrences of a set of characters specified in an array from the <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to trim.</param>
        /// <param name="trimChars">An array of Unicode characters to remove, or null.</param>
        /// <returns>A reference to this instance after the trim operation has completed. If trimChars is null or an empty array, white-space characters are removed instead.</returns>
        public static StringBuilder TrimStart(this StringBuilder sb, params char[] trimChars)
        {
            if (sb == null)
            {
                throw new NullReferenceException();
            }

            if (trimChars == null || trimChars.Length == 0)
            {
                return sb.TrimStart();
            }

            if (sb.Length == 0)
            {
                return sb;
            }

            int remove = 0;
            for (int i = 0; i < sb.Length; i++)
            {
                if (!trimChars.Contains(sb[i]))
                {
                    break;
                }
                remove++;
            }
            sb.Remove(0, remove);

            return sb;
        }

        /// <summary>
        /// Removes all trailing occurrences of a set of characters specified in an array from the <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to trim.</param>
        /// <param name="trimChars">An array of Unicode characters to remove, or null.</param>
        /// <returns>A reference to this instance after the trim operation has completed. If trimChars is null or an empty array, white-space characters are removed instead.</returns>
        public static StringBuilder TrimEnd(this StringBuilder sb, params char[] trimChars)
        {
            if (sb == null)
            {
                throw new NullReferenceException();
            }

            if (trimChars == null || trimChars.Length == 0)
            {
                return sb.TrimEnd();
            }

            if (sb.Length == 0)
            {
                return sb;
            }

            int remove = 0;
            for (int i = sb.Length - 1; i >= 0; i--)
            {
                if (!trimChars.Contains(sb[i]))
                {
                    break;
                }
                remove++;
            }
            sb.Length -= remove;

            return sb;
        }

        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to search.</param>
        /// <param name="value">The <see cref="string"/> to seek.</param>
        /// <returns>true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false.</returns>
        public static bool Contains(this StringBuilder sb, string value)
        {
            return sb.IndexOf(value) != -1;
        }

        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to search.</param>
        /// <param name="value">The <see cref="string"/> to seek.</param>
        /// /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false.</returns>
        public static bool Contains(this StringBuilder sb, string value, StringComparison comparisonType)
        {
            return sb.IndexOf(value, comparisonType) != -1;
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified string in this <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to search.</param>
        /// <param name="value">The <see cref="string"/> to seek.</param>
        /// <returns>The zero-based index position of value if that string is found, or -1 if it is not. If value is System.String.Empty, the return value is 0.</returns>
        public static int IndexOf(this StringBuilder sb, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            int length = sb.Length;
            int searchLength = value.Length;

            for (int i = 0; i <= length - searchLength; i++)
            {
                int foundCount = 0;

                for (int j = 0; j < searchLength; j++)
                {
                    if (sb[i + j] == value[j])
                    {
                        foundCount++;
                    }
                }

                if (foundCount == searchLength)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified string in this <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to search.</param>
        /// <param name="value">The <see cref="string"/> to seek.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>The zero-based index position of value if that string is found, or -1 if it is not. If value is System.String.Empty, the return value is 0.</returns>
        public static int IndexOf(this StringBuilder sb, string value, StringComparison comparisonType)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            int length = sb.Length;
            int searchLength = value.Length;

            for (int i = 0; i <= length - searchLength; i++)
            {
                int foundCount = 0;

                for (int j = 0; j < searchLength; j++)
                {
                    if (string.Equals(sb[i + j].ToString(), value[j].ToString(), comparisonType))
                    {
                        foundCount++;
                    }
                }

                if (foundCount == searchLength)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
