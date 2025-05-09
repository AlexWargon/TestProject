using UnityEngine;
using Wargon.ezs;
using Wargon.ezs.Unity;

namespace Wargon.TestGame
{
    public partial class EnemyAnimationSystem : UpdateSystem
    {
        private int death;
        private int idle;
        private int run;

        protected override void OnCreate()
        {
            idle = Animator.StringToHash("Idle");
            run = Animator.StringToHash("Run");
            death = Animator.StringToHash("Death");
        }

        public override void Update()
        {
            entities.Without<Inactive, DamageEvent>().Each((Entity e, AnimatorRef animRef, EnemyTag enemyTag) =>
            {
                if (e.Has<DeadState>())
                    animRef.Value.Play(death);
                else if (e.Has<ChaseState>())
                    animRef.Value.Play(run);
                else if (e.Has<IdleState>())
                    animRef.Value.Play(idle);
            });
        }
    }
}