using UnityEngine;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class IdleWanderSystem : UpdateSystem
    {
        private TimeService time;

        public override void Update()
        {
            entities.Each((IdleWanderData wander, TransformRef tr, Direction dir, Speed speed, IdleState state) =>
            {
                var dist = Vector3.Distance(tr.Value.position, wander.StartPosition);

                if (dist >= wander.MaxDistance)
                    wander.DirectionSign *= -1f;

                var forward = tr.Value.forward;
                dir.Value = forward * wander.DirectionSign;
                speed.Value = 0.5f;
            });
        }
    }
}