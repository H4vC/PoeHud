namespace PoeHUD.Poe.Elements
{
    public class Inventory : Element
    {
        public RemoteMemoryObjects.Inventory InventoryModel => base.ReadObject<RemoteMemoryObjects.Inventory>(Address + 2436);
    }
}