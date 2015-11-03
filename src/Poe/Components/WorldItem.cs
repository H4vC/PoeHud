namespace PoeHUD.Poe.Components
{
    public class WorldItem : Component
    {
        public Entity ItemEntity => Address != 0 ? base.ReadObject<Entity>(Address + 20) : base.GetObject<Entity>(0);
    }
}