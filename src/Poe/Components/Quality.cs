namespace PoeHUD.Poe.Components
{
    public class Quality : Component
    {
        public int ItemQuality => Address != 0 ? M.ReadInt(Address + 12) : 0;
    }
}