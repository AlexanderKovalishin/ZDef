using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZDef.Bootsrtap
{
    [RequireComponent(typeof(Animator))]
    public class BootstrapUIMessage : MonoBehaviour
    {
        [SerializeField] private TMP_Text _message;
        [SerializeField] private Button _backButton;

        private BootstrapUIAnimator _animator;
        private UniTaskCompletionSource _completion;

        public async UniTask ShowPopup(string message)
        {
            _message.SetText(message);
            await _animator.Show();
            _completion = new UniTaskCompletionSource();
            await _completion.Task;
            await _animator.Hide();
        }

        private void Awake()
        {
            _animator = new BootstrapUIAnimator(GetComponent<Animator>());
            _backButton.onClick.AddListener(BackButtonOnClick);
        }

        private void BackButtonOnClick()
        {
            _completion.TrySetResult();
        }
    }
}