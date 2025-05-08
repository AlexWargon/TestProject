using UnityEngine;

namespace Wargon.TestGame
{
    [EcsComponent]
    public struct CameraRef
    {
        public Camera Value;
        public Vector3 Offset;
        public float FollowSpeed;
    }
}