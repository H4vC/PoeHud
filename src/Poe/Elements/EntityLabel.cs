namespace PoeHUD.Poe.Elements
{
    public class EntityLabel : Element
    {
        public string Text
        {
            get {

                int num = M.ReadInt(Address + 2468);
                if (num <= 0 || num > 256)
                {
                    return "";
                }
                return num >= 8 ? M.ReadStringU(M.ReadInt(Address + 2452), num * 2) : M.ReadStringU(Address + 2452, num * 2);
            }
        }
    }
}