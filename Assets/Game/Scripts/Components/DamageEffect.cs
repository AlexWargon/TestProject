using UnityEngine;

namespace Wargon.TestGame
{
    [EcsComponent]
    public struct DamageEffect
    {
        public Material Material;
        public Material DefaultMaterial;
        public float Time;
    }
}