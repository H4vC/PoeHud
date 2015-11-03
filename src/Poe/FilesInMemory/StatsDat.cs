using System;
using System.Collections.Generic;
using PoeHUD.Framework;

namespace PoeHUD.Poe.FilesInMemory
{
    public class StatsDat : FileInMemory
    {
        public enum StatType
        {
            Percents = 1,
            Value2 = 2,
            IntValue = 3,
            Boolean = 4,
            Precents5 = 5
        }

        public Dictionary<string, StatRecord> records =
            new Dictionary<string, StatRecord>(StringComparer.OrdinalIgnoreCase);


        public StatsDat(Memory m, int address) : base(m, address)
        {
            loadItems();
        }

        private void loadItems()
        {
            foreach (int addr in RecordAddresses())
            {
                var r = new StatRecord(M, addr);
                records.Add(r.Key, r);
            }
        }

        public class StatRecord
        {
            public readonly string Key;
            public StatType Type;
            public bool Unknown4;
            public bool Unknown5;
            public bool Unknown6;
            public bool UnknownB;
            public string UserFriendlyName;
            // more fields can be added (see in visualGGPK)


            public StatRecord(Memory m, int addr)
            {
                Key = m.ReadStringU(m.ReadInt(addr + 0), 255);
                Unknown4 = m.ReadByte(addr + 4) != 0;
                Unknown5 = m.ReadByte(addr + 5) != 0;
                Unknown6 = m.ReadByte(addr + 6) != 0;
                Type = Key.Contains("%") ? StatType.Percents : (StatType)m.ReadInt(addr + 7);
                UnknownB = m.ReadByte(addr + 0xB) != 0;
                UserFriendlyName = m.ReadStringU(m.ReadInt(addr + 0xC), 255);
            }

            public override string ToString()
            {
                return String.IsNullOrWhiteSpace(UserFriendlyName) ? Key : UserFriendlyName;
            }

            internal string ValueToString(int val)
            {
                switch (Type)
                {
                    case StatType.Boolean:
                        return val != 0 ? "True" : "False";
                    case StatType.IntValue:
                    case StatType.Value2:
                        return val.ToString("+#;-#");
                    case StatType.Percents:
                    case StatType.Precents5:
                        return val.ToString("+#;-#") + "%";
                }

                //throw new NotImplementedException();
                return "";
            }
        }
    }
}