using UnityEngine;
using Wargon.ezs;
using Wargon.ezs.Unity;

namespace Wargon.TestGame
{
    public partial class SpawnEnemiesSystem : UpdateSystem
    {
        private EntityViewMap entityViewMap;
        private ObjectPool objectPool;
        private TimeService timeService;

        public override void Update()
        {
            entities.Each((EnemySpawner spawner) =>
            {
                spawner.TimeBetweenSpawnsCounter -= timeService.DeltaTime;
                if (spawner.TimeBetweenSpawnsCounter <= 0)
                {
                    spawner.TimeBetweenSpawnsCounter = spawner.TimeBetweenSpawns;
                    entities.Each((Car car, TransformRef carTransformRef) =>
                    {
                        var enemiesToSpawn = Random.Range(spawner.AmountToSpawnMin, spawner.AmountToSpawnMax);
                        var spawnRect = spawner.SpawnArea;
                        spawnRect.y += carTransformRef.Value.position.z;
                        spawnRect.w += carTransformRef.Value.position.z;
                        for (; enemiesToSpawn > 0; --enemiesToSpawn)
                        {
                            var x = Random.Range(spawnRect.x, spawnRect.z);
                            var z = Random.Range(spawnRect.y, spawnRect.w);
                            var spawnPosition = new Vector3(x, 0, z);
                            var enemyView = objectPool.Spawn(spawner.Prefab, spawnPosition, Quaternion.identity);
                            ref var e = ref entityViewMap.GetEntity(enemyView.GetInstanceID());
                            ref var health = ref e.Get<Health>();
                            health.Value = health.MaxValue;
                            e.Remove<Inactive>();
                            e.Add<SpawnedEvent>();
                        }
                    });
                }
            });
        }
    }

    public struct SpawnedEvent
    {
    }
}