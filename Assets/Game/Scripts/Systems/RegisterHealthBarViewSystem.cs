using UnityEngine;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class RegisterHealthBarViewSystem : UpdateSystem
    {
        private GameData gameData;
        private ObjectPool objectPool;

        public override void Update()
        {
            entities.Each((Entity e, EnemyTag Tag, PooledEvent pooledEvent, HealthBarOffset healthBarMovement,
                TransformRef transformRef) =>
            {
                var hpBar = objectPool.Spawn(gameData.HealthBarViewPrefab,
                    transformRef.Value.position + healthBarMovement.Value, Quaternion.identity);
                e.Add(new HealthBarViewRef
                {
                    Value = hpBar
                });
                hpBar.Restart();
                hpBar.gameObject.SetActive(false);
            });
        }
    }
}