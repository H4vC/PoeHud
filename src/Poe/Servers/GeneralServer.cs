namespace PoeHUD.Poe.Servers
{
    public class GeneralServer : IServer
    {
        public int IsInGameOffset => 0x303C;
        public int IngameUIElementsOffset => 0;
    }
}