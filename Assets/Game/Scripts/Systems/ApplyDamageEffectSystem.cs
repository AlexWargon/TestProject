using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class ApplyDamageEffectSystem : UpdateSystem
    {
        private TimeService time;

        public override void Update()
        {
            entities.Each((Entity e, DamageEffect damageEffect, Renderer renderer, DamageEvent damageEvent) =>
            {
                renderer.Value.material = damageEffect.Material;
                e.Add(new RenderDamageEffectTime { Value = damageEffect.Time });
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
        }
    }
}