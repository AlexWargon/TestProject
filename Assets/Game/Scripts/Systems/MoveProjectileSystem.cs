using Wargon.ezs;
using Wargon.ezs.Unity;

namespace Wargon.TestGame
{
    public partial class MoveProjectileSystem : UpdateSystem
    {
        private TimeService time;

        public override void Update()
        {
            entities.Without<Inactive>().Each((ProjectileTag tag, Speed speed, TransformRef transformRef) =>
            {
                transformRef.Value.position += transformRef.Value.up * speed.Value * time.DeltaTime;
            });
        }
    }
}