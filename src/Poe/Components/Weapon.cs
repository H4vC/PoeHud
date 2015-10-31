namespace PoeHUD.Poe.Components
{
    public class Weapon : Component
    {
        public int DamageMin => Address != 0 ? M.ReadInt(Address + 16, 4) : 0;
        public int DamageMax => Address != 0 ? M.ReadInt(Address + 16, 8) : 0;
        public int AttackTime => Address != 0 ? M.ReadInt(Address + 16, 12) : 1;
        public int CritChance => Address != 0 ? M.ReadInt(Address + 16, 16) : 0;
    }
}