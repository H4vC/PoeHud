namespace PoeHUD.Poe.Components
{
    public class Chest : Component
    {
        public bool IsOpened => Address != 0 && M.ReadByte(Address + 36) == 1;

        public bool IsStrongbox => Address != 0 && M.ReadInt(Address + 60) != 0;
    }
}