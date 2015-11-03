using PoeHUD.Hud.UI;
using PoeHUD.Models.Enums;
using SharpDX;

namespace PoeHUD.Hud
{
    public class HudTexture
    {
        private string fileName;

        private readonly Color color;

        public HudTexture(string fileName) : this(fileName, Color.White) {}

        public HudTexture(string fileName, MonsterRarity rarity)
            : this(fileName, Color.White)
        {
            switch (rarity)
            {
                case MonsterRarity.Magic:
                    color = HudSkin.MagicColor;
                    break;
                case MonsterRarity.Rare:
                    color = HudSkin.RareColor;
                    break;
                case MonsterRarity.Unique:
                    color = HudSkin.UniqueColor;
                    break;
            }
        }

        public HudTexture(string fileName, Color color)
        {
            this.fileName = fileName;
            this.color = color;
        }

        public void Draw(Graphics graphics, RectangleF rectangle)
        {
            graphics.DrawImage(fileName, rectangle, color);
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                if (this.fileName != value)
                    this.fileName = value;
            }
        }

    }
}