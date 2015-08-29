using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PoeHUD.Poe.RemoteMemoryObjects
{
    public  class EntityList : RemoteMemoryObject
    {
        
        public List<Entity> Entities => EntitiesAsDictionary.Values.ToList();

        public Dictionary<int, Entity> EntitiesAsDictionary
        {
            get
            {
                var dictionary = new Dictionary<int, Entity>();
                CollectEntities(M.ReadInt(Address + 12), dictionary);
                return dictionary;
            }
        }

        private void CollectEntities(int addr, Dictionary<int, Entity> list)
        {
            int num = addr;
            addr = M.ReadInt(addr + 4);
            var hashSet = new HashSet<int>();
            var queue = new Queue<int>();
            queue.Enqueue(addr);
            while (queue.Count > 0)
            {
                int nextAddr = queue.Dequeue();
                if (hashSet.Contains(nextAddr))
                    continue;

                hashSet.Add(nextAddr);
                if (M.ReadByte(nextAddr + 21) == 0 && nextAddr != num && nextAddr != 0)
                {
                    int key = M.ReadInt(nextAddr + 12);
                    if (!list.ContainsKey(key))
                    {
                        int address = M.ReadInt(nextAddr + 16);
                        var entity = base.GetObject<Entity>(address);
                        list.Add(key, entity);
                    }
                    queue.Enqueue(M.ReadInt(nextAddr));
                    queue.Enqueue(M.ReadInt(nextAddr + 8));
                }
            }
        }
    }
}