namespace PoeHUD.Poe
{
    public abstract class Component : RemoteMemoryObject
    {
        protected Entity Owner => base.ReadObject<Entity>(Address + 4);
    }
}