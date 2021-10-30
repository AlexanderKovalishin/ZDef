using UnityEngine;
using UnityEngine.UI;

namespace ZDef.Game.UI
{
    public class UIHealthSliderView : UIHealthView
    {
        [SerializeField] private Slider _slider;

        public override void SetValue(int health, int max)
        {
            _slider.minValue = 0; 
            _slider.maxValue = max; 
            _slider.value = health;
        }
    }
}