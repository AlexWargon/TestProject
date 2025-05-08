using Wargon.ezs;
using Wargon.ezs.Unity;

namespace Wargon.TestGame
{
    public partial class LifeTimeSystem : UpdateSystem
    {
        private ObjectPool objectPool;
        private TimeService time;

        public override void Update()
        {
            entities.Without<Inactive>().Each(
                (Entity e, LifeTime lifeTime, PrefabKey prefabKey, TransformRef transformRef) =>
                {
                    lifeTime.CurrentValue -= time.DeltaTime;
                    if (lifeTime.CurrentValue <= 0)
                    {
                        objectPool.Release(transformRef.Value.gameObject, prefabKey.Value);
                        e.Add<Inactive>();
                        lifeTime.CurrentValue = lifeTime.Value;
                        if (e.Has<Particle>()) e.Get<Particle>().Value.Stop();
                    }
                });
        }
    }
}