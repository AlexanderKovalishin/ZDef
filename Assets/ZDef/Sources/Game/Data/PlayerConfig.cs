using UnityEngine;

namespace ZDef.Game.Data
{
    [CreateAssetMenu(menuName = "ZDef/PlayerConfig", fileName = "PlayerConfig", order = 0)]
    public class PlayerConfig: ScriptableObject
    {
        [SerializeField] private float _velocity;
        [SerializeField] private int _health;

        public int Health => _health;
        public float Velocity => _velocity;
    }
}