using UnityEngine;
using Wargon.ezs;
using Wargon.ezs.Unity;

namespace Wargon.TestGame
{
    public partial class DistanceFromEnemyToPlayerSystem : UpdateSystem
    {
        public override void Update()
        {
            entities.Each((Car car, TransformRef transformRef) =>
            {
                entities.Without<Inactive>()
                    .Each((Distance distance, TransformRef enemyTransformRef, EnemyTag tag) =>
                    {
                        distance.Value = Vector3.Distance(transformRef.Value.position,
                            enemyTransformRef.Value.position);
                    });
            });
        }
    }
}