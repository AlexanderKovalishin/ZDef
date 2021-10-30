using UnityEngine;

namespace ZDef.Game.Player
{
    public class LookAtTarget : MonoBehaviour
    {
        [SerializeField] private SmoothRotation _smoothRotation;
        private float _defaultRotation;
        private Transform _target;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            enabled = false;
            _defaultRotation = _transform.rotation.eulerAngles.z;
        }

        public void CaptureTarget(Transform target)
        {
            _target = target;
            enabled = _target != null;
            if (!enabled)
            {
                _smoothRotation.SetRotation(Quaternion.Euler(0, 0, _defaultRotation));
            }
        }
        
        void Update()
        {
            Vector2 targetPosition = _target.position; 
            Vector2 thisPosition = _transform.position;
            Vector3 direction = (targetPosition - thisPosition).normalized;
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _smoothRotation.SetRotation(Quaternion.Euler(0, 0, rotation - 90f));
        }
    }
}
