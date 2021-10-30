using UnityEngine;

namespace ZDef.Game.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _runParameterName = "Run";
        [SerializeField] private string _dieTrigger = "Die";

        public void SetRun(bool value)
        {
            _animator.SetBool(_runParameterName, value);
        }
        
        public void SetDead()
        {
            _animator.SetTrigger(_dieTrigger);
        }

    }
}