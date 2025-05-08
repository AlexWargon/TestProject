using UnityEngine;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class IdleInitSystem : UpdateSystem
    {
        public override void Update()
        {
            entities.Each((Entity e, TransformRef tr, SpawnedEvent spawnedEvent) =>
            {
                e.Get<IdleWanderData>() = new IdleWanderData
                {
                    StartPosition = tr.Value.position,
                    MaxDistance = Random.Range(2f, 3f),
                    DirectionSign = 1f
                };
                e.Remove<DeadState>();
                e.Add<IdleState>();
            });
        }
    }
}