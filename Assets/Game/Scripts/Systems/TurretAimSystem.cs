using UnityEngine;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class TurretAimSystem : UpdateSystem
    {
        private const float X_OFFSET = -90f;
        private TimeService timeService;

        public override void Update()
        {
            entities.Each((Turret turret, TransformRef transformRef) =>
            {
                entities.Each((CameraRef cameraRef) =>
                {
                    var ray = cameraRef.Value.ScreenPointToRay(Input.mousePosition);
                    var groundPlane = new Plane(Vector3.up, transformRef.Value.position);

                    if (groundPlane.Raycast(ray, out var enter))
                    {
                        var hitPoint = ray.GetPoint(enter);
                        var direction = hitPoint - transformRef.Value.position;
                        direction.y = 0f;

                        if (direction.sqrMagnitude > 0.001f)
                        {
                            var lookRotation = Quaternion.LookRotation(direction);
                            var yRotation = lookRotation.eulerAngles.y;
                            var targetRotation = Quaternion.Euler(X_OFFSET, yRotation, 0f);
                            transformRef.Value.rotation = Quaternion.RotateTowards(
                                transformRef.Value.rotation,
                                targetRotation,
                                turret.RotationSpeed * timeService.DeltaTime
                            );
                        }
                    }
                });
            });
        }
    }
}