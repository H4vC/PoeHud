using System.Collections.Generic;

using PoeHUD.Models.Enums;

using SharpDX;

namespace PoeHUD.Hud.Loot
{
    public sealed class AlertDrawStyle
    {
        private static readonly Dictionary<ItemRarity, Color> colors = new Dictionary<ItemRarity, Color>
        {
            { ItemRarity.White, Color.White },
            { ItemRarity.Magic, HudSkin.MagicColor },
            { ItemRarity.Rare, HudSkin.RareColor },
            { ItemRarity.Unique, HudSkin.UniqueColor },
        };

        public AlertDrawStyle(Color color, ItemRarity rarity, int frameWidth, string text, int iconIndex)
        {
            FrameWidth = frameWidth;
            Text = text;
            IconIndex = iconIndex;

            if (color != Color.Black) // Checking against default of black as Color is a struct and I doubt anyone would use this color
            {
                AlertColor = color;
            }
            else
            {
                Color tempColor;
                AlertColor = colors.TryGetValue(rarity, out tempColor) ? tempColor : Color.White;
            }
        }

        public Color AlertColor { get; private set; }

        public int FrameWidth { get; private set; }

        public string Text { get; private set; }

        public int IconIndex { get; private set; }
    }
}