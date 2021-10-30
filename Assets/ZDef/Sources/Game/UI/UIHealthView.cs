using UnityEngine;

namespace ZDef.Game.UI
{
    public abstract class UIHealthView : MonoBehaviour
    {
        public abstract void SetValue(int health, int max);
    }
}