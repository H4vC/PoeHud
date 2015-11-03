namespace PoeHUD.Poe.Servers
{
    public interface IServer
    {
        int IsInGameOffset { get; }
        int IngameUIElementsOffset { get; }
    }
}