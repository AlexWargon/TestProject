using UnityEngine;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class ActionOnDelaySystem : UpdateSystem
    {
        private TimeService time;

        public override void Update()
        {
            entities.Each((Entity e, EntityActionOnDelay action) =>
            {
                action.Delay -= time.DeltaTime;
                if (action.Delay <= 0)
                {
                    action.Action(action.Target);
                    e.Destroy();
                }
            });
            
            entities.Each((Entity e, ActionOnDelay action) =>
            {
                action.Delay -= time.DeltaTime;
                if (action.Delay <= 0)
                {
                    action.Action();
                    e.Destroy();
                }
            });
        }
    }
}