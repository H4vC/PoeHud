using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PoeHUD.Controllers;
using PoeHUD.Framework;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud.UI;
using PoeHUD.Models;
using PoeHUD.Models.Enums;
using PoeHUD.Models.Interfaces;
using PoeHUD.Poe.Components;
using SharpDX;
using SharpDX.Direct3D9;

namespace PoeHUD.Hud.KillCounter
{
    public class KillCounterPlugin : SizedPlugin<KillCounterSettings>
    {
        private readonly HashSet<EntityWrapper> aliveEntities;
        private readonly Dictionary<MonsterRarity, int> counters;
        private int summaryCounter;
        private int summaryCounterPerSession;
        private readonly GameController gameController;
        private readonly Dictionary<int, HashSet<long>> countedIds;
        private bool holdKey;

        public KillCounterPlugin(GameController gameController, Graphics graphics, KillCounterSettings settings)
            : base(gameController, graphics, settings)
        {
            this.gameController = gameController;
            aliveEntities = new HashSet<EntityWrapper>();
            countedIds = new Dictionary<int, HashSet<long>>();
            counters = new Dictionary<MonsterRarity, int>();
            Init();
            GameController.Area.OnAreaChange += area =>
            {
                if (!Settings.Enable)
                {
                    return;
                }
                aliveEntities.Clear();
                counters.Clear();
                summaryCounterPerSession += summaryCounter;
                summaryCounter = 0;
                Init();
            };
        }

        public override void Render()
        {
            if (!holdKey && WinApi.IsKeyDown(Keys.F10)){ return; }
            if (!Settings.Enable || GameController.Area.CurrentArea.IsHideout || GameController.Area.CurrentArea.IsTown)
            {
                return;
            }
            List<EntityWrapper> deadEntities = aliveEntities.Where(entity => !entity.IsAlive).ToList();
            foreach (EntityWrapper entity in deadEntities)
            {
                Calc(entity);
                aliveEntities.Remove(entity);
            }

            Vector2 position = StartDrawPointFunc();
            var size = new Size2();
            if (Settings.ShowDetail)
            {
                size = DrawCounters(position - 4);
            }
            var perSessionText = Settings.PerSession ? $" ({summaryCounterPerSession + summaryCounter})" : string.Empty;
            Size2 size2 = Graphics.DrawText($"kills: {summaryCounter} {perSessionText}", Settings.KillsFontSize, position.Translate(-size.Width / 1.5f - 7, size.Height + 4), Settings.FontColor, Settings.ShowDetail ? FontDrawFlags.Center : FontDrawFlags.Right);
            int width = Math.Max(size.Width, size2.Width);
            var bounds = new RectangleF(position.X - width - 69, position.Y - 1, width + 70, size.Height + size2.Height + 10);
            Graphics.DrawImage("preload-end.png", bounds, Settings.BackgroundColor);
            Graphics.DrawImage("preload-start.png", bounds, Settings.BackgroundColor);
            Size = bounds.Size;
            Margin = new Vector2(5, 0);
        }

        protected override void OnEntityAdded(EntityWrapper entityWrapper)
        {
            if (!Settings.Enable)
            {
                return;
            }
            if (!entityWrapper.HasComponent<Monster>()) return;
            if (entityWrapper.IsAlive)
            {
                aliveEntities.Add(entityWrapper);
            }
            else
            {
                Calc(entityWrapper);
            }
        }

        protected override void OnEntityRemoved(EntityWrapper entityWrapper)
        {
            if (aliveEntities.Contains(entityWrapper))
            {
                aliveEntities.Remove(entityWrapper);
            }
        }

        private void Calc(IEntity entityWrapper)
        {
            HashSet<long> monsterHashSet;
            var areaHash = gameController.Area.CurrentArea.Hash;
            if (!countedIds.TryGetValue(areaHash, out monsterHashSet))
            {
                monsterHashSet = new HashSet<long>();
                countedIds[areaHash] = monsterHashSet;
            }
            if (monsterHashSet.Contains(entityWrapper.LongId)) return;
            monsterHashSet.Add(entityWrapper.LongId);
            MonsterRarity rarity = entityWrapper.GetComponent<ObjectMagicProperties>().Rarity;
            if (!entityWrapper.IsHostile || !counters.ContainsKey(rarity)) return;
            counters[rarity]++;
            summaryCounter++;
        }

        private Size2 DrawCounter(Vector2 position, string label, string counterValue, Color color)
        {
            Size2 measuredSize1 = Graphics.MeasureText(counterValue, Settings.LabelFontSize, FontDrawFlags.Right);
            Size2 measuredSize2 = Graphics.MeasureText(label, 10, FontDrawFlags.Right);
            if (measuredSize1.Width > measuredSize2.Width)
            {
                Size2 size = Graphics.DrawText(counterValue, Settings.LabelFontSize, position, color, FontDrawFlags.Right);
                Size2 size2 = Graphics.DrawText(label, 10, position.Translate(-size.Width / 1f, size.Height), Settings.FontColor,
                    FontDrawFlags.Center);
                return new Size2(size.Width, size.Height + size2.Height);
            }
            else
            {
                Size2 size2 = Graphics.DrawText(label, 10, position.Translate(0, measuredSize1.Height), Settings.FontColor,
                    FontDrawFlags.Right);
                Size2 size = Graphics.DrawText(counterValue, Settings.LabelFontSize, position.Translate(-size2.Width / 1f, 0), color,
                    FontDrawFlags.Center);
                return new Size2(size2.Width, size.Height + size2.Height);
            }
        }

        private Size2 DrawCounters(Vector2 position)
        {
            const int INNER_MARGIN = 10;
            Size2 size = DrawCounter(position.Translate(-2, 6), "", counters[MonsterRarity.White].ToString(), Settings.FontColor);
            size = new Size2(DrawCounter(position.Translate(-size.Width - 20, 6), "",
                counters[MonsterRarity.Magic].ToString(), HudSkin.MagicColor).Width + size.Width + INNER_MARGIN, size.Height);
            size = new Size2(DrawCounter(position.Translate(-size.Width - 30, 6), "",
                counters[MonsterRarity.Rare].ToString(), HudSkin.RareColor).Width + size.Width + INNER_MARGIN, size.Height);
            size = new Size2(DrawCounter(position.Translate(-size.Width - 40, 6), "",
                counters[MonsterRarity.Unique].ToString(), HudSkin.UniqueColor).Width + size.Width + INNER_MARGIN, size.Height);
            return size;
        }

        private void Init()
        {
            foreach (MonsterRarity rarity in Enum.GetValues(typeof(MonsterRarity)))
            {
                counters[rarity] = 0;
            }
        }
    }
}