using System;
using System.IO;
using System.Text;

namespace MyLibs.IO
{
    /// <summary>
    /// Extensions for streams.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Reads the next character from the <see cref="TextReader"/> and advances the character position by one character.
        /// </summary>
        /// <param name="reader">The <see cref="TextReader"/> to read from.</param>
        /// <param name="c">If a character was successfully read from the <see cref="TextReader"/>, the character that was read; otherwise the '\0' character.</param>
        /// <returns><see langword="true"/> if a character was successfully read from the <see cref="TextReader"/>; otherwise <see langword="false"/>.</returns>
        public static bool TryReadChar(this TextReader reader, out char c)
        {
            if (reader == null)
            {
                throw new NullReferenceException();
            }

            var read = reader.Read();

            if (read == -1)
            {
                c = '\0';
                return false;
            }

            c = (char)read;
            return true;
        }

        /// <summary>
        /// Reads the next character without changing the state of the <see cref="TextReader"/>.
        /// </summary>
        /// <param name="reader">The <see cref="TextReader"/> to peek from.</param>
        /// <param name="c">If a character was successfully peeked from the <see cref="TextReader"/>, the character that was peeked; otherwise the '\0' character.</param>
        /// <returns><see langword="true"/> if a character was successfully peeked from the <see cref="TextReader"/>; otherwise <see langword="false"/>.</returns>
        public static bool TryPeekChar(this TextReader reader, out char c)
        {
            if (reader == null)
            {
                throw new NullReferenceException();
            }

            var read = reader.Peek();

            if (read == -1)
            {
                c = '\0';
                return false;
            }

            c = (char)read;
            return true;
        }

        /// <summary>
        /// Reads the next character from the <see cref="TextReader"/> if the character fits within a single UTF-16 character, otherwise reads two characters from the <see cref="TextReader"/>. If the character being read is not a valid Unicode character, then an invalid character (\uFFFD/'�') is returned.
        /// </summary>
        /// <param name="reader">The <see cref="TextReader"/> to read from.</param>
        /// <param name="str">The read characters from the stream if the read is successful, "\uFFFD" if the read characters are invalid Unicode, or <see langword="null"/> if the read is unsuccessful.</param>
        /// <returns><see langword="true"/> if any characters are read from the stream, even invalid characters; otherwise <see langword="false"/>.</returns>
        public static bool TryReadCharFull(this TextReader reader, out string str)
        {
            if (reader == null)
            {
                throw new NullReferenceException();
            }

            if (reader.TryReadChar(out char c))
            {
                if (char.IsHighSurrogate(c))
                {
                    if (reader.TryReadChar(out char lowSurrogate) && char.IsLowSurrogate(lowSurrogate))
                    {
                        str = $"{c}{lowSurrogate}";
                    }
                    else
                    {
                        str = "\uFFFD"; // '�'
                    }
                }
                else if (char.IsLowSurrogate(c))
                {
                    str = "\uFFFD"; // '�'
                }
                else
                {
                    str = c.ToString();
                }

                return true;
            }
            else
            {
                str = null;
                return false;
            }
        }

        /// <summary>
        /// Read a string from the <see cref="TextReader"/> until a '\0' character is encountered.
        /// </summary>
        /// <param name="reader">The <see cref="TextReader"/> to read from.</param>
        /// <returns>A <see cref="string"/> from the current position in the <see cref="TextReader"/> until the first '\0' character.</returns>
        public static string ReadCString(this TextReader reader)
        {
            if (!reader.TryPeekChar(out char _))
            {
                return null;
            }

            var sb = new StringBuilder();

            while (reader.TryReadChar(out char c) && c != '\0')
            {
                sb.Append(c);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Reads a byte from the stream and advances the position within the stream by one byte.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to read from.</param>
        /// <param name="b">If a <see cref="byte"/> was successfully read from the stream, the <see cref="byte"/> read; otherwise, 0.</param>
        /// <returns><see langword="true"/> if a byte was successfully read from the <see cref="Stream"/>; otherwise, <see langword="false"/>.</returns>
        public static bool TryReadByte(this Stream stream, out byte b)
        {
            var read = stream.ReadByte();

            if (read == -1)
            {
                b = 0;
                return false;
            }

            b = (byte)read;
            return true;
        }

        /// <summary>
        /// Reads the specified number of bytes from the current stream and writes them to another stream.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to copy from.</param>
        /// <param name="destination">The <see cref="Stream"/> to copy to.</param>
        /// <param name="count">The number of bytes to copy.</param>
        /// <param name="bufferSize">The size of the buffer. This value must be greater than zero. The default size is 81920.</param>
        public static void CopyTo(this Stream stream, Stream destination, long count, int bufferSize = 81920)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize), bufferSize, $"Positive number required.");
            }

            var buffer = new byte[bufferSize];

            while (true)
            {
                var read = stream.Read(buffer, 0, (count < buffer.Length) ? (int)count : buffer.Length);
                destination.Write(buffer, 0, read);
                count -= read;

                if (read == 0 || count == 0)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Skip the specified number of bytes in the <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to skip bytes from.</param>
        /// <param name="numBytes">The number of bytes to skip.</param>
        public static void SkipBytes(this Stream stream, long numBytes)
        {
            if (stream.CanSeek)
            {
                stream.Seek(numBytes, SeekOrigin.Current);
            }
            else
            {
                stream.CopyTo(Stream.Null, numBytes);
            }
        }

        /// <summary>
        /// Skip to the end of the <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to skip to the end of.</param>
        public static void SkipToEnd(this Stream stream)
        {
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.End);
            }
            else
            {
                stream.CopyTo(Stream.Null);
            }
        }
    }
}
