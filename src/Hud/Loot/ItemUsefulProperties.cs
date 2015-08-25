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

        private int quality, frameWidth, alertIcon = -1;

        private string alertText;

        private Color fontColor, frameColor;

        public ItemUsefulProperties(string name, IEntity item, CraftingBase craftingBase)
        {
            _name = name;
            _item = item;
            _craftingBase = craftingBase;
        }

        public AlertDrawStyle GetDrawStyle()
        {
            return new AlertDrawStyle((new Color().Equals(fontColor) ? (object)rarity : fontColor), frameWidth, alertText, alertIcon, frameColor);
        }

        public bool ShouldAlert(HashSet<string> currencyNames, ItemAlertSettings settings)
        {
            Mods mods = _item.GetComponent<Mods>();
            QualityItemsSettings qualitySettings = settings.QualityItems;

            rarity = mods.ItemRarity;

            if (_item.HasComponent<Quality>())
            {
                quality = _item.GetComponent<Quality>().ItemQuality;
            }

            alertText = string.Concat(quality > 0 ? "Superior " : string.Empty, _name);

            if (settings.Maps && (_item.HasComponent<Map>() || _item.Path.Contains("VaalFragment")))
            {
                frameWidth = 1;
                frameColor = settings.FrameMapsColor;
                fontColor = settings.MapsColor;
                return true;
            }

            if (settings.Currency && _item.Path.Contains("Currency"))
            {
                if (_name.Contains("Divine Orb") ||
                    _name.Contains("Exalted Orb") ||
                    _name.Contains("Mirror of Kalandra") ||
                    _name.Contains("Fishing Rod"))
                {
                    frameWidth = 1;
                    frameColor = settings.FrameExaltedColor;
                    fontColor = settings.ExaltedColor;
                    return true;
                }
                if (_name.Contains("Chaos Orb") ||
                    _name.Contains("Blessed Orb") ||
                    _name.Contains("Regal Orb") ||
                    _name.Contains("Vaal Orb") ||
                    _name.Contains("Gemcutter's Prism") ||
                    _name.Contains("Orb of Regret") ||
                    _name.Contains("Orb of Alchemy") ||
                    _name.Contains("Orb of Scouring") ||
                    _name.Contains("Orb of Fusing") ||
                    _name.Contains("Albino Rhoa Feather"))
                {
                    frameWidth = 1;
                    frameColor = settings.FrameChaosColor;
                    fontColor = settings.ChaosColor;
                    return true;
                }
                if (_name.Contains("Jeweller's Orb") ||
                    _name.Contains("Cartographer's Chisel") ||
                    _name.Contains("Chromatic Orb") ||
                    _name.Contains("Orb of Chance") ||
                    _name.Contains("Orb of Alteration") ||
                    _name.Contains("Orb of Augmentation") ||
                    _name.Contains("Orb of Transmutation"))
                {
                    frameWidth = 1;
                    frameColor = settings.FrameCurrencyColor;
                    fontColor = settings.CurrencyColor;
                    return true;
                }
            }

            if (settings.Jewels && _item.Path.Contains("Jewels"))
            {
                switch (rarity)
                {
                    case ItemRarity.Rare:
                        frameWidth = 1;
                        frameColor = settings.FrameJewelsColor;
                        fontColor = HudSkin.RareColor;
                        return true;
                    case ItemRarity.Unique:
                        frameWidth = 1;
                        frameColor = HudSkin.UniqueColor;
                        fontColor = HudSkin.UniqueColor;
                        return true;
                }
                frameWidth = 1;
                frameColor = settings.FrameJewelsColor;
                fontColor = settings.JewelsColor;
                return true;
            }

            if (settings.Cards && _item.Path.Contains("DivinationCards"))
            {
                frameWidth = 1;
                frameColor = settings.FrameCardsColor;
                fontColor = settings.CardsColor;
                return true;
            }

            Sockets sockets = _item.GetComponent<Sockets>();

            if (sockets.LargestLinkSize >= settings.MinLinks)
            {
                alertIcon = 3;
                frameWidth = 1;
                frameColor = settings.FrameLinkedColor;
                fontColor = settings.LinkedColor;
                return true;
            }

            if (IsCraftingBase(mods.ItemLevel))
            {
                alertIcon = 2;
                frameWidth = 1;
                frameColor = settings.FrameCraftingColor;
                fontColor = settings.CraftingColor;
                return true;
            }

            if (sockets.NumberOfSockets >= settings.MinSockets)
            {
                alertIcon = 0;
                frameWidth = 1;
                frameColor = settings.FrameSocketsColor;
                fontColor = settings.SocketsColor;
                return true;
            }

            if (settings.Rgb && sockets.IsRGB)
            {
                alertIcon = 1;
                frameWidth = 1;
                frameColor = settings.FrameRGBColor;
                fontColor = settings.RGBColor;
                return true;
            }

            switch (rarity)
            {
                case ItemRarity.Rare:
                    return settings.Rares;

                case ItemRarity.Unique:
                    frameWidth = 1;
                    frameColor = HudSkin.UniqueColor;
                    fontColor = HudSkin.UniqueColor;
                    return true;
            }

            if (!qualitySettings.Enable) return false;

            if (qualitySettings.Flask.Enable && _item.HasComponent<Flask>())
            {
                return (quality >= qualitySettings.Flask.MinQuality);
            }
            if (qualitySettings.SkillGem.Enable && _item.HasComponent<SkillGem>())
            {
                if (quality > 0)
                {
                    frameWidth = 1;
                    frameColor = HudSkin.SkillGemColor;
                }
                fontColor = HudSkin.SkillGemColor;
                return (quality >= qualitySettings.SkillGem.MinQuality);
            }
            if (qualitySettings.Weapon.Enable && _item.HasComponent<Weapon>())
            {
                return (quality >= qualitySettings.Weapon.MinQuality);
            }
            if (qualitySettings.Armour.Enable && _item.HasComponent<Armour>())
            {
                return (quality >= qualitySettings.Armour.MinQuality);
            }

            return false;
        }

        private bool IsCraftingBase(int itemLevel)
        {
            return (!string.IsNullOrEmpty(_craftingBase.Name) && itemLevel >= _craftingBase.MinItemLevel && quality >= _craftingBase.MinQuality && (_craftingBase.Rarities == null || _craftingBase.Rarities.Contains(rarity)));
        }
    }
}