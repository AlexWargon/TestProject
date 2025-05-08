using UnityEngine;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public class CollisionEmitter : MonoBehaviour
    {
        [SerializeField] private int world;
        private EntityViewMap entityViewMap;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out CollisionEmitter emitter))
            {
                ref var otherEntity = ref entityViewMap.GetEntity(emitter.gameObject.GetInstanceID());
                ref var entity = ref entityViewMap.GetEntity(gameObject.GetInstanceID());
                Worlds.GetWorld(world).CreateEntity().Add(new CollisionEvent
                {
                    Other = otherEntity,
                    Entity = entity
                });
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CollisionEmitter emitter))
            {
                ref var otherEntity = ref entityViewMap.GetEntity(emitter.gameObject.GetInstanceID());
                ref var entity = ref entityViewMap.GetEntity(gameObject.GetInstanceID());
                Worlds.GetWorld(world).CreateEntity().Add(new CollisionEvent
                {
                    Other = otherEntity,
                    Entity = entity
                });
            }
        }
    }

    public struct CollisionEvent
    {
        public Entity Other;
        public Entity Entity;
    }
}