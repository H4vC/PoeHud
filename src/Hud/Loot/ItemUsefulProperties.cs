using System;
using System.Collections.Generic;
using System.Linq;

using PoeHUD.Models.Enums;
using PoeHUD.Models.Interfaces;
using PoeHUD.Poe.Components;
using PoeHUD.Poe;

namespace PoeHUD.Hud.Loot
{
    public class ItemUsefulProperties
    {
        private readonly string _name;

        private readonly IEntity _item;

        private readonly CraftingBase _craftingBase;

        private ItemRarity rarity;

        private int quality = 0, alertWidth = 0, alertIcon = -1;

        private string alertText;

        private SharpDX.Color color = SharpDX.Color.Black; // Fully qualify to prevent confusion on Component

        public ItemUsefulProperties(string name, IEntity item, CraftingBase craftingBase)
        {
            _name = name;
            alertText = _name; // initialize alertText to be just it's name for now
            _item = item;
            _craftingBase = craftingBase;
        }

        public AlertDrawStyle GetDrawStyle()
        {
            return new AlertDrawStyle(color, rarity, alertWidth, alertText, alertIcon);
        }

        public bool ShouldAlert(HashSet<string> currencyNames, ItemAlertSettings settings)
        {
            Mods mods = _item.GetComponent<Mods>();
            Sockets sockets = _item.GetComponent<Sockets>();
            QualityItemsSettings qualitySettings = settings.QualityItems;

            rarity = mods.ItemRarity; // set rarity

            // If quality > 0 concat 'Superior ' to item name
            if (_item.HasComponent<Quality>()) 
            {
                quality = _item.GetComponent<Quality>().ItemQuality; // update quality variable
                alertText = string.Concat("Superior ", _name);
            }

            // Check if Map/Vaal Frag
            if (_item.HasComponent<Map>() || _item.Path.Contains("VaalFragment"))
            {
                alertWidth = 1;
                return settings.Maps;
            }

            // Check if Currency
            if (_item.Path.Contains("Currency"))
            {
                color = HudSkin.CurrencyColor;
                return (settings.Currency && ((currencyNames != null && currencyNames.Contains(_name)) || !_name.Contains("Scroll")));
            }

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
            if (sockets.IsRGB)
            {
                alertIcon = 1;
                return true;
            }

            // meets rarity conidtions
            switch (rarity)
            {
                case ItemRarity.Rare:
                    return settings.Rares;
                case ItemRarity.Unique:
                    return settings.Uniques;
                default:
                    break;
            }

            // Other (no icon change)
            if (qualitySettings.Enable)
            {
                if (_item.HasComponent<Flask>())
                {
                    return (qualitySettings.Flask.Enable && quality >= qualitySettings.Flask.MinQuality);
                }
                else if (_item.HasComponent<SkillGem>())
                {
                    color = HudSkin.SkillGemColor;
                    return (qualitySettings.SkillGem.Enable && quality >= qualitySettings.SkillGem.MinQuality);
                }
                else if (_item.HasComponent<Weapon>())
                {
                    return (qualitySettings.Weapon.Enable && quality >= qualitySettings.Weapon.MinQuality);
                }
                else if (_item.HasComponent<Armour>())
                {
                    return (qualitySettings.Armour.Enable && quality >= qualitySettings.Armour.MinQuality);
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