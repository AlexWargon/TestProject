using Wargon.ezs;
using Wargon.ezs.Unity;

namespace Wargon.TestGame
{
    public partial class EnemyChaseTriggerSystem : UpdateSystem
    {
        public override void Update()
        {
            entities.Without<Inactive>().Each(
                (Entity e, Distance distance, AgroDistance agroDistance, IdleState state) =>
                {
                    if (agroDistance.Value > distance.Value)
                    {
                        e.Remove<IdleState>();
                        e.Add<ChaseState>();
                    }
                });
        }
    }
}