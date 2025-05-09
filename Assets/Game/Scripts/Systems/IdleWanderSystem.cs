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
                var toTarget = wander.CurrentTarget - pos;
                toTarget.y = 0f;

                if (toTarget.sqrMagnitude < 0.2f)
                {
                    var offset = Random.insideUnitCircle * wander.MaxDistance;
                    wander.CurrentTarget = wander.StartPosition + new Vector3(offset.x, 0f, offset.y);
                    toTarget = wander.CurrentTarget - pos;
                    toTarget.y = 0f;
                }

                var moveDir = toTarget.normalized;
                dir.Value = moveDir;
                speed.Value = .5f;

                tr.Value.forward = Vector3.Lerp(tr.Value.forward, moveDir, wander.LerpFactor);
            });
        }
    }
}