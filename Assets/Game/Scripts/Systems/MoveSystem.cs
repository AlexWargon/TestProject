using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class MoveSystem : UpdateSystem
    {
        private TimeService timeService;

        public override void Update()
        {
            entities.Without<StaticTag>().Each((Speed speed, Direction direction, TransformRef transform) =>
            {
                transform.Value.position += direction.Value * speed.Value * timeService.DeltaTime;
            });
        }
    }
}