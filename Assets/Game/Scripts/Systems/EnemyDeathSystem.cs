﻿using UnityEngine;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class EnemyDeathSystem : UpdateSystem
    {
        private EntityViewMap entityViewMap;
        private GameData gameData;
        private ObjectPool objectPool;

        public override void Update()
        {
            entities.Each((
                Entity e,
                DeathEvent deathEvent,
                DeathEffect deathEffect,
                EnemyTag tag,
                Speed speed,
                HealthBarViewRef healthBarViewRef) =>
            {
                e.Remove<IdleState>();
                e.Remove<ChaseState>();

                e.Add<DeadState>();
                e.Remove<DeathEvent>();
                speed.Value = 0;
                objectPool.Release(healthBarViewRef.Value, gameData.HealthBarViewPrefab.GetInstanceID());
                world.CreateEntity().Add(new EntityActionOnDelay
                {
                    Delay = 0.25f,
                    Action = PlayDeathEffect,
                    Target = e
                });
            });
        }

        private void PlayDeathEffect(Entity e)
        {
            e.Add<ClearViewEvent>();
            var transform = e.Get<TransformRef>().Value;
            var effect = objectPool.Spawn(e.Get<DeathEffect>().ParticleSystem, transform.position + Vector3.up * 0.2f, Quaternion.identity);
            ref var effectE = ref entityViewMap.GetEntity(effect.gameObject.GetInstanceID());
            ref var lifeTime = ref effectE.Get<LifeTime>();
            lifeTime.CurrentValue = lifeTime.Value;
        }
    }
}