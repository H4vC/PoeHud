using Newtonsoft.Json;

using PoeHUD.Hud.Settings;

namespace PoeHUD.Hud.Loot
{
    public sealed class ItemAlertSettings : SettingsBase
    {
        public ItemAlertSettings()
        {
            Enable = true;
            ShowItemOnMap = true;
            Crafting = true;
            ShowText = true;
            HideOthers = false;
            PlaySound = true;
            TextSize = new RangeNode<int>(16, 10, 50);
            Rares = true;
            Uniques = true;
            Maps = true;
            Currency = true;
            DivinationCards = true;
            Jewels = true;
            Rgb = true;
            MinLinks = new RangeNode<int>(5, 0, 6);
            MinSockets = new RangeNode<int>(6, 0, 6);
            QualityItems = new QualityItemsSettings();
            BorderSettings = new BorderSettings();
            WithBorder = false;
            WithSound = false;
            Alternative = true;
            FilePath = string.Empty;
        }

        public ToggleNode ShowItemOnMap { get; set; }

        public ToggleNode Crafting { get; set; }

        public ToggleNode ShowText { get; set; }

        public ToggleNode HideOthers { get; set; }

        public ToggleNode PlaySound { get; set; }

        public RangeNode<int> TextSize { get; set; }

        public ToggleNode Rares { get; set; }

        public ToggleNode Uniques { get; set; }

        public ToggleNode Maps { get; set; }

        public ToggleNode Currency { get; set; }

        public ToggleNode DivinationCards { get; set; }

        public ToggleNode Jewels { get; set; }

        [JsonProperty("RGB")]
        public ToggleNode Rgb { get; set; }

        public RangeNode<int> MinLinks { get; set; }

        public RangeNode<int> MinSockets { get; set; }

        [JsonProperty("Show quality items")]
        public QualityItemsSettings QualityItems { get; set; }

        public BorderSettings BorderSettings { get; set; }

        public ToggleNode WithBorder { get; set; }

        public ToggleNode WithSound { get; set; }

        public ToggleNode Alternative { get; set; }

        public FileNode FilePath { get; set; }
    }
}