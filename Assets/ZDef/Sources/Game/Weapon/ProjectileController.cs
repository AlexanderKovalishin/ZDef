using UnityEngine;
using ZDef.Core.PoolFactory;

namespace ZDef.Game.Weapon
{
    public class ProjectileController : MonoBehaviour, IFactoryInit<ProjectileInitArgs>, IReturnToPoolCallback<ProjectileController>
    {
        private Transform _transform;
        private Transform _target;
        private float _velocity;
        private void Awake()
        {
            _transform = transform;
        }

        public void Init(ProjectileInitArgs args)
        {
            _target = args.Target;
            _velocity = args.Velocity;
            _transform.position = args.StartPosition.position;
        }

        private void Update()
        {
            Vector2 targetPosition = _target.position; 
            Vector2 thisPosition = _transform.position;
            float distance = Vector2.Distance(targetPosition, thisPosition);
            float deltaDistance = _velocity * Time.deltaTime;
            if (deltaDistance < distance)
            {
                Vector3 direction = (targetPosition - thisPosition) / distance;
                float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _transform.position += direction * deltaDistance;
                _transform.rotation = Quaternion.Euler(0, 0, rotation);
            }
            else
            {
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
