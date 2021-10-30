using System;
using System.Collections.Generic;
using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;
using ZDef.Game.Data;
using ZDef.Game.Enemies;

namespace ZDef.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Transform _playerPosition;
        [SerializeField] private EnemiesFilter _enemiesFilter;
        [SerializeField] private PlayerWeaponController[] _weapons;

        private int _health; 

        private readonly HashSet<EnemyController> _enemies = new HashSet<EnemyController>();
        private EventBus _eventBus;
        
        private void Awake()
        {
            _health = _playerConfig.Health;
            _eventBus = ServiceLocator.Locate<EventBus>();
            _eventBus.Subscribe<PlayerHitEvent>(PlayerHitEventListener);
        }

        private void OnDestroy()
        {
            _eventBus.UnSubscribe<PlayerHitEvent>(PlayerHitEventListener);
        }

        private void PlayerHitEventListener(PlayerHitEvent args)
        {
            _health -= args.Damage;
            if (_health < 0)
            {
                _eventBus.Send(new DefeatEvent());
                _health = 0;
            }
            _eventBus.Send(new PlayerUpdateHealthEvent(_health));
        }


        private void Start()
        {
            _eventBus.Send(new PlayerUpdateHealthEvent(_health));
        }

        private void Update()
        {
            _enemiesFilter.FindEnemies(_playerPosition.position, _weapons.Length, _enemies);
            foreach (PlayerWeaponController weapon in _weapons)
            {
                EnemyController enemy = FindEnemyForWeapon(weapon);
                _enemies.Remove(enemy);
                weapon.CaptureTarget(enemy);
            }
        }

        private EnemyController FindEnemyForWeapon(PlayerWeaponController weapon)
        {
            foreach (EnemyController enemy in _enemies)
            {
                float distance = Vector2.Distance(enemy.Transform.position, _playerPosition.position);
                if (distance > weapon.AttackRange) continue;
                return enemy;
            }
            return null;
        }
    }
}