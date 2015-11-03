using PoeHUD.Hud.Settings;

namespace PoeHUD.Hud.Loot
{
    public sealed class ItemCounterSettings : SettingsBase
    {
        public ItemCounterSettings()
        {
            Enable = false;
            ShowDetail = true;
        }

        public ToggleNode ShowDetail { get; set; }
    }
}
