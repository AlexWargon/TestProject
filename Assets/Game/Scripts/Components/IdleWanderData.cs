using UnityEngine;

namespace Wargon.TestGame
{
    [EcsComponent]
    public struct IdleWanderData
    {
        public Vector3 StartPosition;
        public Vector3 CurrentTarget;
        public float MaxDistance;
        public float LerpFactor;
    }
}