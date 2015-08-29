using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using PoeHUD.Controllers;
using PoeHUD.Poe;
using PoeHUD.Poe.UI.Elements;

namespace PoeHUD.Models
{
    public sealed class EntityListWrapper
    {
        private readonly GameController gameController;
        private readonly HashSet<string> ignoredEntities;

        private Dictionary<int, EntityWrapper> entityCache;

        public EntityListWrapper(GameController gameController)
        {
            this.gameController = gameController;
            entityCache = new Dictionary<int, EntityWrapper>();
            ignoredEntities = new HashSet<string>();
            gameController.Area.OnAreaChange += OnAreaChanged;
        }

        public ICollection<EntityWrapper> Entities
        {
            get { return entityCache.Values; }
        }

        public EntityWrapper Player { get; private set; }
        public event Action<EntityWrapper> EntityAdded;

        public event Action<EntityWrapper> EntityRemoved;


        private void OnAreaChanged(AreaController area)
        {
            ignoredEntities.Clear();
            RemoveOldEntitiesFromCache();

            //int address = gameController.Game.IngameState.Data.LocalPlayer.Address;
            //if (Player == null || Player.Address != address)
            //{
            //    Player = new EntityWrapper(gameController, address);
            //}
        }

        private void RemoveOldEntitiesFromCache()
        {
            foreach (var current in Entities)
            {
                EntityRemoved?.Invoke(current);
                current.IsInList = false;
            }
            entityCache.Clear();
        }

        public void RefreshState()
        {
            int address = gameController.Game.IngameState.Data.LocalPlayer.Address;
            if ((Player == null) || (Player.Address != address))
            {
                Player = new EntityWrapper(gameController, address);
            }
            if (gameController.Area.CurrentArea == null)
                return;
            
            Dictionary<int, Entity> newEntities = gameController.Game.IngameState.Data.EntityList.EntitiesAsDictionary;
            var newCache = new Dictionary<int, EntityWrapper>();
            foreach (var keyEntity in newEntities)
            {
                if (!keyEntity.Value.IsValid)
                    continue;

                int entityAddress = keyEntity.Key;
                string uniqueEntityName = keyEntity.Value.Path + entityAddress;

                if (ignoredEntities.Contains(uniqueEntityName))
                    continue;

                if (entityCache.ContainsKey(entityAddress) && entityCache[entityAddress].IsValid)
                {
                    newCache.Add(entityAddress, entityCache[entityAddress]);
                    entityCache[entityAddress].IsInList = true;
                    entityCache.Remove(entityAddress);
                    continue;
                }

                var entity = new EntityWrapper(gameController, keyEntity.Value);
                if ((entity.Path.StartsWith("Metadata/Effects") || ((entityAddress & 0x80000000L) != 0L)) || entity.Path.StartsWith("Metadata/Monsters/Daemon"))
                {
                    ignoredEntities.Add(uniqueEntityName);
                    continue;
                }
                EntityAdded?.Invoke(entity);
                newCache.Add(entityAddress, entity);
            }
            RemoveOldEntitiesFromCache();
            entityCache = newCache;
        }

        public EntityWrapper GetEntityById(int id)
        {
            EntityWrapper result;
            return entityCache.TryGetValue(id, out result) ? result : null;
        }


        public EntityLabel GetLabelForEntity(EntityWrapper entity)
        {
            var hashSet = new HashSet<int>();
            int entityLabelMap = gameController.Game.IngameState.EntityLabelMap;
            int num = entityLabelMap;
            while (true)
            {
                hashSet.Add(num);
                if (gameController.Memory.ReadInt(num + 8) == entity.Address)
                {
                    break;
                }
                num = gameController.Memory.ReadInt(num);
                if (hashSet.Contains(num) || num == 0 || num == -1)
                {
                    return null;
                }
            }
            return gameController.Game.ReadObject<EntityLabel>(num + 12);
        }
    }
}