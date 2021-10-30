using System;
using System.Collections;
using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;
using Random = UnityEngine.Random;

namespace ZDef.Game.Enemies
{
    public class EnemiesSpawnSystem: MonoBehaviour
    {
        [SerializeField] private EnemySpawner[] _spawners;
        [SerializeField] private Transform _deadLine;

        private EventBus _eventBus;
        private int _totalProbabilityWeight;
        
        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
            _eventBus.Subscribe<StartSpawnEnemies>(StartSpawnEnemiesListener);
            foreach (EnemySpawner spawner in _spawners)
            {
                _totalProbabilityWeight += spawner.ProbabilityWeight;
            }
        }

        private void OnDestroy()
        {
            _eventBus.UnSubscribe<StartSpawnEnemies>(StartSpawnEnemiesListener);
        }

        private void StartSpawnEnemiesListener(StartSpawnEnemies args)
        {
            StartCoroutine(SpawnLoop(args.EnemiesCount, args.MinSpawnTimeout, args.MaxSpawnTimeout));
        }

        
        private IEnumerator SpawnLoop(int count, float minSpawnTimeout, float maxSpawnTimeout)
        {
            for (var i = 0; i < count; i++)
            {
                yield return new WaitForSeconds(Random.Range(minSpawnTimeout, maxSpawnTimeout));
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