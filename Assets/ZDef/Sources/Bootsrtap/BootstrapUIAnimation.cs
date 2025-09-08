using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ZDef.Bootsrtap
{

    public class BootstrapUIAnimation: MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
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