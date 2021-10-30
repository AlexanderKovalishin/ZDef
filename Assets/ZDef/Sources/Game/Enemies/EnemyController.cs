using System.Collections;
using DG.Tweening;
using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Core.PoolFactory;
using ZDef.Game.BusEvents;
using ZDef.Game.Weapon;

namespace ZDef.Game.Enemies
{
    public class EnemyController : MonoBehaviour, IFactoryInit<EnemyControllerInitArgs>,
        IReturnToPoolCallback<EnemyController>, IProjectileTarget
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private AnimatorSpeedByVelocity _speedByVelocity;
        [SerializeField] private float _deathTimeout = 10f;
        [SerializeField] private float _showDuration = 0.1f;
        [SerializeField] private float _hideDuration = 1f;
        [SerializeField] private float _deadlineOffset = 1f;
        [SerializeField] private Transform _fireTarget;

        private EventBus _eventBus;

        private int _health;
        private float _velocity;
        private int _damage;
        private float _deadLine;

        private Transform _transform;

        public bool IsAlive => _health > 0;
        public Transform Transform => _fireTarget;
        public Vector3 Position => _fireTarget.position;

        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
            _transform = transform;
        }

        private void OnDestroy()
        {
            _sprite.DOKill();
        }

        public void Init(EnemyControllerInitArgs args)
        {
            _sprite.DOFade(1, _showDuration);
            _animator.SetAlive(true);
            _transform.position = args.StartPosition;
            _health = args.Health;
            _velocity = args.Velocity;
            _deadLine = args.DeadLine;
            _damage = args.Damage;
            _speedByVelocity.SetVelocity(_velocity);
        }

        public void Hit(HitArgs args)
        {
            if (!IsAlive) return;
            _health -= args.Damage;
            _eventBus.Send(new HitFxEvent(_fireTarget.position, args.Source, args.Damage));
            if (!IsAlive)
            {
                _eventBus.Send(new EnemyDeadEvent(this));
                _animator.SetAlive(false);
                StartCoroutine(ReturnToPoolAfterTimeout(_deathTimeout));
            }
        }

        private void Update()
        {
            if (!IsAlive) return;
            float step = _velocity * Time.deltaTime;
            _transform.position += new Vector3(0, -step);

            if (_transform.position.y >= _deadLine) return;

            _health = 0;
            Vector3 position = _transform.position;
            var hitPosition = new Vector3(position.x, _deadLine - _deadlineOffset, position.z);
            _eventBus.Send(new PlayerHitEvent(_damage, hitPosition, hitPosition));
            _eventBus.Send(new EnemyDeadEvent(this));
            InvokeReturnToPool();
        }

        private IEnumerator ReturnToPoolAfterTimeout(float timeout)
        {
            yield return new WaitForSeconds(timeout);
            _sprite.DOFade(0, _hideDuration);
            yield return new WaitForSeconds(_hideDuration);
            _sprite.DOKill();
            InvokeReturnToPool();
        }

        private void InvokeReturnToPool()
        {
            ReturnToPool?.Invoke(this);
        }

        public event DestroyDelegate<EnemyController> ReturnToPool;
    }
}
