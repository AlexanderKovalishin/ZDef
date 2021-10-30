using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private PlayerAnimator _animator;
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
                enabled = false;
                _animator.SetDead();
                foreach (PlayerWeaponController weapon in _weapons)
                {
                    weapon.CaptureTarget(null);
                }
            }
            _eventBus.Send(new PlayerUpdateHealthEvent(_health));
        }

        private void Start()
        {
            _eventBus.Send(new PlayerUpdateHealthEvent(_health));
        }

        private void Update()
        {
            float attackRange = _weapons.First().AttackRange;
            _enemiesFilter.FindEnemies(_playerPosition.position, _weapons.Length, attackRange, _enemies);
            EnemyController lastEnemy = null;
            foreach (PlayerWeaponController weapon in _weapons)
            {
                if (!weapon.IsFree(_playerPosition.position)) continue;
                EnemyController enemy = _enemies.FirstOrDefault();
                if (enemy != null)
                {
                    lastEnemy = enemy;
                }
                _enemies.Remove(enemy);
                weapon.CaptureTarget(lastEnemy);
            }
        }
    }
}