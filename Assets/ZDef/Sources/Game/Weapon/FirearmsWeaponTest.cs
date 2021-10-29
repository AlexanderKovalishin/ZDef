using UnityEngine;

namespace ZDef.Game.Weapon
{
    public class FirearmsWeaponTest: MonoBehaviour
    {
        [SerializeField] private FirearmsWeapon _weapon;
        [SerializeField] private Transform _startPosition;
        [SerializeField] private Transform _target;

        private void Start()
        {
            _weapon.СaptureTarget(_startPosition, _target);
        }
    }
}