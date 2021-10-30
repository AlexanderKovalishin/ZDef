using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;
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
        private EventBus _eventBus;
        
        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
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
                _enemyConfig.Damage,
                _enemyConfig.GetRandomVelocity(),
                deadLine);
            
            EnemyController instance = _enemyControllerFactory.Instantiate(initArgs);
            instance.ReturnToPool += EnemyOnReturnToPool;
            _eventBus.Send(new InstantiateEnemyEvent(instance, initArgs));
        }

        private void EnemyOnReturnToPool(EnemyController sender)
        {
            sender.ReturnToPool -= EnemyOnReturnToPool;
            _eventBus.Send(new ReturnEnemyEvent(sender));
        }
    }
}