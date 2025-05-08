using UnityEngine;

namespace Wargon.TestGame
{
    [EcsComponent]
    public struct Turret
    {
        public float RotationSpeed;
        public float FireCooldown;
        public float FireTime;
        public GameObject Projectile;
        public GameObject MuzzleFlash;
        public Transform FirePoint;
    }
}