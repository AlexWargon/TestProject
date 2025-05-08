using Wargon.DI;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class RegisterCollisionEmittersSystem : UpdateSystem
    {
        private EntityViewMap entityViewMap;

        public override void Update()
        {
            entities.Each((Entity e, EntityConvertedEvent convertedEvent, CollisionEmitterRef emitterRef) =>
            {
                entityViewMap.AddEntity(emitterRef.Value.gameObject.GetInstanceID(), e);
                Injector.Resolve(emitterRef.Value);
            });
        }
    }
}