using Cysharp.Threading.Tasks;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZDef.Bootsrtap
{
    [RequireComponent(typeof(Animator))]
    public class BootstrapUIClientRoomMenu : MonoBehaviour
    {
        [SerializeField] private string _roomTitleFormat = "Room ID: {0}";
        [SerializeField] private string _playersInfoFormat = "Waiting players: {0}/{1}";
        [SerializeField] private TMP_Text _roomTitle;
        [SerializeField] private TMP_Text _playersInfo;
        [SerializeField] private Button _backButton;
        
        private BootstrapUIAnimator _animator;
        private UniTaskCompletionSource<DialogResult> _completion;
        private Room _room;
        
        public async UniTask<DialogResult> ShowPopup(Room room)
        {
            _room = room;
            _roomTitle.SetText(string.Format(_roomTitleFormat, room.Name));
            _playersInfo.SetText(string.Format(_playersInfoFormat, _room.Players.Count - 1, _room.MaxPlayers - 1)); // exclude host watcher
            await _animator.Show();
            _completion = new UniTaskCompletionSource<DialogResult>();
            var result = await _completion.Task;
            await _animator.Hide();
            return result;
        }

        private void Awake()
        {
            _animator = new BootstrapUIAnimator(GetComponent<Animator>()); 
            _backButton.onClick.AddListener(BackButtonOnClick);
        }

        private void BackButtonOnClick()
        {
            _completion.TrySetResult(DialogResult.Cancel);
        }

        private void LateUpdate()
        {
            if (_room == null)
            {
                _playersInfo.SetText(string.Format(_playersInfoFormat, 0, 0));
            }
            else
            {
                _playersInfo.SetText(string.Format(_playersInfoFormat, _room.Players.Count - 1, _room.MaxPlayers - 1)); // exclude host watcher
            }
        }
    }
}