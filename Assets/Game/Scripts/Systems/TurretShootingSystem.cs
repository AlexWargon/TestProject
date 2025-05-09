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
                    objectPool.Spawn(turret.Projectile, turret.FirePoint.position,
                        turret.FirePoint.rotation);
                    objectPool.Spawn(turret.MuzzleFlash, turret.FirePoint.position,
                        turret.FirePoint.rotation);

                    turret.FireTime = turret.FireCooldown;

                    transformRef.Value.DOPunchPosition(transformRef.Value.up * -0.1f, turret.FireTime);
                }
            });
        }
    }
}