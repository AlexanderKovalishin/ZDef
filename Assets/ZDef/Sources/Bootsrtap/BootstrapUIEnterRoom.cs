using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZDef.Bootsrtap
{

    [RequireComponent(typeof(Animator))]
    public class BootstrapUIEnterRoom : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _enterButton;

        private BootstrapUIAnimator _animator;
        private UniTaskCompletionSource<EnterRoomResult> _completion;
        
        public async UniTask<EnterRoomResult> ShowPopup()
        {
            _inputField.text = string.Empty;
            await _animator.Show();
            _completion = new UniTaskCompletionSource<EnterRoomResult>();
            var result = await _completion.Task;
            await _animator.Hide();
            return result;
        }

        private void Awake()
        {
            _animator = new BootstrapUIAnimator(GetComponent<Animator>()); 
            _backButton.onClick.AddListener(BackButtonOnClick);
            _enterButton.onClick.AddListener(EnterButtonOnClick);
        }

        private void EnterButtonOnClick()
        {
            _completion.TrySetResult(new EnterRoomResult(_inputField.text.ToLower(), DialogResult.Ok));
        }

        private void BackButtonOnClick()
        {
            _completion.TrySetResult(new EnterRoomResult(string.Empty, DialogResult.Cancel));
        }

    }
}