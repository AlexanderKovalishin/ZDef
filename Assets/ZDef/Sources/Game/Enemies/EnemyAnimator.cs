using UnityEngine;

namespace ZDef.Game.Enemies
{
    public class EnemyAnimator: MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _aliveParameterName = "Alive";

        public void SetAlive(bool value)
        {
            _animator.SetBool(_aliveParameterName, value);
        }
    }
}