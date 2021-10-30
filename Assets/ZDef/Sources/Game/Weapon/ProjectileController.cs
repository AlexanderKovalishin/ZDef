using UnityEngine;
using ZDef.Core.PoolFactory;

namespace ZDef.Game.Weapon
{
    public class ProjectileController : MonoBehaviour, IFactoryInit<ProjectileInitArgs>, IReturnToPoolCallback<ProjectileController>
    {
        private Transform _transform;
        private IProjectileTarget _target;
        private Vector2 _startPosition;
        private Vector2 _targetPosition;
        private Vector3 _direction;
        private float _velocity;
        private int _damage;
        private void Awake()
        {
            _transform = transform;
        }

        public void Init(ProjectileInitArgs args)
        {
            _target = args.Target;
            _velocity = args.Velocity;
            _damage = args.Damage;
            _startPosition = args.StartPosition.position;
            _transform.position = _startPosition;
        }

        private void Update()
        {
            if (_target is { IsAlive: true })
            {
                _targetPosition = _target.Transform.position;
            }
            else
            { 
                _target = null;
            }

            Vector2 thisPosition = _transform.position;
            float distance = Vector2.Distance(_targetPosition, thisPosition);
            float deltaDistance = _velocity * Time.deltaTime;

            if (deltaDistance < distance)
            {
                _direction = (_targetPosition - thisPosition) / distance;
                float rotation = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                _transform.position += _direction * deltaDistance;
                _transform.rotation = Quaternion.Euler(0, 0, rotation);
            }
            else
            {
                _target?.Hit(new HitArgs(_damage, _startPosition));
                InvokeReturnToPool();
            }
        }

        private void InvokeReturnToPool()
        {
            ReturnToPool?.Invoke(this);
        }

        public event DestroyDelegate<ProjectileController> ReturnToPool;
    }
}
