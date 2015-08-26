using Newtonsoft.Json;
using PoeHUD.Hud.Settings;
using SharpDX;

namespace PoeHUD.Hud.Loot
{
    public sealed class ItemAlertSettings : SettingsBase
    {
        public ItemAlertSettings()
        {
            Enable = true;
            ShowItemOnMap = true;
            ShowScrolls = true;
            Crafting = true;
            ShowText = true;
            HideOthers = false;
            PlaySound = true;
            TextSize = new RangeNode<int>(16, 10, 20);
            Rares = true;
            Uniques = true;
            Maps = true;
            Jewels = true;
            Cards = true;
            Chaos = true;
            Exalted = true;
            Currency = true;
            Rgb = true;
            Sockets = true;
            Linked = true;
            MinLinks = new RangeNode<int>(5, 0, 6);
            MinSockets = new RangeNode<int>(6, 0, 6);
            QualityItems = new QualityItemsSettings();
            BorderSettings = new BorderSettings();

            FrameExaltedColor = new ColorBGRA(220, 0, 0, 255);
            ExaltedColor = new ColorBGRA(220, 0, 0, 255);
            FrameJewelsColor = new ColorBGRA(74, 230, 58, 255);
            JewelsColor = new ColorBGRA(153, 255, 153, 255);
            FrameCardsColor = new ColorBGRA(220, 0, 0, 255);
            CardsColor = new ColorBGRA(220, 0, 0, 255);
            FrameCurrencyColor = new ColorBGRA(153, 0, 153, 255);
            CurrencyColor = new ColorBGRA(170, 158, 130, 255);
            FrameMapsColor = new ColorBGRA(222, 184, 135, 255);
            MapsColor = new ColorBGRA(222, 184, 135, 255);
            FrameRGBColor = new ColorBGRA(220, 0, 0, 255);
            RGBColor = new ColorBGRA(255, 255, 255, 255);
            FrameCraftingColor = new ColorBGRA(0, 150, 225, 255);
            CraftingColor = new ColorBGRA(255, 255, 225, 255);
            FrameSocketsColor = new ColorBGRA(74, 230, 58, 255);
            SocketsColor = new ColorBGRA(50, 255, 0, 255);
            FrameLinkedColor = new ColorBGRA(30, 144, 255, 255);
            LinkedColor = new ColorBGRA(30, 144, 255, 255);
            FrameChaosColor = new ColorBGRA(170, 0, 170, 255);
            ChaosColor = new ColorBGRA(220, 0, 220, 255);
            ScrollsColor = new ColorBGRA(170, 158, 130, 255);
            BackgroundColor = new ColorBGRA(0, 0, 0, 220);
        }

        public ColorNode ScrollsColor { get; set; }
        public ColorNode BackgroundColor { get; set; }
        public ColorNode FrameExaltedColor { get; set; }
        public ColorNode ExaltedColor { get; set; }
        public ColorNode FrameJewelsColor { get; set; }
        public ColorNode JewelsColor { get; set; }
        public ColorNode FrameCardsColor { get; set; }
        public ColorNode CardsColor { get; set; }
        public ColorNode FrameCurrencyColor { get; set; }
        public ColorNode CurrencyColor { get; set; }
        public ColorNode FrameMapsColor { get; set; }
        public ColorNode MapsColor { get; set; }
        public ColorNode FrameRGBColor { get; set; }
        public ColorNode RGBColor { get; set; }
        public ColorNode FrameCraftingColor { get; set; }
        public ColorNode CraftingColor { get; set; }
        public ColorNode FrameSocketsColor { get; set; }
        public ColorNode SocketsColor { get; set; }
        public ColorNode FrameLinkedColor { get; set; }
        public ColorNode LinkedColor { get; set; }
        public ColorNode FrameChaosColor { get; set; }
        public ColorNode ChaosColor { get; set; }
        public ToggleNode ShowItemOnMap { get; set; }
        public ToggleNode ShowScrolls { get; set; }
        public ToggleNode Crafting { get; set; }
        public ToggleNode ShowText { get; set; }
        public ToggleNode HideOthers { get; set; }
        public ToggleNode PlaySound { get; set; }
        public RangeNode<int> TextSize { get; set; }
        public ToggleNode Rares { get; set; }
        public ToggleNode Uniques { get; set; }
        public ToggleNode Maps { get; set; }
        public ToggleNode Currency { get; set; }
        public ToggleNode Jewels { get; set; }
        public ToggleNode Cards { get; set; }
        public ToggleNode Chaos { get; set; }
        public ToggleNode Exalted { get; set; }

        [JsonProperty("RGB")]
        public ToggleNode Rgb { get; set; }

        public RangeNode<int> MinLinks { get; set; }
        public ToggleNode Linked { get; set; }
        public RangeNode<int> MinSockets { get; set; }
        public ToggleNode Sockets { get; set; }

        [JsonProperty("Show quality items")]
        public QualityItemsSettings QualityItems { get; set; }

        public BorderSettings BorderSettings { get; set; }
    }
}