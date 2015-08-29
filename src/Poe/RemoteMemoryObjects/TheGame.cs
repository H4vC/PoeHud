using PoeHUD.Framework;

namespace PoeHUD.Poe.RemoteMemoryObjects
{
    public sealed class TheGame : RemoteMemoryObject
    {
        public TheGame(Memory m)
        {
            M = m;
            Address = m.ReadInt(m.AddressOfProcess + Offsets.Base, new[] {4, 124});
            Game = this;
        }

        public IngameState IngameState => base.ReadObject<IngameState>(Address + 0x9C);

        public int AreaChangeCount => M.ReadInt(M.AddressOfProcess + Offsets.AreaChangeCount);
    }
}