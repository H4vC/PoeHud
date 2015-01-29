using System;

using PoeHUD.Controllers;
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

        public XpRatePlugin(GameController gameController, Graphics graphics, XpRateSettings settings)
            : base(gameController, graphics, settings)
        {
            Reset();
            GameController.Area.OnAreaChange += area => Reset();
        }

        public override void Render()
        {
            base.Render();
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

            Vector2 position = StartDrawPointFunc();
            int fontSize = Settings.TextSize;

            Size2 xpRateSize = Graphics.DrawText(xpRate, fontSize, position, FontDrawFlags.Right);
            Vector2 secondLine = position.Translate(0, xpRateSize.Height);
            Size2 xpLeftSize = Graphics.DrawText(timeLeft, fontSize, secondLine, FontDrawFlags.Right);
            Vector2 thirdLine = secondLine.Translate(0, xpLeftSize.Height);
            string areaName = GameController.Area.CurrentArea.DisplayName;
            Size2 areaNameSize = Graphics.DrawText(areaName, fontSize, thirdLine, FontDrawFlags.Right);

            string timer = AreaInstance.GetTimeString(nowTime - GameController.Area.CurrentArea.TimeEntered);
            Size2 timerSize = Graphics.MeasureText(timer, fontSize);

            float boxWidth = MathHepler.Max(xpRateSize.Width, xpLeftSize.Width, areaNameSize.Width + timerSize.Width + 20) + 15;
            float boxHeight = xpRateSize.Height + xpLeftSize.Height + areaNameSize.Height;
            var bounds = new RectangleF(position.X - boxWidth + 5, position.Y - 5, boxWidth + 5, boxHeight + 10);

            string systemTime = string.Format("{0} ({1})", nowTime.ToShortTimeString(), GameController.Game.IngameState.CurFps);
            Graphics.DrawText(systemTime, fontSize, new Vector2(bounds.X + 5, position.Y), Color.White);
            Graphics.DrawText(timer, fontSize, new Vector2(bounds.X + 5, thirdLine.Y), Color.White);

            Graphics.DrawBox(bounds, Settings.BackgroundColor);
            Size = bounds.Size;
            Margin = new Vector2(0, 5);
        }

        private void CalculateXp(DateTime nowTime)
        {
            long currentXp = GameController.Player.GetComponent<Player>().XP;
            double rate = (currentXp - startXp) / (nowTime - startTime).TotalHours;
            xpRate = string.Format("{0} XP/h", ConvertHelper.ToShorten(rate, "0.00"));
            int level = GameController.Player.GetComponent<Player>().Level;
            if (level >= 0 && level + 1 < Constants.PlayerXpLevels.Length && rate > 1)
            {
                long xpLeft = Constants.PlayerXpLevels[level + 1] - currentXp;
                TimeSpan time = TimeSpan.FromHours(xpLeft / rate);
                timeLeft = string.Format("{0}h {1}M {2}s until level up", time.Hours, time.Minutes, time.Seconds);
            }
        }

        private void Reset()
        {
            if (GameController.InGame)
            {
                startXp = GameController.Player.GetComponent<Player>().XP;
            }

            startTime = lastTime = DateTime.Now;
            xpRate = "0.00 XP/h";
            timeLeft = "--h --m --s until level up";
        }
    }
}