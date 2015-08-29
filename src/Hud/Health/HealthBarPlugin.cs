using Newtonsoft.Json;
using PoeHUD.Controllers;
using PoeHUD.Framework.Helpers;
using PoeHUD.Models;
using PoeHUD.Poe.Components;
using PoeHUD.Poe.RemoteMemoryObjects;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Color = SharpDX.Color;
using Graphics = PoeHUD.Hud.UI.Graphics;
using RectangleF = SharpDX.RectangleF;

namespace PoeHUD.Hud.Health
{
    public class HealthBarPlugin : Plugin<HealthBarSettings>
    {
        private readonly Dictionary<CreatureType, List<HealthBar>> healthBars;
        private readonly DebuffPanelConfig debuffPanelConfig;

        public HealthBarPlugin(GameController gameController, Graphics graphics, HealthBarSettings settings)
            : base(gameController, graphics, settings)
        {
            CreatureType[] types = Enum.GetValues(typeof(CreatureType)).Cast<CreatureType>().ToArray();
            healthBars = new Dictionary<CreatureType, List<HealthBar>>(types.Length);
            foreach (CreatureType type in types)
            {
                healthBars.Add(type, new List<HealthBar>());
            }

            string json = File.ReadAllText("config/debuffPanel.json");
            debuffPanelConfig = JsonConvert.DeserializeObject<DebuffPanelConfig>(json);
        }

        public override void Render()
        {
            if (!Settings.Enable || !GameController.InGame || !Settings.ShowInTown && GameController.Area.CurrentArea.IsTown)
            {
                return;
            }

            RectangleF windowRectangle = GameController.Window.GetWindowRectangle();
            var windowSize = new Size2F(windowRectangle.Width / 2560, windowRectangle.Height / 1600);

            Camera camera = GameController.Game.IngameState.Camera;
            Func<HealthBar, bool> showHealthBar = x => x.IsShow(Settings.ShowEnemies);
            Parallel.ForEach(healthBars, x => x.Value.RemoveAll(hp => !hp.Entity.IsValid));
            foreach (HealthBar healthBar in healthBars.SelectMany(x => x.Value).AsParallel().AsOrdered().Where(hp => showHealthBar(hp) && hp.Entity.IsAlive))
            {
                Vector3 worldCoords = healthBar.Entity.Pos;
                Vector2 mobScreenCoords = camera.WorldToScreen(worldCoords.Translate(0, 0, -170), healthBar.Entity);
                if (mobScreenCoords != new Vector2())
                {
                    /**
                     * Render the health bar including the colours showing the proportion of life lost,
                    * and the flat numbers which overlay it.
                    */
                    var life = healthBar.Entity.GetComponent<Life>();
                    float scaledWidth = healthBar.Settings.Width * windowSize.Width;
                    float scaledHeight = healthBar.Settings.Height * windowSize.Height;
                    Color color = healthBar.Settings.Color;
                    float hpPercent = life.HPPercentage;
                    float esPercent = life.ESPercentage;
                    float hpWidth = hpPercent * scaledWidth;
                    float esWidth = esPercent * scaledWidth;
                    var bg = new RectangleF(mobScreenCoords.X - scaledWidth / 2, mobScreenCoords.Y - scaledHeight / 2, scaledWidth, scaledHeight);
                    if (Settings.ShowDebuffPanel)
                    {
                        DrawDebuffPanel(bg, healthBar, life);
                    }
                    if (hpPercent <= 0.1f)
                    {
                        color = healthBar.Settings.Under10Percent;
                    }
                    bg.Y = DrawFlatLifeAmount(life, hpPercent, healthBar.Settings, bg);
                    DrawFlatESAmount(life, healthBar.Settings, bg);
                    DrawPercents(healthBar.Settings, hpPercent, bg);
                    DrawBackground(color, healthBar.Settings.Outline, bg, hpWidth, esWidth);
                }
            }
        }

        private void DrawDebuffPanel(RectangleF bg, HealthBar healthBar, Life life)
        {
            var buffs = life.Buffs;
            if (buffs.Count > 0)
            {
                var isHostile = healthBar.Entity.IsHostile;
                var startY = bg.Top - Settings.DebuffPanelIconSize - 2;
                var startX = bg.Left;
                int debuffTable = 0;
                foreach (var buff in buffs)
                {
                    var buffName = buff.Name;
                    if (HasDebuff(debuffPanelConfig.Bleeding, buffName, isHostile))
                        debuffTable |= 1;
                    else if (HasDebuff(debuffPanelConfig.Poisoned, buffName, isHostile))
                        debuffTable |= 2;
                    else if (HasDebuff(debuffPanelConfig.ChilledFrozen, buffName, isHostile))
                        debuffTable |= 4;
                    else if (HasDebuff(debuffPanelConfig.Burning, buffName, isHostile))
                        debuffTable |= 8;
                    else if (HasDebuff(debuffPanelConfig.Shocked, buffName, isHostile))
                        debuffTable |= 16;
                    else if (HasDebuff(debuffPanelConfig.WeakenedSlowed, buffName, isHostile))
                        debuffTable |= 32;
                    else
                        debuffTable |= 0;
                }

                DrawAllDebuff(debuffTable, startX, startY);
            }
        }

