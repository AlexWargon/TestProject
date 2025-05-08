using UnityEngine;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class DebugDrawSpawnAreaSystem : UpdateSystem
    {
        public override void Update()
        {
            entities.Each((EnemySpawner spawner) =>
            {
                entities.Without<DeadTag>().Each((Car car, TransformRef transformRef) =>
                {
                    var area = spawner.SpawnArea;
                    area.y += transformRef.Value.position.z;
                    area.w += transformRef.Value.position.z;
                    var bottomLeft = new Vector3(area.x, 0, area.y);
                    var bottomRight = new Vector3(area.z, 0, area.y);
                    var topRight = new Vector3(area.z, 0, area.w);
                    var topLeft = new Vector3(area.x, 0, area.w);

                    Debug.DrawLine(bottomLeft, bottomRight, Color.green);
                    Debug.DrawLine(bottomRight, topRight, Color.green);
                    Debug.DrawLine(topRight, topLeft, Color.green);
                    Debug.DrawLine(topLeft, bottomLeft, Color.green);
                });
            });
        }
    }
}