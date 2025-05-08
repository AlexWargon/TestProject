using UnityEngine;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class ChaseDirectionSystem : UpdateSystem
    {
        public override void Update()
        {
            entities.Each((Car car, TransformRef playerTransformRef) =>
            {
                entities.Each((ChaseState state, TransformRef tr, Direction dir, Speed speed, ChaseSpeed chaseSpeed) =>
                {
                    var moveDir = (playerTransformRef.Value.position - tr.Value.position).normalized;
                    dir.Value = moveDir;
                    speed.Value = chaseSpeed.Value;

                    tr.Value.forward = Vector3.Lerp(tr.Value.forward, moveDir, 0.5f);
                });
            });
        }
    }
}