using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ZDef.Bootsrtap
{
    public class BootstrapUIMenu: MonoBehaviour
    {
        [SerializeField] private BootstrapUIAnimation _animator;
        [SerializeField] private Button _creatRoomButton;
        [SerializeField] private Button _enterRoomButton;

        public event Action CreatRoomClick;
        public event Action EnterRoomClick;
        
        private void Awake()
        {
            _creatRoomButton.onClick.AddListener(CreatRoomButtonOnClick);
            _enterRoomButton.onClick.AddListener(JoinRoomButtonOnClick);
        }

        private void CreatRoomButtonOnClick()
        {
            CreatRoomClick?.Invoke();
        }
        
        private void JoinRoomButtonOnClick()
        {
            EnterRoomClick?.Invoke();
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