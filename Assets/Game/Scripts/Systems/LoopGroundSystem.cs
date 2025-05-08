using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class LoopGroundSystem : UpdateSystem
    {
        public override void Update()
        {
            entities.Each((Ground ground, TransformRef transformRef) =>
            {
                entities.Without<DeadTag>().Each((Car car, TransformRef carTransformRef) =>
                {
                    if (ground.TriggerPosition.z <= carTransformRef.Value.position.z)
                    {
                        var currentPosition = ground.NewPosition;
                        ground.NewPosition.z += ground.Step;
                        ground.TriggerPosition.z += ground.Step;
                        transformRef.Value.position = currentPosition;
                    }
                });
            });
        }
    }
}