using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ZDef.Bootsrtap
{
    [RequireComponent(typeof(Animator))]
    public class BootstrapUILoading : MonoBehaviour
    {
        private BootstrapUIAnimator _animator;

        private void Awake()
        {
            _animator = new BootstrapUIAnimator(GetComponent<Animator>());
        }

        public async UniTask Show()
        {
            await _animator.Show();
        }
        
        public async UniTask Hide()
        {
            await _animator.Hide();
        }

    }
}