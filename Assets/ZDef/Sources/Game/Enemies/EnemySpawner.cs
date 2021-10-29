using UnityEngine;
using ZDef.Game.Data;

namespace ZDef.Game.Enemies
{
    [RequireComponent(typeof(EnemyControllerFactory))]
    public class EnemySpawner: MonoBehaviour
    {
        [SerializeField] private float _offset = 1.0f;
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
            Transform spawnPoint = _points.DequeuePoint();
            Vector3 spawnPosition = spawnPoint.position;
            spawnPosition.x += Random.Range(-_offset, _offset);
            
            var initArgs = new EnemyControllerInitArgs(
                spawnPosition,
                _enemyConfig.Health,
                _enemyConfig.GetRandomVelocity(),
                deadLine);
            _enemyControllerFactory.Instantiate(initArgs);
        }
    }
}