        private void DrawAllDebuff(int debuffTable, float startX, float startY)
        {
            startX += DrawDebuff(() => (debuffTable & 1) == 1, startX, startY, 0, 4);
            startX += DrawDebuff(() => (debuffTable & 2) == 2, startX, startY, 1, 4);
            startX += DrawDebuff(() => (debuffTable & 4) == 4, startX, startY, 2);
            startX += DrawDebuff(() => (debuffTable & 8) == 8, startX, startY, 3, 4.5f);
            startX += DrawDebuff(() => (debuffTable & 16) == 16, startX, startY, 4, 5);
            DrawDebuff(() => (debuffTable & 32) == 32, startX, startY, 5);
        }

        private bool HasDebuff(Dictionary<string, int> dictionary, string buffName, bool isHostile)
        {
            int filterId;
            if (dictionary.TryGetValue(buffName, out filterId))
            {
                return filterId == 0 || isHostile == (filterId == 1);
            }
            return false;
        }

        private float DrawDebuff(Func<bool> predicate, float startX, float startY, int index, float marginFix = 0f)
        {
            if (predicate())
            {
                var size = Settings.DebuffPanelIconSize;
                const float ICON_COUNT = 6;
                float oneIconWidth = 1.0f / ICON_COUNT;
                if (marginFix > 0)
                    marginFix = oneIconWidth / marginFix;
                Graphics.DrawImage("debuff_panel.png", new RectangleF(startX, startY, size, size), new RectangleF(index / ICON_COUNT + marginFix, 0, oneIconWidth - marginFix, 1f), Color.White);
                return size - 1.2f * size * marginFix * ICON_COUNT;
            }
            return 0;
        }

        protected override void OnEntityAdded(EntityWrapper entity)
        {
            var healthbarSettings = new HealthBar(entity, Settings);
            if (healthbarSettings.IsValid)
            {
                healthBars[healthbarSettings.Type].Add(healthbarSettings);
            }
        }

        private void DrawBackground(Color color, Color outline, RectangleF bg, float hpWidth, float esWidth)
        {
            if (outline != Color.Black)
            {
                Graphics.DrawFrame(bg, 2, outline);
            }
            string healthBar = Settings.ShowIncrements ? "healthbar_increment.png" : "healthbar.png";
            Graphics.DrawImage("healthbar_bg.png", bg, color);
            var hpRectangle = new RectangleF(bg.X, bg.Y, hpWidth, bg.Height);
            Graphics.DrawImage(healthBar, hpRectangle, color, hpWidth * 10 / bg.Width);
            if (Settings.ShowES)
            {
                bg.Width = esWidth;
                Graphics.DrawImage("esbar.png", bg);
            }
        }

        /**
		 * Draw actual life amount over the life bar. Numbers over 1000 are
		 * truncated to 1k
		 */

        private float DrawFlatLifeAmount(Life life, float hpPercent,
            UnitSettings settings, RectangleF bg)
        {
            if (!settings.ShowHealthText)
            {
                return bg.Y;
            }

            string curHp = ConvertHelper.ToShorten(life.CurHP);
            string maxHp = ConvertHelper.ToShorten(life.MaxHP);
            string text = $"{curHp}/{maxHp}";
            Color color = hpPercent <= 0.1f ? settings.HealthTextColorUnder10Percent :
                settings.HealthTextColor;
            var position = new Vector2(bg.X + bg.Width / 2, bg.Y);
            Size2 size = Graphics.DrawText(text, settings.TextSize, position, color,
                FontDrawFlags.Center);
            return (int)bg.Y + (size.Height - bg.Height) / 2;
        }

        /**
		 * I didn't bother to have ES change colour as it gets low, sorry CI
		 * players!
		 */

        private void DrawFlatESAmount(Life life, UnitSettings settings, RectangleF bg)
        {
            if (!settings.ShowHealthText || life.MaxES == 0)
            {
                return;
            }

            string curES = ConvertHelper.ToShorten(life.CurES);
            string maxES = ConvertHelper.ToShorten(life.MaxES);
            string text = $"{curES}/{maxES}";
            Color color = settings.HealthTextColor;
            var position = new Vector2(bg.X + bg.Width / 2, (bg.Y - 12));
            Graphics.DrawText(text, settings.TextSize, position,
                color, FontDrawFlags.Center);
        }

        private void DrawPercents(UnitSettings settings, float hpPercent, RectangleF bg)
        {
            if (settings.ShowPercents)
            {
                string text = Convert.ToString((int)(hpPercent * 100));
                var position = new Vector2(bg.X + bg.Width + 4, bg.Y);
                Graphics.DrawText(text, settings.TextSize, position, settings.PercentTextColor);
            }
        }
    }
}