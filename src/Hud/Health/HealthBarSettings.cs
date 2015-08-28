using PoeHUD.Hud.Settings;
using SharpDX;

namespace PoeHUD.Hud.Health
{
    public sealed class HealthBarSettings : SettingsBase
    {
        public HealthBarSettings()
        {
            Enable = false;
            ShowInTown = false;
            ShowES = true;
            //ShowBK = true;
            ShowIncrements = true;
            //HpColor = new ColorBGRA(255, 255, 255, 255);
            //EsColor = new ColorBGRA(255, 255, 255, 255);
            //BkColor = new ColorBGRA(255, 255, 255, 255);
            //EsWidth = new RangeNode<int>(10, - 50, 100);
            ShowEnemies = false;
            Players = new UnitSettings(0x008000ff, 0);
            //Me = true;
            Minions = new UnitSettings(0x90ee90ff, 0);
            NormalEnemy = new UnitSettings(0xff0000ff, 0, 0x66ff66ff, false);
            MagicEnemy = new UnitSettings(0xff0000ff, 0x8888ffff, 0x66ff99ff, false);
            RareEnemy = new UnitSettings(0xff0000ff, 0xffff77ff, 0x66ff99ff, true);
            UniqueEnemy = new UnitSettings(0xff0000ff, 0xffa500ff, 0x66ff99ff, true);
            ShowDebuffPanel = true;
            DebuffPanelIconSize = new RangeNode<int>(20, 15, 40);
        }
        //public RangeNode<int> EsWidth { get; set; }
        //public ColorNode EsColor { get; set; }
        //public ColorNode BkColor { get; set; }
        //public ColorNode HpColor { get; set; }
        public ToggleNode ShowInTown { get; set; }

        public ToggleNode ShowES { get; set; }

        //public ToggleNode ShowBK { get; set; }

        public ToggleNode ShowIncrements { get; set; }

        public ToggleNode ShowEnemies { get; set; }

        public UnitSettings Players { get; set; }
        //public ToggleNode Me { get; set; }

        public UnitSettings Minions { get; set; }

        public UnitSettings NormalEnemy { get; set; }

        public UnitSettings MagicEnemy { get; set; }

        public UnitSettings RareEnemy { get; set; }

        public UnitSettings UniqueEnemy { get; set; }

        public ToggleNode ShowDebuffPanel { get; set; }

        public RangeNode<int> DebuffPanelIconSize { get; set; }

    }
}