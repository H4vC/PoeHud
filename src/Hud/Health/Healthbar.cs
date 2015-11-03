using System.Collections.Generic;
using System.Diagnostics;
using PoeHUD.Models;
using PoeHUD.Models.Enums;
using PoeHUD.Poe.Components;

namespace PoeHUD.Hud.Health
{
    public class HealthBar
    {
        private readonly bool isHostile;
        private int lastHp;
        private readonly Stopwatch dpsStopwatch = Stopwatch.StartNew();
        private const int DPS_CHECK_TIME = 1000;
        private const int DPS_FAST_CHECK_TIME = 200;
        private const int DPS_POP_TIME = 2000;

        public HealthBar(EntityWrapper entity, HealthBarSettings settings)
        {
            Entity = entity;
            if (entity.HasComponent<Player>())
            {
                Type = CreatureType.Player;
                Settings = settings.Players;
                IsValid = true;
            }
            else if (entity.HasComponent<Monster>())
            {
                IsValid = true;
                if (entity.IsHostile)
                {
                    isHostile = true;
                    switch (entity.GetComponent<ObjectMagicProperties>().Rarity)
                    {
                        case MonsterRarity.White:
                            Type = CreatureType.Normal;
                            Settings = settings.NormalEnemy;
                            break;
                        case MonsterRarity.Magic:
                            Type = CreatureType.Magic;
                            Settings = settings.MagicEnemy;
                            break;
                        case MonsterRarity.Rare:
                            Settings = settings.RareEnemy;
                            Type = CreatureType.Rare;
                            break;
                        case MonsterRarity.Unique:
                            Settings = settings.UniqueEnemy;
                            Type = CreatureType.Unique;
                            break;
                    }
                }
                else
                {
                    Type = CreatureType.Minion;
                    Settings = settings.Minions;
                }
            }
            Life = Entity.GetComponent<Life>();
            lastHp = GetFullHp();
        }

        public Life Life { get; }
        public EntityWrapper Entity { get; }
        public bool IsValid { get; private set; }
        public UnitSettings Settings { get; }
        public CreatureType Type { get; private set; }

        public bool IsShow(bool showEnemy)
        {
            return !isHostile ? Settings.Enable.Value : Settings.Enable && showEnemy && isHostile;
        }

        public LinkedList<int> DpsQueue { get; } = new LinkedList<int>();

        public void DpsRefresh()
        {
            var chechTime = (DpsQueue.Count > 0 ? DPS_CHECK_TIME : DPS_FAST_CHECK_TIME);
            if (dpsStopwatch.ElapsedMilliseconds < chechTime) return;
            var hp = GetFullHp();
            if (hp <= -1000000 || hp >= 10000000 || lastHp == hp) return;
            DpsQueue.AddFirst(-(lastHp - hp));
            if (DpsQueue.Count > Settings.FloatingCombatStackSize)
            {
                DpsQueue.RemoveLast();
                dpsStopwatch.Restart();
            }
            lastHp = hp;
        }

        public void DpsDequeue()
        {
            if (dpsStopwatch.ElapsedMilliseconds < DPS_POP_TIME) return;
            if (DpsQueue.Count > 0)
            {
                DpsQueue.RemoveLast();
            }
            dpsStopwatch.Restart();
        }

        private int GetFullHp()
        {
            return Life.CurHP + Life.CurES;
        }
    }
}