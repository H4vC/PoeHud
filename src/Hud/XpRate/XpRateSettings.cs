using PoeHUD.Hud.Settings;

using SharpDX;

namespace PoeHUD.Hud.XpRate
{
    public sealed class XpRateSettings : SettingsBase
    {
        public XpRateSettings()
        {
            Enable = true;
            OnlyAreaName = false;
            ShowLatency = true;
            FontSize = new RangeNode<int>(16, 10, 20);
            BackgroundColor = new ColorBGRA(255, 255, 255, 255);

            AreaFontColor = new ColorBGRA(140, 200, 255, 255);
            XphFontColor = new ColorBGRA(254, 192, 118, 255);
            TimeLeftColor = new ColorBGRA(254, 192, 118, 255);
            FpsFontColor = new ColorBGRA(254, 192, 118, 255);
            TimerFontColor = new ColorBGRA(254, 192, 118, 255);
            LatencyFontColor = new ColorBGRA(254, 192, 118, 255);
        }
        public ToggleNode ShowLatency { get; set; }
        public ToggleNode OnlyAreaName { get; set; }
        public RangeNode<int> FontSize { get; set; }
        public ColorNode BackgroundColor { get; set; }
        public ColorNode AreaFontColor { get; set; }
        public ColorNode XphFontColor { get; set; }
        public ColorNode TimeLeftColor { get; set; }
        public ColorNode FpsFontColor { get; set; }
        public ColorNode TimerFontColor { get; set; }
        public ColorNode LatencyFontColor { get; set; }
    }
}