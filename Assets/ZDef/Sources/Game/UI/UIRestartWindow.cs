using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;

namespace ZDef.Game.UI
{
    public class UIRestartWindow: MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _visibleParameter = "Visible";
        [SerializeField] private string _visibleState = "Visible";
        [SerializeField] private string _hiddenState = "Hidden";
        [SerializeField] private Button _restartButton;

        private EventBus _eventBus;
        private bool _isRunning;
        
        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
            _restartButton.onClick.AddListener(RestartButtonOnClick);
        }

        private void RestartButtonOnClick()
        {
            _isRunning = false; 
        }

        public IEnumerator ShowAsync()
        {
            _isRunning = true;
            yield return _animator.SetBoolAndWaitStateAsync(_visibleParameter, true, _visibleState);
            while (_isRunning)
            {
                yield return 0;
            }
            yield return _animator.SetBoolAndWaitStateAsync(_visibleParameter, false, _hiddenState);
        }
    }
};