using UnityEngine;
using ZDef.Game.Data;

namespace ZDef.Game.Weapon
{
    [RequireComponent(typeof(ProjectileControllerFactory))]
    public class FirearmsWeapon: MonoBehaviour
    {
        [SerializeField] private FirearmsConfig _weaponConfig;
        private ProjectileControllerFactory _factory;
        private Transform _startPosition;
        private Transform _actualTarget;
        private float _timer;
            
        private void Awake()
        {
            _factory = GetComponent<ProjectileControllerFactory>();
            enabled = false;
        }

        public void СaptureTarget(Transform startPosition, Transform target)
        {
            _startPosition = startPosition;
            _actualTarget = target;
            enabled = target != null;
        }

        public void ResetTarget()
        {
            СaptureTarget(null, null);
        }

        private void DoTheShot()
        {
            _factory.Instantiate(new ProjectileInitArgs(_startPosition, _actualTarget, _weaponConfig.ProjectileVelocity));
        }

        private void Update()
        {
            float shotsTimeout = _weaponConfig.ShotsTimeout;
            _timer += Time.deltaTime;
            while (_timer > shotsTimeout)
            {
                DoTheShot();
                _timer -= shotsTimeout;
            }
        }
    }
}