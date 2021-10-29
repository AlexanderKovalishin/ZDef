using System;
using UnityEngine;
using ZDef.Game.Data;

namespace ZDef.Game.Enemies
{
    [RequireComponent(typeof(EnemyControllerFactory))]
    public class EnemySpawner: MonoBehaviour
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private EnemySpawnPoints _points;

        public int ProbabilityWeight => _enemyConfig.ProbabilityWeight;
        
        private EnemyControllerFactory _enemyControllerFactory;
        private void Awake()
        {
            _enemyControllerFactory = GetComponent<EnemyControllerFactory>();
        }

        public void SpawnEnemy(float deadLine)
        {
            var initArgs = new EnemyControllerInitArgs(
                _points.DequeuePoint(),
                _enemyConfig.Health,
                _enemyConfig.GetRandomVelocity(),
                deadLine);
            _enemyControllerFactory.Instantiate(initArgs);
        }
    }
}