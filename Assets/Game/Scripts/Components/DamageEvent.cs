using Wargon.ezs;

namespace Wargon.TestGame
{
    [EcsComponent]
    public struct DamageEvent
    {
        public Entity From;
        public int Value;
    }
}