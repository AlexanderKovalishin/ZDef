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
        private float _velocity;
        private float _time;
        private float _duration;
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
            _duration = Vector3.Distance(_startPosition, _target.Transform.position) / _velocity;
            _time = 0;
        }

        private void Update()
        {
            _time += Time.deltaTime;

            if (_time < _duration)
            {
                Vector3 targetPosition = _target.Transform.position;
                Vector3 startPosition = _startPosition;
                _transform.position = Vector3.Lerp(startPosition, targetPosition, _time / _duration);
                 Vector3 direction = (targetPosition - startPosition).normalized;
                 float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                 _transform.rotation = Quaternion.Euler(0, 0, rotation);
            }
            else
            {
                if (_target.IsAlive)
                {
                    _target?.Hit(new HitArgs(_damage, _startPosition));
                }

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
