using UnityEngine;

namespace ZDef.Game.Data
{
    [CreateAssetMenu(menuName = "ZDef/KeyboardConfig", fileName = "KeyboardConfig", order = 0)]
    public class KeyboardConfig: ScriptableObject
    {
        [SerializeField] private KeyCode[] _moveLeft;
        [SerializeField] private KeyCode[] _moveRight;

        public KeyCode[] MoveLeft => _moveLeft;
        public KeyCode[] MoveRight => _moveRight;
    }
}