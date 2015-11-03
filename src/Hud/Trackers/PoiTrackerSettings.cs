using PoeHUD.Hud.Settings;

namespace PoeHUD.Hud.Trackers
{
    public sealed class PoiTrackerSettings : SettingsBase
    {
        public PoiTrackerSettings()
        {
            Enable = true;
            Masters = true;
            Strongboxes = true;
            Chests = true;
        }

        public ToggleNode Masters { get; set; }
        public ToggleNode Strongboxes { get; set; }
        public ToggleNode Chests { get; set; }
    }
}