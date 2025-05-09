using System;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public struct EntityActionOnDelay
    {
        public float Delay;
        public Entity Target;
        public Action<Entity> Action;
    }

    public struct ActionOnDelay
    {
        public float Delay;
        public Action Action;
    }
}