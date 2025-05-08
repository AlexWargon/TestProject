using UnityEngine;

namespace Wargon.TestGame
{
    [EcsComponent]
    public struct PlayerInput
    {
        public Vector2 Axis;
        public Vector3 MousePosition;
        public bool Fire;
    }
}