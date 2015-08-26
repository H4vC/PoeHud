using System;
using System.Globalization;
using SharpDX;

namespace PoeHUD.Framework.Helpers
{
    public static class ConvertHelper
    {
        public static string ToShorten(double value, string format = "0")
        {
            double abs = Math.Abs(value);
            if (abs >= 1000000)
            {
                return string.Concat((value / 1000000).ToString("F2"), "M");
            }

            if (abs >= 1000)
            {
                return string.Concat((value / 1000).ToString("F1"), "K");
            }

            return value.ToString(format);
        }

        public static Color ToBGRAColor(this string value)
        {
            uint bgra;
            return uint.TryParse(value, NumberStyles.HexNumber, null, out bgra)
                ? Color.FromBgra(bgra)
                : Color.Black;
        }


        public static Color? ConfigColorValueExtractor(this string[] line, int index)
        {
            return IsNotNull(line, index) ? (Color?)line[index].ToBGRAColor() : null;
        }

        public static string ConfigValueExtractor(this string[] line, int index)
        {
            return IsNotNull(line, index) ? line[index] : null;
        }

        private static bool IsNotNull(string[] line, int index)
        {
            return line.Length > index && !string.IsNullOrEmpty(line[index]);
        }
    }
}