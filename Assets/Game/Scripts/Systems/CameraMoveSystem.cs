using UnityEngine;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class CameraMoveSystem : UpdateSystem
    {
        private TimeService time;

        public override void Update()
        {
            entities.Each((CameraRef cameraRef, TransformRef transformRef) =>
            {
                entities.Each((Car car, TransformRef carTransform) =>
                {
                    var targetPos = carTransform.Value.position + cameraRef.Offset;
                    var lerpSpeed = cameraRef.FollowSpeed;
                    var dt = time.DeltaTime;

                    transformRef.Value.position = Vector3.Lerp(
                        transformRef.Value.position,
                        targetPos,
                        lerpSpeed * dt
                    );
                });
            });
        }
    }
}