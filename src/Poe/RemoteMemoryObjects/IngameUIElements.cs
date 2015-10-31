using System.Collections.Generic;
using PoeHUD.Poe.Elements;

namespace PoeHUD.Poe.RemoteMemoryObjects
{
    public class IngameUIElements : RemoteMemoryObject
    {
        public Element Uknown0 => ReadObjectAt<Element>(0x40);
        public Element HpGlobe => ReadObjectAt<Element>(0x44);
        public Element ManaGlobe => ReadObjectAt<Element>(0x48);
        public Element Flasks => ReadObjectAt<Element>(0x50);
        public Element XpBar => ReadObjectAt<Element>(0x54);
        public Element MenuButton => ReadObjectAt<Element>(0x58);
        public Element ShopButton => ReadObjectAt<Element>(0x84);
        public Element HideoutEditButton => ReadObjectAt<Element>(0x88);
        public Element HideoutStashButton => ReadObjectAt<Element>(0x8C);
        public Element Mouseposition => ReadObjectAt<Element>(0xA4);
        public Element ActionButtons => ReadObjectAt<Element>(0xA8);
        public Element Chat => ReadObjectAt<Element>(0xe8);
        public Element QuestTracker => ReadObjectAt<Element>(0xf0);
        public Element MtxInventory => ReadObjectAt<Element>(0xf4);
        public Element MtxShop => ReadObjectAt<Element>(0xf8);
        public Element InventoryPanel => ReadObjectAt<Element>(0x108);// + M.Server.IngameUIElementsOffset);
        public Element StashPanel => ReadObjectAt<Element>(0x108);
        public Element SocialPanel => ReadObjectAt<Element>(0x114);
        public Element TreePanel => ReadObjectAt<Element>(0x11c);// + M.Server.IngameUIElementsOffset);
        public Element CharacterPanel => ReadObjectAt<Element>(0x11c);
        public Element OptionsPanel => ReadObjectAt<Element>(0x120);
        public Element AchievementsPanel => ReadObjectAt<Element>(0x124);
        public Element WorldPanel => ReadObjectAt<Element>(0x12c);
        public Map Map => ReadObjectAt<Map>(0x134);// + M.Server.IngameUIElementsOffset);

        public IEnumerable<ItemsOnGroundLabelElement> ItemsOnGroundLabels
        {
            get
            {
                var itemsOnGroundLabelRoot = ReadObjectAt<ItemsOnGroundLabelElement>(0x138);// + M.Server.IngameUIElementsOffset);
                return itemsOnGroundLabelRoot.Children;
            }
        }

        public List<HPbarElement> MonsterHpLabels
        {
            get
            {
                var monsterHpLabelsRoot = ReadObjectAt<HPbarElement>(0x12c);
                return monsterHpLabelsRoot.Children;
            }
        }

        public Element Buffs => ReadObjectAt<Element>(0x138);
        public Element Buffs2 => ReadObjectAt<Element>(0x194);
        public Element OpenLeftPanel => ReadObjectAt<Element>(0x16c);// + M.Server.IngameUIElementsOffset);
        public Element OpenRightPanel => ReadObjectAt<Element>(0x170);// + M.Server.IngameUIElementsOffset);
        public Element OpenNpcDialogPanel => ReadObjectAt<Element>(0x168);
        public Element CreatureInfoPanel => ReadObjectAt<Element>(0x18c);
        public Element InstanceManagerPanel => ReadObjectAt<Element>(0x1a0);
        public Element InstanceManagerPanel2 => ReadObjectAt<Element>(0x1a4);
        public Element SwitchingZoneInfo => ReadObjectAt<Element>(0x1CC);
        public Element GemLvlUpPanel => ReadObjectAt<Element>(0x21c);// + M.Server.IngameUIElementsOffset);
        public ItemOnGroundTooltip ItemOnGroundTooltip => ReadObjectAt<ItemOnGroundTooltip>(0x22c);// + M.Server.IngameUIElementsOffset);
    }
}