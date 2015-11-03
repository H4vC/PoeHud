namespace PoeHUD.Poe.RemoteMemoryObjects
{
    public class Buff : RemoteMemoryObject
    {
        public string Name => M.ReadStringU(M.ReadInt(Address + 4, 0));

        public int Charges => M.ReadInt(Address + 24);

        public int SkillId => M.ReadInt(Address + 36);

        public float Timer => M.ReadFloat(Address + 12);
    }
}