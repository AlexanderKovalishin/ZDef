using UnityEngine;

namespace ZDef.Core
{
    public class AnimatorSpeedByVelocity: MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _speedParameter = "Speed";
        [SerializeField] private float _velocityIdentity = 1f;
        [SerializeField] private float _velocityEps = 0.001f;
        public void SetVelocity(float velocity)
        {
            if (Mathf.Abs(velocity) < _velocityEps)
                velocity = _velocityEps;
            float speed = velocity / _velocityIdentity;
            _animator.SetFloat(_speedParameter, speed);
        }
    }
}