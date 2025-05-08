using Wargon.ezs;
using Wargon.ezs.Unity;

namespace Wargon.TestGame
{
    public partial class CarMoveSystem : UpdateSystem
    {
        private TimeService timeService;

        public override void Update()
        {
            entities.Without<Inactive>().Each((TransformRef transform, Speed speed, Car car) =>
            {
                var pos = transform.Value.position;
                var add = speed.Value * timeService.DeltaTime;
                pos.z += add;
                transform.Value.position = pos;
                car.DistanceTraveled += add;
            });
        }
    }
}