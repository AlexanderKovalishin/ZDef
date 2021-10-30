using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Core.PoolFactory;
using ZDef.Game.BusEvents;
using ZDef.Game.Weapon;

namespace ZDef.Game.Enemies
{
    public class EnemyController : MonoBehaviour, IFactoryInit<EnemyControllerInitArgs>, IReturnToPoolCallback<EnemyController>, IProjectileTarget
    {
        private EventBus _eventBus;

        private int _health; 
        private float _velocity;
        private int _damage;
        private float _deadLine;

        private Transform _transform;

        public bool IsAlive => _health > 0;
        public Transform Transform => _transform;
        public Vector3 Position => _transform.position;

        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
            _transform = transform;
        }

        public void Init(EnemyControllerInitArgs args)
        {
            _transform.position = args.StartPosition;
            _health = args.Health;
            _velocity = args.Velocity;
            _deadLine = args.DeadLine;
            _damage = args.Damage;
        }

        public void Hit(HitArgs args)
        {
            _health -= args.Damage;
            _eventBus.Send(new HitFxEvent(_transform.position, args.Source, args.Damage));
        }

        private void Update()
        {
            float step = _velocity * Time.deltaTime;
            _transform.position += new Vector3(0, -step);
            if (_health <= 0)
            {
                InvokeReturnToPool();
                return;
            }

            if (_transform.position.y < _deadLine)
            {
                Vector3 hitPosition = _transform.position;
                _eventBus.Send(new PlayerHitEvent(_damage, hitPosition, hitPosition));
                InvokeReturnToPool();
            }
        }

        private void InvokeReturnToPool()
        {
            ReturnToPool?.Invoke(this);
        }

        public event DestroyDelegate<EnemyController> ReturnToPool;
    }
}
