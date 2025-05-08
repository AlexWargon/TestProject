using Wargon.ezs;
using Wargon.ezs.Unity;

namespace Wargon.TestGame
{
    public partial class ClearViewSystem : UpdateSystem
    {
        private ObjectPool objectPool;

        public override void Update()
        {
            entities.Without<Inactive>().Each(
                (Entity e, ClearViewEvent clearViewEvent, PrefabKey prefabKey, TransformRef transformRef) =>
                {
                    objectPool.Release(transformRef.Value.gameObject, prefabKey.Value);
                    e.Remove<ClearViewEvent>();
                    e.Add<Inactive>();
                });
        }
    }
}