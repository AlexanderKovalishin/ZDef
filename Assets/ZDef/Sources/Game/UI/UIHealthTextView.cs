using TMPro;
using UnityEngine;

namespace ZDef.Game.UI
{
    public class UIHealthTextView : UIHealthView
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private string _format;

        public override void SetValue(int health, int max)
        {
            _healthText.text = string.Format(_format, health);
        }
    }
}
