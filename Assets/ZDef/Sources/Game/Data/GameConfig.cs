using UnityEngine;

namespace ZDef.Game.Data
{
    [CreateAssetMenu(menuName = "ZDef/GameConfig", fileName = "GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [Header("Enemies Count")]
        [SerializeField] private int _minEnemies;
        [SerializeField] private int _maxEnemies;
        [Header("Enemies Spawn Timeout")]
        [SerializeField] private float _minSpawnTimeout;
        [SerializeField] private float _maxSpawnTimeout;

        public int GetRandomEnemiesCount() => Random.Range(_minEnemies, _maxEnemies);
        public float GetRandomSpawnTimeout() => Random.Range(_minSpawnTimeout, _maxSpawnTimeout);

    }
}