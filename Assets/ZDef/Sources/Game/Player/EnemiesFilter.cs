using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;
using ZDef.Game.Enemies;

namespace ZDef.Game.Player
{
    // todo: use sensor for enemies registry
    // todo: use raycast, for enemy choose

    public class EnemiesFilter : MonoBehaviour
    {
        private EventBus _eventBus;
        private readonly HashSet<EnemyController> _enemies = new HashSet<EnemyController>();
        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
            _eventBus.Subscribe<InstantiateEnemyEvent>(InstantiateEnemyEventListener);
            _eventBus.Subscribe<ReturnEnemyEvent>(ReturnEnemyEventListener);
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

        public void FindEnemies(Vector3 position, int count, HashSet<EnemyController> result)
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
            _eventBus.UnSubscribe<ReturnEnemyEvent>(ReturnEnemyEventListener);
        }

        private void InstantiateEnemyEventListener(InstantiateEnemyEvent args)
        {
            _enemies.Add(args.EnemyController);
        }

        private void ReturnEnemyEventListener(ReturnEnemyEvent args)
        {
            _enemies.Remove(args.EnemyController);
        }
    }
}