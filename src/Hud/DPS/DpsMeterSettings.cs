using PoeHUD.Hud.Settings;

using SharpDX;

namespace PoeHUD.Hud.Dps
{
    public sealed class DpsMeterSettings : SettingsBase
    {
        public DpsMeterSettings()
        {
            Enable = false;
            DpsTextSize = new RangeNode<int>(16, 10, 20);
            PeakDpsTextSize = new RangeNode<int>(16, 10, 20);
            DpsFontColor = new ColorBGRA(254, 192, 118, 255);
            PeakFontColor = new ColorBGRA(254, 192, 118, 255);
            BackgroundColor = new ColorBGRA(255, 255, 255, 255);
        }

        public RangeNode<int> DpsTextSize { get; set; }

        public RangeNode<int> PeakDpsTextSize { get; set; }

        public ColorNode DpsFontColor { get; set; }

        public ColorNode PeakFontColor { get; set; }

        public ColorNode BackgroundColor { get; set; }
    }
}