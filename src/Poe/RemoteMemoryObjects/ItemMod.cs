namespace PoeHUD.Poe.RemoteMemoryObjects
{
    public class ItemMod : RemoteMemoryObject
    {
        private int level;
        private string name;
        private string rawName;

        public int Value1 => M.ReadInt(Address, 0);

        public int Value2 => M.ReadInt(Address, 4);

        public int Value3 => M.ReadInt(Address, 8);

        public int Value4 => M.ReadInt(Address, 12);

        public string RawName
        {
            get
            {
                if (rawName == null)
                    ParseName();
                return rawName;
            }
        }

        public string Name
        {
            get
            {
                if (rawName == null)
                    ParseName();
                return name;
            }
        }

        public int Level
        {
            get
            {
                if (rawName == null)
                    ParseName();
                return level;
            }
        }

        private void ParseName()
        {
            rawName = M.ReadStringU(M.ReadInt(Address + 20, 0));
            name = rawName.Replace("_", ""); // Master Crafted mod can have underscore on the end, need to ignore
            int ixDigits = name.IndexOfAny("0123456789".ToCharArray());
            if (ixDigits < 0 || !int.TryParse(name.Substring(ixDigits), out level))
            {
                level = 1;
            }
            else
            {
                name = name.Substring(0, ixDigits);
            }
        }
    }
}