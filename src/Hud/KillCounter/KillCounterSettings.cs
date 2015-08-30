using PoeHUD.Hud.Settings;
using SharpDX;

namespace PoeHUD.Hud.KillCounter
{
    public sealed class KillCounterSettings : SettingsBase
    {
        public KillCounterSettings()
        {
            Enable = false;
            ShowDetail = true;
            FontColor = new ColorBGRA(254, 192, 118, 255);
            BackgroundColor = new ColorBGRA(255, 255, 255, 255);
            LabelFontSize = new RangeNode<int>(16, 10, 20);
            KillsFontSize = new RangeNode<int>(16, 10, 20);
        }

        public ToggleNode ShowDetail { get; set; }

        public ColorNode FontColor { get; set; }

        public ColorNode BackgroundColor { get; set; }

        public RangeNode<int> LabelFontSize { get; set; }

        public RangeNode<int> KillsFontSize { get; set; }
    }
}