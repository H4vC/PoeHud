using System;

namespace PoeHUD.Poe.Elements
{
    public class InventoryItemIcon : Element
    {
        private readonly Func<Element> inventoryItemTooltip;
        private readonly Func<Element> itemInChatTooltip;
        private readonly Func<ItemOnGroundTooltip> toolTipOnground;
        private ToolTipType? toolTip;

        public InventoryItemIcon()
        {
            toolTipOnground = () => Game.IngameState.IngameUi.ItemOnGroundTooltip;
            inventoryItemTooltip = () => ReadObject<Element>(Address + 0x80C);
            itemInChatTooltip = () => ReadObject<Element>(Address + 0x808); //bug wrong
        }

        public ToolTipType ToolTipType => (ToolTipType) (toolTip ?? (toolTip = GetToolTipType()));


        public Element Tooltip
        {
            get
            {
                switch (ToolTipType)
                {
                    case ToolTipType.ItemOnGround:
                        return toolTipOnground();
                    case ToolTipType.InventoryItem:
                        return inventoryItemTooltip();
                    case ToolTipType.ItemInChat:
                        return itemInChatTooltip();
                }
                return null;
            }
        }

        public Entity Item
        {
            get
            {
                switch (ToolTipType)
                {
                    case ToolTipType.ItemOnGround:
                        return toolTipOnground().Item;
                    case ToolTipType.InventoryItem:
                        return ReadObject<Entity>(Address + 0xA98);
                }
                return null;
            }
        }

        private ToolTipType GetToolTipType()
        {
            Element tlTipOnground = toolTipOnground().ToolTip;
            if (tlTipOnground != null && tlTipOnground.IsVisible)
                return ToolTipType.ItemOnGround;
            
            //if (itemInChatTooltip() != null && itemInChatTooltip().IsVisible)
            //    return ToolTipType.ItemInChat;

            if (inventoryItemTooltip() != null)
                return ToolTipType.InventoryItem;
            return ToolTipType.None;
        }
    }

    public enum ToolTipType
    {
        None,
        InventoryItem,
        ItemOnGround,
        ItemInChat
    }
}