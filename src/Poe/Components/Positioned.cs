using SharpDX;

namespace PoeHUD.Poe.Components
{
    public class Positioned : Component
    {
        public float X => Address != 0 ? M.ReadFloat(Address + 0x1C) : 0f;
        public float Y => Address != 0 ? M.ReadFloat(Address + 0x20) : 0f;
        public int GridX => Address != 0 ? M.ReadInt(Address + 0x10) : 0;
        public int GridY => Address != 0 ? M.ReadInt(Address + 0x14) : 0;
        public Vector2 GridPos => new Vector2(GridX, GridY);
    }
}