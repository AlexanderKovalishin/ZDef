using System;
using UnityEngine;
using ZDef.Core.PoolFactory;

namespace ZDef.Game.Enemies
{
    public class EnemyController : MonoBehaviour, IFactoryInit<EnemyControllerInitArgs>, IReturnToPoolCallback<EnemyController>
    {
        private float _health; 
        private float _velocity;
        private float _deadLine;

        private Transform _transform; 

        private void Awake()
        {
            _transform = transform;
        }

        public void Init(EnemyControllerInitArgs args)
        {
            _transform.position = args.StartPosition.position;
            _health = args.Health;
            _velocity = args.Velocity;
            _deadLine = args.DeadLine;
        }

        private void Update()
        {
            float step = _velocity * Time.deltaTime;
            _transform.position += new Vector3(0, -step);
            if (_transform.position.y < _deadLine)
            {
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
