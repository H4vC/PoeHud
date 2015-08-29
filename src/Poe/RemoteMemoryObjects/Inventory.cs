using System.Collections.Generic;

namespace PoeHUD.Poe.RemoteMemoryObjects
{
    public class Inventory : RemoteMemoryObject
    {
        public int Width => M.ReadInt(Address + 28);

        public int Height => M.ReadInt(Address + 32);

        private int ListStart => M.ReadInt(Address + 48);

        private int ListEnd => M.ReadInt(Address + 52);

        public List<Entity> Items
        {
            get
            {
                var list = new List<Entity>();
                int num = (ListEnd - ListStart)/4;
                if (num > 1000 || num <= 0)
                {
                    return list;
                }
                var hashSet = new HashSet<int>();
                for (int i = 0; i < num; i++)
                {
                    int num2 = M.ReadInt(ListStart + i*4);
                    if (num2 != 0 && !hashSet.Contains(num2))
                    {
                        list.Add(base.ReadObject<Entity>(num2));
                        hashSet.Add(num2);
                    }
                }
                return list;
            }
        }
    }
}