using SharpDX;

namespace PoeHUD.Poe.Components
{
    public class Render : Component
    {
        public float X => Address != 0 ? M.ReadFloat(Address + 0x70) : 0f;
        public float Y => Address != 0 ? M.ReadFloat(Address + 0x74) : 0f;
        public float Z => Address != 0 ? M.ReadFloat(Address + 0x8C) : 0f;
        public Vector3 Pos => new Vector3(X, Y, Z);
        public string DisplayName
        {
            get
            {
                if (Address == 0)
                {
                    return "";
                }
                int num = M.ReadInt(Address + 88);
                return num < 8 ? M.ReadStringU(Address + 72, 16) : M.ReadStringU(M.ReadInt(Address + 72));
            }
        }

        public int UknownValue => M.ReadInt(Address + 4, 4, 8);
    }
}
