using System;
using System.Collections;
using UnityEngine;
using ZDef.Game.Data;
using Random = UnityEngine.Random;

namespace ZDef.Game.Enemies
{
    public class EnemiesSpawnSystem: MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private EnemySpawner[] _spawners;
        [SerializeField] private Transform _deadLine;

        private int _totalProbabilityWeight;
        
        private void Awake()
        {
            foreach (EnemySpawner spawner in _spawners)
            {
                _totalProbabilityWeight += spawner.ProbabilityWeight;
            }
        }

        private void Start()
        {
            StartCoroutine(SpawnLoop());
        }

        public IEnumerator SpawnLoop()
        {
            int count = _gameConfig.GetRandomEnemiesCount(); 
            for (var i = 0; i < count; i++)
            {
                yield return new WaitForSeconds(_gameConfig.GetRandomSpawnTimeout());
                // todo: choose spawner
                EnemySpawner spawner = GetRandomSpawner();
                spawner.SpawnEnemy(_deadLine.position.y);
            }
        }

        public EnemySpawner GetRandomSpawner()
        {
            int enemyIndex = Random.Range(0, _totalProbabilityWeight - 1);
            var index = 0;
            foreach (EnemySpawner spawner in _spawners)
            {
                index += spawner.ProbabilityWeight;
                if (enemyIndex < index)
                    return spawner;
            }
            throw new ArgumentException("field _spawners is empty");
        }
    }
}