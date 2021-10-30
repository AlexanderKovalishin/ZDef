using UnityEngine;
using UnityEngine.Assertions.Must;
using ZDef.Game.Weapon;

namespace ZDef.Game.Player
{
    public class PlayerWeaponController: MonoBehaviour
    {
        [SerializeField] private LookAtTarget _lookAtTarget;
        [SerializeField] private FirearmsWeapon _weapon;
        
        public float AttackRange => _weapon.AttackRange;

        public void CaptureTarget(IProjectileTarget target)
        {
            _weapon.CaptureTarget(target);
            _lookAtTarget.CaptureTarget(target?.Transform);
        }

        public bool IsFree(Vector3 position)
        {
            return _weapon.IsFree(position);
        }
    }
}