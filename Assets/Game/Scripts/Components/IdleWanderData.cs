using UnityEngine;

namespace Wargon.TestGame
{
    [EcsComponent]
    public struct IdleWanderData
    {
        public Vector3 StartPosition;
        public float MaxDistance;
        public float Timer;
        public float DirectionSign;
    }
}