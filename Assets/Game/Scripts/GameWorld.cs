using UnityEngine;
using Wargon.ezs;
using Wargon.ezs.Unity;

namespace Wargon.TestGame
{
    [DefaultExecutionOrder(ExecutionOrder.WORLD)]
    public class GameWorld : MonoBehaviour
    {
        private World world;
        private Systems systems;
        private Systems lateSystems;
        private TimeService timeService;
        private GameRuntimeData runtimeData;

        public void InitializeWorld(out World ecsWorld)
        {
            world = Worlds.New();
            MonoConverter.Init(world);
            ecsWorld = world;
        }

        public void InitializeSystems()
        {
            systems = new Systems(world);
            systems
                .Add(new PlayerInputWriteSystem())
                .Add(new MoveSystem())
                .Add(new CarMoveSystem())
                .Add(new LoopGroundSystem())
                .Add(new SpawnEnemiesSystem())
                .Add(new TurretAimSystem())
                .Add(new TurretShootingSystem())
                .Add(new MoveProjectileSystem())
                .Add(new RegisterCollisionEmittersSystem())
                .Add(new RegisterHealthBarViewSystem())
                .Add(new HealthBarUpdatePositionSystem())
                .Add(new DistanceFromEnemyToPlayerSystem())
                .Add(new IdleInitSystem())
                .Add(new IdleWanderSystem())
                .Add(new EnemyChaseTriggerSystem())
                .Add(new ChaseDirectionSystem())
                .Add(new DamageOnCollisionSystem())
                .Add(new ProjectileOnCollisionSystem())
                .Add(new DamageSystem())
                .Add(new ApplyDamageEffectSystem())
                .Add(new StunEnemyOnDamageSystem())
                .Add(new EnemyAnimationSystem())
                .Add(new WinSystem())
                .Add(new EnemyDeathSystem())
                .Add(new ActionOnDelaySystem())
                .Add(new LifeTimeSystem())
                .Add(new PlayParticleSystem())
                .Add(new PlayerDeathSystem())
                .Add(new ClearEnemiesViewOnDistanceSystem())
                .Add(new ClearViewSystem())
#if UNITY_EDITOR
                .Add(new DebugDrawSpawnAreaSystem())
#endif
                .Add(new RemoveComponentSystem(typeof(DeathEvent)))
                .Add(new RemoveComponentSystem(typeof(EntityConvertedEvent)))
                .Add(new RemoveComponentSystem(typeof(DamageEvent)))
                .Add(new RemoveComponentSystem(typeof(CollisionEvent)))
                .Add(new RemoveComponentSystem(typeof(SpawnedEvent)))
                .Add(new RemoveComponentSystem(typeof(PooledEvent)))
                .Init();
            lateSystems = new Systems(world);
            lateSystems
                .Add(new CameraMoveSystem())
                .Init();
#if UNITY_EDITOR
            _ = new DebugInfo(world);
#endif
        }

        private void Update()
        {
            if (runtimeData.State != GameRuntimeData.GameState.GameRunning) return;

            timeService.DeltaTime = Time.deltaTime;
            timeService.FixedDeltaTime = Time.fixedDeltaTime;
            systems.OnUpdate();
        }

        private void LateUpdate()
        {
            lateSystems.OnUpdate();
        }

        private void OnDestroy()
        {
            world.Destroy();
        }
    }

    public partial class StunEnemyOnDamageSystem : UpdateSystem
    {
        private int damageAnim;
        protected override void OnCreate()
        {
            damageAnim = Animator.StringToHash("Death");
        }

        public override void Update()
        {
            entities.Each((RenderDamageEffectTime renderDamageEffectTime, Speed speed, EnemyTag tag) =>
            {
                speed.Value = 0;
            });
            entities.Each((DamageEvent damageEvnt, EnemyTag tag, AnimatorRef animatorRef) =>
            {
                animatorRef.Value.Play(damageAnim);
            });
        }
    }
}