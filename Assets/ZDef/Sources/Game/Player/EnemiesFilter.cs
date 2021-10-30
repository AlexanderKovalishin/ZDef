using System.Collections.Generic;
using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;
using ZDef.Game.Enemies;

namespace ZDef.Game.Player
{
    public class EnemiesFilter : MonoBehaviour
    {
        private EventBus _eventBus;
        private readonly HashSet<EnemyController> _enemies = new HashSet<EnemyController>();
        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
            _eventBus.Subscribe<InstantiateEnemyEvent>(InstantiateEnemyEventListener);
            _eventBus.Subscribe<EnemyDeadEvent>(EnemyDeadEventEventListener);
        }

        public EnemyController FindEnemy(Vector3 position, float radius)
        {
            var minDistance = float.MaxValue;
            EnemyController result = null;
            foreach (EnemyController enemy in _enemies)
            {
                float d = Vector3.Distance(enemy.Position, position);
                if (d > radius) continue;
                if (minDistance < d) continue;
                minDistance = d;
                result = enemy;
            }
            return result;
        }

        public void FindEnemies(Vector3 position, int count, float radius, HashSet<EnemyController> result)
        {
            result.Clear();
            for (var i = 0; i < count; i++)
            {
                var minDistance = float.MaxValue;
                EnemyController minResult = null;
                foreach (EnemyController enemy in _enemies)
                {
                    if (result.Contains(enemy)) continue;
                    float d = Vector3.Distance(enemy.Position, position);
                    if (d > radius) continue;
                    if (minDistance < d) continue;
                    minDistance = d;
                    minResult = enemy;
                }
                if (minResult == null) continue;
                result.Add(minResult);
            }
        }

        private void OnDestroy()
        {
            _eventBus.UnSubscribe<InstantiateEnemyEvent>(InstantiateEnemyEventListener);
            _eventBus.UnSubscribe<EnemyDeadEvent>(EnemyDeadEventEventListener);
        }

        private void InstantiateEnemyEventListener(InstantiateEnemyEvent args)
        {
            _enemies.Add(args.EnemyController);
        }

        private void EnemyDeadEventEventListener(EnemyDeadEvent args)
        {
            _enemies.Remove(args.Instance);
        }
    }
}