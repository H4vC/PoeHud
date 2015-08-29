using PoeHUD.Hud.Settings;
using SharpDX;

namespace PoeHUD.Hud.AdvancedTooltip
{
    public sealed class WeaponDpsSettings : SettingsBase
    {
        public WeaponDpsSettings()
        {
            Enable = true;
            FontColor = new ColorBGRA(254, 192, 118, 255);
            DamageFontSize = new RangeNode<int>(20, 10, 50);
            FontSize = new RangeNode<int>(13, 10, 50);
        }
        public ColorNode FontColor { get; set; }
        public RangeNode<int> FontSize { get; set; }
        public RangeNode<int> DamageFontSize { get; set; }
    }
}