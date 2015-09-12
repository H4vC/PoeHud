using System;
using System.Collections.Generic;
using System.Linq;

using PoeHUD.Models.Enums;
using PoeHUD.Models.Interfaces;
using PoeHUD.Poe.Components;

using SharpDX;

namespace PoeHUD.Hud.Loot
{
    public class ItemUsefulProperties
    {
        private readonly string _name;

        private readonly IEntity _item;

        private readonly CraftingBase _craftingBase;

        private ItemRarity rarity;

        private int quality, borderWidth, alertIcon = -1;

        private string alertText;

        private Color color; // Fully qualify to prevent confusion on Component

        public ItemUsefulProperties(string name, IEntity item, CraftingBase craftingBase)
        {
            _name = name;
            _item = item;
            _craftingBase = craftingBase;
        }

        public AlertDrawStyle GetDrawStyle()
        {
            return new AlertDrawStyle((new Color().Equals(color) ? (object)rarity : color), borderWidth, alertText, alertIcon);
        }

        public bool ShouldAlert(HashSet<string> currencyNames, ItemAlertSettings settings)
        {
            Mods mods = _item.GetComponent<Mods>();
            QualityItemsSettings qualitySettings = settings.QualityItems;

            rarity = mods.ItemRarity; // set rarity

            if (_item.HasComponent<Quality>())
            {
                quality = _item.GetComponent<Quality>().ItemQuality; // update quality variable
            }

            alertText = string.Concat(quality > 0 ? "Superior " : String.Empty, _name);

            // Check if Map/Vaal Frag
            if (settings.Maps && (_item.HasComponent<Map>() || _item.Path.Contains("VaalFragment")))
            {
                borderWidth = 1;
                return true;
            }

            // Check if Currency
            if (settings.Currency && _item.Path.Contains("Currency"))
            {
                color = HudSkin.CurrencyColor;
                return currencyNames?.Contains(_name) ?? (!_name.Contains("Wisdom") && !_name.Contains("Portal"));
            }

            // Check if DivinationCard
            if (settings.DivinationCards && _item.Path.Contains("DivinationCards"))
            {
                color = HudSkin.DivinationCardColor;
                return true;
            }

            Sockets sockets = _item.GetComponent<Sockets>();
            // Check link REQ.
            if (sockets.LargestLinkSize >= settings.MinLinks)
            {
                if (sockets.LargestLinkSize == 6) // If 6 link change icon
                {
                    alertIcon = 3;
                }
                return true;
            }

            // Check if Crafting Base
            if (IsCraftingBase(mods.ItemLevel))
            {
                alertIcon = 2;
                return true;
            }

            // Check # socket REQ.
            if (sockets.NumberOfSockets >= settings.MinSockets)
            {
                alertIcon = 0;
                return true;
            }

            // RGB
            if (settings.Rgb && sockets.IsRGB)
            {
                alertIcon = 1;
                return true;
            }

            // Check if Jewel
            if (settings.Jewels && _item.Path.Contains("Jewels"))
            {
                return true;
            }

            // meets rarity conidtions
            switch (rarity)
            {
                case ItemRarity.Rare:
                    return settings.Rares;
                case ItemRarity.Unique:
                    return settings.Uniques;
            }

            // Other (no icon change)
            if (qualitySettings.Enable)
            {
                if (qualitySettings.Flask.Enable && _item.HasComponent<Flask>())
                {
                    return (quality >= qualitySettings.Flask.MinQuality);
                }
                else if (qualitySettings.SkillGem.Enable && _item.HasComponent<SkillGem>())
                {
                    color = HudSkin.SkillGemColor;
                    return (quality >= qualitySettings.SkillGem.MinQuality);
                }
                else if (qualitySettings.Weapon.Enable && _item.HasComponent<Weapon>())
                {
                    return (quality >= qualitySettings.Weapon.MinQuality);
                }
                else if (qualitySettings.Armour.Enable && _item.HasComponent<Armour>())
                {
                    return (quality >= qualitySettings.Armour.MinQuality);
                }
            }

            return false; // Meets no checks
        }

        private bool IsCraftingBase(int itemLevel)
        {
            return (!String.IsNullOrEmpty(_craftingBase.Name) && itemLevel >= _craftingBase.MinItemLevel && quality >= _craftingBase.MinQuality && (_craftingBase.Rarities == null || _craftingBase.Rarities.Contains(rarity)));
        }
    }
}