using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class HealthBarUpdatePositionSystem : UpdateSystem
    {
        public override void Update()
        {
            entities.Each(
                (EnemyTag tag, HealthBarViewRef healthBarViewRef, TransformRef transformRef,
                    HealthBarOffset healthBarMovement) =>
                {
                    healthBarViewRef.Value.transform.position = transformRef.Value.position + healthBarMovement.Value;
                });
        }
    }
}