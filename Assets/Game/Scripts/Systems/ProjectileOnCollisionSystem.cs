using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class ProjectileOnCollisionSystem : UpdateSystem
    {
        public override void Update()
        {
            entities.Each((Entity e, CollisionEvent collisionEvent) =>
            {
                if (collisionEvent.Entity.Has<ProjectileTag>()) collisionEvent.Entity.Add<ClearViewEvent>();
            });
        }
    }
}