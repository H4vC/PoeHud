using System.Collections.Generic;

namespace PoeHUD.Poe.Elements
{
  public  class HPbarElement:Element
    {

      public Entity MonsterEntity => base.ReadObject<Entity>(Address + 2412);
      public new List<HPbarElement> Children => GetChildren<HPbarElement>();
    }
}
