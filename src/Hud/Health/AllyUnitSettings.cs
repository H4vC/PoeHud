using PoeHUD.Hud.Settings;

namespace PoeHUD.Hud.Health
{
    public class AllyUnitSettings : SettingsBase
    {
        public AllyUnitSettings() {}

        public AllyUnitSettings(uint color, uint outline)
        {
            Enable = true;
            Width = new RangeNode<float>(140, 50, 180);
            Height = new RangeNode<float>(20, 10, 50);
            Color = color;
            Outline = outline;
        }

        public RangeNode<float> Width { get; set; }

        public RangeNode<float> Height { get; set; }

        public ColorNode Color { get; set; }

        public ColorNode Outline { get; set; }
    }
}