using System.Collections.Generic;
using UnityEngine;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public class EntityViewMap
    {
        private readonly Dictionary<int, int> viewInstanceToEntity = new();
        private readonly World world;

        public EntityViewMap(World world)
        {
            this.world = world;
        }

        public ref Entity GetEntity(int instanceID)
        {
            if (viewInstanceToEntity.TryGetValue(instanceID, out var value)) return ref world.GetEntity(value);
            Debug.LogError($"Entity with view '{instanceID}' not found");
            return ref Entity.Null;
        }

        public bool TryGetEntity(int instanceID, out Entity entity)
        {
            if (viewInstanceToEntity.TryGetValue(instanceID, out var value))
            {
                entity = world.GetEntity(value);
                return true;
            }
            entity = Entity.Null;
            return false;
        }
        public void AddEntity(int instanceID, Entity entity)
        {
            viewInstanceToEntity[instanceID] = entity.id;
        }
    }
}