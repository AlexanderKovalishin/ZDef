using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ZDef.Game.UI
{
    public class UIHealthSliderView : UIHealthView
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _duration = 0.2f;

        public override void SetValue(int health, int max)
        {
            _slider.minValue = 0; 
            _slider.maxValue = max;
            _slider.DOValue(health, _duration);
        }

        private void OnDestroy()
        {
            _slider.DOKill();
        }
    }
}