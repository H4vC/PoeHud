using System.Collections.Generic;
using PoeHUD.Framework;

namespace PoeHUD.Poe
{
    public abstract class FileInMemory
    {
        public FileInMemory(Memory m, int address)
        {
            M = m;
            Address = address;
        }

        public Memory M { get; private set; }
        public int Address { get; private set; }

        private int NumberOfRecords => M.ReadInt(Address + 0x44);

        protected IEnumerable<int> RecordAddresses()
        {
            int firstRec = M.ReadInt(Address + 0x30);
            int lastRec = M.ReadInt(Address + 0x34);
            int cnt = NumberOfRecords;

            int recLen = (lastRec - firstRec)/cnt;
            for (int i = 0; i < cnt; i++)
                yield return firstRec + i*recLen;
        }
    }
}