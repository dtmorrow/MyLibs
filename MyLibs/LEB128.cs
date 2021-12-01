using MyLibs.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace MyLibs
{
    /// <summary>
    /// Methods to encode and decode integers using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see> encoding.
    /// </summary>
    public static class LEB128
    {
        /// <summary>
        /// Get the minimum number of bytes that an unsigned integer could fit in.
        /// </summary>
        /// <param name="value">The value to calculate to the size for.</param>
        /// <returns>An integer containing the minimum number of bytes the value could fit in. Either 1, 2, 4, or 8.</returns>
        public static int MinimumByteSize(ulong value)
        {
            if (value <= byte.MaxValue)
            {
                return sizeof(byte);
            }
            else if (value <= ushort.MaxValue)
            {
                return sizeof(ushort);
            }
            else if (value <= uint.MaxValue)
            {
                return sizeof(uint);
            }
            else
            {
                return sizeof(ulong);
            }
        }

        /// <summary>
        /// Get the minimum number of bytes that a signed integer could fit in.
        /// </summary>
        /// <param name="value">The value to calculate to the size for.</param>
        /// <returns>An integer containing the minimum number of bytes the value could fit in. Either 1, 2, 4, or 8.</returns>
        public static int MinimumByteSize(long value)
        {
            if (value <= sbyte.MaxValue && value >= sbyte.MinValue)
            {
                return sizeof(sbyte);
            }
            else if (value <= short.MaxValue && value >= short.MinValue)
            {
                return sizeof(short);
            }
            else if (value <= int.MaxValue && value >= int.MinValue)
            {
                return sizeof(int);
            }
            else
            {
                return sizeof(long);
            }
        }

        /// <summary>
        /// Encode an unsigned integer to an <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see> byte array.
        /// </summary>
        /// <param name="value">The value to be encoded.</param>
        /// <returns>A byte array containing the value encoded using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see>.</returns>
        public static byte[] EncodeLEB128(ulong value)
        {
            var data = new List<byte>();

            while (true)
            {
                // Get low 7-bits
                var b = value & 0b0111_1111;

                // Shift out read bits
                value >>= 7;

                // If value is 0, then we've read in all available bits. Return.
                if (value == 0)
                {
                    data.Add((byte)b);
                    return data.ToArray();
                }
                
                // Else, we need to read in more bits. Set high bit and continue
                data.Add((byte)(b | 0b1000_0000));
            }
        }

        /// <summary>
        /// Encode a signed integer to an <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see> byte array.
        /// </summary>
        /// <param name="value">The value to be encoded.</param>
        /// <returns>A byte array containing the value encoded using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see>.</returns>
        public static byte[] EncodeLEB128(long value)
        {
            var data = new List<byte>();

            while (true)
            {
                // Get low 7-bits
                var b = (value & 0b0111_1111);

                // Shift out read bits
                value >>= 7;

                // Are low 6-bits positive?
                var positive = (b & 0b0100_0000) == 0;

                // If low 7-bits are positive and value == 0, or if low 7-bits are negative and value == -1, then we've read in all available bits. Return.
                if ((value == 0 && positive) || (value == -1 && !positive))
                {
                    data.Add((byte)b);
                    return data.ToArray();
                }

                // Else, we need to read in more bits. Set high bit and continue
                data.Add((byte)(b | 0b1000_0000));
            }
        }

        /// <summary>
        /// Encode a <see cref="BigInteger"/> to an <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see> byte array.
        /// </summary>
        /// <param name="value">The value to be encoded.</param>
        /// <returns>A byte array containing the value encoded using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see>.</returns>
        public static byte[] EncodeLEB128(BigInteger value)
        {
            var data = new List<byte>();

            while (true)
            {
                // Get low 7-bits
                var b = (value & 0b0111_1111);

                // Shift out read bits
                value >>= 7;

                // Are low 6-bits positive?
                var positive = (b & 0b0100_0000) == 0;

                // If low 7-bits are positive and value == 0, or if low 7-bits are negative and value == -1, then we've read in all available bits. Return.
                if ((value == 0 && positive) || (value == -1 && !positive))
                {
                    data.Add((byte)b);
                    return data.ToArray();
                }

                // Else, we need to read in more bits. Set high bit and continue
                data.Add((byte)(b | 0b1000_0000));
            }
        }

        /// <summary>
        /// Decode a byte array that has been encoded using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see> to an unsigned integer.
        /// </summary>
        /// <param name="data">A byte array that has been encoded using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see>.</param>
        /// <returns>An unsigned integer decoded from <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see>.</returns>
        public static ulong DecodeUnsignedLEB128(byte[] data)
        {
            ulong value = 0; // The return value
            int shift = 0;   // How many bits that have been read

            for (int i = 0; i < data.Length; i++)
            {
                var b = data[i];

                // Shift and set low 7-bits of byte into value
                value |= (b & 0b0111_1111UL) << shift;
                shift += 7;

                // If high bit is not set, we're done reading bytes
                if ((b & 0b1000_0000) == 0)
                {
                    return value;
                }
                // If high bit isn't set and we've read in more than 64-bits, then the value won't fit in a ulong
                else if (shift > 64)
                {
                    throw new ArgumentOutOfRangeException(nameof(data), "Data cannot fit into 64-bit value.");
                }
            }

            throw new ArgumentException("Reached end of byte array without finding ending byte.", nameof(data));
        }

        /// <summary>
        /// Decode a byte array that has been encoded using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see> to a signed integer.
        /// </summary>
        /// <param name="data">A byte array that has been encoded using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see>.</param>
        /// <returns>A signed integer decoded from <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see>.</returns>
        public static long DecodeSignedLEB128(byte[] data)
        {
            long value = 0; // The return value
            int shift = 0;  // How many bits that have been read

            for (int i = 0; i < data.Length; i++)
            {
                var b = data[i];

                // Shift and set low 7-bits of byte into value
                value |= (b & 0b0111_1111L) << shift;
                shift += 7;

                // If high bit is not set, we're done reading bytes
                if ((b & 0b1000_0000) == 0)
                {
                    // If value is positive and last byte is negative, extend sign bit
                    if (value > 0 && (b & 0b0100_0000) != 0)
                    {
                        value |= (-1L << shift);
                    }

                    return value;
                }
                // If high bit isn't set and we've read in more than 64-bits, then the value won't fit in a long
                else if (shift > 64)
                {
                    throw new ArgumentOutOfRangeException(nameof(data), "Data cannot fit into 64-bit value.");
                }
            }

            throw new ArgumentException("Reached end of byte array without finding ending byte.", nameof(data));
        }

        /// <summary>
        /// Decode a byte array that has been encoded using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see> to a <see cref="BigInteger"/>.
        /// </summary>
        /// <param name="data">A byte array that has been encoded using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see>.</param>
        /// <returns>A <see cref="BigInteger"/> decoded from <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see>.</returns>
        public static BigInteger DecodeBigLEB128(byte[] data)
        {
            BigInteger bitMask = 0b0111_1111;

            BigInteger value = 0; // The return value
            int shift = 0;        // How many bits that have been read

            for (int i = 0; i < data.Length; i++)
            {
                var b = data[i];

                // Shift and set low 7-bits of byte into value
                value |= (b & bitMask) << shift;
                shift += 7;

                // If high bit is not set, we're done reading bytes
                if ((b & 0b1000_0000) == 0)
                {
                    // If value is positive and last byte is negative, extend sign bit
                    if (value > 0 && (b & 0b0100_0000) != 0)
                    {
                        value |= (BigInteger.MinusOne << shift);
                    }

                    return value;
                }
            }

            throw new ArgumentException("Reached end of byte array without finding ending byte.", nameof(data));
        }

        /// <summary>
        /// Write an unsigned integer to a <see cref="Stream"/> encoded using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see>.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteLEB128(this Stream stream, ulong value)
        {
            while (true)
            {
                // Get low 7-bits
                var b = value & 0b0111_1111;

                // Shift out read bits
                value >>= 7;

                // If value is 0, then we've read in all available bits
                if (value == 0)
                {
                    stream.WriteByte((byte)b);
                    return;
                }
                
                // Else, we need to read in more bits. Set high bit and continue
                stream.WriteByte((byte)(b | 0b1000_0000));
            }
        }

        /// <summary>
        /// Write a signed integer to a <see cref="Stream"/> encoded using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see>.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteLEB128(this Stream stream, long value)
        {
            while (true)
            {
                // Get low 7-bits
                var b = (value & 0b0111_1111);

                // Shift out read bits
                value >>= 7;

                // Are low 6-bits positive?
                var positive = (b & 0b0100_0000) == 0;

                // If low 7-bits are positive and value == 0, or if low 7-bits are negative and value == -1, then we've read in all available bits. Return.
                if ((value == 0 && positive) || (value == -1 && !positive))
                {
                    stream.WriteByte((byte)b);
                    return;
                }

                // Else, we need to read in more bits. Set high bit and continue
                stream.WriteByte((byte)(b | 0b1000_0000));
            }
        }

        /// <summary>
        /// Write a <see cref="BigInteger"/> to a <see cref="Stream"/> encoded using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see>.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="value">The value to write to the stream.</param>
        public static void WriteLEB128(this Stream stream, BigInteger value)
        {
            while (true)
            {
                // Get low 7-bits
                var b = (value & 0b0111_1111);

                // Shift out read bits
                value >>= 7;

                // Are low 6-bits positive?
                var positive = (b & 0b0100_0000) == 0;

                // If low 7-bits are positive and value == 0, or if low 7-bits are negative and value == -1, then we've read in all available bits. Return.
                if ((value == 0 && positive) || (value == -1 && !positive))
                {
                    stream.WriteByte((byte)b);
                    return;
                }

                // Else, we need to read in more bits. Set high bit and continue
                stream.WriteByte((byte)(b | 0b1000_0000));
            }
        }

        /// <summary>
        /// Read an unsigned integer from a <see cref="Stream"/> that has been encoded using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see>.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <returns>An unsigned integer read from the stream.</returns>
        public static ulong ReadUnsignedLEB128(this Stream stream)
        {
            ulong value = 0; // The return value
            int shift = 0;   // How many bits that have been read

            while (stream.TryReadByte(out byte b))
            {
                // Shift and set low 7-bits of byte into value
                value |= (b & 0b0111_1111UL) << shift;
                shift += 7;

                // If high bit is not set, we're done reading bytes
                if ((b & 0b1000_0000) == 0)
                {
                    return value;
                }
                // If high bit isn't set and we've read in more than 64 bits, then the value won't fit in a ulong
                else if (shift > 64)
                {
                    throw new ArgumentOutOfRangeException(nameof(stream), "Data cannot fit into 64-bit value.");
                }
            }

            throw new EndOfStreamException("Reached end of stream without finding ending byte.");
        }

        /// <summary>
        /// Read a signed integer from a <see cref="Stream"/> that has been encoded using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see>.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <returns>A signed integer read from the stream.</returns>
        public static long ReadSignedLEB128(this Stream stream)
        {
            long value = 0; // The return value
            int shift = 0;  // How many bits that have been read

            while (stream.TryReadByte(out byte b))
            {
                // Shift and set low 7-bits of byte into value
                value |= (b & 0b0111_1111L) << shift;
                shift += 7;

                // If high bit is not set, we're done reading bytes
                if ((b & 0b1000_0000) == 0)
                {
                    // If value is positive and last byte is negative, extend sign bit
                    if (value > 0 && (b & 0b0100_0000) != 0)
                    {
                        value |= (-1L << shift);
                    }

                    return value;
                }
                // If high bit isn't set and we've read in more than 64 bits, then the value won't fit in a long
                else if (shift > 64)
                {
                    throw new ArgumentOutOfRangeException(nameof(stream), "Data cannot fit into 64-bit value.");
                }
            }

            throw new EndOfStreamException("Reached end of stream without finding ending byte.");
        }

        /// <summary>
        /// Read a <see cref="BigInteger"/> from a <see cref="Stream"/> that has been encoded using <see href="https://en.wikipedia.org/wiki/LEB128">LEB128</see>.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <returns>A <see cref="BigInteger"/> read from the stream.</returns>
        public static BigInteger ReadBigLEB128(this Stream stream)
        {
            BigInteger bitMask = 0b0111_1111;

            BigInteger value = 0; // The return value
            int shift = 0;        // How many bits that have been read

            while (stream.TryReadByte(out byte b))
            {
                // Shift and set low 7-bits of byte into value
                value |= (b & bitMask) << shift;
                shift += 7;

                // If high bit is not set, we're done reading bytes
                if ((b & 0b1000_0000) == 0)
                {
                    // If value is positive and last byte is negative, extend sign bit
                    if (value > 0 && (b & 0b0100_0000) != 0)
                    {
                        value |= (BigInteger.MinusOne << shift);
                    }

                    return value;
                }
            }

            throw new EndOfStreamException("Reached end of stream without finding ending byte.");
        }
    }
}
