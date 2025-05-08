using UnityEngine;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class DamageSystem : UpdateSystem
    {
        public override void Update()
        {
            entities.Each((Entity e, Health health, DamageEvent damageEvent) =>
            {
                health.Value -= damageEvent.Value;
                health.Value = Mathf.Clamp(health.Value, 0, health.MaxValue);
                if (health.Value == 0) e.Add<DeathEvent>();
            });
        }
    }
}