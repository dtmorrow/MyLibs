using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibs.Tests
{
    [TestClass]
    public class StringUtilitiesTests
    {
        private static string RotateExpected(string expected)
        {
            return expected[1..] + expected[0];
        }

        [TestMethod]
        public void RotateLettersTest()
        {
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var expectedLower = lower;
            var expectedUpper = upper;

            for (int i = 1; i <= 26; i++)
            {
                expectedLower = RotateExpected(expectedLower);
                expectedUpper = RotateExpected(expectedUpper);

                Assert.AreEqual(expectedLower, StringUtilities.RotateLetters(lower, i));
                Assert.AreEqual(expectedUpper, StringUtilities.RotateLetters(upper, i));
            }

            const string normal = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string rot13 = "nopqrstuvwxyzabcdefghijklmNOPQRSTUVWXYZABCDEFGHIJKLM";

            Assert.AreEqual(rot13, StringUtilities.RotateLetters(normal, 13));
            Assert.AreEqual(normal, StringUtilities.RotateLetters(rot13, 13));
        }

        [TestMethod]
        public void LinearizeStringTest()
        {
            const string input = @"                
                                <xml>                                            
	                                <element>                                    
		                                <index>0</index>                         
		                                <name>The First Element</name>           
		                                <nested>                                 
			                                <alphabet>A</alphabet>               
			                                <alphabet>B</alphabet>               
			                                <alphabet>C</alphabet>               
		                                </nested>                                
	                                </element>                                   
                                    <element>                                    
                                        <index>1</index>                         
                                        <name>The Second Element</name>          
                                        <nested>                                 
                                            <alphabet>D</alphabet>               
                                            <alphabet>E</alphabet>               
                                            <alphabet>F</alphabet>               
                                        </nested>                                
                                    </element>                                   
	                                <element>                                    
		                                <index>2</index>                         
		                                <name>The Third Element</name>           
		                                <nested>                                 
			                                <alphabet>G</alphabet>               
			                                <alphabet>H</alphabet>               
			                                <alphabet>I</alphabet>               
		                                </nested>                                
	                                </element>                                   
                                </xml>         ";

            const string expectedNoSpaces = "<xml><element><index>0</index><name>The First Element</name><nested><alphabet>A</alphabet><alphabet>B</alphabet><alphabet>C</alphabet></nested></element><element><index>1</index><name>The Second Element</name><nested><alphabet>D</alphabet><alphabet>E</alphabet><alphabet>F</alphabet></nested></element><element><index>2</index><name>The Third Element</name><nested><alphabet>G</alphabet><alphabet>H</alphabet><alphabet>I</alphabet></nested></element></xml>";
            const string expectedSpaces = "<xml> <element> <index>0</index> <name>The First Element</name> <nested> <alphabet>A</alphabet> <alphabet>B</alphabet> <alphabet>C</alphabet> </nested> </element> <element> <index>1</index> <name>The Second Element</name> <nested> <alphabet>D</alphabet> <alphabet>E</alphabet> <alphabet>F</alphabet> </nested> </element> <element> <index>2</index> <name>The Third Element</name> <nested> <alphabet>G</alphabet> <alphabet>H</alphabet> <alphabet>I</alphabet> </nested> </element> </xml>";

            Assert.AreEqual(expectedNoSpaces, StringUtilities.LinearizeString(input, false));
            Assert.AreEqual(expectedSpaces, StringUtilities.LinearizeString(input, true));
        }

        [TestMethod]
        public void ConvertLineEndingsToUnixTest()
        {
            const string test1 = "a\r\nb\r\nc\r\n";
            Assert.AreEqual("a\nb\nc\n", StringUtilities.ConvertLineEndingsToUnix(test1));

            const string test2 = "a\rb\r\nc\r";
            Assert.AreEqual("a\rb\nc\r", StringUtilities.ConvertLineEndingsToUnix(test2));
        }

        [TestMethod]
        public void ConvertLineEndingsToWindowsTest()
        {
            const string test1 = "a\nb\nc\n";
            Assert.AreEqual("a\r\nb\r\nc\r\n", StringUtilities.ConvertLineEndingsToWindows(test1));

            const string test2 = "a\rb\r\nc\n";
            Assert.AreEqual("a\rb\r\nc\r\n", StringUtilities.ConvertLineEndingsToWindows(test2));
        }

        [TestMethod]
        public void RemoveDoubleSpacesTest()
        {
            const string test = "     a       b       c      d      ";
            Assert.AreEqual(" a b c d ", StringUtilities.RemoveDoubleSpaces(test));
        }

        [TestMethod]
        public void IsHexCharTest()
        {
            // Check all Hex Chars
            Assert.IsTrue(StringUtilities.IsHexChar('0'));
            Assert.IsTrue(StringUtilities.IsHexChar('1'));
            Assert.IsTrue(StringUtilities.IsHexChar('2'));
            Assert.IsTrue(StringUtilities.IsHexChar('3'));
            Assert.IsTrue(StringUtilities.IsHexChar('4'));
            Assert.IsTrue(StringUtilities.IsHexChar('5'));
            Assert.IsTrue(StringUtilities.IsHexChar('6'));
            Assert.IsTrue(StringUtilities.IsHexChar('7'));
            Assert.IsTrue(StringUtilities.IsHexChar('8'));
            Assert.IsTrue(StringUtilities.IsHexChar('9'));
            Assert.IsTrue(StringUtilities.IsHexChar('A'));
            Assert.IsTrue(StringUtilities.IsHexChar('B'));
            Assert.IsTrue(StringUtilities.IsHexChar('C'));
            Assert.IsTrue(StringUtilities.IsHexChar('D'));
            Assert.IsTrue(StringUtilities.IsHexChar('E'));
            Assert.IsTrue(StringUtilities.IsHexChar('F'));
            Assert.IsTrue(StringUtilities.IsHexChar('a'));
            Assert.IsTrue(StringUtilities.IsHexChar('b'));
            Assert.IsTrue(StringUtilities.IsHexChar('c'));
            Assert.IsTrue(StringUtilities.IsHexChar('d'));
            Assert.IsTrue(StringUtilities.IsHexChar('e'));
            Assert.IsTrue(StringUtilities.IsHexChar('f'));

            // Test for off-by-one errors
            Assert.IsFalse(StringUtilities.IsHexChar((char)('0' - 1)));
            Assert.IsFalse(StringUtilities.IsHexChar((char)('9' + 1)));
            Assert.IsFalse(StringUtilities.IsHexChar((char)('A' - 1)));
            Assert.IsFalse(StringUtilities.IsHexChar((char)('F' + 1)));
            Assert.IsFalse(StringUtilities.IsHexChar((char)('a' - 1)));
            Assert.IsFalse(StringUtilities.IsHexChar((char)('f' + 1)));

            // Test a few random characters
            Assert.IsFalse(StringUtilities.IsHexChar('\u0000'));
            Assert.IsFalse(StringUtilities.IsHexChar('\u0001'));
            Assert.IsFalse(StringUtilities.IsHexChar('\u0010'));
            Assert.IsFalse(StringUtilities.IsHexChar('\u0100'));
            Assert.IsFalse(StringUtilities.IsHexChar('\u1000'));
        }

        [TestMethod]
        public void HexCharsToByteTest()
        {
            Assert.AreEqual(0x00, StringUtilities.HexCharsToByte('0', '0'));
            Assert.AreEqual(0x01, StringUtilities.HexCharsToByte('0', '1'));
            Assert.AreEqual(0x02, StringUtilities.HexCharsToByte('0', '2'));
            Assert.AreEqual(0x03, StringUtilities.HexCharsToByte('0', '3'));
            Assert.AreEqual(0x04, StringUtilities.HexCharsToByte('0', '4'));
            Assert.AreEqual(0x05, StringUtilities.HexCharsToByte('0', '5'));
            Assert.AreEqual(0x06, StringUtilities.HexCharsToByte('0', '6'));
            Assert.AreEqual(0x07, StringUtilities.HexCharsToByte('0', '7'));
            Assert.AreEqual(0x08, StringUtilities.HexCharsToByte('0', '8'));
            Assert.AreEqual(0x09, StringUtilities.HexCharsToByte('0', '9'));
            Assert.AreEqual(0x0A, StringUtilities.HexCharsToByte('0', 'A'));
            Assert.AreEqual(0x0B, StringUtilities.HexCharsToByte('0', 'B'));
            Assert.AreEqual(0x0C, StringUtilities.HexCharsToByte('0', 'C'));
            Assert.AreEqual(0x0D, StringUtilities.HexCharsToByte('0', 'D'));
            Assert.AreEqual(0x0E, StringUtilities.HexCharsToByte('0', 'E'));
            Assert.AreEqual(0x0F, StringUtilities.HexCharsToByte('0', 'F'));
            Assert.AreEqual(0x10, StringUtilities.HexCharsToByte('1', '0'));
            Assert.AreEqual(0x11, StringUtilities.HexCharsToByte('1', '1'));
            Assert.AreEqual(0x12, StringUtilities.HexCharsToByte('1', '2'));
            Assert.AreEqual(0x13, StringUtilities.HexCharsToByte('1', '3'));
            Assert.AreEqual(0x14, StringUtilities.HexCharsToByte('1', '4'));
            Assert.AreEqual(0x15, StringUtilities.HexCharsToByte('1', '5'));
            Assert.AreEqual(0x16, StringUtilities.HexCharsToByte('1', '6'));
            Assert.AreEqual(0x17, StringUtilities.HexCharsToByte('1', '7'));
            Assert.AreEqual(0x18, StringUtilities.HexCharsToByte('1', '8'));
            Assert.AreEqual(0x19, StringUtilities.HexCharsToByte('1', '9'));
            Assert.AreEqual(0x1A, StringUtilities.HexCharsToByte('1', 'A'));
            Assert.AreEqual(0x1B, StringUtilities.HexCharsToByte('1', 'B'));
            Assert.AreEqual(0x1C, StringUtilities.HexCharsToByte('1', 'C'));
            Assert.AreEqual(0x1D, StringUtilities.HexCharsToByte('1', 'D'));
            Assert.AreEqual(0x1E, StringUtilities.HexCharsToByte('1', 'E'));
            Assert.AreEqual(0x1F, StringUtilities.HexCharsToByte('1', 'F'));
            Assert.AreEqual(0x20, StringUtilities.HexCharsToByte('2', '0'));
            Assert.AreEqual(0x21, StringUtilities.HexCharsToByte('2', '1'));
            Assert.AreEqual(0x22, StringUtilities.HexCharsToByte('2', '2'));
            Assert.AreEqual(0x23, StringUtilities.HexCharsToByte('2', '3'));
            Assert.AreEqual(0x24, StringUtilities.HexCharsToByte('2', '4'));
            Assert.AreEqual(0x25, StringUtilities.HexCharsToByte('2', '5'));
            Assert.AreEqual(0x26, StringUtilities.HexCharsToByte('2', '6'));
            Assert.AreEqual(0x27, StringUtilities.HexCharsToByte('2', '7'));
            Assert.AreEqual(0x28, StringUtilities.HexCharsToByte('2', '8'));
            Assert.AreEqual(0x29, StringUtilities.HexCharsToByte('2', '9'));
            Assert.AreEqual(0x2A, StringUtilities.HexCharsToByte('2', 'A'));
            Assert.AreEqual(0x2B, StringUtilities.HexCharsToByte('2', 'B'));
            Assert.AreEqual(0x2C, StringUtilities.HexCharsToByte('2', 'C'));
            Assert.AreEqual(0x2D, StringUtilities.HexCharsToByte('2', 'D'));
            Assert.AreEqual(0x2E, StringUtilities.HexCharsToByte('2', 'E'));
            Assert.AreEqual(0x2F, StringUtilities.HexCharsToByte('2', 'F'));
            Assert.AreEqual(0x30, StringUtilities.HexCharsToByte('3', '0'));
            Assert.AreEqual(0x31, StringUtilities.HexCharsToByte('3', '1'));
            Assert.AreEqual(0x32, StringUtilities.HexCharsToByte('3', '2'));
            Assert.AreEqual(0x33, StringUtilities.HexCharsToByte('3', '3'));
            Assert.AreEqual(0x34, StringUtilities.HexCharsToByte('3', '4'));
            Assert.AreEqual(0x35, StringUtilities.HexCharsToByte('3', '5'));
            Assert.AreEqual(0x36, StringUtilities.HexCharsToByte('3', '6'));
            Assert.AreEqual(0x37, StringUtilities.HexCharsToByte('3', '7'));
            Assert.AreEqual(0x38, StringUtilities.HexCharsToByte('3', '8'));
            Assert.AreEqual(0x39, StringUtilities.HexCharsToByte('3', '9'));
            Assert.AreEqual(0x3A, StringUtilities.HexCharsToByte('3', 'A'));
            Assert.AreEqual(0x3B, StringUtilities.HexCharsToByte('3', 'B'));
            Assert.AreEqual(0x3C, StringUtilities.HexCharsToByte('3', 'C'));
            Assert.AreEqual(0x3D, StringUtilities.HexCharsToByte('3', 'D'));
            Assert.AreEqual(0x3E, StringUtilities.HexCharsToByte('3', 'E'));
            Assert.AreEqual(0x3F, StringUtilities.HexCharsToByte('3', 'F'));
            Assert.AreEqual(0x40, StringUtilities.HexCharsToByte('4', '0'));
            Assert.AreEqual(0x41, StringUtilities.HexCharsToByte('4', '1'));
            Assert.AreEqual(0x42, StringUtilities.HexCharsToByte('4', '2'));
            Assert.AreEqual(0x43, StringUtilities.HexCharsToByte('4', '3'));
            Assert.AreEqual(0x44, StringUtilities.HexCharsToByte('4', '4'));
            Assert.AreEqual(0x45, StringUtilities.HexCharsToByte('4', '5'));
            Assert.AreEqual(0x46, StringUtilities.HexCharsToByte('4', '6'));
            Assert.AreEqual(0x47, StringUtilities.HexCharsToByte('4', '7'));
            Assert.AreEqual(0x48, StringUtilities.HexCharsToByte('4', '8'));
            Assert.AreEqual(0x49, StringUtilities.HexCharsToByte('4', '9'));
            Assert.AreEqual(0x4A, StringUtilities.HexCharsToByte('4', 'A'));
            Assert.AreEqual(0x4B, StringUtilities.HexCharsToByte('4', 'B'));
            Assert.AreEqual(0x4C, StringUtilities.HexCharsToByte('4', 'C'));
            Assert.AreEqual(0x4D, StringUtilities.HexCharsToByte('4', 'D'));
            Assert.AreEqual(0x4E, StringUtilities.HexCharsToByte('4', 'E'));
            Assert.AreEqual(0x4F, StringUtilities.HexCharsToByte('4', 'F'));
            Assert.AreEqual(0x50, StringUtilities.HexCharsToByte('5', '0'));
            Assert.AreEqual(0x51, StringUtilities.HexCharsToByte('5', '1'));
            Assert.AreEqual(0x52, StringUtilities.HexCharsToByte('5', '2'));
            Assert.AreEqual(0x53, StringUtilities.HexCharsToByte('5', '3'));
            Assert.AreEqual(0x54, StringUtilities.HexCharsToByte('5', '4'));
            Assert.AreEqual(0x55, StringUtilities.HexCharsToByte('5', '5'));
            Assert.AreEqual(0x56, StringUtilities.HexCharsToByte('5', '6'));
            Assert.AreEqual(0x57, StringUtilities.HexCharsToByte('5', '7'));
            Assert.AreEqual(0x58, StringUtilities.HexCharsToByte('5', '8'));
            Assert.AreEqual(0x59, StringUtilities.HexCharsToByte('5', '9'));
            Assert.AreEqual(0x5A, StringUtilities.HexCharsToByte('5', 'A'));
            Assert.AreEqual(0x5B, StringUtilities.HexCharsToByte('5', 'B'));
            Assert.AreEqual(0x5C, StringUtilities.HexCharsToByte('5', 'C'));
            Assert.AreEqual(0x5D, StringUtilities.HexCharsToByte('5', 'D'));
            Assert.AreEqual(0x5E, StringUtilities.HexCharsToByte('5', 'E'));
            Assert.AreEqual(0x5F, StringUtilities.HexCharsToByte('5', 'F'));
            Assert.AreEqual(0x60, StringUtilities.HexCharsToByte('6', '0'));
            Assert.AreEqual(0x61, StringUtilities.HexCharsToByte('6', '1'));
            Assert.AreEqual(0x62, StringUtilities.HexCharsToByte('6', '2'));
            Assert.AreEqual(0x63, StringUtilities.HexCharsToByte('6', '3'));
            Assert.AreEqual(0x64, StringUtilities.HexCharsToByte('6', '4'));
            Assert.AreEqual(0x65, StringUtilities.HexCharsToByte('6', '5'));
            Assert.AreEqual(0x66, StringUtilities.HexCharsToByte('6', '6'));
            Assert.AreEqual(0x67, StringUtilities.HexCharsToByte('6', '7'));
            Assert.AreEqual(0x68, StringUtilities.HexCharsToByte('6', '8'));
            Assert.AreEqual(0x69, StringUtilities.HexCharsToByte('6', '9'));
            Assert.AreEqual(0x6A, StringUtilities.HexCharsToByte('6', 'A'));
            Assert.AreEqual(0x6B, StringUtilities.HexCharsToByte('6', 'B'));
            Assert.AreEqual(0x6C, StringUtilities.HexCharsToByte('6', 'C'));
            Assert.AreEqual(0x6D, StringUtilities.HexCharsToByte('6', 'D'));
            Assert.AreEqual(0x6E, StringUtilities.HexCharsToByte('6', 'E'));
            Assert.AreEqual(0x6F, StringUtilities.HexCharsToByte('6', 'F'));
            Assert.AreEqual(0x70, StringUtilities.HexCharsToByte('7', '0'));
            Assert.AreEqual(0x71, StringUtilities.HexCharsToByte('7', '1'));
            Assert.AreEqual(0x72, StringUtilities.HexCharsToByte('7', '2'));
            Assert.AreEqual(0x73, StringUtilities.HexCharsToByte('7', '3'));
            Assert.AreEqual(0x74, StringUtilities.HexCharsToByte('7', '4'));
            Assert.AreEqual(0x75, StringUtilities.HexCharsToByte('7', '5'));
            Assert.AreEqual(0x76, StringUtilities.HexCharsToByte('7', '6'));
            Assert.AreEqual(0x77, StringUtilities.HexCharsToByte('7', '7'));
            Assert.AreEqual(0x78, StringUtilities.HexCharsToByte('7', '8'));
            Assert.AreEqual(0x79, StringUtilities.HexCharsToByte('7', '9'));
            Assert.AreEqual(0x7A, StringUtilities.HexCharsToByte('7', 'A'));
            Assert.AreEqual(0x7B, StringUtilities.HexCharsToByte('7', 'B'));
            Assert.AreEqual(0x7C, StringUtilities.HexCharsToByte('7', 'C'));
            Assert.AreEqual(0x7D, StringUtilities.HexCharsToByte('7', 'D'));
            Assert.AreEqual(0x7E, StringUtilities.HexCharsToByte('7', 'E'));
            Assert.AreEqual(0x7F, StringUtilities.HexCharsToByte('7', 'F'));
            Assert.AreEqual(0x80, StringUtilities.HexCharsToByte('8', '0'));
            Assert.AreEqual(0x81, StringUtilities.HexCharsToByte('8', '1'));
            Assert.AreEqual(0x82, StringUtilities.HexCharsToByte('8', '2'));
            Assert.AreEqual(0x83, StringUtilities.HexCharsToByte('8', '3'));
            Assert.AreEqual(0x84, StringUtilities.HexCharsToByte('8', '4'));
            Assert.AreEqual(0x85, StringUtilities.HexCharsToByte('8', '5'));
            Assert.AreEqual(0x86, StringUtilities.HexCharsToByte('8', '6'));
            Assert.AreEqual(0x87, StringUtilities.HexCharsToByte('8', '7'));
            Assert.AreEqual(0x88, StringUtilities.HexCharsToByte('8', '8'));
            Assert.AreEqual(0x89, StringUtilities.HexCharsToByte('8', '9'));
            Assert.AreEqual(0x8A, StringUtilities.HexCharsToByte('8', 'A'));
            Assert.AreEqual(0x8B, StringUtilities.HexCharsToByte('8', 'B'));
            Assert.AreEqual(0x8C, StringUtilities.HexCharsToByte('8', 'C'));
            Assert.AreEqual(0x8D, StringUtilities.HexCharsToByte('8', 'D'));
            Assert.AreEqual(0x8E, StringUtilities.HexCharsToByte('8', 'E'));
            Assert.AreEqual(0x8F, StringUtilities.HexCharsToByte('8', 'F'));
            Assert.AreEqual(0x90, StringUtilities.HexCharsToByte('9', '0'));
            Assert.AreEqual(0x91, StringUtilities.HexCharsToByte('9', '1'));
            Assert.AreEqual(0x92, StringUtilities.HexCharsToByte('9', '2'));
            Assert.AreEqual(0x93, StringUtilities.HexCharsToByte('9', '3'));
            Assert.AreEqual(0x94, StringUtilities.HexCharsToByte('9', '4'));
            Assert.AreEqual(0x95, StringUtilities.HexCharsToByte('9', '5'));
            Assert.AreEqual(0x96, StringUtilities.HexCharsToByte('9', '6'));
            Assert.AreEqual(0x97, StringUtilities.HexCharsToByte('9', '7'));
            Assert.AreEqual(0x98, StringUtilities.HexCharsToByte('9', '8'));
            Assert.AreEqual(0x99, StringUtilities.HexCharsToByte('9', '9'));
            Assert.AreEqual(0x9A, StringUtilities.HexCharsToByte('9', 'A'));
            Assert.AreEqual(0x9B, StringUtilities.HexCharsToByte('9', 'B'));
            Assert.AreEqual(0x9C, StringUtilities.HexCharsToByte('9', 'C'));
            Assert.AreEqual(0x9D, StringUtilities.HexCharsToByte('9', 'D'));
            Assert.AreEqual(0x9E, StringUtilities.HexCharsToByte('9', 'E'));
            Assert.AreEqual(0x9F, StringUtilities.HexCharsToByte('9', 'F'));
            Assert.AreEqual(0xA0, StringUtilities.HexCharsToByte('A', '0'));
            Assert.AreEqual(0xA1, StringUtilities.HexCharsToByte('A', '1'));
            Assert.AreEqual(0xA2, StringUtilities.HexCharsToByte('A', '2'));
            Assert.AreEqual(0xA3, StringUtilities.HexCharsToByte('A', '3'));
            Assert.AreEqual(0xA4, StringUtilities.HexCharsToByte('A', '4'));
            Assert.AreEqual(0xA5, StringUtilities.HexCharsToByte('A', '5'));
            Assert.AreEqual(0xA6, StringUtilities.HexCharsToByte('A', '6'));
            Assert.AreEqual(0xA7, StringUtilities.HexCharsToByte('A', '7'));
            Assert.AreEqual(0xA8, StringUtilities.HexCharsToByte('A', '8'));
            Assert.AreEqual(0xA9, StringUtilities.HexCharsToByte('A', '9'));
            Assert.AreEqual(0xAA, StringUtilities.HexCharsToByte('A', 'A'));
            Assert.AreEqual(0xAB, StringUtilities.HexCharsToByte('A', 'B'));
            Assert.AreEqual(0xAC, StringUtilities.HexCharsToByte('A', 'C'));
            Assert.AreEqual(0xAD, StringUtilities.HexCharsToByte('A', 'D'));
            Assert.AreEqual(0xAE, StringUtilities.HexCharsToByte('A', 'E'));
            Assert.AreEqual(0xAF, StringUtilities.HexCharsToByte('A', 'F'));
            Assert.AreEqual(0xB0, StringUtilities.HexCharsToByte('B', '0'));
            Assert.AreEqual(0xB1, StringUtilities.HexCharsToByte('B', '1'));
            Assert.AreEqual(0xB2, StringUtilities.HexCharsToByte('B', '2'));
            Assert.AreEqual(0xB3, StringUtilities.HexCharsToByte('B', '3'));
            Assert.AreEqual(0xB4, StringUtilities.HexCharsToByte('B', '4'));
            Assert.AreEqual(0xB5, StringUtilities.HexCharsToByte('B', '5'));
            Assert.AreEqual(0xB6, StringUtilities.HexCharsToByte('B', '6'));
            Assert.AreEqual(0xB7, StringUtilities.HexCharsToByte('B', '7'));
            Assert.AreEqual(0xB8, StringUtilities.HexCharsToByte('B', '8'));
            Assert.AreEqual(0xB9, StringUtilities.HexCharsToByte('B', '9'));
            Assert.AreEqual(0xBA, StringUtilities.HexCharsToByte('B', 'A'));
            Assert.AreEqual(0xBB, StringUtilities.HexCharsToByte('B', 'B'));
            Assert.AreEqual(0xBC, StringUtilities.HexCharsToByte('B', 'C'));
            Assert.AreEqual(0xBD, StringUtilities.HexCharsToByte('B', 'D'));
            Assert.AreEqual(0xBE, StringUtilities.HexCharsToByte('B', 'E'));
            Assert.AreEqual(0xBF, StringUtilities.HexCharsToByte('B', 'F'));
            Assert.AreEqual(0xC0, StringUtilities.HexCharsToByte('C', '0'));
            Assert.AreEqual(0xC1, StringUtilities.HexCharsToByte('C', '1'));
            Assert.AreEqual(0xC2, StringUtilities.HexCharsToByte('C', '2'));
            Assert.AreEqual(0xC3, StringUtilities.HexCharsToByte('C', '3'));
            Assert.AreEqual(0xC4, StringUtilities.HexCharsToByte('C', '4'));
            Assert.AreEqual(0xC5, StringUtilities.HexCharsToByte('C', '5'));
            Assert.AreEqual(0xC6, StringUtilities.HexCharsToByte('C', '6'));
            Assert.AreEqual(0xC7, StringUtilities.HexCharsToByte('C', '7'));
            Assert.AreEqual(0xC8, StringUtilities.HexCharsToByte('C', '8'));
            Assert.AreEqual(0xC9, StringUtilities.HexCharsToByte('C', '9'));
            Assert.AreEqual(0xCA, StringUtilities.HexCharsToByte('C', 'A'));
            Assert.AreEqual(0xCB, StringUtilities.HexCharsToByte('C', 'B'));
            Assert.AreEqual(0xCC, StringUtilities.HexCharsToByte('C', 'C'));
            Assert.AreEqual(0xCD, StringUtilities.HexCharsToByte('C', 'D'));
            Assert.AreEqual(0xCE, StringUtilities.HexCharsToByte('C', 'E'));
            Assert.AreEqual(0xCF, StringUtilities.HexCharsToByte('C', 'F'));
            Assert.AreEqual(0xD0, StringUtilities.HexCharsToByte('D', '0'));
            Assert.AreEqual(0xD1, StringUtilities.HexCharsToByte('D', '1'));
            Assert.AreEqual(0xD2, StringUtilities.HexCharsToByte('D', '2'));
            Assert.AreEqual(0xD3, StringUtilities.HexCharsToByte('D', '3'));
            Assert.AreEqual(0xD4, StringUtilities.HexCharsToByte('D', '4'));
            Assert.AreEqual(0xD5, StringUtilities.HexCharsToByte('D', '5'));
            Assert.AreEqual(0xD6, StringUtilities.HexCharsToByte('D', '6'));
            Assert.AreEqual(0xD7, StringUtilities.HexCharsToByte('D', '7'));
            Assert.AreEqual(0xD8, StringUtilities.HexCharsToByte('D', '8'));
            Assert.AreEqual(0xD9, StringUtilities.HexCharsToByte('D', '9'));
            Assert.AreEqual(0xDA, StringUtilities.HexCharsToByte('D', 'A'));
            Assert.AreEqual(0xDB, StringUtilities.HexCharsToByte('D', 'B'));
            Assert.AreEqual(0xDC, StringUtilities.HexCharsToByte('D', 'C'));
            Assert.AreEqual(0xDD, StringUtilities.HexCharsToByte('D', 'D'));
            Assert.AreEqual(0xDE, StringUtilities.HexCharsToByte('D', 'E'));
            Assert.AreEqual(0xDF, StringUtilities.HexCharsToByte('D', 'F'));
            Assert.AreEqual(0xE0, StringUtilities.HexCharsToByte('E', '0'));
            Assert.AreEqual(0xE1, StringUtilities.HexCharsToByte('E', '1'));
            Assert.AreEqual(0xE2, StringUtilities.HexCharsToByte('E', '2'));
            Assert.AreEqual(0xE3, StringUtilities.HexCharsToByte('E', '3'));
            Assert.AreEqual(0xE4, StringUtilities.HexCharsToByte('E', '4'));
            Assert.AreEqual(0xE5, StringUtilities.HexCharsToByte('E', '5'));
            Assert.AreEqual(0xE6, StringUtilities.HexCharsToByte('E', '6'));
            Assert.AreEqual(0xE7, StringUtilities.HexCharsToByte('E', '7'));
            Assert.AreEqual(0xE8, StringUtilities.HexCharsToByte('E', '8'));
            Assert.AreEqual(0xE9, StringUtilities.HexCharsToByte('E', '9'));
            Assert.AreEqual(0xEA, StringUtilities.HexCharsToByte('E', 'A'));
            Assert.AreEqual(0xEB, StringUtilities.HexCharsToByte('E', 'B'));
            Assert.AreEqual(0xEC, StringUtilities.HexCharsToByte('E', 'C'));
            Assert.AreEqual(0xED, StringUtilities.HexCharsToByte('E', 'D'));
            Assert.AreEqual(0xEE, StringUtilities.HexCharsToByte('E', 'E'));
            Assert.AreEqual(0xEF, StringUtilities.HexCharsToByte('E', 'F'));
            Assert.AreEqual(0xF0, StringUtilities.HexCharsToByte('F', '0'));
            Assert.AreEqual(0xF1, StringUtilities.HexCharsToByte('F', '1'));
            Assert.AreEqual(0xF2, StringUtilities.HexCharsToByte('F', '2'));
            Assert.AreEqual(0xF3, StringUtilities.HexCharsToByte('F', '3'));
            Assert.AreEqual(0xF4, StringUtilities.HexCharsToByte('F', '4'));
            Assert.AreEqual(0xF5, StringUtilities.HexCharsToByte('F', '5'));
            Assert.AreEqual(0xF6, StringUtilities.HexCharsToByte('F', '6'));
            Assert.AreEqual(0xF7, StringUtilities.HexCharsToByte('F', '7'));
            Assert.AreEqual(0xF8, StringUtilities.HexCharsToByte('F', '8'));
            Assert.AreEqual(0xF9, StringUtilities.HexCharsToByte('F', '9'));
            Assert.AreEqual(0xFA, StringUtilities.HexCharsToByte('F', 'A'));
            Assert.AreEqual(0xFB, StringUtilities.HexCharsToByte('F', 'B'));
            Assert.AreEqual(0xFC, StringUtilities.HexCharsToByte('F', 'C'));
            Assert.AreEqual(0xFD, StringUtilities.HexCharsToByte('F', 'D'));
            Assert.AreEqual(0xFE, StringUtilities.HexCharsToByte('F', 'E'));
            Assert.AreEqual(0xFF, StringUtilities.HexCharsToByte('F', 'F'));
        }

        [TestMethod]
        public void HexCharToLeastSignificantNibbleTest()
        {
            Assert.AreEqual(0x00, StringUtilities.HexCharToLeastSignificantNibble('0'));
            Assert.AreEqual(0x01, StringUtilities.HexCharToLeastSignificantNibble('1'));
            Assert.AreEqual(0x02, StringUtilities.HexCharToLeastSignificantNibble('2'));
            Assert.AreEqual(0x03, StringUtilities.HexCharToLeastSignificantNibble('3'));
            Assert.AreEqual(0x04, StringUtilities.HexCharToLeastSignificantNibble('4'));
            Assert.AreEqual(0x05, StringUtilities.HexCharToLeastSignificantNibble('5'));
            Assert.AreEqual(0x06, StringUtilities.HexCharToLeastSignificantNibble('6'));
            Assert.AreEqual(0x07, StringUtilities.HexCharToLeastSignificantNibble('7'));
            Assert.AreEqual(0x08, StringUtilities.HexCharToLeastSignificantNibble('8'));
            Assert.AreEqual(0x09, StringUtilities.HexCharToLeastSignificantNibble('9'));
            Assert.AreEqual(0x0A, StringUtilities.HexCharToLeastSignificantNibble('A'));
            Assert.AreEqual(0x0B, StringUtilities.HexCharToLeastSignificantNibble('B'));
            Assert.AreEqual(0x0C, StringUtilities.HexCharToLeastSignificantNibble('C'));
            Assert.AreEqual(0x0D, StringUtilities.HexCharToLeastSignificantNibble('D'));
            Assert.AreEqual(0x0E, StringUtilities.HexCharToLeastSignificantNibble('E'));
            Assert.AreEqual(0x0F, StringUtilities.HexCharToLeastSignificantNibble('F'));
        }

        [TestMethod]
        public void HexCharToMostSignificantNibbleTest()
        {
            Assert.AreEqual(0x00, StringUtilities.HexCharToMostSignificantNibble('0'));
            Assert.AreEqual(0x10, StringUtilities.HexCharToMostSignificantNibble('1'));
            Assert.AreEqual(0x20, StringUtilities.HexCharToMostSignificantNibble('2'));
            Assert.AreEqual(0x30, StringUtilities.HexCharToMostSignificantNibble('3'));
            Assert.AreEqual(0x40, StringUtilities.HexCharToMostSignificantNibble('4'));
            Assert.AreEqual(0x50, StringUtilities.HexCharToMostSignificantNibble('5'));
            Assert.AreEqual(0x60, StringUtilities.HexCharToMostSignificantNibble('6'));
            Assert.AreEqual(0x70, StringUtilities.HexCharToMostSignificantNibble('7'));
            Assert.AreEqual(0x80, StringUtilities.HexCharToMostSignificantNibble('8'));
            Assert.AreEqual(0x90, StringUtilities.HexCharToMostSignificantNibble('9'));
            Assert.AreEqual(0xA0, StringUtilities.HexCharToMostSignificantNibble('A'));
            Assert.AreEqual(0xB0, StringUtilities.HexCharToMostSignificantNibble('B'));
            Assert.AreEqual(0xC0, StringUtilities.HexCharToMostSignificantNibble('C'));
            Assert.AreEqual(0xD0, StringUtilities.HexCharToMostSignificantNibble('D'));
            Assert.AreEqual(0xE0, StringUtilities.HexCharToMostSignificantNibble('E'));
            Assert.AreEqual(0xF0, StringUtilities.HexCharToMostSignificantNibble('F'));
        }

        [TestMethod]
        public void EscapeStringTest()
        {
            var escape = "\' \" \\ \0 \a \b \f \n \r \t \v";
            var unescape = "\\' \\\" \\\\ \\0 \\a \\b \\f \\n \\r \\t \\v";

            Assert.AreEqual(unescape, StringUtilities.EscapeString(escape));
            Assert.AreEqual(escape, StringUtilities.UnescapeString(unescape));

            var utf16_e = "\uABCD";
            var utf32_e = "\U0010ABCD";
            var utf16_ue = "\\uABCD";
            var utf32_ue = "\\U0010ABCD";

            var test16 = StringUtilities.UnescapeString(utf16_ue);
            Assert.AreEqual(utf16_e, test16);

            var test32 = StringUtilities.UnescapeString(utf32_ue);
            Assert.AreEqual(utf32_e, test32);

            var hex4_e = "\xA";
            var hex8_e = "\xAB";
            var hex12_e = "\xABC";
            var hex16_e = "\xABCD";

            var hex4_ue = "\\xA";
            var hex8_ue = "\\xAB";
            var hex12_ue = "\\xABC";
            var hex16_ue = "\\xABCD";

            var thex4 = StringUtilities.UnescapeString(hex4_ue);
            var thex8 = StringUtilities.UnescapeString(hex8_ue);
            var thex12 = StringUtilities.UnescapeString(hex12_ue);
            var thex16 = StringUtilities.UnescapeString(hex16_ue);

            Assert.AreEqual(hex4_e, thex4);
            Assert.AreEqual(hex8_e, thex8);
            Assert.AreEqual(hex12_e, thex12);
            Assert.AreEqual(hex16_e, thex16);

            var copyright = "©";
            var nonAscii = "\u00A9";
            var escapedNonascii = "\\u00A9";

            Assert.AreEqual(copyright, nonAscii);
            Assert.AreEqual(escapedNonascii, StringUtilities.EscapeString(nonAscii));
            Assert.AreEqual(nonAscii, StringUtilities.UnescapeString(escapedNonascii));
            Assert.AreEqual(utf16_ue, StringUtilities.EscapeString(utf16_e));
            Assert.AreEqual(utf16_e, StringUtilities.UnescapeString(utf16_ue));

            var unicode = "白上フブキ";
            var escapedUnicode = "\\u767D\\u4E0A\\u30D5\\u30D6\\u30AD";

            Assert.AreEqual(escapedUnicode, StringUtilities.EscapeString(unicode));
            Assert.AreEqual(unicode, StringUtilities.UnescapeString(escapedUnicode));

            var emoji = "\U0001F928"; // 🤨
            var emojiEscaped = "\\U0001F928";

            Assert.AreEqual(emojiEscaped, StringUtilities.EscapeString(emoji));
            Assert.AreEqual(emoji, StringUtilities.UnescapeString(emojiEscaped));

            //// Errors
            const string invalidChar = "\uFFFD";
            // UTF-16 Errors
            var unpairedHighSurrogate = "\uD800X";
            var unpairedLowSurrogate = "\uDC00X";
            var swappedSurrogates = "\uDC00\uD800";

            var unpairedHighSurrogateEscaped = "\\uD800X";
            var unpairedHighSurrogateEscaped2 = "\\uD800\\u0058";
            var unpairedHighSurrogateEscaped3 = "\\uD800\\uD800\\uDC00";
            var unpairedLowSurrogateEscaped = "\\uDC00X";
            var swappedSurrogatesEscaped = "\\uDC00\\uD800";

            Assert.AreEqual(StringUtilities.EscapeString(invalidChar + "X"), StringUtilities.EscapeString(unpairedHighSurrogate));
            Assert.AreEqual(StringUtilities.EscapeString(invalidChar + "X"), StringUtilities.EscapeString(unpairedLowSurrogate));
            Assert.AreEqual(StringUtilities.EscapeString(invalidChar + invalidChar), StringUtilities.EscapeString(swappedSurrogates));

            Assert.AreEqual(invalidChar + "X", StringUtilities.UnescapeString(unpairedHighSurrogateEscaped));
            Assert.AreEqual(invalidChar + "X", StringUtilities.UnescapeString(unpairedHighSurrogateEscaped2));
            Assert.AreEqual(invalidChar + "\uD800\uDC00", StringUtilities.UnescapeString(unpairedHighSurrogateEscaped3));
            Assert.AreEqual(invalidChar + "X", StringUtilities.UnescapeString(unpairedLowSurrogateEscaped));
            Assert.AreEqual(invalidChar + invalidChar, StringUtilities.UnescapeString(swappedSurrogatesEscaped));

            // UTF-32 Errors
            const int maxUtf32 = 0x0010FFFF;
            var outOfRange1 = $"\\U{(maxUtf32 + 1):X8}";
            var outOfRange2 = $"\\U{(-1):X8}";
            var outOfRange3 = $"\\U{(0x0000D800):X8}";
            var outOfRange4 = $"\\U{(0x0000DFFF):X8}";

            Assert.AreEqual(invalidChar, StringUtilities.UnescapeString(outOfRange1));
            Assert.AreEqual(invalidChar, StringUtilities.UnescapeString(outOfRange2));
            Assert.AreEqual(invalidChar, StringUtilities.UnescapeString(outOfRange3));
            Assert.AreEqual(invalidChar, StringUtilities.UnescapeString(outOfRange4));
        }
    }
}