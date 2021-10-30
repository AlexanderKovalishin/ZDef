using System.Collections;
using UnityEngine;
using ZDef.Core;

namespace ZDef.Game.UI
{
    public class UILoadingFader : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _visibleParameter = "Visible";
        [SerializeField] private string _visibleState = "Visible";
        [SerializeField] private string _hiddenState = "Hidden";
        
        public IEnumerator HideAsync()
        {
            return _animator.SetBoolAndWaitStateAsync(_visibleParameter, false, _hiddenState);
        }

        public IEnumerator ShowAsync()
        {
            return _animator.SetBoolAndWaitStateAsync(_visibleParameter, true, _visibleState);
        }
    }
}