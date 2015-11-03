namespace PoeHUD.Poe.RemoteMemoryObjects
{
    public class ServerData : RemoteMemoryObject
    {
        //public bool IsInGame => M.ReadInt(Address + M.Server.IsInGameOffset) == 3;
        public bool IsInGame => M.ReadInt(Address + Offsets.InGameOffset) == 3;

        public InventoryList PlayerInventories => base.GetObject<InventoryList>(Address + 10496);
    }
}
