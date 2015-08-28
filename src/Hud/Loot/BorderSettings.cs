using PoeHUD.Hud.Settings;

using SharpDX;

namespace PoeHUD.Hud.Loot
{
    public sealed class BorderSettings : SettingsBase
    {
        public BorderSettings()
        {
            Enable = false;
            BorderColor = Color.FromAbgr(0xbb252ff);
            CantPickUpBorderColor = Color.Red;
            NotMyItemBorderColor = Color.Yellow;
            ShowTimer = true;
            BorderWidth = new RangeNode<int>(1, 1, 10);
            TimerTextSize = new RangeNode<int>(10, 8, 40);
        }

        public ColorNode BorderColor { get; set; }

        public ColorNode CantPickUpBorderColor { get; set; }

        public ColorNode NotMyItemBorderColor { get; set; }

        public ToggleNode ShowTimer { get; set; }

        public RangeNode<int> BorderWidth { get; set; }

        public RangeNode<int> TimerTextSize { get; set; }
    }
}