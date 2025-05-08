using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class DamageOnCollisionSystem : UpdateSystem
    {
        public override void Update()
        {
            entities.Each((CollisionEvent collisionEvent) =>
            {
                if (!(collisionEvent.Other.Has<EnemyTag>() && collisionEvent.Entity.Has<EnemyTag>()))
                    if (collisionEvent.Other.Has<Damage>())
                        collisionEvent.Entity.Add(new DamageEvent
                            { Value = collisionEvent.Other.Get<Damage>().Value, From = collisionEvent.Other });
            });
        }
    }
}