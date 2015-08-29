namespace PoeHUD.Poe.UI.Elements

{
    public class WindowState : Element
    {
        public bool IsVisibleLocal => M.ReadInt(this.Address + 0x860) == 1;
    }
}
