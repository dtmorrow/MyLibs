using System;
using System.Collections.Generic;
using System.Text;
using static MyLibs.Generated.UnicodeStyling;

namespace MyLibs
{
    /// <summary>
    /// Convert letters and numbers to special Unicode character styling.
    /// </summary>
    public static class UnicodeStyle
    {
        /// <summary>
        /// Types of Unicode Styling.
        /// </summary>
        public enum Style
        {
            SerifBold, SerifItalic, SerifBoldItalic,
            SansSerif, SansSerifBold, SansSerifItalic, SansSerifBoldItalic,
            Script, ScriptBold,
            Fraktur, FrakturBold,
            DoubleStruck,
            Monospace,
            Circled, CircledNegative, Squared, SquaredNegative, Parenthesized, RegionalIndicator,
            Fullwidth, SmallCapitals,
            DigitFullStop, DigitDoubleCircled
        };

        /// <summary>
        /// Convert letters and numbers to special Unicode character styling.
        /// </summary>
        /// <param name="text">The text to style.</param>
        /// <param name="style">The type of style.</param>
        /// <returns>A <see cref="string"/> with the text styled using Unicode.</returns>
        public static string StyleText(string text, Style style)
        {
            var sb = new StringBuilder(text.Length);

            foreach (var c in text)
            {
                // All of these conversion methods a generated in "UnicodeStyling.tt" in the "Generated Code" directory.
                switch (style)
                {
                    case Style.SerifBold: sb.Append(ConvertToSerifBold(c)); break;
                    case Style.SerifItalic: sb.Append(ConvertToSerifItalic(c)); break;
                    case Style.SerifBoldItalic: sb.Append(ConvertToSerifBoldItalic(c)); break;
                    case Style.SansSerif: sb.Append(ConvertToSansSerif(c)); break;
                    case Style.SansSerifBold: sb.Append(ConvertToSansSerifBold(c)); break;
                    case Style.SansSerifItalic: sb.Append(ConvertToSansSerifItalic(c)); break;
                    case Style.SansSerifBoldItalic: sb.Append(ConvertToSansSerifBoldItalic(c)); break;
                    case Style.Script: sb.Append(ConvertToScript(c)); break;
                    case Style.ScriptBold: sb.Append(ConvertToScriptBold(c)); break;
                    case Style.Fraktur: sb.Append(ConvertToFraktur(c)); break;
                    case Style.FrakturBold: sb.Append(ConvertToFrakturBold(c)); break;
                    case Style.DoubleStruck: sb.Append(ConvertToDoubleStruck(c)); break;
                    case Style.Monospace: sb.Append(ConvertToMonospace(c)); break;
                    case Style.Circled: sb.Append(ConvertToCircled(c)); break;
                    case Style.CircledNegative: sb.Append(ConvertToCircledNeg(c)); break;
                    case Style.Squared: sb.Append(ConvertToSquared(c)); break;
                    case Style.SquaredNegative: sb.Append(ConvertToSquaredNeg(c)); break;
                    case Style.Parenthesized: sb.Append(ConvertToParenth(c)); break;
                    case Style.Fullwidth: sb.Append(ConvertToFullwidth(c)); break;
                    case Style.SmallCapitals: sb.Append(ConvertToSmallCapitals(c)); break;
                    case Style.RegionalIndicator: sb.Append(ConvertToRegionalIndicators(c)); break;
                    case Style.DigitFullStop: sb.Append(ConvertToDigitFullStop(c)); break;
                    case Style.DigitDoubleCircled: sb.Append(ConvertToDigitDoubleCircled(c)); break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return sb.ToString();
        }
    }
}
