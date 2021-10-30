using UnityEngine;

namespace ZDef.Game.Player
{
    public class SmoothRotation: MonoBehaviour
    {
        private Transform _transform;
        [SerializeField] private float _k = 50f;

        private Quaternion _rotation;
        private void Awake()
        {
            _transform = transform;
            _rotation = _transform.rotation;
        }

        public void SetRotation(Quaternion value)
        {
            _rotation = value;
        }

        private void Update()
        {
            _transform.rotation = Quaternion.Lerp(_transform.rotation, _rotation, Mathf.Clamp01(Time.deltaTime * _k));
        }
    }
}