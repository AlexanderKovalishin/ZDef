using UnityEngine;
using ZDef.Game.Data;

namespace ZDef.Game.Weapon
{
    [RequireComponent(typeof(ProjectileControllerFactory))]
    public class FirearmsWeapon: MonoBehaviour
    {
        [SerializeField] private FirearmsConfig _weaponConfig;
        [SerializeField] private Transform _anchor;
        [SerializeField] private Transform _rangeIndicator;
        
        private ProjectileControllerFactory _factory;
        private IProjectileTarget _actualTarget;
        private float _timer;
        private Vector3 _originIndicatorScale;

        public float AttackRange => _weaponConfig.AttackRange;
        
        private void Awake()
        {
            _originIndicatorScale = _rangeIndicator.localScale;
            _factory = GetComponent<ProjectileControllerFactory>();
            enabled = false;
        }

        public bool IsFree(Vector3 position)
        {
            if (_actualTarget == null) return true;
            if (!_actualTarget.IsAlive) return true;
            // todo: raycast check
            return Vector3.Distance(position, _actualTarget.Transform.position) > AttackRange;
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
            _rangeIndicator.transform.localScale = _originIndicatorScale*_weaponConfig.AttackRange;

            if (_actualTarget == null) return;
            if (!_actualTarget.IsAlive) return;

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