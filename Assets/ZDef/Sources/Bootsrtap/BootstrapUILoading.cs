using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ZDef.Bootsrtap
{
    public class BootstrapUILoading : MonoBehaviour
    {
        [SerializeField] private BootstrapUIAnimation _animator;

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