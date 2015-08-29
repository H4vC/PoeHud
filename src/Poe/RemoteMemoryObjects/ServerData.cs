namespace PoeHUD.Poe.RemoteMemoryObjects
{
    public class ServerData : RemoteMemoryObject
    {
        public bool IsInGame => M.ReadInt(Address + 0x2FBC) == 3;

        public InventoryList PlayerInventories => base.GetObject<InventoryList>(Address + 10496);
    }
}