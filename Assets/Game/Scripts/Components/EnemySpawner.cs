using UnityEngine;

namespace Wargon.TestGame
{
    [EcsComponent]
    public struct EnemySpawner
    {
        public GameObject Prefab;
        public int AmountToSpawnMin;
        public int AmountToSpawnMax;
        public Vector4 SpawnArea;
        public float TimeBetweenSpawns;
        public float TimeBetweenSpawnsCounter;
    }
}