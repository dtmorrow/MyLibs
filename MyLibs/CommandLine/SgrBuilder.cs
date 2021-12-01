using System;
using System.Collections.Generic;

namespace MyLibs.CommandLine
{
    /// <summary>
    /// A class that builds a Select Graphic Rendition string.
    /// </summary>
    public class SgrBuilder
    {
        private string weight = "";
        private string italic = "";
        private string underline = "";
        private string blink = "";
        private string invert = "";
        private string conceal = "";
        private string strikeout = "";
        private string font = "";
        private string proportional = "";
        private string framing = "";
        private string overlined = "";
        private readonly List<string> custom = new List<string>();

        public enum FontWeights
        {
            Normal = 22,
            Bold = 1,
            Faint = 2
        }

        public FontWeights FontWeight
        {
            get
            {
                return weight switch
                {
                    "1;" => FontWeights.Bold,
                    "2;" => FontWeights.Faint,
                    _ => FontWeights.Normal,
                };
            }
            set
            {
                weight = value switch
                {
                    FontWeights.Bold => "1;",
                    FontWeights.Faint => "2;",
                    _ => "22;",
                };
            }
        }

        public bool Italic
        {
            get => italic == "3;";
            set => italic = (value ? "3;" : "23;");
        }

        public enum UnderlineTypes
        {
            None = 24,
            Single = 4,
            Double = 21,
        }

        public UnderlineTypes Underline
        {
            get
            {
                return underline switch
                {
                    "4;" => UnderlineTypes.Single,
                    "21;" => UnderlineTypes.Double,
                    _ => UnderlineTypes.None,
                };
            }
            set
            {
                underline = value switch
                {
                    UnderlineTypes.Single => "4;",
                    UnderlineTypes.Double => "21;",
                    _ => "24;",
                };
            }
        }

        public enum BlinkSpeeds
        {
            Off = 25,
            Slow = 5,
            Fast = 6,
        }

        public BlinkSpeeds BlinkSpeed
        {
            get
            {
                return blink switch
                {
                    "5;" => BlinkSpeeds.Slow,
                    "6;" => BlinkSpeeds.Fast,
                    _ => BlinkSpeeds.Off,
                };
            }
            set
            {
                blink = value switch
                {
                    BlinkSpeeds.Slow => "5;",
                    BlinkSpeeds.Fast => "6;",
                    _ => "25;",
                };
            }
        }

        public bool Invert
        {
            get => invert == "7;";
            set => invert = (value ? "7;" : "27;");
        }

        public bool Conceal
        {
            get => conceal == "8;";
            set => conceal = (value ? "8;" : "28;");
        }

        public bool Strikeout
        {
            get => strikeout == "9;";
            set => strikeout = (value ? "9;" : "29;");
        }

        public int AlternateFont // TODO: Is Fraktur (20) an Alternate Font or Italic (conflicts with 3 and disables with 23)?
        {
            get => (string.IsNullOrEmpty(font)) ? 10 : font[1] - '0' + 10;
            set => font = $"{Math.Clamp(value, 0, 9) + 10};";
        }

        public bool ProportionalSpacing
        {
            get => proportional == "26;";
            set => proportional = (value ? "26;" : "50;");
        }

        public ForegroundColor ForegroundColor { get; set; } = ForegroundColor.NoChange();
        public BackgroundColor BackgroundColor { get; set; } = BackgroundColor.NoChange();
        public UnderLineColor UnderLineColor { get; set; } = UnderLineColor.NoChange();

        public enum FramingTypes
        {
            None = 54,
            Framed = 51,
            Encircled = 52,
        }
        
        public FramingTypes Framing
        {
            get
            {
                return framing switch
                {
                    "51;" => FramingTypes.Framed,
                    "52;" => FramingTypes.Encircled,
                    _ => FramingTypes.None,
                };
            }
            set
            {
                framing = value switch
                {
                    FramingTypes.Framed=> "51;",
                    FramingTypes.Encircled => "52;",
                    _ => "54;",
                };
            }
        }

        public bool Overlined
        {
            get => overlined == "53;";
            set => overlined = (value ? "53;" : "55;");
        }

        /// <summary>
        /// Add custom SGR parameters.
        /// </summary>
        /// <param name="values">An array of SGR parameters.</param>
        public void AddCustom(params string[] values)
        {
            custom.AddRange(values);
        }

        /// <summary>
        /// Clear all custom SGR parameters.
        /// </summary>
        public void ClearCustom()
        {
            custom.Clear();
        }

        /// <summary>
        /// Clear all formatting from the <see cref="SgrBuilder"/>.
        /// </summary>
        public void ClearAll()
        {
            weight = "";
            italic = "";
            underline = "";
            blink = "";
            invert = "";
            conceal = "";
            strikeout = "";
            font = "";
            proportional = "";
            framing = "";
            overlined = "";
            ForegroundColor = ForegroundColor.NoChange();
            BackgroundColor = BackgroundColor.NoChange();
            UnderLineColor = UnderLineColor.NoChange();
            custom.Clear();
        }

        /// <summary>
        /// Returns a <see cref="string"/> to write to the console to activate the ANSI sequence.
        /// </summary>
        /// <returns>The <see cref="string"/> to write to the console to activate the ANSI sequence.</returns>
        public override string ToString()
        {
            var format = $"{weight}{italic}{underline}{blink}{invert}{conceal}{strikeout}{font}{proportional}{ForegroundColor}{BackgroundColor}{UnderLineColor}{framing}{overlined}{string.Join(';', custom)}".TrimEnd(';');

            return $"\u001B[{format}m";
        }
    }
}
