using System;
using System.Windows.Forms;
using PoeHUD.Controllers;
using PoeHUD.Framework;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud.UI;
using PoeHUD.Models;
using PoeHUD.Poe.Components;
using SharpDX;
using SharpDX.Direct3D9;

namespace PoeHUD.Hud.XpRate
{
    public class XpRatePlugin : SizedPlugin<XpRateSettings>
    {
        private string xpRate, timeLeft;
        private DateTime startTime, lastTime;
        private long startXp;
        private bool holdKey;
        public XpRatePlugin(GameController gameController, Graphics graphics, XpRateSettings settings)
            : base(gameController, graphics, settings)
        {
            Reset();
            GameController.Area.OnAreaChange += area => Reset();
        }

        public override void Render()
        {
            base.Render();

            if (!holdKey && WinApi.IsKeyDown(Keys.F10))
            {
                holdKey = true;
                Settings.Enable.Value = !Settings.Enable.Value;
            }
            else if (holdKey && !WinApi.IsKeyDown(Keys.F10))
            {
                holdKey = false;
            }

            if (!Settings.Enable || (GameController.Player != null && GameController.Player.GetComponent<Player>().Level >= 100))
            {
                return;
            }

            DateTime nowTime = DateTime.Now;
            TimeSpan elapsedTime = nowTime - lastTime;
            if (elapsedTime.TotalSeconds > 1)
            {
                CalculateXp(nowTime);
                lastTime = nowTime;
            }

            //this should be simplified or combined with xph
            if (Settings.OnlyAreaName)
            {
                var position = StartDrawPointFunc();
                string latency = $"( {GameController.Game.IngameState.CurLatency} )";
                string areaName = $"{GameController.Area.CurrentArea.DisplayName}";
                var areaNameSize = Graphics.MeasureText(areaName, Settings.FontSize);
                float boxHeight = areaNameSize.Height;
                float boxWidth = MathHepler.Max(areaNameSize.Width);
                var bounds = new RectangleF(position.X - 72 - boxWidth, position.Y - 5, boxWidth + 80, boxHeight + 12);

                Graphics.DrawText(areaName, Settings.FontSize, new Vector2(bounds.X + 74, position.Y), Settings.AreaFontColor);
                Graphics.DrawImage("preload-start.png", bounds, Settings.BackgroundColor);
                Graphics.DrawImage("preload-end.png", bounds, Settings.BackgroundColor);
                if (Settings.ShowLatency)
                {
                    Graphics.DrawText(latency, Settings.FontSize, new Vector2(bounds.X + 25, position.Y), Settings.LatencyFontColor);
                }
                Size = bounds.Size;
                Margin = new Vector2(0, 5);
            }

            if (!Settings.OnlyAreaName)
            {
                Vector2 position = StartDrawPointFunc();
                string areaName = GameController.Area.CurrentArea.DisplayName;
                Size2 areaNameSize = Graphics.DrawText(areaName, Settings.FontSize, position, Settings.AreaFontColor, FontDrawFlags.Right);
                Vector2 secondLine = position.Translate(0, areaNameSize.Height);
                Size2 xpRateSize = Graphics.DrawText(timeLeft, Settings.FontSize, secondLine, Settings.TimeLeftColor, FontDrawFlags.Right);
                Vector2 thirdLine = secondLine.Translate(0, xpRateSize.Height);
                Size2 xpLeftSize = Graphics.DrawText(xpRate, Settings.FontSize, thirdLine, Settings.TimeLeftColor, FontDrawFlags.Right);
                string timer = AreaInstance.GetTimeString(nowTime - GameController.Area.CurrentArea.TimeEntered);
                Size2 timerSize = Graphics.MeasureText(timer, Settings.FontSize);

                float boxWidth = MathHepler.Max(xpRateSize.Width, xpLeftSize.Width, areaNameSize.Width + 85, timerSize.Width);
                float boxHeight = xpRateSize.Height + xpLeftSize.Height + areaNameSize.Height;
                var bounds = new RectangleF(position.X - boxWidth - 81, position.Y - 5, boxWidth + 90, boxHeight + 13);

                string fps = $"fps ( {GameController.Game.IngameState.CurFps} )";
                string ping = $"ping ( {GameController.Game.IngameState.CurLatency} )";
                Size2 timeFpsSize = Graphics.MeasureText(fps, Settings.FontSize);
                var dif = bounds.Width - (12 + timeFpsSize.Width + xpRateSize.Width);
                if (dif < 0)
                {
                    bounds.X += dif;
                    bounds.Width -= dif;
                }
                Graphics.DrawText(fps, Settings.FontSize, new Vector2(bounds.X + 45, position.Y), Settings.FpsFontColor);
                Graphics.DrawText(timer, Settings.FontSize, new Vector2(bounds.X + 45, secondLine.Y), Settings.TimerFontColor);
                Graphics.DrawText(ping, Settings.FontSize, new Vector2(bounds.X + 45, thirdLine.Y), Settings.LatencyFontColor);
                Graphics.DrawImage("preload-start.png", bounds, Settings.BackgroundColor);
                Graphics.DrawImage("preload-end.png", bounds, Settings.BackgroundColor);
                Size = bounds.Size;
                Margin = new Vector2(0, 5);
            }
        }

        private void CalculateXp(DateTime nowTime)
        {
            long currentXp = GameController.Player.GetComponent<Player>().XP;
            double rate = (currentXp - startXp) / (nowTime - startTime).TotalHours;
            xpRate = $"{ConvertHelper.ToShorten(rate, "0.00")} xp/h";
            int level = GameController.Player.GetComponent<Player>().Level;
            if (level >= 0 && level + 1 < Constants.PlayerXpLevels.Length && rate > 1)
            {
                long xpLeft = Constants.PlayerXpLevels[level + 1] - currentXp;
                TimeSpan time = TimeSpan.FromHours(xpLeft / rate);
                timeLeft = $"{time.Hours}h {time.Minutes}m {time.Seconds}s to level up";
            }
        }

        private void Reset()
        {
            if (GameController.InGame)
            {
                startXp = GameController.Player.GetComponent<Player>().XP;
            }
            startTime = lastTime = DateTime.Now;
            xpRate = "0.00 xp/h";
            timeLeft = "--h --m --s to level up";
        }
    }
}