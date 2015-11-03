namespace PoeHUD.Poe.Servers
{
    public abstract class GarenaServer : IServer
    {
        public int IsInGameOffset => 0x303C;
        public int IngameUIElementsOffset => 0x4;
    }
}