using System;
using SharpDX;

namespace PoeHUD.Framework
{
	public static class ColorUtils
	{
        [Obsolete]
		public static void ColorToHsv(Color color, out double hue, out double saturation, out double value)
		{
			int max = Math.Max(color.R, Math.Max(color.G, color.B));
			int min = Math.Min(color.R, Math.Min(color.G, color.B));

			hue = color.GetHue();
			saturation = (max == 0) ? 0 : 1d - (1d * min / max);
			value = max / 255d;
		}

		public static Color ColorFromHsv(double hue, double saturation, double value)
		{
			int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
			double f = hue / 60 - Math.Floor(hue / 60);

			value = value * 255;
            byte v = Convert.ToByte(value);
			byte p = Convert.ToByte(value * (1 - saturation));
            byte q = Convert.ToByte(value * (1 - f * saturation));
            byte t = Convert.ToByte(value * (1 - (1 - f) * saturation));

			switch (hi)
			{
				case 0:
                    return new ColorBGRA(v, t, p, 255);
				case 1:
                    return new ColorBGRA(q, v, p, 255);
				case 2:
                    return new ColorBGRA(p, v, t, 255);
				case 3:
                    return new ColorBGRA(p, q, v, 255);
				case 4:
                    return new ColorBGRA(t, p, v, 255);
				default:
                    return new ColorBGRA(v, p, q, 255);
			}
		}
	}
}
