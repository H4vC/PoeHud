using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoeHUD.Poe.UI;

namespace PoeHUD.Poe.Elements
{
  public  class HPbarElement:Element
    {

      public Entity MonsterEntity => base.ReadObject<Entity>(Address + 2412);

      public new List<HPbarElement> Children => GetChildren<HPbarElement>();
    }
}
