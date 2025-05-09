using UnityEngine;

namespace Wargon.TestGame
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Game", order = 1)]
    public class GameData : ScriptableObject
    {
        public float WinDistance;
        public HealthBarView HealthBarViewPrefab;
    }
}