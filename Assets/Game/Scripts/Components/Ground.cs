using UnityEngine;

namespace Wargon.TestGame
{
    [EcsComponent]
    public struct Ground
    {
        public Vector3 NewPosition;
        public Vector3 TriggerPosition;
        public float Step;
    }
}