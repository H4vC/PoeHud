using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using PoeHUD.Controllers;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud.Settings;
using PoeHUD.Hud.UI;
using PoeHUD.Models;
using PoeHUD.Models.Enums;
using PoeHUD.Models.Interfaces;
using PoeHUD.Poe;
using PoeHUD.Poe.Components;
using PoeHUD.Poe.Elements;
using PoeHUD.Poe.RemoteMemoryObjects;
using PoeHUD.Poe.UI.Elements;

using SharpDX;
using SharpDX.Direct3D9;

namespace PoeHUD.Hud.Loot
{
    public class ItemAlertPlugin : SizedPluginWithMapIcons<ItemAlertSettings>
    {
        private readonly HashSet<long> playedSoundsCache;

        private readonly Dictionary<EntityWrapper, AlertDrawStyle> currentAlerts;

        private readonly Dictionary<string, CraftingBase> craftingBases;

        private readonly HashSet<string> currencyNames;

        private Dictionary<int, ItemsOnGroundLabelElement> currentLabels;

        public ItemAlertPlugin(GameController gameController, Graphics graphics, ItemAlertSettings settings)
            : base(gameController, graphics, settings)
        {
            playedSoundsCache = new HashSet<long>();
            currentAlerts = new Dictionary<EntityWrapper, AlertDrawStyle>();
            currentLabels = new Dictionary<int, ItemsOnGroundLabelElement>();
            currencyNames = LoadCurrency();
            craftingBases = LoadCraftingBases();

            GameController.Area.OnAreaChange += OnAreaChange;
        }

        public override void Dispose()
        {
            GameController.Area.OnAreaChange -= OnAreaChange;
        }

        public override void Render()
        {
            base.Render();
            if (!Settings.Enable || !Settings.ShowText)
            {
                return;
            }

            Vector2 playerPos = GameController.Player.GetComponent<Positioned>().GridPos;
            Vector2 position = StartDrawPointFunc();
            const int BOTTOM_MARGIN = 2;
            foreach (KeyValuePair<EntityWrapper, AlertDrawStyle> kv in currentAlerts.Where(x => x.Key.IsValid))
            {
                string text = GetItemName(kv);
                if (text == null)
                {
                    continue;
                }

                if (Settings.BorderSettings.Enable)
                {
                    DrawBorder(kv.Key.Address);
                }

                var padding = new Vector2(5, 2);
                Vector2 delta = kv.Key.GetComponent<Positioned>().GridPos - playerPos;
                Vector2 itemSize = DrawItem(kv.Value, delta, position, padding, text);
                position.Y += itemSize.Y + BOTTOM_MARGIN;
            }
            Size = new Size2F(0, position.Y); //bug absent width
        }

        protected override void OnEntityAdded(EntityWrapper entity)
        {
            if (!GameController.Area.CurrentArea.IsTown && Settings.Enable && !currentAlerts.ContainsKey(entity) && entity.HasComponent<WorldItem>())
            {
                IEntity item = entity.GetComponent<WorldItem>().ItemEntity;
                // Check if we should alert WHILE loading item to preven useless compute
                ItemUsefulProperties props = initItem(item);

                if (props.ShouldAlert(currencyNames, Settings))
                {
                    AlertDrawStyle drawStyle = props.GetDrawStyle();
                    currentAlerts.Add(entity, drawStyle);
                    CurrentIcons[entity] = new MapIcon(entity, new HudTexture("minimap_default_icon.png", drawStyle.AlertColor), () => Settings.ShowItemOnMap, 8);

                    if (Settings.PlaySound && !playedSoundsCache.Contains(entity.LongId))
                    {
                        playedSoundsCache.Add(entity.LongId);
                        Sounds.AlertSound.Play();
                    }
                }
            }
        }

        protected override void OnEntityRemoved(EntityWrapper entity)
        {
            base.OnEntityRemoved(entity);
            currentAlerts.Remove(entity);
            currentLabels.Remove(entity.Address);
        }

        private static Dictionary<string, CraftingBase> LoadCraftingBases()
        {
            if (!File.Exists("config/crafting_bases.txt"))
            {
                return new Dictionary<string, CraftingBase>();
            }
            var dictionary = new Dictionary<string, CraftingBase>(StringComparer.OrdinalIgnoreCase);
            var parseErrors = new List<string>();
            string[] array = File.ReadAllLines("config/crafting_bases.txt");
            foreach (string text in array.Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x) && !x.StartsWith("#")))
            {
                string[] parts = text.Split(new[] { ',' });
                string itemName = parts[0].Trim();

                var item = new CraftingBase { Name = itemName };

                int tmpVal;
                if (parts.Length > 1 && int.TryParse(parts[1], out tmpVal))
                {
                    item.MinItemLevel = tmpVal;
                }

                if (parts.Length > 2 && int.TryParse(parts[2], out tmpVal))
                {
                    item.MinQuality = tmpVal;
                }

                const int RARITY_POSITION = 3;
                if (parts.Length > RARITY_POSITION)
                {
                    item.Rarities = new ItemRarity[parts.Length - 3];
                    for (int i = RARITY_POSITION; i < parts.Length; i++)
                    {
                        if (!Enum.TryParse(parts[i], true, out item.Rarities[i - RARITY_POSITION]))
                        {
                            parseErrors.Add("Incorrect rarity definition at line: " + text);
                            item.Rarities = null;
                        }
                    }
                }

                if (!dictionary.ContainsKey(itemName))
                {
                    dictionary.Add(itemName, item);
                }
                else
                {
                    parseErrors.Add("Duplicate definition for item was ignored: " + text);
                }
            }

