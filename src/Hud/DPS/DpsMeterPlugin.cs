using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PoeHUD.Controllers;
using PoeHUD.Framework;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud.UI;
using PoeHUD.Models;
using PoeHUD.Poe.Components;
using SharpDX;
using SharpDX.Direct3D9;

namespace PoeHUD.Hud.Dps
{
    public class DpsMeterPlugin : SizedPlugin<DpsMeterSettings>
    {
        private const double DPS_PERIOD = 0.2;
        private DateTime lastTime;
        private readonly Dictionary<long, int> lastMonsters = new Dictionary<long, int>();
        private double[] damageMemory = new double[10];
        private int damageMemoryIndex;
        private int maxDps;
        private bool holdKey;

        public DpsMeterPlugin(GameController gameController, Graphics graphics, DpsMeterSettings settings)
            : base(gameController, graphics, settings)
        {
            lastTime = DateTime.Now;
            GameController.Area.OnAreaChange += area =>
            {
                lastTime = DateTime.Now;
                maxDps = 0;
                damageMemory = new double[10];
                lastMonsters.Clear();
            };
        }

        public override void Render()
        {
            base.Render();
            if (!holdKey && WinApi.IsKeyDown(Keys.F10)){ return; }
            if (!Settings.Enable || GameController.Area.CurrentArea.IsHideout || GameController.Area.CurrentArea.IsTown)
            {
                return;
            }
            DateTime nowTime = DateTime.Now;
            TimeSpan elapsedTime = nowTime - lastTime;
            if (elapsedTime.TotalSeconds > DPS_PERIOD)
            {
                damageMemoryIndex++;
                if (damageMemoryIndex >= damageMemory.Length)
                {
                    damageMemoryIndex = 0;
                }
                damageMemory[damageMemoryIndex] = CalculateDps();
                lastTime = nowTime;
            }

            Vector2 position = StartDrawPointFunc();
            var dps = (int)damageMemory.Sum();
            maxDps = Math.Max(dps, maxDps);

            string dpsText = dps + " dps";
            string peakText = maxDps + " top dps";
            Size2 dpsSize = Graphics.DrawText(dpsText, Settings.DpsTextSize, position, Settings.DpsFontColor, FontDrawFlags.Right);
            Size2 peakSize = Graphics.DrawText(peakText, Settings.PeakDpsTextSize, position.Translate(0, dpsSize.Height),
                Settings.PeakFontColor, FontDrawFlags.Right);

            int width = Math.Max(peakSize.Width, dpsSize.Width);
            int height = dpsSize.Height + peakSize.Height;
            var bounds = new RectangleF(position.X - 15 - width - 20, position.Y - 5, width + 40, height + 10);
            Graphics.DrawImage("preload-end.png", bounds, Settings.BackgroundColor);
            Graphics.DrawImage("preload-start.png", bounds, Settings.BackgroundColor);
            Size = bounds.Size;
            Margin = new Vector2(5, 0);
        }

        private double CalculateDps()
        {
            int totalDamage = 0;
            foreach (EntityWrapper monster in GameController.Entities.Where(x => x.HasComponent<Monster>() && x.IsHostile))
            {
                var life = monster.GetComponent<Life>();
                int hp = monster.IsAlive ? life.CurHP + life.CurES : 0;
                if (hp <= -1000000 || hp >= 10000000) continue;
                int lastHP;
                if (lastMonsters.TryGetValue(monster.LongId, out lastHP))
                {
                    if (lastHP != hp)
                    {
                        totalDamage += lastHP - hp;
                    }
                }

                lastMonsters[monster.LongId] = hp;
            }
            return totalDamage < 0 ? 0 : totalDamage;
        }
    }
}