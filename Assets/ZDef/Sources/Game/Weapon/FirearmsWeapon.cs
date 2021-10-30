using UnityEngine;
using ZDef.Game.Data;

namespace ZDef.Game.Weapon
{
    [RequireComponent(typeof(ProjectileControllerFactory))]
    public class FirearmsWeapon: MonoBehaviour
    {
        [SerializeField] private FirearmsConfig _weaponConfig;
        [SerializeField] private Transform _anchor;
        
        private ProjectileControllerFactory _factory;
        private IProjectileTarget _actualTarget;
        private float _timer;

        public float AttackRange => _weaponConfig.AttackRange;
        
        private void Awake()
        {
            _factory = GetComponent<ProjectileControllerFactory>();
            enabled = false;
        }

        public void CaptureTarget(IProjectileTarget target)
        {
            if (_actualTarget == target) return;
            _actualTarget = target;
            enabled = target != null;
        }

        private void DoTheShot()
        {
            _factory.Instantiate(new ProjectileInitArgs(_anchor, _actualTarget, _weaponConfig.Damage, _weaponConfig.ProjectileVelocity));
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