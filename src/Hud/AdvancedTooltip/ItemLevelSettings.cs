using PoeHUD.Hud.Settings;

namespace PoeHUD.Hud.AdvancedTooltip
{
    public sealed class ItemLevelSettings : SettingsBase
    {
        public ItemLevelSettings()
        {
            Enable = true;
            TextSize = new RangeNode<int>(15, 10, 50);
        }

        public RangeNode<int> TextSize { get; set; }
    }
}