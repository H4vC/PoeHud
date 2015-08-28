using PoeHUD.Hud.Settings;

using SharpDX;

namespace PoeHUD.Hud.Trackers
{
    public sealed class MonsterTrackerSettings : SettingsBase
    {
        public MonsterTrackerSettings()
        {
            Enable = true;
            Monsters = true;
            Minions = true;
            PlaySound = false;
            ShowText = true;
            TextSize = new RangeNode<int>(27, 10, 50);
            BackgroundColor = new ColorBGRA(0, 0, 0, 128);
            TextPositionX = new RangeNode<int>(50, 0, 100);
            TextPositionY = new RangeNode<int>(15, 0, 100);
            DefaultTextColor = Color.Red;
        }

        public ToggleNode Monsters { get; set; }

        public ToggleNode Minions { get; set; }

        public ToggleNode PlaySound { get; set; }

        public ToggleNode ShowText { get; set; }

        public RangeNode<int> TextSize { get; set; }

        public ColorNode DefaultTextColor { get; set; }

        public ColorNode BackgroundColor { get; set; }

        public RangeNode<int> TextPositionX { get; set; }

        public RangeNode<int> TextPositionY { get; set; }
    }
}