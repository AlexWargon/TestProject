using DG.Tweening;
using UnityEngine;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class ApplyDamageEffectSystem : UpdateSystem
    {
        private TimeService time;

        public override void Update()
        {
            entities.Each((Entity e, DamageEffect damageEffect, Renderer renderer, DamageEvent damageEvent,
                Health health, HealthBarViewRef healthBarViewRef) =>
            {
                renderer.Value.material = damageEffect.Material;
                e.Add(new RenderDamageEffectTime { Value = damageEffect.Time });
                healthBarViewRef.Value.ApplyDamage(health.Value, health.MaxValue);
            });

            entities.Each((Entity e, DamageEffect damageEffect, Renderer renderer, RenderDamageEffectTime renderTime) =>
            {
                renderTime.Value -= time.DeltaTime;
                if (renderTime.Value <= 0)
                {
                    renderer.Value.material = damageEffect.DefaultMaterial;
                    e.Remove<RenderDamageEffectTime>();
                }
            });

            entities.Without<RenderDamageEffectTime>()
                .Each((Car car, DamageEvent damageEvent, TransformRef transformRef) =>
                {
                    transformRef.Value.DOPunchScale(Vector3.one * 0.1f, 0.1f);
                });
        }
    }
}