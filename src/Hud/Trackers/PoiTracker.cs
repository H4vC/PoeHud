using PoeHUD.Controllers;
using PoeHUD.Hud.UI;
using PoeHUD.Models;
using PoeHUD.Poe.Components;
using System.Collections.Generic;

namespace PoeHUD.Hud.Trackers
{
    public class PoiTracker : PluginWithMapIcons<PoiTrackerSettings>
    {
        private static readonly List<string> masters = new List<string>
        {
            "Metadata/NPC/Missions/Wild/Dex",
            "Metadata/NPC/Missions/Wild/DexInt",
            "Metadata/NPC/Missions/Wild/Int",
            "Metadata/NPC/Missions/Wild/Str",
            "Metadata/NPC/Missions/Wild/StrDex",
            "Metadata/NPC/Missions/Wild/StrDexInt",
            "Metadata/NPC/Missions/Wild/StrInt"
        };

        public PoiTracker(GameController gameController, Graphics graphics, PoiTrackerSettings settings)
            : base(gameController, graphics, settings)
        { }

        public override void Render()
        {
            if (!Settings.Enable) { }
        }

        protected override void OnEntityAdded(EntityWrapper entity)
        {
            if (!Settings.Enable)
            {
                return;
            }
            MapIcon icon = GetMapIcon(entity);
            if (null != icon)
            {
                CurrentIcons[entity] = icon;
            }
        }

        private MapIcon GetMapIcon(EntityWrapper e)
        {
            if (e.HasComponent<NPC>() && masters.Contains(e.Path))
            {
                return new CreatureMapIcon(e, "ms-cyan.png", () => Settings.Masters, 8);
            }
            if (e.HasComponent<Chest>() && !e.GetComponent<Chest>().IsOpened)
            {
                return e.GetComponent<Chest>().IsStrongbox
                    ? new ChestMapIcon(e, new HudTexture("strongbox.png", e.GetComponent<ObjectMagicProperties>().Rarity),
                        () => Settings.Strongboxes, 16)
                    : new ChestMapIcon(e, new HudTexture("chest.png"), () => Settings.Chests, 3);
            }
            return null;
        }
    }
}