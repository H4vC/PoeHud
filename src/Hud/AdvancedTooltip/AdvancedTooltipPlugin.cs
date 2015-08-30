using PoeHUD.Controllers;
using PoeHUD.Framework;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud.Settings;
using PoeHUD.Hud.UI;
using PoeHUD.Models.Enums;
using PoeHUD.Poe;
using PoeHUD.Poe.Components;
using PoeHUD.Poe.Elements;
using PoeHUD.Poe.FilesInMemory;
using PoeHUD.Poe.RemoteMemoryObjects;
using PoeHUD.Poe.UI;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PoeHUD.Hud.AdvancedTooltip
{
    public class AdvancedTooltipPlugin : Plugin<AdvancedTooltipSettings>
    {
        private bool holdKey;
        private readonly SettingsHub settingsHub;

        private static readonly Color[] elementalDmgColors =
        {
            Color.White, HudSkin.DmgFireColor, HudSkin.DmgColdColor, HudSkin.DmgLightingColor, HudSkin.DmgChaosColor
        };

        private Entity itemEntity;

        private List<ModValue> mods = new List<ModValue>();

        public AdvancedTooltipPlugin(GameController gameController, Graphics graphics, AdvancedTooltipSettings settings, SettingsHub settingsHub)
            : base(gameController, graphics, settings)
        {
            this.settingsHub = settingsHub;
        }

        public override void Render()
        {
            if (!holdKey && WinApi.IsKeyDown(Keys.F9))
            {
                holdKey = true;
                Settings.ItemMods.Enable.Value = !Settings.ItemMods.Enable.Value;
                if (!Settings.ItemMods.Enable.Value)
                {
                    SettingsHub.Save(settingsHub);
                }
            }
            else if (holdKey && !WinApi.IsKeyDown(Keys.F9))
            {
                holdKey = false;
            }

            if (!Settings.Enable)
            {
                return;
            }

            Element uiHover = GameController.Game.IngameState.UIHover;
            var inventoryItemIcon = uiHover.AsObject<InventoryItemIcon>();
            Element tooltip = GetTooltip(inventoryItemIcon);
            Entity poeEntity = inventoryItemIcon.Item;
            if (tooltip == null || poeEntity.Address == 0 || !poeEntity.IsValid)
            {
                return;
            }

            RectangleF tooltipRect = tooltip.GetClientRect();
            var modsComponent = poeEntity.GetComponent<Mods>();
            if (itemEntity == null || itemEntity.Id != poeEntity.Id)
            {
                List<ItemMod> itemMods = modsComponent.ItemMods;
                mods = itemMods.Select(item => new ModValue(item, GameController.Files, modsComponent.ItemLevel)).ToList();
                itemEntity = poeEntity;
            }

            if (Settings.ItemLevel.Enable)
            {
                string itemLevel = Convert.ToString(modsComponent.ItemLevel);
                Graphics.DrawText(itemLevel, Settings.ItemLevel.TextSize, tooltipRect.TopLeft.Translate(1, -3), Color.Yellow);
            }

            if (Settings.ItemMods.Enable)
            {
                float bottomTooltip = tooltipRect.Bottom + 5;
                var modPosition = new Vector2(tooltipRect.X + 50, bottomTooltip + 4);
                float height = mods.Aggregate(modPosition, (position, item) => DrawMod(item, position)).Y - bottomTooltip;
                if (height > 4)
                {
                    var modsRect = new RectangleF(tooltipRect.X + 1, bottomTooltip, tooltipRect.Width, height);
                    Graphics.DrawBox(modsRect, Settings.ItemMods.BackgroundColor);
                }
            }

            if (Settings.WeaponDps.Enable && poeEntity.HasComponent<Weapon>())
            {
                DrawWeaponDps(tooltipRect);
            }
        }

        private static Element GetTooltip(InventoryItemIcon inventoryItemIcon)
        {
            Element tooltip = inventoryItemIcon.Tooltip;

            Element child = tooltip?.GetChildAtIndex(0);
            return child?.GetChildAtIndex(1);
        }

        private Vector2 DrawMod(ModValue item, Vector2 position)
        {
            const float EPSILON = 0.001f;
            const int MARGIN_BOTTOM = 4, MARGIN_LEFT = 50;

            Vector2 oldPosition = position;
            ItemModsSettings settings = Settings.ItemMods;

            string prefix = item.AffixType == ModsDat.ModType.Prefix
                ? "[P]"
                : item.AffixType == ModsDat.ModType.Suffix ? "[S]" : "[?]";
            if (item.AffixType != ModsDat.ModType.Hidden)
            {
                if (item.CouldHaveTiers())
                {
                    prefix += $" T{item.Tier} ";
                }

                Graphics.DrawText(prefix, settings.ModTextSize, position.Translate(5 - MARGIN_LEFT, 0));
                Size2 textSize = Graphics.DrawText(item.AffixText, settings.ModTextSize, position, item.Color);
                if (textSize != new Size2())
                {
                    position.Y += textSize.Height;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                IntRange range = item.Record.StatRange[i];
                if (range.Min == 0 && range.Max == 0)
                {
                    continue;
                }

                StatsDat.StatRecord stat = item.Record.StatNames[i];
                int value = item.StatValue[i];
                if (value <= -1000 || stat == null)
                {
                    continue;
                }
                float percents = range.GetPercentage(value);
                bool noSpread = !range.HasSpread();
                double hue = percents > 1 ? 180 : 120 * percents;
                if (noSpread)
                {
                    hue = 300;
                }

                string line2 = string.Format(noSpread ? "{0}" : "{0} [{1}]", stat, range);
                Graphics.DrawText(line2, settings.ModTextSize, position, Color.White);

                string statText = stat.ValueToString(value);
                Vector2 statPosition = position.Translate(-5, 0);
                Color statColor = ColorUtils.ColorFromHsv(hue, 1, 1);
                Size2 txSize = Graphics.DrawText(statText, settings.ModTextSize, statPosition, statColor, FontDrawFlags.Right);
                position.Y += txSize.Height;
            }
            return Math.Abs(position.Y - oldPosition.Y) > EPSILON ? position.Translate(0, MARGIN_BOTTOM) : oldPosition;
        }

        private void DrawWeaponDps(RectangleF clientRect)
        {
            var weapon = itemEntity.GetComponent<Weapon>();
            float aSpd = 1000f / weapon.AttackTime;
            int cntDamages = Enum.GetValues(typeof(DamageType)).Length;
            var doubleDpsPerStat = new float[cntDamages];
            float physDmgMultiplier = 1;
            doubleDpsPerStat[(int)DamageType.Physical] = weapon.DamageMax + weapon.DamageMin;
            foreach (ModValue mod in mods)
            {
                for (int iStat = 0; iStat < 4; iStat++)
                {
                    IntRange range = mod.Record.StatRange[iStat];
                    if (range.Min == 0 && range.Max == 0)
                    {
                        continue;
                    }

                    StatsDat.StatRecord theStat = mod.Record.StatNames[iStat];
                    int value = mod.StatValue[iStat];
                    switch (theStat.Key)
                    {
                        case "physical_damage_+%":
                        case "local_physical_damage_+%":
                            physDmgMultiplier += value / 100f;
                            break;

                        case "local_attack_speed_+%":
                            aSpd *= (100f + value) / 100;
                            break;

                        case "local_minimum_added_physical_damage":
                        case "local_maximum_added_physical_damage":
                            doubleDpsPerStat[(int)DamageType.Physical] += value;
                            break;

                        case "local_minimum_added_fire_damage":
                        case "local_maximum_added_fire_damage":
                        case "unique_local_minimum_added_fire_damage_when_in_main_hand":
                        case "unique_local_maximum_added_fire_damage_when_in_main_hand":
                            doubleDpsPerStat[(int)DamageType.Fire] += value;
                            break;

                        case "local_minimum_added_cold_damage":
                        case "local_maximum_added_cold_damage":
                        case "unique_local_minimum_added_cold_damage_when_in_off_hand":
                        case "unique_local_maximum_added_cold_damage_when_in_off_hand":
                            doubleDpsPerStat[(int)DamageType.Cold] += value;
                            break;

                        case "local_minimum_added_lightning_damage":
                        case "local_maximum_added_lightning_damage":
                            doubleDpsPerStat[(int)DamageType.Lightning] += value;
                            break;

                        case "unique_local_minimum_added_chaos_damage_when_in_off_hand":
                        case "unique_local_maximum_added_chaos_damage_when_in_off_hand":
                        case "local_minimum_added_chaos_damage":
                        case "local_maximum_added_chaos_damage":
                            doubleDpsPerStat[(int)DamageType.Chaos] += value;
                            break;
                    }
                }
            }

            doubleDpsPerStat[(int)DamageType.Physical] *= physDmgMultiplier;
            int quality = itemEntity.GetComponent<Quality>().ItemQuality;
            if (quality > 0)
            {
                doubleDpsPerStat[(int)DamageType.Physical] += (weapon.DamageMax + weapon.DamageMin) * quality / 100f;
            }

            float pDps = doubleDpsPerStat[(int)DamageType.Physical] / 2 * aSpd;
            float eDps = 0;
            int firstEmg = 0;
            Color eDpsColor = Color.White;

            for (int i = 1; i < cntDamages; i++)
            {
                eDps += doubleDpsPerStat[i] / 2 * aSpd;
                if (doubleDpsPerStat[i] > 0)
                {
                    if (firstEmg == 0)
                    {
                        firstEmg = i;
                        eDpsColor = elementalDmgColors[i];
                    }
                    else
                    {
                        eDpsColor = Color.DarkViolet;
                    }
                }
            }

            WeaponDpsSettings settings = Settings.WeaponDps;
            var textPosition = new Vector2(clientRect.Right - 2, clientRect.Y + 1);
            Size2 pDpsSize = pDps > 0
                ? Graphics.DrawText(pDps.ToString("#.#"), settings.DamageFontSize, textPosition, FontDrawFlags.Right)
                : new Size2();
            Size2 eDpsSize = eDps > 0
                ? Graphics.DrawText(eDps.ToString("#.#"), settings.DamageFontSize,
                    textPosition.Translate(0, pDpsSize.Height), eDpsColor, FontDrawFlags.Right)
                : new Size2();
            Vector2 dpsTextPosition = textPosition.Translate(0, pDpsSize.Height + eDpsSize.Height);
            Graphics.DrawText("dps", settings.FontSize, dpsTextPosition, settings.FontColor, FontDrawFlags.Right);
        }
    }
}