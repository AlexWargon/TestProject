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
                var pos = tr.Value.position;
                var dist = Vector3.Distance(pos, wander.StartPosition);

                if (dist >= wander.MaxDistance)
                    wander.DirectionSign *= -1f;

                var moveDir = tr.Value.forward * wander.DirectionSign;
                moveDir.y = 0f;
                moveDir.Normalize();

                dir.Value = moveDir;

                tr.Value.forward = Vector3.Lerp(tr.Value.forward, moveDir, 0.5f);

                speed.Value = 1f;
            });
        }
    }
}