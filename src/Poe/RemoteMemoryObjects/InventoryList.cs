using System.Collections.Generic;
using PoeHUD.Models.Enums;

namespace PoeHUD.Poe.RemoteMemoryObjects
{
    public class InventoryList : RemoteMemoryObject
    {
        private int ListStart => M.ReadInt(Address + 112);

        private int ListEnd => M.ReadInt(Address + 116);

        public int InventoryCount => (ListEnd - ListStart)/16;

        public List<Inventory> Inventories
        {
            get
            {
                var list = new List<Inventory>();
                int inventoryCount = InventoryCount;
                if (inventoryCount > 50 || inventoryCount <= 0)
                {
                    return list;
                }
                for (int i = 0; i < inventoryCount; i++)
                {
                    list.Add(base.ReadObject<Inventory>(ListStart + 8 + i*16));
                }
                return list;
            }
        }

        public Inventory this[InventoryIndex inv]
        {
            get
            {
                var num = (int) inv;
                return base.ReadObject<Inventory>((ListStart + 8) + (num*0x10));
            }
        }
    }
}