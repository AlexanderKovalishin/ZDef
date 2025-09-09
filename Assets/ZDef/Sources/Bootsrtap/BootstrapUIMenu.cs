using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ZDef.Bootsrtap
{
    [RequireComponent(typeof(Animator))]
    public class BootstrapUIMenu: MonoBehaviour
    {
        [SerializeField] private Button _creatRoomButton;
        [SerializeField] private Button _enterRoomButton;

        private BootstrapUIAnimator _animator;
        private UniTaskCompletionSource<StartMenuAction> _completion;

        private void Awake()
        {
            _animator = new BootstrapUIAnimator(GetComponent<Animator>());
            _creatRoomButton.onClick.AddListener(CreatRoomButtonOnClick);
            _enterRoomButton.onClick.AddListener(JoinRoomButtonOnClick);
        }

        private void CreatRoomButtonOnClick()
        {
            _completion.TrySetResult(StartMenuAction.CreateRoom);
        }
        
        private void JoinRoomButtonOnClick()
        {
            _completion.TrySetResult(StartMenuAction.EnterRoom);
        }

        public async UniTask<StartMenuAction> ShowPopup()
        {
            await _animator.Show();
            _completion = new UniTaskCompletionSource<StartMenuAction>();
            var result = await _completion.Task;
            await _animator.Hide();
            return result;
        }

    }

}