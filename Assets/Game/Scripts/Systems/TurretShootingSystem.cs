using DG.Tweening;
using Wargon.ezs;
using Wargon.ezs.Unity;

namespace Wargon.TestGame
{
    public partial class TurretShootingSystem : UpdateSystem
    {
        private EntityViewMap entityViewMap;
        private ObjectPool objectPool;
        private TimeService time;

        public override void Update()
        {
            entities.Without<Inactive>().Each((Turret turret, TransformRef transformRef) =>
            {
                turret.FireTime -= time.DeltaTime;
                if (turret.FireTime <= 0f)
                {
                    var projectile = objectPool.Spawn(turret.Projectile, turret.FirePoint.position,
                        turret.FirePoint.rotation);
                    ref var projectileE = ref entityViewMap.GetEntity(projectile.GetInstanceID());
                    projectileE.Remove<Inactive>();

                    var muzzle = objectPool.Spawn(turret.MuzzleFlash, turret.FirePoint.position,
                        turret.FirePoint.rotation);

                    turret.FireTime = turret.FireCooldown;
                    var originalScale = transformRef.Value.localScale;
                    var scaledUp = originalScale * 1.3f;

                    transformRef.Value.DOScale(scaledUp, 0.1f).OnComplete(() =>
                    {
                        transformRef.Value.localScale = originalScale;
                    });
                }
            });
        }
    }
}