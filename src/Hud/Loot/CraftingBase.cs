using System;
using PoeHUD.Models.Enums;

namespace PoeHUD.Hud.Loot
{
    public struct CraftingBase
    {
        public string Name { get; set; }
        public int MinItemLevel { get; set; }
        public int MinQuality { get; set; }
        public ItemRarity[] Rarities { get; set; }

        public override int GetHashCode()
        {
            return Name.ToLowerInvariant().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var strct = (CraftingBase)obj;
            return Name.Equals(strct.Name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}