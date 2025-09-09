using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ZDef.Bootsrtap
{
    public class BootstrapUIAnimator
    {
        private readonly Animator _animator;

        public BootstrapUIAnimator(Animator animator)
        {
            _animator = animator;
        }

        private static readonly int Visible = Animator.StringToHash("Visible");

        public void SetVisible(bool value)
        {
            _animator.SetBool(Visible, value);
        }
        
        public async UniTask Show()
        {
            SetVisible(true);
            await UniTask.WaitForSeconds(0.25f);
        }
        
        public async UniTask Hide()
        {
            SetVisible(false);
            await UniTask.WaitForSeconds(0.25f);
        }
    }
}