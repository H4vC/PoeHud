using System.Collections.Generic;
using PoeHUD.Poe.RemoteMemoryObjects;

namespace PoeHUD.Poe.Components
{
    public class Life : Component
    {
        public int MaxHP => Address != 0 ? M.ReadInt(Address + 0x30) : 1;
        public int CurHP => Address != 0 ? M.ReadInt(Address + 0x34) : 1;
        public int ReservedHP => Address != 0 ? M.ReadInt(Address + 0x3C) : 0;
        public int MaxMana => Address != 0 ? M.ReadInt(Address + 0x54) : 1;
        public int CurMana => Address != 0 ? M.ReadInt(Address + 0x58) : 1;
        public int ReservedMana => Address != 0 ? M.ReadInt(Address + 0x60) : 0;
        public int MaxES => Address != 0 ? M.ReadInt(Address + 0x78) : 0;
        public int CurES => Address != 0 ? M.ReadInt(Address + 0x7c) : 0;
        public float HPPercentage => CurHP/(float) (MaxHP - ReservedHP);
        public float MPPercentage => CurMana/(float) (MaxMana - ReservedMana);
        public float ESPercentage
        {
            get
            {
                if (MaxES != 0)
                {
                    return CurES/(float) MaxES;
                }
                return 0f;
            }
        }

        public bool CorpseUsable => M.ReadBytes(Address + 212, 1)[0] == 1;

        public List<Buff> Buffs
        {
            get
            {
                var list = new List<Buff>();
                int start = M.ReadInt(Address + 0x98);
                int end = M.ReadInt(Address + 0x9C);
                int count = (end - start)/4;
                if (count <= 0 || count > 32)
                {
                    return list;
                }
                for (int i = 0; i < count; i++)
                {
                    list.Add(base.ReadObject<Buff>(M.ReadInt(start + i*4) + 4));
                }
                return list;
            }
        }

        public bool HasBuff(string buff)
        {
            return Buffs.Exists((Buff x) => x.Name == buff);
        }
    }
}