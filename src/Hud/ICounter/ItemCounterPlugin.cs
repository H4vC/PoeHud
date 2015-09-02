/*
 * (c) chuede (Siriais)
 * 
 * this Plugin cant be used currently as the Item id Always changes ins therefore can be counted a lot more than one time.
 * Als long as i cannot find a Uniqe Id to avoid an item beeing counted multiple times, this can never beused.
 * 
 * to Enable this in the Hud , Uncomment the entries in ExternalOverlay.cs and 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using PoeHUD.Controllers;
using PoeHUD.Framework.Helpers;
using PoeHUD.Models.Interfaces;
using PoeHUD.Hud.UI;
using PoeHUD.Models;
using PoeHUD.Models.Enums;
using PoeHUD.Poe.Components;

using SharpDX;
using SharpDX.Direct3D9;


namespace PoeHUD.Hud.ICounter
{
    class ItemCounterPlugin : SizedPlugin<ItemCounterSettings>
    {
        private readonly Dictionary<ItemRarity, int> counters;
        private readonly HashSet<long> countedIds;
        private int totalDrops;

        public ItemCounterPlugin(GameController gameController, Graphics graphics, ItemCounterSettings settings)
            : base(gameController, graphics, settings)
        {
            countedIds = new HashSet<long>();
            counters = new Dictionary<ItemRarity, int>();
            totalDrops = 0;
            countedIds.Clear();
            // Initialize Rarity-Counter
            counters.Clear();
            foreach (ItemRarity rarity in Enum.GetValues(typeof(ItemRarity)))
                counters.Add(rarity, 0);
            GameController.Area.OnAreaChange += area =>
            {
                if (!Settings.Enable) // no need to do Anything if this plugin isnt enabled
                        return;
                totalDrops = 0;
                countedIds.Clear();
                // Initialize Rarity-Counter
                counters.Clear();
                foreach (ItemRarity rarity in Enum.GetValues(typeof(ItemRarity)))
                    counters.Add(rarity, 0);
            };
        }

        public override void Render()
        {
            base.Render();
            if (!Settings.Enable)
            {
                return;
            }

            Vector2 position = StartDrawPointFunc();
            var size = new Size2();
            if (Settings.ShowDetail)
            {
                size = DrawDetail(position);
            }
            Size2 size2 = Graphics.DrawText(string.Format("Items {0}", totalDrops), 14,
                position.Translate(-size.Width / 2f, size.Height),
                Settings.ShowDetail ? FontDrawFlags.Center : FontDrawFlags.Right);
            int width = Math.Max(size.Width, size2.Width);
            var bounds = new RectangleF(position.X - width - 5, position.Y - 5, width + 10, size.Height + size2.Height + 10);
            Graphics.DrawBox(bounds, new ColorBGRA(0, 0, 0, 180));
            Size = bounds.Size;
            Margin = new Vector2(5, 0);
        }

        protected override void OnEntityAdded(EntityWrapper entity)
        {
            if (!Settings.Enable) // Plugin not enabled
                return;
            if (entity.HasComponent<WorldItem>()) // Dropped in World ?
            {
                IEntity item = entity.GetComponent<WorldItem>().ItemEntity;
                if (countedIds.Contains(item.Id)) // Already counted. Do Noting !
                    return;
                countedIds.Add(item.Id);
                var mods = item.GetComponent<Mods>();
                ItemRarity rarity = mods.ItemRarity;
                counters[rarity] += 1;
                totalDrops += 1;
                File.AppendAllText(Environment.CurrentDirectory+"\\drops.txt",String.Format ("{0} -> {1}{2}",item.Id,item.ToString(),Environment.NewLine));
            }
        }

        private Size2 DrawDetail(Vector2 position)
        {
            const int INNER_MARGIN = 7;
            Size2 size = DrawSingleCounter(position, "white", counters[ItemRarity.Normal].ToString(), Color.White);
            size = new Size2(
                    DrawSingleCounter(position.Translate(-size.Width - INNER_MARGIN, 0), "magic",
                        counters[ItemRarity.Magic].ToString(), HudSkin.MagicColor).Width + size.Width + INNER_MARGIN,
                    size.Height);
            size = new Size2(
                    DrawSingleCounter(position.Translate(-size.Width - INNER_MARGIN, 0), "rare", counters[ItemRarity.Rare].ToString(),
                        HudSkin.RareColor).Width + size.Width + INNER_MARGIN, size.Height);
            size = new Size2(
                    DrawSingleCounter(position.Translate(-size.Width - INNER_MARGIN, 0), "uniq",
                        counters[ItemRarity.Unique].ToString(), HudSkin.UniqueColor).Width + size.Width + INNER_MARGIN,
                    size.Height);
            return size;
        }

        private Size2 DrawSingleCounter(Vector2 position, string label, string counterValue, Color color)
        {
            Size2 measuredSize1 = Graphics.MeasureText(counterValue, 25, FontDrawFlags.Right);
            Size2 measuredSize2 = Graphics.MeasureText(label, 11, FontDrawFlags.Right);
            if (measuredSize1.Width > measuredSize2.Width)
            {
                Size2 size = Graphics.DrawText(counterValue, 25, position, color, FontDrawFlags.Right);
                Size2 size2 = Graphics.DrawText(label, 11, position.Translate(-size.Width / 2f, size.Height), Color.White,
                    FontDrawFlags.Center);
                return new Size2(size.Width, size.Height + size2.Height);
            }
            else
            {
                Size2 size2 = Graphics.DrawText(label, 11, position.Translate(0, measuredSize1.Height), Color.White,
                    FontDrawFlags.Right);
                Size2 size = Graphics.DrawText(counterValue, 25, position.Translate(-size2.Width / 2f, 0), color,
                    FontDrawFlags.Center);
                return new Size2(size2.Width, size.Height + size2.Height);
            }
        }

    }
}
