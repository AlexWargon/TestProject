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
}