            if (parseErrors.Any())
            {
                throw new Exception("Error parsing config/crafting_bases.txt\r\n" + string.Join(Environment.NewLine, parseErrors));
            }

            return dictionary;
        }

        private static HashSet<string> LoadCurrency()
        {
            if (!File.Exists("config/currency.txt"))
            {
                return null;
            }
            var hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string[] lines = File.ReadAllLines("config/currency.txt");
            lines.Where(x => !string.IsNullOrWhiteSpace(x)).ForEach(x => hashSet.Add(x.Trim().ToLowerInvariant()));
            return hashSet;
        }

        private void DrawBorder(int entityAddres)
        {
            IngameUIElements ui = GameController.Game.IngameState.IngameUi;
            if (currentLabels.ContainsKey(entityAddres))
            {
                ItemsOnGroundLabelElement entitylabel = currentLabels[entityAddres];
                if (entitylabel.IsVisible)
                {
                    RectangleF rect = entitylabel.Label.GetClientRect();
                    if (ui.OpenLeftPanel.IsVisible && ui.OpenLeftPanel.GetClientRect().Intersects(rect)
                        || ui.OpenRightPanel.IsVisible && ui.OpenRightPanel.GetClientRect().Intersects(rect))
                    {
                        return;
                    }

                    ColorNode borderColor = Settings.BorderSettings.BorderColor;
                    if (!entitylabel.CanPickUp)
                    {
                        borderColor = Settings.BorderSettings.NotMyItemBorderColor;
                        TimeSpan timeLeft = entitylabel.TimeLeft;
                        if (Settings.BorderSettings.ShowTimer && timeLeft.TotalMilliseconds > 0)
                        {
                            borderColor = Settings.BorderSettings.CantPickUpBorderColor;
                            Graphics.DrawText(timeLeft.ToString(@"mm\:ss"), Settings.BorderSettings.TimerTextSize,
                                rect.TopRight.Translate(4, 0));
                        }
                    }
                    Graphics.DrawFrame(rect, Settings.BorderSettings.BorderWidth, borderColor);
                }
            }
            else
            {
                currentLabels = ui.ItemsOnGroundLabels.ToDictionary(y => y.ItemOnGround.Address, y => y);
            }
        }

        private Vector2 DrawItem(AlertDrawStyle drawStyle, Vector2 delta, Vector2 position, Vector2 padding, string text)
        {
            padding.X -= drawStyle.FrameWidth;
            padding.Y -= drawStyle.FrameWidth;
            double phi;
            double distance = delta.GetPolarCoordinates(out phi);
            float compassOffset = Settings.TextSize + 8;
            Vector2 textPos = position.Translate(-padding.X - compassOffset, padding.Y);
            Size2 textSize = Graphics.DrawText(text, Settings.TextSize, textPos, drawStyle.AlertColor, FontDrawFlags.Right);
            int iconSize = drawStyle.IconIndex >= 0 ? textSize.Height : 0;

            float fullHeight = textSize.Height + 2 * padding.Y + 2 * drawStyle.FrameWidth;
            float fullWidth = textSize.Width + 2 * padding.X + iconSize + 2 * drawStyle.FrameWidth + compassOffset;
            var boxRect = new RectangleF(position.X - fullWidth, position.Y, fullWidth - compassOffset, fullHeight);
            Graphics.DrawBox(boxRect, new ColorBGRA(0, 0, 0, 180));

            RectangleF rectUV = GetDirectionsUV(phi, distance);
            var rectangleF = new RectangleF(position.X - padding.X - compassOffset + 6, position.Y + padding.Y,
                textSize.Height, textSize.Height);
            Graphics.DrawImage("directions.png", rectangleF, rectUV);

            if (iconSize > 0)
            {
                const float ICONS_IN_SPRITE = 4;
                var iconPos = new RectangleF(textPos.X - iconSize - textSize.Width, textPos.Y, iconSize, iconSize);
                float iconX = drawStyle.IconIndex / ICONS_IN_SPRITE;
                var uv = new RectangleF(iconX, 0, (drawStyle.IconIndex + 1) / ICONS_IN_SPRITE - iconX, 1);
                Graphics.DrawImage("item_icons.png", iconPos, uv);
            }
            if (drawStyle.FrameWidth > 0)
            {
                Graphics.DrawFrame(boxRect, drawStyle.FrameWidth, drawStyle.AlertColor);
            }
            return new Vector2(fullWidth, fullHeight);
        }

        private ItemUsefulProperties initItem(IEntity item)
        {
            string name = GameController.Files.BaseItemTypes.Translate(item.Path);

            CraftingBase craftingBase = new CraftingBase();
            if (Settings.Crafting)
            {
                craftingBases.TryGetValue(name, out craftingBase);
            }

            return new ItemUsefulProperties(name, item, craftingBase);
        }

        private string GetItemName(KeyValuePair<EntityWrapper, AlertDrawStyle> kv)
        {
            string text;
            EntityLabel labelForEntity = GameController.EntityListWrapper.GetLabelForEntity(kv.Key);
            if (labelForEntity == null)
            {
                Entity itemEntity = kv.Key.GetComponent<WorldItem>().ItemEntity;
                if (!itemEntity.IsValid)
                {
                    return null;
                }
                text = kv.Value.Text;
            }
            else
            {
                text = labelForEntity.Text;
            }
            return text;
        }

        private void OnAreaChange(AreaController area)
        {
            playedSoundsCache.Clear();
            currentLabels.Clear();
        }
    }
}