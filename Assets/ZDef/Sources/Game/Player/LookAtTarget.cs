using System;
using UnityEngine;

namespace ZDef.Game.Player
{
    public class LookAtTarget : MonoBehaviour
    {
        private Transform _target;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            enabled = false;
        }

        public void СaptureTarget(Transform target)
        {
            _target = target;
            enabled = _target != null;
        }

        public void ResetTarget()
        {
            СaptureTarget(null);
        }
        
        void Update()
        {
            Vector2 targetPosition = _target.position; 
            Vector2 thisPosition = _transform.position;
            Vector3 direction = (targetPosition - thisPosition).normalized;
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}
