using UnityEngine;

namespace ZDef.Game.Player
{
    public class PlayerLookAtTargetTest: MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private LookAtTarget _playerLookAtTarget;

        private void Start()
        {
            _playerLookAtTarget.СaptureTarget(_target);
        }
    }
}