using Wargon.ezs;
using Wargon.ezs.Unity;

namespace Wargon.TestGame
{
    public partial class ClearEnemiesViewOnDistanceSystem : UpdateSystem
    {
        private const float DESPAWN_DISTANCE = 180f;
        private ObjectPool objectPool;

        public override void Update()
        {
            entities.Without<Inactive>().Each(
                (Entity e, Distance distance, PrefabKey prefabKey, TransformRef enemyTransformRef, EnemyTag tag) =>
                {
                    if (distance.Value > DESPAWN_DISTANCE) e.Add<ClearViewEvent>();
                });
        }
    }
}