using PoeHUD.Hud.Settings;

namespace PoeHUD.Hud.Health
{
    public class UnitSettings : SettingsBase
    {
        public UnitSettings() { }

        public UnitSettings(uint color, uint outline)
        {
            Enable = true;
            Width = new RangeNode<float>(160, 50, 180);
            Height = new RangeNode<float>(20, 10, 50);
            Color = color;
            Outline = outline;
            Under10Percent = 0xffffffff;
            PercentTextColor = 0xffffffff;
            HealthTextColor = 0xffffffff;
            HealthTextColorUnder10Percent = 0xffff00ff;
            ShowPercents = false;
            ShowHealthText = false;
            ShowFloatingCombatDamage = false;
            FloatingCombatFontSize = new RangeNode<int>(15, 10, 30);
            FloatingCombatDamageColor = SharpDX.Color.Yellow;
            FloatingCombatHealColor = SharpDX.Color.Lime;
            TextSize = new RangeNode<int>(15, 10, 50);
            FloatingCombatStackSize = new RangeNode<int>(1, 1, 10);
        }

        public UnitSettings(uint color, uint outline, uint percentTextColor, bool showText)
          : this(color, outline)
        {
            PercentTextColor = percentTextColor;
            ShowPercents = showText;
            ShowHealthText = showText;
        }

        public RangeNode<float> Width { get; set; }
        public RangeNode<float> Height { get; set; }
        public ColorNode Color { get; set; }
        public ColorNode Outline { get; set; }
        public ColorNode Under10Percent { get; set; }
        public ColorNode PercentTextColor { get; set; }
        public ColorNode HealthTextColor { get; set; }
        public ColorNode HealthTextColorUnder10Percent { get; set; }
        public ToggleNode ShowPercents { get; set; }
        public ToggleNode ShowHealthText { get; set; }
        public RangeNode<int> TextSize { get; set; }
        public ToggleNode ShowFloatingCombatDamage { get; set; }
        public RangeNode<int> FloatingCombatFontSize { get; set; }
        public ColorNode FloatingCombatDamageColor { get; set; }
        public ColorNode FloatingCombatHealColor { get; set; }
        public RangeNode<int> FloatingCombatStackSize { get; set; }
    }
}